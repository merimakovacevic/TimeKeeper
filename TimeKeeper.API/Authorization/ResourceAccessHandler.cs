using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;

namespace TimeKeeper.API.Authorization
{
    public class ResouceAccessHandler
    {
        private UnitOfWork _unit;
        public ResouceAccessHandler(UnitOfWork unit)
        {
            _unit = unit;
        }
        public bool CanAccessTask(UserRoleModel _userClaims, JobDetail newTask)
        {
            Project project = _unit.Projects.Get(newTask.Project.Id);
            Day day = _unit.Calendar.Get(newTask.Day.Id);

            if (_userClaims.Role == "lead" && !project.Team.Members.Any(x => x.Employee.Id == _userClaims.UserId) ||
                _userClaims.Role == "user" && !(day.Employee.Id == _userClaims.UserId))
            {
                return false;
            }

            return true;
        }

        public bool CanGetDay(UserRoleModel _userClaims, Day day)
        {
            if (_userClaims.Role == "lead" && !day.JobDetails.Any(x => x.Project.Team.Members.Any(y => y.Employee.Id == _userClaims.UserId)) ||
                    _userClaims.Role == "user" && !day.JobDetails.Any(x => x.Project.Team.Members.Any(y => y.Employee.Id == _userClaims.UserId)))
            {
                return false;
            }

            return true;
        }
        public bool CanModifyDay(UserRoleModel _userClaims, Day newDay)
        {
            Day day = _unit.Calendar.Get(newDay.Id);
            if (_userClaims.Role != "admin" && !(day.Employee.Id == _userClaims.UserId))
            {
                return false;
            }
            return true;
        }

    public async Task<List<JobDetail>> GetAuthorizedTasks(UserRoleModel _userClaims)
        {
            List<JobDetail> query;

            if (_userClaims.Role == "lead")
            {
                var task = await _unit.Tasks.GetAsync(x => x.Project.Team.Members.Any(y => y.Employee.Id == _userClaims.UserId));
                query = task.ToList();
            }
            else if (_userClaims.Role == "user")
            {
                var task = await _unit.Tasks.GetAsync(x => x.Day.Employee.Id == _userClaims.UserId);
                query = task.ToList();
            }
            else
            {
                var task = await _unit.Tasks.GetAsync();
                query = task.ToList();
            }

            return query;
        }
    }
}
