using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Customer: BaseClass
    {
        public Customer()
        {
            Projects = new List<Project>();
        }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }
        public virtual Address HomeAddress { get; set; }        //Add address to constructor?
        public virtual CustomerStatus Status { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}
