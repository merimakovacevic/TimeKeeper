using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AnnualOverview
{
    public class AnnualRawModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
        public decimal Hours { get; set; }
    }
}
