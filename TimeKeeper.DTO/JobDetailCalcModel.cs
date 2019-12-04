using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO
{
    public class JobDetailCalcModel
    {
        public int Id { get; set; }
        public MasterModel Project { get; set; }
        public decimal Hours { get; set; }
    }
}
