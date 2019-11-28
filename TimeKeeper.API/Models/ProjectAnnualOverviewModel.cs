using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class ProjectAnnualOverviewModel
    {
        public ProjectAnnualOverviewModel()
        {
            Months = new Dictionary<int, decimal>();
        }
        public MasterModel Project { get; set; }
        public Dictionary<int, decimal> Months { get; set; }
        public decimal Total { get; set; }
    }
}
