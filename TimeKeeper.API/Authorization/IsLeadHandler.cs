using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Authorization
{
    public class IsLeadHandler: AuthorizationHandler<IsRoleRequirement>
    {
        protected UnitOfWork Unit;
        public IsLeadHandler(TimeKeeperContext context)
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

            if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            string userRole = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value;

            //lead has all access only to get methods
            if (userRole == "lead" && HttpMethods.IsGet(filterContext.HttpContext.Request.Method))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
