using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class CompanyTeamModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public decimal PaidTimeOff { get; set; }
        public decimal Overtime { get; set; }
    }
}
