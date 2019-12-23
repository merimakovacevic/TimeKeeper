using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Authorization.ResourceAccessServices
{
    public class ResourceAccessService
    {
        protected UnitOfWork Unit;
        public ResourceAccessService(UnitOfWork unit)
        {
            Unit = unit;
        }
        public bool CanPutTask(int userId, string userRole, int taskId)
        {
            JobDetail task = Unit.Tasks.Get(taskId);

            if (userRole == "lead" && !task.Project.Team.Members.Any(x => x.Employee.Id == userId) ||
                userRole == "user" && !(task.Day.Employee.Id == userId))
            {
                return false;
            }

            return true;
        }
    }
}
