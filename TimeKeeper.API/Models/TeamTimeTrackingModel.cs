using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class TeamTimeTrackingModel
    {
        public TeamTimeTrackingModel()
        {
            hourTypes = new Dictionary<string, int>();
        }
        public MasterModel Employee { get; set; }
        public Dictionary<string, int> hourTypes { get; set; }
    }
}
