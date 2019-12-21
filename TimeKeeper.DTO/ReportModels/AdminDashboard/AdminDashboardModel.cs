using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminDashboardModel
    {
        public AdminDashboardModel()
        {
            PaidTimeOff = new List<AdminRawPTOModel>();
            Teams = new List<AdminTeamDashboardModel>();
            Projects = new List<AdminProjectDashboardModel>();
            Roles = new List<AdminRolesDashboardModel>();
        }
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public decimal MissingEntries { get; set; }
        public List<AdminRawPTOModel> PaidTimeOff { get; set; }
        public List<AdminTeamDashboardModel> Teams { get; set; }
        public List<AdminProjectDashboardModel> Projects { get; set; }
        public List<AdminRolesDashboardModel> Roles { get; set; }
    }
}
