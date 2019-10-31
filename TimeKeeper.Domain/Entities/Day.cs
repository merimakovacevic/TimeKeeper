using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    [Table("Calendar")]
    public class Day : BaseClass
    {
        public Day()
        {
            JobDetails = new List<JobDetail>();
        }

        public virtual Employee Employee{ get; set; }
        public DateTime Date { get; set; }        
        public virtual DayType DayType { get; set; }
        public virtual IList<JobDetail> JobDetails { get; set; }//Shouldn't this property be called tasks?

        [NotMapped]
        public decimal TotalHours { get
                //This hardcoded logic will need to be refactored
            {
                if (DayType.Name == "Workday") { return JobDetails.Sum(x => x.Hours); } else { return 8; };
            }
        }    
    }
}
