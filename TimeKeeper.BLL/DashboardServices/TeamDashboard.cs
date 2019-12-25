using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.BLL.ReportServices;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.DTO.ReportModels.TeamDashboard;

namespace TimeKeeper.BLL.DashboardServices
{
    public class TeamDashboard : CalendarService
    {
        protected TimeTracking _timeTracking;
        protected StoredProcedureService _storedProcedures;
        public TeamDashboard(UnitOfWork unit) : base(unit)
        {
            _timeTracking = new TimeTracking(unit);
            _storedProcedures = new StoredProcedureService(unit);
        }

        private List<TeamMemberDashboardModel> GetTeamMembersDashboard(Team team, int year, int month)
        {
            List<EmployeeTimeModel> employeeTimes = _timeTracking.GetTeamMonthReport(team, year, month);
            List<TeamMemberDashboardModel> teamMembers = new List<TeamMemberDashboardModel>();
            foreach (EmployeeTimeModel employeeTime in employeeTimes)
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

        public TeamDashboardModel GetTeamDashboardStored(int teamId, int year, int month)
        {
            Team team = _unit.Teams.Get(teamId);
            TeamDashboardModel teamDashboard = new TeamDashboardModel();
            List<TeamRawModel> rawData = _storedProcedures.GetStoredProcedure<TeamRawModel>("TeamDashboard", new int[] { team.Id, year, month });
            List<TeamRawPTOModel> rawDataPTO = _storedProcedures.GetStoredProcedure<TeamRawPTOModel>("GetMemberPTOHours", new int[] { team.Id, year, month });
            teamDashboard.Year = year;
            teamDashboard.Month = month;
            teamDashboard.Team = new MasterModel { Id = team.Id, Name = team.Name };
            teamDashboard.NumberOfEmployees = rawData.GroupBy(x => x.EmployeeId).Count();
            teamDashboard.TotalWorkingHours = rawData.Sum(x => x.WorkingHours);
            List<TeamRawCountModel> rawDataProjectsCount = _storedProcedures.GetStoredProcedure<TeamRawCountModel>("CountProjects", new int[] { team.Id, year, month });
            teamDashboard.NumberOfProjects = rawDataProjectsCount.Count;
            foreach (TeamRawModel r in rawData)
            {
                teamDashboard.EmployeeTimes.Add(new TeamMemberDashboardModel
                {
                    Employee = new MasterModel { Id = r.EmployeeId, Name = r.EmployeeName },
                    TotalHours = 0,
                    Overtime = 0,
                    PaidTimeOff = (rawDataPTO == null || rawDataPTO.FirstOrDefault(x => x.MemberId == r.EmployeeId) == null) ? 0 : rawDataPTO.FirstOrDefault(x => x.MemberId == r.EmployeeId).PaidTimeOff,
                    WorkingHours = r.WorkingHours,
                    MissingEntries = 0
                });
            }
            return teamDashboard;
        }
    }
}
