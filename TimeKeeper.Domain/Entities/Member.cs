using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Member: BaseClass
    {
        [Required]
        public Team Team { get; set; }
        [Required]
        public Employee Employee { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public decimal HoursWeekly { get; set; }
    }
}
