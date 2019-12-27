using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels.CompanyDashboard
{
    public class CompanyDashboardModel
    {
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public List<CompanyTeamModel> Teams { get; set; }
        public List<CompanyProjectsDashboardModel> Projects { get; set; }
        public List<CompanyRolesDashboardModel> Roles { get; set; }
    }
}
