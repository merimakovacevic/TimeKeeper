using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.BLL.ReportServices;
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

        public DashboardService(UnitOfWork unit) : base(unit)
        {
            _queryService = new QueryService(unit);
            _timeTracking = new TimeTracking(unit);
        }

        public AdminDashboardModel GetAdminDashboardInfo(int year, int month)
        {
            //Is the missing entries chart in Admin dashboard referring to missing entries per team or?
            List<string> roles = _unit.Roles.Get().Select(x => x.Name).ToList();
            AdminDashboardModel adminDashboard = new AdminDashboardModel(roles);
           
            //no of employees at current month/year
            //Project and Employees count are already calculated at TeamDashboardLevel
            adminDashboard.EmployeesCount = _queryService.GetNumberOfEmployeesForTimePeriod(month, year);

            //projects in a current month/year            
            List<Project> projects = _queryService.GetProjectsForTimePeriod(month, year);
            //Adds all ProjectDashboardModels to adminDashboard
            adminDashboard.Projects.AddRange(projects.Select(x => GetAdminProjectDashboard(x, year, month)).ToList());

            adminDashboard.ProjectsCount = adminDashboard.Projects.Count();

            //total hours; what is total hours?
            //adminDashboardModel.BaseTotalHours = GetMonthlyWorkingDays(year, month) * 8;
            decimal monthlyBaseHours = GetMonthlyWorkingDays(year, month) * 8;
            adminDashboard.TotalHours = monthlyBaseHours * adminDashboard.EmployeesCount;
            adminDashboard.TotalWorkingHours = 0;

            //We are unable to fetch teams this way because the projects don't tasks in calendar in the database
            //List<Team> teams = projects.Select(x => x.Team).ToList();
            //List<MasterModel> teams = _unit.Teams.Get().Select(x => x.Master()).ToList();      

            List<int> teams = _unit.Teams.Get().Select(x => x.Id).ToList();

            //This calendar will only be calculated once and passed down 
            //List<DayModel> calendar = GetEmptyGenericCalendar(year, month);

            foreach (int teamId in teams)
            {
                MasterModel team = _unit.Teams.Get(teamId).Master();
                TeamDashboardModel teamDashboard = GetTeamDashboard(teamId, year, month);

                AdminTeamDashboardModel teamDashboardModel = GetAdminTeamDashboard(teamDashboard, team);

                //TOTAL HOURS DON'T ADD UP
                GetAdminRolesDashboard(adminDashboard.Roles, teamDashboard, team);

                adminDashboard.Teams.Add(teamDashboardModel);
                adminDashboard.TotalWorkingHours += teamDashboardModel.WorkingHours;
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
                    return project.Tasks.Sum(x => x.Hours * 15);
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

        private void GetAdminRolesDashboard(List<AdminRolesDashboardModel> roles, TeamDashboardModel teamDashboard, MasterModel team)
        {
            foreach (TeamMemberDashboardModel member in teamDashboard.EmployeeTimes)
            {
                Role role = _unit.Employees.Get(member.Employee.Id).Members.FirstOrDefault(x => x.Team.Id == team.Id).Role;
                roles.FirstOrDefault(x => x.RoleName == role.Name).TotalHours += member.TotalHours;
                roles.FirstOrDefault(x => x.RoleName == role.Name).WorkingHours += member.WorkingHours;
            }
        }

        public TeamDashboardModel GetTeamDashboard(int teamId, int year, int month)
        {
            //The DashboardService shouldn't really depend on the report service, this should be handled in another way
            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = GetTeamMembersDashboard(teamId, year, month)
            };

            //projects for this month!!!
            teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
            teamDashboard.ProjectsCount = _unit.Teams.Get(teamId).Projects.Count();

            foreach (TeamMemberDashboardModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.TotalWorkingHours += employeeTime.WorkingHours;
                teamDashboard.TotalMissingEntries += employeeTime.MissingEntries;
            }

            return teamDashboard;
        }

        private List<TeamMemberDashboardModel> GetTeamMembersDashboard(int teamId, int year, int month)
        {
            List<EmployeeTimeModel> employeeTimes = _timeTracking.GetTeamMonthReport(teamId, year, month);
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

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }
        public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year, int month)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year, month);
            decimal totalHours = GetMonthlyWorkingDays(year, month) * 8;

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
