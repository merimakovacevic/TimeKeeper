using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public MasterModel Team { get; set; }
        public MasterModelEmployee Employee { get; set; }
        public MasterModel Role { get; set; }
        public decimal HoursWeekly { get; set; }
    }
}
