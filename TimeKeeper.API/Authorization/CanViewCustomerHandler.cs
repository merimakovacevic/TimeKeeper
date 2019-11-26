using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Authorization
{
    public class CanViewCustomerHandler : AuthorizationHandler<HasAccessToCustomer>
    {
        protected UnitOfWork Unit;
        public CanViewCustomerHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessToCustomer requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var role = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();
            if (role == "admin")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (role == "user")
            {
                context.Fail();
                return Task.CompletedTask;
            }


            var empid = context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            if (!int.TryParse(empid, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var teams = Unit.Employees.Get(empId).Members.GroupBy(x => x.Team.Id).Select(y => y.Key).ToList();

            if (!int.TryParse((filterContext.RouteData.Values["id"]).ToString(), out int customerId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var projects = Unit.Customers.Get(customerId).Projects.GroupBy(x => x.Team.Id).Select(y => y.Key).ToList();

            foreach (var p in projects)
            {
                if (teams.Contains(p))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
