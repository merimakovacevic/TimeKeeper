using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.ReportModels.PersonalDashboard
{
    public class PersonalDashboardModel
    {
        public MasterModel Employee;
        /// <summary>
        /// Represents the total hours in a month for an employee, excluded weekends and overtime
        /// </summary>
        public decimal TotalHours;
        public decimal WorkingHours;
        public decimal BradfordFactor;
    }
}
