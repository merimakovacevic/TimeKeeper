using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Authorization
{
    public class IsLeadOrEmployeeHandler : AuthorizationHandler<IsRoleRequirement>
    {
        protected UnitOfWork Unit;
        public IsLeadOrEmployeeHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRoleRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int teamId))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            string employeeId = context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            if (!int.TryParse(employeeId, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            string userRole = context.User.Claims.FirstOrDefault(x => x.Type == "role").Value;

            if (userRole == "admin")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            string userTeam = context.User.Claims.FirstOrDefault(x => x.Type == "team").Value;

            return Task.CompletedTask;
        }
    }
}
