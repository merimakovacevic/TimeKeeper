using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class TasksRepository: Repository<JobDetail>
    {
        public TasksRepository(TimeKeeperContext context) : base(context) { }

        public override void Insert(JobDetail jobDetail)
        {
            jobDetail.Build(_context);
            jobDetail.Validate();
            base.Insert(jobDetail);
        }

        public override void Update(JobDetail jobDetail, int id)
        {
            JobDetail old = Get(id);
            ValidateUpdate(jobDetail, id);

            if (old != null)
            {
                jobDetail.Build(_context);
                _context.Entry(old).CurrentValues.SetValues(jobDetail);
                old.Update(jobDetail);
            }
        }
    }
}
