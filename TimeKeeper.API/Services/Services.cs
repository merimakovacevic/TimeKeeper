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

        public static List<DayType> CreateInMemoryDayTypes(this UnitOfWork unit)
        {
            List<DayType> dayTypesInMemory = unit.DayTypes.Get().ToList();

            dayTypesInMemory.Add(new DayType { Id = 10, Name = "Future" });
            dayTypesInMemory.Add(new DayType { Id = 11, Name = "Empty" });
            dayTypesInMemory.Add(new DayType { Id = 12, Name = "Weekend" });
            dayTypesInMemory.Add(new DayType { Id = 13, Name = "N/A" });

            return dayTypesInMemory;
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
