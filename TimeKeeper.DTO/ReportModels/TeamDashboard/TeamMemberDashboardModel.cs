using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.TeamDashboard
{
    public class TeamMemberDashboardModel
    {
        public TeamMemberDashboardModel()
        {
            Overtime = 0;
            PaidTimeOff = 0;
            MissingEntries = 0;
        }
        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public decimal Overtime { get; set; }
        public decimal PaidTimeOff { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal MissingEntries { get; set; }
    }
}
