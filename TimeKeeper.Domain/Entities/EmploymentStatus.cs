using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class EmploymentStatus:  BaseStatus
    {
        public EmploymentStatus()
        {
            Employees = new List<Employee>();
        }
        public virtual IList<Employee> Employees { get; set; }
    }
}
