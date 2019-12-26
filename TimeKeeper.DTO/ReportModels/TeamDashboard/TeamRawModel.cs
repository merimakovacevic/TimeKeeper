using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.TeamDashboard
{
    public class TeamRawModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Value { get; set; }
    }
}
