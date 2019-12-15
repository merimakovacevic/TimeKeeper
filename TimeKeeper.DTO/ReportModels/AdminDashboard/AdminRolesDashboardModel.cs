using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminRolesDashboardModel
    {
        public string RoleName { get; set; }
        public decimal TotalHours { get; set; }
        public decimal WorkingHours { get; set; }
    }
}
