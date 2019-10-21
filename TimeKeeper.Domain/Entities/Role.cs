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
        public decimal HourlyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public virtual IList<Member> Members { get; set; }
    }
}
