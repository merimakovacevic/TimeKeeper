using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Models
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            Projects = new List<MasterModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }
        public MasterModelAddress HomeAddress { get; set; }
        public MasterModel Status { get; set; }
        public List<MasterModel> Projects { get; set; }
    }
}
