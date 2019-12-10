using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.MonthlyOverview
{
    public class MonthlyRawData
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int ProjId { get; set; }
        public string ProjName { get; set; }
        public decimal Hours { get; set; }
    }
}
