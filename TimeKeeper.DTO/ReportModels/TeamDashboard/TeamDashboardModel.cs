using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels.TeamDashboard
{
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            EmployeeTimes = new List<TeamMemberDashboardModel>();
        }
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public decimal TotalMissingEntries { get; set; }
        public List<TeamMemberDashboardModel> EmployeeTimes { get; set; }
    }
}
