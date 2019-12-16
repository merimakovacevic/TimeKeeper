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
        public int TeamId { get; set; }
        public int ProjectId { get; set; }
        public decimal WorkingHours { get; set; }
    }
}
