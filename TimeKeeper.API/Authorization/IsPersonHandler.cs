using Microsoft.AspNetCore.Authorization;
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
    public class IsPersonHandler: AuthorizationHandler<HasAccessToEmployee>
    {
        protected UnitOfWork Unit;
        public IsPersonHandler(TimeKeeperContext context)
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
            string userRole = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value;

            List<EmployeeModel> employees = Unit.GetEmployeeTeams(int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value));
            if (employees.Any(x => x.Id == employeeId))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
