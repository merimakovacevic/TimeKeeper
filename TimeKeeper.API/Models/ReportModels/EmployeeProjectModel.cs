using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models.ReportModels
{
    public class EmployeeProjectModel
    {
        public EmployeeProjectModel()
        {
            HoursByProject = new Dictionary<string, decimal>();
        }

        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public decimal PaidTimeOff { get; set; }
        public Dictionary<string, decimal> HoursByProject { get; set; }
    }
}
