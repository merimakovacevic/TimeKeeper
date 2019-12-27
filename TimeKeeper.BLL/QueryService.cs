using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.BLL
{
    public class QueryService
    {
        protected UnitOfWork _unit;
        public QueryService(UnitOfWork unit)
        {
            _unit = unit;

        }

        public List<EmployeeModel> GetEmployeeTeamMembers(int employeeId)
        {
            List<Team> userTeams = GetEmployeeTeams(employeeId);
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

        public List<Member> GetTeamMembers(int employeeId)
        {
            List<Team> userTeams = GetEmployeeTeams(employeeId);
            //Teams.Get(x => x.Members.Any(y => y.Employee.Id == userId)).ToList();
            HashSet<Member> employees = new HashSet<Member>();
            foreach (Team team in userTeams)
            {
                foreach (Member member in team.Members)
                {
                    employees.Add(member);
                }
            }
            return employees.ToList();
        }

        public List<Team> GetEmployeeTeams(int employeeId)
        {
            return _unit.Teams.Get(x => x.Members.Any(y => y.Employee.Id == employeeId)).ToList();
        }

        public List<Project> GetEmployeeProjects(int employeeId)
        {
            List<Team> employeeTeams = GetEmployeeTeams(employeeId);
            List<Project> employeeProjects = new List<Project>();

            foreach (Team team in employeeTeams)
            {
                employeeProjects.AddRange(team.Projects);
            }

            return employeeProjects;
        }
        public int GetNumberOfEmployeesForTimePeriod(int month, int year)
        {
            return _unit.Employees.Get(x => x.BeginDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if employees begin date is in required month
                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // still works in company, or left company after required month          
        }
        public int GetNumberOfProjectsForTimePeriod(int month, int year)
        {
            return _unit.Projects.Get(x => x.StartDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if project began is in required month
                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // project still in progress, or ended after the required month          
        }
        public List<ProjectModel> GetProjectModelsForTimePeriod(int month, int year)
        {
            List<Project> query = GetProjectsForTimePeriod(month, year);
            List <ProjectModel> projects = query.Select(x => x.Create()).ToList(); // project still in progress, or ended after the required month    
            return projects;
        }

        public List<Project> GetProjectsForTimePeriod(int month, int year)
        {
            /*List<Project> projects = _unit.Projects.Get(x => x.StartDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if project began is in required month
                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).ToList();*/
            List<Project> projects = _unit.Projects.Get().ToList();
            List<Project> projects1 = projects.Where(x => x.StartDate < new DateTime(year, month, DateTime.DaysInMonth(year, month))).ToList();
            List<Project> projects2 = projects1.Where(x => x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, 1)).ToList();
            return projects2;
        }

    }
}
