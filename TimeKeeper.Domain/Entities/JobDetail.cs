using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    [Table("Tasks")]
    public class JobDetail: BaseClass
    {
        public virtual Day Day { get; set; }
        public  virtual Project Project { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }
    }
}
