using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Services
{
    public static class Services
    {
        //public static bool IsTeamMember(this Employee firstEmployee, Employee secondEmployee, Team team)
        //{
        //    bool isTeamMember = true;
            
        //}

        public static List<EmployeeModel> GetEmployeeTeamMembers(this UnitOfWork unit, int userId)
        {
            List<Team> userTeams = unit.GetEmployeeTeams(userId);
                //Teams.Get(x => x.Members.Any(y => y.Employee.Id == userId)).ToList();
            HashSet<Employee> employees = new HashSet<Employee>();
            foreach (Team team in userTeams)
            {
                // team.Members.Select(x => employees.Add(x.Employee));
                foreach (Member member in team.Members)
                {
                    employees.Add(member.Employee);
                }
            }
            return employees.Select(x => x.Create()).ToList();
        }

        public static List<Team> GetEmployeeTeams(this UnitOfWork unit, int userId)
        {
            return unit.Teams.Get(x => x.Members.Any(y => y.Employee.Id == userId)).ToList();
        }

        public static List<Project> GetEmployeeProjects(this UnitOfWork unit, int employeeId)
        {
            List<Team> employeeTeams = GetEmployeeTeams(unit, employeeId);
            List<Project> employeeProjects = new List<Project>();

            foreach(Team team in employeeTeams)
            {
                employeeProjects.AddRange(team.Projects);
            }

            return employeeProjects;
        }

        public static Task FilterContextCheck(AuthorizationHandlerContext context/*, UnitOfWork unit*/)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            /*
            if (!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int teamId))
            {
                context.Fail();
                return Task.CompletedTask;
            }*/

            if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int employeeId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
