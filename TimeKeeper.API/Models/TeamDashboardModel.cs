using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            PaidTimeOff = new Dictionary<EmployeeModel, decimal>();
            Overtime = new Dictionary<EmployeeModel, decimal>();
            EmployeeTimes = new List<EmployeeTimeModel>();
        }
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public int TotalHours { get; set; }
        public int WorkingHours { get; set; }
        Dictionary<EmployeeModel, decimal> PaidTimeOff { get; set; }
        Dictionary<EmployeeModel, decimal> Overtime { get; set; }
        List<EmployeeTimeModel> EmployeeTimes { get; set; }
    }
}
