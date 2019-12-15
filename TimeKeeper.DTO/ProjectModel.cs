using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            Tasks = new List<MasterModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public MasterModel Team { get; set; }
        public MasterModel Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MasterModel Status { get; set; }
        public MasterModel Pricing { get; set; }
        public decimal Amount { get; set; }
        public IList<MasterModel> Tasks { get; set; }
    }
}
