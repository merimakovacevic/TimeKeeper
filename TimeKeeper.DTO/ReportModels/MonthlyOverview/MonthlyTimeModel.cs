using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.ReportModels.ProjectHistory;

namespace TimeKeeper.DTO.ReportModels.MonthlyOverview
{
    public class MonthlyTimeModel
    {
        public MonthlyTimeModel()
        {
            Projects = new List<MasterModel>();
            Employees = new List<EmployeeProjectModel>();
        }
        public List<MasterModel> Projects { get; set; }
        public List<EmployeeProjectModel> Employees { get; set; }
    }
}
