using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    [Table("Tasks")]
    public class JobDetail: BaseClass<int>
    {
        [Required]
        public virtual Day Day { get; set; }
        [Required]
        public  virtual Project Project { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Hours { get; set; }
    }
}
