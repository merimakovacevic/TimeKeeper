using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Employee: BaseClass
    {
        public Employee()
        {
            Members = new List<Member>();
            Calendar = new List<Day>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        /// Employee's full name - not mapped - calculated
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual EmployeePosition Position { get; set; }
        public decimal Salary { get; set; }//this property has been added since the last seed - it can be found in the Requirements
        public DateTime Birthday { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual EmploymentStatus Status { get; set; }
        public virtual IList<Day> Calendar { get; set; }
        public virtual IList<Member> Members { get; set; }
    }
}
