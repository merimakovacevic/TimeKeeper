using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
{
    public class MonthlyRawData
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal Hours { get; set; }
    }

    public class EmployeeProjectModel
    {
        public EmployeeProjectModel(List<int> projects)
        {
            Hours = new Dictionary<int, decimal>();
            foreach (int p in projects) Hours.Add(p, 0);
        }
        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public Dictionary<int, decimal> Hours { get; set; }
    }
}
