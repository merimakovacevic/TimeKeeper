using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models.ReportModels
{
    public class AdminDashboardModel
    {
        public AdminDashboardModel()
        {
            TeamDashboardModels = new List<TeamDashboardModel>();
        }
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        //public decimal BaseTotalHours { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public List<TeamDashboardModel> TeamDashboardModels { get; set; }
    }
}
