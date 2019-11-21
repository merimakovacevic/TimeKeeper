using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class EmployeeTimeModel
    {
        public EmployeeTimeModel()
        {
            HourTypes = new Dictionary<string, decimal>();
        }
        public EmployeeModel Employee { get; set; }
        public decimal Overtime { get; set; }
        public decimal PaidTimeOff { get; set; }
        public Dictionary<string, decimal> HourTypes { get; set; }
    }
}
