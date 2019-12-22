using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminOvertimeModel
    {
        public int EmployeeId { get; set; }
        public string  EmployeeName { get; set; }
        public decimal OvertimeHours { get; set; }
    }
}
