using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Models
{
    public class TeamTimeTrackingModel
    {
        public TeamTimeTrackingModel()
        {
            hourTypes = new Dictionary<string, decimal>();
        }
        public Employee Employee { get; set; }
        public Dictionary<string, decimal> hourTypes { get; set; }
    }
}
