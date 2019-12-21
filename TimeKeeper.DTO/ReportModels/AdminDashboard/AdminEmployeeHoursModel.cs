using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminEmployeeHoursModel
    {
        public int EmployeeId { get; set; }
        public decimal SumOfHours { get; set; }
    }
}
