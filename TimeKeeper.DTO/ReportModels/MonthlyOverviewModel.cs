﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels
{
    public class MonthlyOverviewModel
    {
        public MonthlyOverviewModel()
        {
            HoursByProject = new Dictionary<string, decimal>();
        }
        public List<EmployeeTotalTimeModel> EmployeeProjectHours { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalPossibleWorkingHours { get; set; }
        public int TotalWorkingDays { get; set; }
        public Dictionary<string, decimal> HoursByProject { get; set; }
    }
}
