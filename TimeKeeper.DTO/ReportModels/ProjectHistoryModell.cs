using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
{
    public class ProjectHistoryModell
    {
        public ProjectHistoryModell()
        {
            Employees = new List<EmployeeProjectHistory>();
        }
        public int[] years { get; set; }
        public List<EmployeeProjectHistory> Employees { get; set; }
    }
}
