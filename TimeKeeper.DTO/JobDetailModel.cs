using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO
{
    public class JobDetailModel
    {
        public int Id { get; set; }
        public MasterModel Day { get; set; }
        public MasterModel Project { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }
    }
}