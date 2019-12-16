using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using TimeKeeper.BLL.ReportServices;
using TimeKeeper.BLL.Utilities;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.DTO.ReportModels.AdminDashboard;
using TimeKeeper.DTO.ReportModels.PersonalDashboard;
using TimeKeeper.DTO.ReportModels.TeamDashboard;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.BLL
{
    public class DashboardService : CalendarService
    {
        protected QueryService _queryService;
        protected TimeTracking _timeTracking;
        protected StoredProcedureService _storedProcedures;

        public DashboardService(UnitOfWork unit) : base(unit)
        {
            _queryService = new QueryService(unit);
            _timeTracking = new TimeTracking(unit);
            _storedProcedures = new StoredProcedureService(unit);
        }

        public AdminDashboardModel GetAdminDashboardInfo(int year, int month)
        {
            //Is the missing entries chart in Admin dashboard referring to missing entries per team or?
            //List<string> roles = _unit.Roles.Get().Select(x => x.Name).ToList();
            AdminDashboardModel adminDashboard = new AdminDashboardModel();
            decimal baseHours = GetMonthlyWorkingDays(year, month) * 8;

            //no of employees at current month/year
            //adminDashboard.EmployeesCount = _queryService.GetNumberOfEmployeesForTimePeriod(month, year); 

            adminDashboard.PaidTimeOff = _storedProcedures.GetStoredProcedure<AdminRawPTOModel>("AdminPTOHours", new int[] { year, month });
            List<AdminRawModel> rawData = _storedProcedures.GetStoredProcedure<AdminRawModel>("AdminDashboard", new int[] { year, month });
                        
            adminDashboard.EmployeesCount = rawData.GroupBy(x => x.EmployeeId).Count();
            adminDashboard.ProjectsCount = rawData.GroupBy(x => x.ProjectId).Count();
            adminDashboard.TotalHours = adminDashboard.EmployeesCount * baseHours;
            adminDashboard.TotalWorkingHours = rawData.Sum(x => x.WorkingHours);
            adminDashboard.Roles = rawData.GroupBy(r => r.RoleName).Select(x => new
            {
                RoleName = x.Key,
                WorkingHours = x.Sum(y => y.WorkingHours)
            }).ToList().Select(x => new AdminRolesDashboardModel
            {
                RoleName = x.RoleName,
                WorkingHours = x.WorkingHours,
                TotalHours = 0
            }).ToList();

            
            
            //We are unable to fetch teams this way because the projects don't tasks in calendar in the database
            //List<Team> teams = projects.Select(x => x.Team).ToList();      

            List<Team> teams = _unit.Teams.Get().ToList();

            //projects in a current month/year  
            /*Due do inconsistency in database, it is not possible to retrieve projects 
            for time period and match them with days
            So all projects will be taken into consideration*/
            //List<Project> projects = _queryService.GetProjectsForTimePeriod(month, year);    
            List<Project> projects = teams.SelectMany(x => x.Projects).ToList();//_unit.Projects.Get().ToList();

            //Adds all ProjectDashboardModels to adminDashboard
            adminDashboard.Projects.AddRange(projects.Select(x => GetAdminProjectDashboard(x, year, month)).ToList());
            adminDashboard.ProjectsCount = adminDashboard.Projects.Count();

            foreach (Team team in teams)
            {
                MasterModel teamModel = team.Master();//_unit.Teams.Get(teamId).Master();

                //This method also calculates the role utilization
                TeamDashboardModel teamDashboard = GetTeamDashboardForAdmin(team, year, month/*, adminDashboard.Roles*/);

                AdminTeamDashboardModel teamDashboardModel = GetAdminTeamDashboard(teamDashboard, teamModel);

                adminDashboard.Teams.Add(teamDashboardModel);
                //adminDashboard.TotalWorkingHours += teamDashboardModel.WorkingHours;
                //adminDashboard.TotalHours += teamDashboardModel.TotalHours;
            }

            return adminDashboard;
        }

        //FINAL IMPLEMENTATION IS NEEDED
        public decimal GetProjectRevenue(Project project, int year, int month)
        {
            switch(project.Pricing.Name)
            {
                case "Hourly":
                    //DATABASE DOESN'T HAVE COHERENT DATA, THIS IS FOR SHOWCASE ONLY - FURTHER IMPLEMENTATION IS NEEDED!!!
                    //return project.Tasks.Where(x => x.Day.IsDateInPeriod(year, month)).Sum(x => x.Hours * 15);
                    return project.Team.Members.Sum(x => x.Role.MonthlyPrice);
                case "PerCapita":
                    //DATABASE DOESN'T HAVE COHERENT DATA, THIS IS FOR SHOWCASE ONLY - FURTHER IMPLEMENTATION IS NEEDED!!!
                    //Only members who have tasks in this month need to be calculated
                    return project.Team.Members.Sum(x => x.Role.MonthlyPrice);
                case "Fixed bid":
                    return project.Amount;
                default:
                    return 0;
            }
        }
        
        private AdminProjectDashboardModel GetAdminProjectDashboard(Project project, int year, int month)
        {            
            return new AdminProjectDashboardModel
            {
                Project = new MasterModel { Id = project.Id, Name = project.Name},
                Revenue = GetProjectRevenue(project, year, month)
            };
        }

        private AdminTeamDashboardModel GetAdminTeamDashboard(TeamDashboardModel teamDashboard, MasterModel team)
        {  
            return new AdminTeamDashboardModel
            {
                Team = team,
                TotalHours = teamDashboard.TotalHours,
                WorkingHours = teamDashboard.TotalWorkingHours,
                PaidTimeOff = teamDashboard.EmployeeTimes.Sum(x => x.PaidTimeOff),
                MissingEntries = teamDashboard.TotalMissingEntries,
                Overtime = teamDashboard.EmployeeTimes.Sum(x => x.Overtime)
            };
        }

        public TeamDashboardModel GetTeamDashboardForAdmin(Team team, int year, int month/*, List<AdminRolesDashboardModel> roles*/)
        {
            //The DashboardService shouldn't really depend on the report service, this should be handled in another way
            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = GetTeamMembersDashboard(team, year, month)
            };

            //projects for this month!!!
            teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
            teamDashboard.ProjectsCount = team.Projects.Count();//_unit.Teams.Get(teamId).Projects.Count();

            foreach (TeamMemberDashboardModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.TotalWorkingHours += employeeTime.WorkingHours;
                teamDashboard.TotalMissingEntries += employeeTime.MissingEntries;

                //Role utilization is also calculated here
                /*roles.FirstOrDefault(x => x.RoleName == employeeTime.MemberRole).TotalHours += employeeTime.TotalHours;
                roles.FirstOrDefault(x => x.RoleName == employeeTime.MemberRole).WorkingHours += employeeTime.WorkingHours;*/
            }

            return teamDashboard;
        }

        public TeamDashboardModel GetTeamDashboard(int teamId, int year, int month)
        {
            return GetTeamDashboard(_unit.Teams.Get(teamId), year, month);
        }

        public TeamDashboardModel GetTeamDashboard(Team team, int year, int month)
        {
            //The DashboardService shouldn't really depend on the report service, this should be handled in another way
            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = GetTeamMembersDashboard(team, year, month)
            };

            //projects for this month!!!
            teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
            teamDashboard.ProjectsCount = team.Projects.Count();//_unit.Teams.Get(teamId).Projects.Count();

            foreach (TeamMemberDashboardModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.TotalWorkingHours += employeeTime.WorkingHours;
                teamDashboard.TotalMissingEntries += employeeTime.MissingEntries;
            }

            return teamDashboard;
        }

        private List<TeamMemberDashboardModel> GetTeamMembersDashboard(Team team, int year, int month)
        {
            List<EmployeeTimeModel> employeeTimes = _timeTracking.GetTeamMonthReport(team, year, month);
            List<TeamMemberDashboardModel> teamMembers = new List<TeamMemberDashboardModel>();
            foreach(EmployeeTimeModel employeeTime in employeeTimes)
            {
                teamMembers.Add(new TeamMemberDashboardModel
                {
                    Employee = employeeTime.Employee,
                    TotalHours = employeeTime.TotalHours,                    
                    Overtime = employeeTime.Overtime,
                    PaidTimeOff = employeeTime.PaidTimeOff,
                    WorkingHours = employeeTime.HourTypes["Workday"],
                    MissingEntries = employeeTime.HourTypes["Missing entries"]
                });
            }

            return teamMembers;
        }

        public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
            decimal totalHours = GetYearlyWorkingDays(year) * 8;
            //overtime is deducted from total monthly hours
            totalHours -= calendar.CalculateOvertime();

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }

        public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year, int month)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year, month);
            decimal totalHours = GetMonthlyWorkingDays(year, month) * 8;
            //overtime is deducted from total monthly hours
            totalHours -= calendar.CalculateOvertime();

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }

        private PersonalDashboardModel CreatePersonalDashboard(int employeeId, int year, decimal totalHours, List<DayModel> calendar)
        {
            decimal workingHours = calendar.Where(x => x.DayType.Name == "Workday").Sum(x => x.TotalHours);

            return new PersonalDashboardModel
            {
                Employee = _unit.Employees.Get(employeeId).Master(),
                TotalHours = totalHours,
                WorkingHours = workingHours,
                BradfordFactor = GetBradfordFactor(employeeId, year)


            };
        }

        private decimal GetBradfordFactor(int employeeId, int year)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
            //an absence instance are any number of consecutive absence days. 3 consecutive absence days make an instance.
            int absenceInstances = 0;
            int absenceDays = 0;
            calendar = calendar.OrderBy(x => x.Date).ToList();

            //Bradford factor calculates only dates until the present day, because the calendar in argument returns the whole period
            absenceDays = calendar.Where(x => x.DayType.Name == "Sick" && x.Date < DateTime.Now).Count();

            for (int i = 0; i < calendar.Count; i++)
            {
                if (calendar[i].DayType.Name == "Sick" && calendar[i].Date < DateTime.Now)
                {
                    if (i == 0) absenceInstances++;

                    else if (calendar[i - 1].DayType.Name != "Sick")
                    {
                        absenceInstances++;
                    }
                }
            }
            return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
        }

    }
}
