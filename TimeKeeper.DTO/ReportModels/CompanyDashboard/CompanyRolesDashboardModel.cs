using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.CompanyDashboard
{
    public class CompanyRolesDashboardModel
    {
        public MasterModel Role { get; set; }
        public decimal TotalHours { get; set; }
        public decimal WorkingHours { get; set; }
    }
}
