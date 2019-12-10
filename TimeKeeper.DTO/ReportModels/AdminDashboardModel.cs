using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels
{
    public class AdminDashboardModel
    {
        public AdminDashboardModel(List<string> roles)
        {
            //TeamDashboardModels = new List<TeamDashboardModel>();
            Teams = new List<AdminTeamDashboardModel>();
            Projects = new List<AdminProjectDashboardModel>();
            Roles = new List<AdminRolesDashboardModel>();
            Roles.AddRange(roles.Select(x => new AdminRolesDashboardModel
            {
                RoleName = x
            }));            
        }
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        //public decimal BaseTotalHours { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        //public List<TeamDashboardModel> TeamDashboardModels { get; set; }
        public List<AdminTeamDashboardModel> Teams { get; set; }
        public List<AdminProjectDashboardModel> Projects { get; set; }
        public List<AdminRolesDashboardModel> Roles { get; set; }
    }
}
