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
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Authorization
{
    public class HasAccessToEmployeeHandler : AuthorizationHandler<HasAccessToEmployee>
    {
        protected UnitOfWork Unit;
        public HasAccessToEmployeeHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessToEmployee requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int employeeId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            string userRole = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();
            int userId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
            Employee employee = Unit.Employees.Get(employeeId);

            //lead and user can edit only his employee data
            if (userId == employeeId && HttpMethods.IsPut(filterContext.HttpContext.Request.Method))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
