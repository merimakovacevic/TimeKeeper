using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class Project :BaseClass
    {
        public Project()
        {
            Tasks = new List<JobDetail>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Team Team { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public ProjectStatus Status { get; set; }
        [Required]
        public PricingStatus Pricing { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public IList<JobDetail> Tasks;
    }
}
