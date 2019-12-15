﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminTeamDashboardModel
    {
        public MasterModel Team { get; set; }
        public decimal TotalHours { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal PaidTimeOff { get; set; }
        public decimal MissingEntries { get; set; }
        public decimal Overtime { get; set; }
    }
}
