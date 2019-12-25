using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.PersonalDashboard
{
    public class PersonalDashboardStoredModel
    {
        public PersonalDashboardRawModel PersonalDashboardHours { get; set; }
        public decimal UtilizationMonthly { get; set; }
        public decimal UtilizationYearly { get; set; }
        public decimal BradfordFactor { get; set; }
    }
}
