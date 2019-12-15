using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO
{
    public class DayModel
    {
        public DayModel()
        {
            JobDetails = new List<JobDetailModel>();
        }
        public int Id { get; set; }
        public MasterModel Employee { get; set; }
        public DateTime Date { get; set; }
        public MasterModel DayType { get; set; }
        public decimal TotalHours { get; set; }
        //comment property??
        public List<JobDetailModel> JobDetails { get; set; }
    }
}
