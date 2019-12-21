using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class ProjectsRepository: Repository<Project>
    {
        public ProjectsRepository(TimeKeeperContext context) : base(context) { }

        public override void Insert(Project project)
        {
            base.Insert(project);
        }

        public override void Update(Project project, int id)
        {
            Project old = Get(id);
            ValidateUpdate(project, id);

            if (old != null)
            {
                project.Build(_context);
                _context.Entry(old).CurrentValues.SetValues(project);
                old.Update(project);
            }
        }

        public override void Delete(int id)
        {
            Project old = Get(id);

            if(!old.CanDelete())
            {
                Services.ThrowChildrenPresentException();
            }

            Delete(old);
        }
    }
}
