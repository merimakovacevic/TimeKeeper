using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.CompanyDashboard
{
    public class CompanyOvertimeModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal OvertimeHours { get; set; }
    }
}
