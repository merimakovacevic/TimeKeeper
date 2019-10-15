using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    [Table("Tasks")]
    public class JobDetail:BaseClass
    {
        [Required]
        public Day Day { get; set; }
        [Required]
        public Project Project { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Hours { get; set; }
    }
}
