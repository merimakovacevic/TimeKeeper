using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO
{
    public class MemberModel
    {
        public int Id { get; set; }
        public MasterModel Team { get; set; }
        public MasterModel Employee { get; set; }
        public MasterModel Role { get; set; }
        public MasterModel Status { get; set; }
        public decimal HoursWeekly { get; set; }
    }
}
