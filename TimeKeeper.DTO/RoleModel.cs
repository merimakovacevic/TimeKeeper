using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO
{
    public class RoleModel
    {
        public RoleModel()
        {
            Members = new List<MasterModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public IList<MasterModel> Members { get; set; }
    }
}
