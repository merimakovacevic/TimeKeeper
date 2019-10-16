using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Customer: BaseClass<int>
    {
        public Customer()
        {
            Projects = new List<Project>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public virtual Address HomeAddress { get; set; }        
        [Required]
        public virtual CustomerStatus Status { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}
