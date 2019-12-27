using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Authorization
{
    public class AdminLeadOrMemberHandler : AuthorizationHandler<AdminLeadOrMemberRequirement>
    {
        protected UnitOfWork Unit;
        public AdminLeadOrMemberHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminLeadOrMemberRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (context.User.Claims.Count() == 0)
            {
                context.Fail();
                return Task.CompletedTask;
            }
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
            var query = Unit.Teams.Get();
            if (userRole == "admin" || userRole == "lead" ||
                (userRole == "user" && (query.Any(x => x.Members.Any(y => y.Employee.Id == empId)) ||
                                        query.Any(x =>
                                        x.Members.Any(z => z.Employee.Calendar.Any(y => y.Employee.Id == empId))
                                        ))))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
