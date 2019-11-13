using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Authorization
{
    public class IsMemberHandler : AuthorizationHandler<HasAccessToTeam>
    {
        protected UnitOfWork Unit;
        public IsMemberHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessToTeam requirement)
        {
            /*var role = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();
            if (role == "admin" || role == "lead")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }*/

            var filterContext = context.Resource as AuthorizationFilterContext;
            if(filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
  
            if(!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int teamId))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            Team team = Unit.Teams.Get(teamId);

            if(!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if(team.Members.Any(x => x.Employee.Id == empId))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            //context.Fail();
            return Task.CompletedTask;
        }
    }
}
