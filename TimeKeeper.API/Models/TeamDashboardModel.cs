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
            employeeTimes = new List<EmployeeTimeModel>();
        }

        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public int TotalHours { get; set; }
        public int WorkingHours { get; set; }
        public Dictionary<EmployeeModel, decimal> PaidTimeOff { get; set; }
        public Dictionary<EmployeeModel, decimal> Overtime { get; set; }
        List<EmployeeTimeModel> employeeTimes { get; set; }


    }
}
