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

        public static void SetHourTypes(this Dictionary<string, decimal> hourTypes, UnitOfWork unit)
        {
            List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            foreach (DayType day in dayTypes)
            {
                hourTypes.Add(day.Name, 0);
            }

            hourTypes.Add("Missing entries", 0);
        }

        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsWeekend(this DayModel day)
        {
            return day.Date.IsWeekend();
        }
    }
}
