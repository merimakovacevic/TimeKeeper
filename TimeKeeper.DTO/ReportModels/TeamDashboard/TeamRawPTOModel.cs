using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.TeamDashboard
{
    public class TeamRawPTOModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public decimal PaidTimeOff { get; set; }
    }
}
