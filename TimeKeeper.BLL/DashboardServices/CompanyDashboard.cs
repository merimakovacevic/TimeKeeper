using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.BLL.ReportServices;
using TimeKeeper.DAL;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels.CompanyDashboard;

namespace TimeKeeper.BLL.DashboardServices
{
    public class CompanyDashboard: CalendarService
    {
        protected QueryService _queryService;
        protected TimeTracking _timeTracking;
        protected StoredProcedureService _storedProcedures;

        public CompanyDashboard(UnitOfWork unit): base(unit)
        {
            _queryService = new QueryService(unit);
            _timeTracking = new TimeTracking(unit);
            _storedProcedures = new StoredProcedureService(unit);
        }

        public CompanyDashboardModel GetCompanyDashboard(int year, int month)
        {
            CompanyDashboardModel companyDashboard = new CompanyDashboardModel();
            decimal baseHours = GetMonthlyWorkingDays(year, month) * 8;
            List<CompanyDashboardRawModel> rawData = _storedProcedures.GetStoredProcedure<CompanyDashboardRawModel>("CompanyWorkingHoursData", new int[] { year, month });
            List<CompanyEmployeeHoursModel> employeeHours = _storedProcedures.GetStoredProcedure<CompanyEmployeeHoursModel>("EmployeeHoursByDayType", new int[] { year, month });
            List<CompanyOvertimeModel> overtime = _storedProcedures.GetStoredProcedure<CompanyOvertimeModel>("CompanyOvertimeHours", new int[] { year, month });

            List<MasterModel> activeTeams = new List<MasterModel>();
            activeTeams.AddRange(rawData.GroupBy(x => new
            {
                Id = x.TeamId,
                Name = x.TeamName
            }).ToList().Select(x => new MasterModel
            {
                Id = x.Key.Id,
                Name = x.Key.Name
            }).ToList());

            companyDashboard.EmployeesCount = rawData.GroupBy(x => x.EmployeeId).Count();
            companyDashboard.ProjectsCount = rawData.GroupBy(x => x.ProjectId).Count();
            companyDashboard.TotalHours = companyDashboard.EmployeesCount * baseHours;
            companyDashboard.TotalWorkingHours = rawData.Sum(x => x.WorkingHours);
            companyDashboard.Projects = GetCompanyProjectModels(rawData);
            companyDashboard.Roles = GetRoleUtilization(rawData, baseHours);
            companyDashboard.Teams = GetCompanyTeamModels(rawData, employeeHours, activeTeams, overtime);
            GetCompanyMissingEntries(employeeHours, companyDashboard.Teams, baseHours, overtime);

            return companyDashboard;
        }
        private decimal EmployeeRatioInTeam(List<CompanyEmployeeHoursModel> workDays, int teamId, int employeeId)
        {
            decimal empTeamWorkingHours = workDays.Where(x => x.EmployeeId == employeeId && x.TeamId == teamId).Sum(x => x.DayTypeHours);
            decimal empWorkingHours = workDays.Where(x => x.EmployeeId == employeeId).Sum(x => x.DayTypeHours);
            return empTeamWorkingHours / empWorkingHours;
        }
        private void GetCompanyMissingEntries(List<CompanyEmployeeHoursModel> employeeHours, List<CompanyTeamModel> teams, decimal baseHours, List<CompanyOvertimeModel> overtime)
        {
            List<EmployeeMissingEntriesModel> missingEntriesEmployee = employeeHours.GroupBy(x => new { x.EmployeeId, x.EmployeeName })
                .Select(x => new EmployeeMissingEntriesModel
                {
                    Employee = new MasterModel { Id = x.Key.EmployeeId, Name = x.Key.EmployeeName },
                    MissingEntries = baseHours - x.Sum(y => y.DayTypeHours) + overtime.Where(y => y.EmployeeId == x.Key.EmployeeId).Sum(y => y.OvertimeHours)
                }).Where(x => x.MissingEntries > 0).ToList();

            foreach (CompanyTeamModel team in teams)
            {
                foreach (EmployeeMissingEntriesModel employee in missingEntriesEmployee)
                {
                    if (employeeHours.Any(x => x.EmployeeId == employee.Employee.Id && x.TeamId == team.Team.Id))
                    {
                        team.MissingEntries += employee.MissingEntries;
                    }
                }
            }
        }
        private List<CompanyTeamModel> GetCompanyTeamModels(List<CompanyDashboardRawModel> rawData, List<CompanyEmployeeHoursModel> employeeHours, List<MasterModel> activeTeams, List<CompanyOvertimeModel> overtime)
        {
            List<CompanyTeamModel> teams = new List<CompanyTeamModel>();
            teams.AddRange(activeTeams.Select(x => new CompanyTeamModel
            {
                Team = new MasterModel { Id = x.Id, Name = x.Name },
                Overtime = 0,
                PaidTimeOff = 0
            }).OrderBy(x => x.Team.Id).ToList());

            List<CompanyOvertimeModel> overtimeNotNull = overtime.Where(x => x.OvertimeHours > 0).ToList();
            List<CompanyEmployeeHoursModel> paidTimeOff = employeeHours.Where(x => x.DayTypeName != "Workday").ToList();
            List<CompanyEmployeeHoursModel> workDays = employeeHours.Where(x => x.DayTypeName == "Workday").ToList();

            foreach (CompanyEmployeeHoursModel row in workDays)
            {
                if (row.TeamId != 0)
                {
                    if (overtimeNotNull.FirstOrDefault(x => x.EmployeeId == row.EmployeeId) != null)
                    {
                        GetCompanyOvertime(teams, workDays, overtimeNotNull, row.TeamId, row.EmployeeId);
                    }

                    if (paidTimeOff.FirstOrDefault(x => x.EmployeeId == row.EmployeeId) != null)
                    {
                        GetCompanyPaidTimeOff(teams, workDays, paidTimeOff, row.TeamId, row.EmployeeId);
                    }
                }
            }

            return teams;
        }
        private void GetCompanyOvertime(List<CompanyTeamModel> teams, List<CompanyEmployeeHoursModel> workDays, List<CompanyOvertimeModel> overtime, int teamId, int employeeId)
        {
            decimal empOvertime = overtime.Where(x => x.EmployeeId == employeeId).Sum(x => x.OvertimeHours);
            teams.FirstOrDefault(x => x.Team.Id == teamId).Overtime += empOvertime * EmployeeRatioInTeam(workDays, teamId, employeeId);
        }
        private void GetCompanyPaidTimeOff(List<CompanyTeamModel> teams, List<CompanyEmployeeHoursModel> workDays, List<CompanyEmployeeHoursModel> paidTimeOff, int teamId, int employeeId)
        {
            decimal empPaidTimeOff = paidTimeOff.Where(x => x.EmployeeId == employeeId).Sum(x => x.DayTypeHours);
            decimal empRatio = EmployeeRatioInTeam(workDays, teamId, employeeId);
            teams.FirstOrDefault(x => x.Team.Id == teamId).PaidTimeOff += empPaidTimeOff * empRatio;
        }
        private List<CompanyRolesDashboardModel> GetRoleUtilization(List<CompanyDashboardRawModel> rawData, decimal baseHours)
        {
            List<CompanyRolesDashboardModel> roles = new List<CompanyRolesDashboardModel>();

            //Employee and role are grouped, and the roles utilization model is created
            List<CompanyRolesRawModel> rolesRaw = CreateRolesRaw(rawData);

            CompanyRolesDashboardModel role = new CompanyRolesDashboardModel { Role = new MasterModel { Id = 0, Name = "" } };
            foreach (CompanyRolesRawModel row in rolesRaw)
            {
                if (row.RoleName != role.Role.Name)
                {
                    if (role.Role.Name != "") roles.Add(role);
                    role = new CompanyRolesDashboardModel { Role = new MasterModel { Id = row.RoleId, Name = row.RoleName } };
                    role.WorkingHours = rolesRaw.Where(x => x.RoleName == role.Role.Name).Sum(x => x.WorkingHours);
                }
                /*Calculates the ratio of this employees total working hours 
                 * as this role in employees overall total working hours, 
                 * and uses the ratio to extract a number from the monthly base hours*/
                decimal hoursEmployeeRole = rolesRaw.Where(x => x.EmployeeId == row.EmployeeId && x.RoleName == role.Role.Name).Sum(x => x.WorkingHours);
                decimal hoursEmployee = rolesRaw.Where(x => x.EmployeeId == row.EmployeeId).Sum(x => x.WorkingHours);
                role.TotalHours += (hoursEmployeeRole / hoursEmployee) * baseHours;
            }
            if (role.Role.Name != "") roles.Add(role);

            return roles;
        }

