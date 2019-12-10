using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
{
    public class EmployeeProjectHistory
    {
        public EmployeeProjectHistory(int[] years)
        {
            TotalYearlyProjectHours = new Dictionary<int, decimal>();
            foreach (int i in years) TotalYearlyProjectHours.Add(i, 0);
        }
        public MasterModel Employee { get; set; }
        public Dictionary<int, decimal> TotalYearlyProjectHours { get; set; }
        public decimal TotalHoursPerProject { get; set; }
    }
}
