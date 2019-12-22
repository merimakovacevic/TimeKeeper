using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminRawPTOModel
    {
        public int TeamId { get; set; }
        public string Team { get; set; }
        public decimal PaidTimeOff { get; set; }
    }
}
