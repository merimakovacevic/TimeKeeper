using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
{
    public class TeamMissingEntries
    {
        public TeamMissingEntries()
        {
            Team = new MasterModel();
        }
        public MasterModel Team { get; set; }
        public decimal Hours { get; set; }
    }
}
