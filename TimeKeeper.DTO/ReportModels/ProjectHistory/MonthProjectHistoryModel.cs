using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels.ProjectHistory
{
    public class MonthProjectHistoryModel
    {
        public int MonthNumber { get; set; }
        public string MonthName { get; set; }
        // years -> hours
        public Dictionary<int, decimal> HoursPerYears { get; set; }
        public decimal TotalHoursPerMonth { get; set; }
    }
}
