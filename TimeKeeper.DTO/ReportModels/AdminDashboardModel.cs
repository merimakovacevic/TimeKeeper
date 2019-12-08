using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels
{
    public class AdminDashboardModel
    {
        public AdminDashboardModel()
        {
            //TeamDashboardModels = new List<TeamDashboardModel>();
            Teams = new List<AdminTeamDashboardModel>();
            Projects = new List<AdminProjectDashboardModel>();
        }
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        //public decimal BaseTotalHours { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        //public List<TeamDashboardModel> TeamDashboardModels { get; set; }
        public List<AdminTeamDashboardModel> Teams { get; set; }
        public List<AdminProjectDashboardModel> Projects { get; set; }
    }
}
