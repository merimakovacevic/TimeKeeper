using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class TasksRepository: Repository<JobDetail>
    {
        public TasksRepository(TimeKeeperContext context) : base(context) { }

        private void Build(JobDetail jobDetail)
        {
            jobDetail.Day = _context.Calendar.Find(jobDetail.Day.Id);
            jobDetail.Project = _context.Projects.Find(jobDetail.Project.Id);
        }

        public override void Insert(JobDetail jobDetail)
        {
            Build(jobDetail);
            base.Insert(jobDetail);
        }

        public override void Update(JobDetail jobDetail, int id)
        {
            JobDetail old = Get(id);

            if (old != null)
            {
                Build(jobDetail);
                _context.Entry(old).CurrentValues.SetValues(jobDetail);
                old.Day = jobDetail.Day;
                old.Project = jobDetail.Project;
            }
        }
    }
}
