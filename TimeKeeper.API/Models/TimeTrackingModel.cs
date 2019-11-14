using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class TimeTrackingModel
    {
        public string FullName { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal BusinessAbsence { get; set; }
        public decimal PublicHoliday { get; set; }
        public decimal Vacation { get; set; }
        public decimal SickDays { get; set; }
        public decimal MissingEntries { get; set; }
    }
}
