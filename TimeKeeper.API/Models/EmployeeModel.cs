using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            Members = new List<MasterModel>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual MasterModel Position { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual MasterModel Status { get; set; }
        public IList<MasterModel> Members { get; set; }
    }
}
