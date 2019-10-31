using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class ProjectsRepository: Repository<Project>
    {
        public ProjectsRepository(TimeKeeperContext context) : base(context) { }

        public override void Update(Project project, int id)
        {
            Project old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(project);
                old.Team = project.Team;
                old.Customer = project.Customer;
                old.Status = project.Status;
                old.Pricing = project.Pricing;
            }
        }
    }
}
