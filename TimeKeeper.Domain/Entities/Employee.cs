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
        public EmployeePosition Position { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public EmploymentStatus Status { get; set; }
        public IList<Member> Members { get; set; }
    }
}
