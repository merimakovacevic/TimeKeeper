using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Day: BaseClass
    {
        public Day()
        {
            JobDetails = new List<JobDetail>();
        }

        [Required]
        public Employee Employees{ get; set; }
        [Required]
        public DateTime Date { get; set; }
        public decimal TotalHours { get; set; }
        [Required]
        public DayType DayType { get; set; }
        public IList<JobDetail> JobDetails { get; set; }
    }
}
