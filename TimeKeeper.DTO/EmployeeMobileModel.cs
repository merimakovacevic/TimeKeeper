using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO
{
    public class EmployeeMobileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual MasterModel Position { get; set; }
    }
}
