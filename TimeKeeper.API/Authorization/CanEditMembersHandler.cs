using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Authorization
{
    public class CanEditMembersHandler : AuthorizationHandler<HasAccessToMembers>
    {
        protected UnitOfWork Unit;
        public CanEditMembersHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessToMembers requirement)
        {
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

            List<EmployeeModel> teamMembers = Unit.GetEmployeeTeamMembers(int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value));
            string userRole = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();

            //Each employee can only view his team members (Member entity)
            if (userRole == "lead" && teamMembers.Any(x => x.Id == memberId) && HttpMethods.IsPut(filterContext.HttpContext.Request.Method))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            //context.Fail();
            return Task.CompletedTask;
        }
    }
}
