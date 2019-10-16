using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class BaseStatus<K> : BaseClass<K>
    {
        [Required]
        public string Name { get; set; }
    }
}
