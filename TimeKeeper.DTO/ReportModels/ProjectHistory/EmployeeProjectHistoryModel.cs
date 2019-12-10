using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels.ProjectHistory
{
    public class EmployeeProjectHistoryModel
    {
        public string EmployeeName { get; set; }
        // years -> hours
        public Dictionary<int, decimal> HoursPerYears { get; set; }
        public decimal TotalHoursPerProjectPerEmployee { get; set; }
    }
}
