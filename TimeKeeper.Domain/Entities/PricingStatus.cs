﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class PricingStatus: BaseStatus
    {
        public PricingStatus()
        {
            Projects = new List<Project>();
        }
        public IList<Project> Projects;
     }
}
