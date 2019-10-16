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

        [Required]
        public virtual Employee Employee{ get; set; }
        [Required]
        public DateTime Date { get; set; }        
        [Required]
<<<<<<< HEAD
        public DayType DayType { get; set; }
        public IList<JobDetail> JobDetails { get; set; }
=======
        public virtual DayType DayType { get; set; }
        public virtual IList<JobDetail> JobDetails { get; set; }

>>>>>>> 3f11d97fa3a2e0de35654db6f02de2b80a2ac891
        [NotMapped]
        public decimal TotalHours { get; }
        
        //TO DO: implementation of a DayType enum is still needed
        // {
        //if (DayType == DayType.WorkDay) return JobDetails.Sum(x => x.Hours);
        //else return 8;
        // } set; 
        
    }
}
