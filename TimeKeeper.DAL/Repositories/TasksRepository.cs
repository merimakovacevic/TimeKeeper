using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class TasksRepository: Repository<JobDetail>
    {
        public TasksRepository(TimeKeeperContext context) : base(context) { }

        public override void Update(JobDetail jobDetail, int id)
        {
            JobDetail old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(jobDetail);
                old.Day = jobDetail.Day;
                old.Project = jobDetail.Project;
            }
        }
    }
}
