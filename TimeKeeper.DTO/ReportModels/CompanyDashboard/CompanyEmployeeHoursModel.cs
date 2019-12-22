using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.CompanyDashboard
{
    public class CompanyEmployeeHoursModel
    {
        public int TeamId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DayTypeId { get; set; }
        public string DayTypeName { get; set; }
        public decimal DayTypeHours { get; set; }
    }
}
