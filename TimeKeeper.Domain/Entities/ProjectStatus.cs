using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class ProjectStatus: BaseStatus<int>
    {
        public ProjectStatus()
        {
            Projects = new List<Project>();
        }
        public virtual IList<Project> Projects { get; set; }
    }
}
