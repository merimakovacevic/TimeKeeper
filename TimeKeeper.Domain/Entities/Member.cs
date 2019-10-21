using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Member: BaseClass
    {
        public  virtual Team Team { get; set; }
        public virtual Employee Employee { get; set; }
        
        public virtual Role Role { get; set; }
        public decimal HoursWeekly { get; set; }
    }
}
