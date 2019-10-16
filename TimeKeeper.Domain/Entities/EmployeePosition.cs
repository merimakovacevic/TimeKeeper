using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class EmployeePosition: BaseStatus
    {
        public EmployeePosition()
        {
            Employees = new List<Employee>();
        }
        public virtual IList<Employee> Employees { get; set; }
    }
}
