using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels
{
    public class ProjectAnnualOverviewModel
    {
        public ProjectAnnualOverviewModel()
        {
            Months = new Dictionary<int, decimal>();
            //Hours = new decimal[]{0, 0,0 ...}
        }
        public MasterModel Project { get; set; }
        public Dictionary<int, decimal> Months { get; set; }
        //public decimal[] Hours get set
        public decimal Total { get; set; }
    }
}
