using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Authorization
{
    public class IsMemberOnProjectHandler : AuthorizationHandler<HasAccessToProjects>
    {
        protected UnitOfWork Unit;
        public IsMemberOnProjectHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessToProjects requirement)
        {
            /*
                var role = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();
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
                
                if (!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int projectId))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
                
                Project project = Unit.Projects.Get(projectId);
            
                if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
                
                if (project.Team.Members.Any(x => x.Employee.Id == empId))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                
                //context.Fail();
                return Task.CompletedTask;
            }
    }
}





