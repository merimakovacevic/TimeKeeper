using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
{
    public class HistoryRawData
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Hours { get; set; }
        public int Year { get; set; }
    }
}
