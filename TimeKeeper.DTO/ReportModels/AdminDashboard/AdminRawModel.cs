using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminRawModel
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public decimal RoleHourlyPrice { get; set; }
        public decimal RoleMonthlyPrice { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProjectAmount { get; set; }
        public int ProjectPricingId { get; set; }
        public string ProjectPricingName { get; set; }
        public decimal WorkingHours { get; set; }
    }
}
