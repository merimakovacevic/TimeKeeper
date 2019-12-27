using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.CompanyDashboard
{
    public class CompanyTeamModel
    {
        public MasterModel Team { get; set; }
        public decimal MissingEntries { get; set; }
        public decimal PaidTimeOff { get; set; }
        public decimal Overtime { get; set; }
    }
}
