using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.PersonalDashboard
{
    public class PersonalDashboardRawModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal WorkingMonthly { get; set; }
        public decimal WorkingYearly { get; set; }
        public int SickMonthly { get; set; }
        public int SickYearly { get; set; }
    }
}
