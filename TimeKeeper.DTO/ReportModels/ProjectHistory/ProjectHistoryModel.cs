using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.ReportModels.ProjectHistory;

namespace TimeKeeper.DTO.ReportModels
{
    public class ProjectHistoryModel
    {
        public ProjectHistoryModel()
        {
            //Projects = new List<Project>();
            Employees = new List<EmployeeProjectHistoryModel>();
            TotalYearlyProjectHours = new Dictionary<int, decimal>();
        }
        //public List<Project> Projects { get; set; }
        public List<EmployeeProjectHistoryModel> Employees { get; set; }
        public Dictionary<int, decimal> TotalYearlyProjectHours { get; set; }
        public decimal TotalHoursPerProject { get; set; }
    }
}
