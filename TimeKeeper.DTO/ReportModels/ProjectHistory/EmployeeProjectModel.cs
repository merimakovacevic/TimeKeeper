using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.ProjectHistory
{
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
