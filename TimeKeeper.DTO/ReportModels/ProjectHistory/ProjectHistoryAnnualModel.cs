using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.ProjectHistory
{
    public class ProjectHistoryAnnualModel
    {
        public ProjectHistoryAnnualModel()
        {
            Employees = new List<EmployeeProjectHistory>();
            Years = new List<int>();
        }
        public List<int> Years { get; set; }
        public List<EmployeeProjectHistory> Employees { get; set; }
    }
}
