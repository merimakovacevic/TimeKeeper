using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels
{
    public class EmployeeMissingEntries
    {
        public EmployeeMissingEntries()
        {
            Employee = new MasterModel();
        }
        public MasterModel Employee { get; set; }
        public decimal MissingEntries { get; set; }
    }
}
