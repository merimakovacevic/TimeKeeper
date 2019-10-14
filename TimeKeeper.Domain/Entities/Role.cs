using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Role: BaseStatus
    {
        public Role()
        {
            Members = new List<Member>();
        }
        [Required]
        public decimal HourlyPrice { get; set; }
        [Required]
        public decimal MonthlyPrice { get; set; }
        public IList<Member> Members { get; set; }
    }
}
