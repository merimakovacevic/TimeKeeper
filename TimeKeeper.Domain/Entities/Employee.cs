using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Employee: BaseClass
    {
        public Employee()
        {
            Members = new List<Member>();
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public virtual EmployeePosition Position { get; set; }
        [Required]
        public decimal Salary { get; set; }//this property has been added since the last seed - it can be found in the Requirements
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public virtual EmploymentStatus Status { get; set; }
        public virtual IList<Member> Members { get; set; }
    }
}
