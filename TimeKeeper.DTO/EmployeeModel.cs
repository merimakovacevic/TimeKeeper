using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DTO
{
    public class EmployeeModel
    {
        /*public EmployeeModel()
        {
            Members = new List<MasterModel>();
            Calendar = new List<MasterModel>();
        }*/

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual MasterModel Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual MasterModel Status { get; set; }
        public IList<MasterModel> Members { get; set; }
        public IList<MasterModel> Calendar { get; set; }
    }
}
