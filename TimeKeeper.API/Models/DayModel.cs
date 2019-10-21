using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class DayModel
    {
        public DayModel()
        {
            JobDetails = new List<MasterModel>();
        }
        public int Id { get; set; }
        public MasterModelEmployee Employee { get; set; }
        public DateTime Date { get; set; }
        public MasterModel DayType { get; set; }
        public List<MasterModel> JobDetails { get; set; }
    }
}
