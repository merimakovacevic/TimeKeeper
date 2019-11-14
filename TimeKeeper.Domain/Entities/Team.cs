using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Team: BaseClass
    {
        public Team()
        {
            Members = new List<Member>();
            Projects = new List<Project>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool StatusActive { get; set; }
        public virtual IList<Member> Members { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}
