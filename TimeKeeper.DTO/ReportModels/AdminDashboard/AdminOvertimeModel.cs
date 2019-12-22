using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminOvertimeModel
    {
        public int TeamId { get; set; }
        public int EmployeeId { get; set; }
        public int MyProperty { get; set; }
        public string TeamName { get; set; }
        public decimal OvertimeHours { get; set; }
    }
}
