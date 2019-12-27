using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.CompanyDashboard
{
    public class EmployeeMissingEntriesModel
    {
        //public int TeamId { get; set; }
        public MasterModel Employee { get; set; }
        public decimal MissingEntries { get; set; }
    }
}
