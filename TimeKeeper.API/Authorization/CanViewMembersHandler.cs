﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.DTO;
using TimeKeeper.BLL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Authorization
{
    public class CanViewMembersHandler : AuthorizationHandler<HasAccessToMembers>
    {
        protected UnitOfWork Unit;
        protected QueryService queryService;
        public CanViewMembersHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
            queryService = new QueryService(Unit);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessToMembers requirement)
        {
            /*var role = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();
            if (role == "admin" || role == "lead")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }*/

            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int memberId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            List<EmployeeModel> teamMembers = queryService.GetEmployeeTeamMembers(int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value));

            //Each employee can only view his team members (Member entity)
            if (teamMembers.Any(x => x.Id == memberId) && HttpMethods.IsGet(filterContext.HttpContext.Request.Method))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            //context.Fail();
            return Task.CompletedTask;
        }
    }
}
