using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
{
    public class TeamTimeRawModel
    {
        public int EmployeeId { get; set; }
        public int DayType { get; set; }
        public decimal Hours { get; set; }
    }
}
