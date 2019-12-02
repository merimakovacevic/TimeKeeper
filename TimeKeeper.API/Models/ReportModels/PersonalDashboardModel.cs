using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models.ReportModels
{
    public class PersonalDashboardModel
    {
        public MasterModel Employee;
        public decimal TotalHours;
        public decimal WorkingHours;
        public decimal BradfordFactor;
    }
}
