using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class ProjectsRepository: Repository<Project>
    {
        public ProjectsRepository(TimeKeeperContext context) : base(context) { }

        private void Build(Project project)
        {
            project.Team = _context.Teams.Find(project.Team.Id);
            project.Customer = _context.Customers.Find(project.Customer.Id);
            project.Status = _context.ProjectStatuses.Find(project.Status.Id);
            project.Pricing = _context.PricingStatuses.Find(project.Pricing.Id);
        }

        public override void Insert(Project project)
        {
            Build(project);
            base.Insert(project);
        }

        public override void Update(Project project, int id)
        {
            Project old = Get(id);

            if (old != null)
            {
                Build(project);
                _context.Entry(old).CurrentValues.SetValues(project);
                old.Team = project.Team;
                old.Customer = project.Customer;
                old.Status = project.Status;
                old.Pricing = project.Pricing;
            }
        }
    }
}
