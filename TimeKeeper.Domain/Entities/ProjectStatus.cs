﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class ProjectStatus: BaseStatus
    {
        public ProjectStatus()
        {
            Projects = new List<Project>();
        }
        public IList<Project> Projects;
    }
}
