using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class TotalAnnualOverviewModel
    {
        public TotalAnnualOverviewModel()
        {
            Months = new Dictionary<int, decimal>();
        }
        public List<ProjectAnnualOverviewModel> Projects { get; set; }
        public Dictionary<int, decimal> Months { get; set; }
        public decimal SumTotal { get; set; }
    }
}
