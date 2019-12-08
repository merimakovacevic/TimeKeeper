using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
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
