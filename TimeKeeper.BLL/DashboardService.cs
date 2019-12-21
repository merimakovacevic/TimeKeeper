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
            AdminDashboardModel adminDashboard = new AdminDashboardModel();
            List<AdminRawModel> rawData = _storedProcedures.GetStoredProcedure<AdminRawModel>("AdminDashboard", new int[] { year, month });
            decimal baseHours = GetMonthlyWorkingDays(year, month) * 8;

            adminDashboard.EmployeesCount = rawData.GroupBy(x => x.EmployeeId).Count();
            adminDashboard.ProjectsCount = rawData.GroupBy(x => x.ProjectId).Count();
            adminDashboard.TotalHours = adminDashboard.EmployeesCount * baseHours;
            adminDashboard.TotalWorkingHours = rawData.Sum(x => x.WorkingHours);
            adminDashboard.PaidTimeOff = _storedProcedures.GetStoredProcedure<AdminRawPTOModel>("AdminPTOHours", new int[] { year, month });
            adminDashboard.Projects = GetAdminProjectModels(rawData);
            adminDashboard.Roles = GetRoleUtilization(rawData, baseHours);

            List<AdminEmployeeHoursModel> employeeHours = _storedProcedures.GetStoredProcedure<AdminEmployeeHoursModel>("EmployeeDayTypeHours", new int[] { year, month });
            if(employeeHours.Count() != adminDashboard.EmployeesCount)
            {
                throw new Exception("EmployeesCount and total number of employees in missing entries calculation don't match");
            }

            adminDashboard.MissingEntries = adminDashboard.TotalHours - employeeHours.Sum(x => x.SumOfHours);
            
            /*List<Team> teams = _unit.Teams.Get().ToList();           

            foreach (Team team in teams)
            {
                MasterModel teamModel = team.Master();//_unit.Teams.Get(teamId).Master();

                //This method also calculates the role utilization
                TeamDashboardModel teamDashboard = GetTeamDashboardForAdmin(team, year, month);

                AdminTeamDashboardModel teamDashboardModel = GetAdminTeamDashboard(teamDashboard, teamModel);

                adminDashboard.Teams.Add(teamDashboardModel);
                //adminDashboard.TotalWorkingHours += teamDashboardModel.WorkingHours;
                //adminDashboard.TotalHours += teamDashboardModel.TotalHours;
            }*/

            return adminDashboard;
        }

        private List<AdminRolesDashboardModel> GetRoleUtilization(List<AdminRawModel> rawData, decimal baseHours)
        {
            List<AdminRolesDashboardModel> roles = new List<AdminRolesDashboardModel>();

            //Employee and role are grouped, and the roles utilization model is created
            List<AdminRolesRawModel> rolesRaw = CreateAdminRolesRaw(rawData);

            AdminRolesDashboardModel role = new AdminRolesDashboardModel { RoleName = "" };
            foreach (AdminRolesRawModel row in rolesRaw)
            {
                if (row.RoleName != role.RoleName)
                {
                    if (role.RoleName != "") roles.Add(role);
                    role = new AdminRolesDashboardModel { RoleName = row.RoleName };
                    role.WorkingHours = rolesRaw.Where(x => x.RoleName == role.RoleName).Sum(x => x.WorkingHours);
                }
                /*Calculates the ratio of this employees total working hours 
                 * as this role in employees overall total working hours, 
                 * and uses the ratio to extract a number from the monthly base hours*/
                decimal hoursEmployeeRole = rolesRaw.Where(x => x.EmployeeId == row.EmployeeId && x.RoleName == role.RoleName).Sum(x => x.WorkingHours);
                decimal hoursEmployee = rolesRaw.Where(x => x.EmployeeId == row.EmployeeId).Sum(x => x.WorkingHours);
                role.TotalHours += (hoursEmployeeRole / hoursEmployee) * baseHours;
            }
            if (role.RoleName != "") roles.Add(role);

            return roles;
        }

        private List<AdminRolesRawModel> CreateAdminRolesRaw(List<AdminRawModel> rawData)
        {
            List<AdminRolesRawModel> rolesRaw = rawData.GroupBy(x => new { x.EmployeeId, x.RoleId, x.RoleName }).Select(
                x => new AdminRolesRawModel
                {
                    EmployeeId = x.Key.EmployeeId,
                    RoleId = x.Key.RoleId,
                    RoleName = x.Key.RoleName,
                    WorkingHours = x.Sum(y => y.WorkingHours)
                }).ToList().OrderBy(x => x.RoleName).ToList();

            return rolesRaw;
        }

        private List<AdminProjectDashboardModel> GetAdminProjectModels(List<AdminRawModel> rawData)
        {
            List<AdminProjectDashboardModel> projects = new List<AdminProjectDashboardModel>();
            //Data isn't sorted by projects unless a new List is created
            List<AdminRawModel> rawProjects = rawData.OrderBy(x => x.ProjectId).ToList();

            AdminProjectDashboardModel project = new AdminProjectDashboardModel { Project = new MasterModel { Id = 0 } };
            foreach (AdminRawModel row in rawProjects)
            {
                if (row.ProjectId != project.Project.Id)
                {
                    if (project.Project.Id != 0) projects.Add(project);
                    project = new AdminProjectDashboardModel
                    {
                        Project = new MasterModel { Id = row.ProjectId, Name = row.ProjectName },
                        Revenue = GetProjectRevenue(row.ProjectId, row.ProjectPricingName, rawProjects)
                    };
                }
            }
            if (project.Project.Id != 0) projects.Add(project);

            return projects;
        }

        private decimal GetProjectRevenue(int projectId, string pricingType, List<AdminRawModel> rawData)
        {
            switch (pricingType)
            {
                case "Fixed bid":
                    return rawData.FirstOrDefault(x => x.ProjectId == projectId).ProjectAmount;
                case "Hourly":
                    return rawData.Where(x => x.ProjectId == projectId).Sum(x => x.WorkingHours * x.RoleHourlyPrice);
                case "PerCapita":
                    return rawData.Where(x => x.ProjectId == projectId)
                                  .GroupBy(x => new { x.EmployeeId, x.ProjectId, x.RoleMonthlyPrice })
                                  .ToList().Sum(x => x.Key.RoleMonthlyPrice);
                default:
                    return 0;
            }            
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
