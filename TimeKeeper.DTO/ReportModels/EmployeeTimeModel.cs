using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels
{
    public class EmployeeTimeModel
    {
        public EmployeeTimeModel()
        {
            HourTypes = new Dictionary<string, decimal>();
        }
        public MasterModel Employee { get; set; }
        /// <summary>
        /// Represents the total hours in a month for an employee, excluded weekends and overtime
        /// </summary>
        public decimal TotalHours { get; set; }
        public decimal Overtime { get; set; }
        public decimal PaidTimeOff { get; set; }
        public Dictionary<string, decimal> HourTypes { get; set; }
    }
}
