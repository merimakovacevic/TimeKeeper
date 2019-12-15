using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO
{
    public class TeamModel
    {
        public TeamModel()
        {
            Members = new List<MasterModel>();
            Projects = new List<MasterModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool StatusActive { get; set; }
        public List<MasterModel> Members { get; set; }
        public List<MasterModel> Projects { get; set; }
    }
}
