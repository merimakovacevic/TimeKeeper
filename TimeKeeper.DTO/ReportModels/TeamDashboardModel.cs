using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels
{
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            EmployeeTimes = new List<EmployeeTimeModel>();
        }
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public decimal TotalMissingEntries { get; set; }
        public List<EmployeeTimeModel> EmployeeTimes { get; set; }
    }
}