        private List<CompanyRolesRawModel> CreateRolesRaw(List<CompanyDashboardRawModel> rawData)
        {
            List<CompanyRolesRawModel> rolesRaw = rawData.GroupBy(x => new { x.EmployeeId, x.RoleId, x.RoleName }).Select(
                x => new CompanyRolesRawModel
                {
                    EmployeeId = x.Key.EmployeeId,
                    RoleId = x.Key.RoleId,
                    RoleName = x.Key.RoleName,
                    WorkingHours = x.Sum(y => y.WorkingHours)
                }).ToList().OrderBy(x => x.RoleName).ToList();

            return rolesRaw;
        }

        private List<CompanyProjectsDashboardModel> GetCompanyProjectModels(List<CompanyDashboardRawModel> rawData)
        {
            List<CompanyProjectsDashboardModel> projects = new List<CompanyProjectsDashboardModel>();
            //Data isn't sorted by projects unless a new List is created
            List<CompanyDashboardRawModel> rawProjects = rawData.OrderBy(x => x.ProjectId).ToList();

            CompanyProjectsDashboardModel project = new CompanyProjectsDashboardModel { Project = new MasterModel { Id = 0 } };
            foreach (CompanyDashboardRawModel row in rawProjects)
            {
                if (row.ProjectId != project.Project.Id)
                {
                    if (project.Project.Id != 0) projects.Add(project);
                    project = new CompanyProjectsDashboardModel
                    {
                        Project = new MasterModel { Id = row.ProjectId, Name = row.ProjectName },
                        Revenue = GetProjectRevenue(rawProjects, row.ProjectId, row.ProjectPricingName)
                    };
                }
            }
            if (project.Project.Id != 0) projects.Add(project);

            return projects;
        }

        private decimal GetProjectRevenue(List<CompanyDashboardRawModel> rawData, int projectId, string pricingType)
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

    }
}
