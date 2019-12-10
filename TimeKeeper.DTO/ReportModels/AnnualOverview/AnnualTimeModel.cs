using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AnnualOverview
{
    public class AnnualTimeModel
    {
        public AnnualTimeModel()
        {
            Hours = new decimal[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        public MasterModel Project { get; set; }
        public decimal[] Hours { get; set; }
        public decimal Total { get; set; }
    }
}
