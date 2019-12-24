using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.BLL;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;

namespace TimeKeeper.API.Authorization
{
    public class ResouceAccessHandler
    {
        private UnitOfWork _unit;
        private QueryService queryService;
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

        public async Task<List<Customer>> GetAuthorizedCustomers(UserRoleModel _userClaims)
        {
            List<Customer> query;

            if (_userClaims.Role == "lead")
            {
                var employee = await _unit.Employees.GetAsync(_userClaims.UserId);
                var teams = employee.Members.GroupBy(x => x.Team.Id).Select(y => y.Key).ToList();
                List<Project> projects = new List<Project>();

                foreach (var team in teams)
                {
                    projects.AddRange(_unit.Projects.Get(x => x.Team.Id == team));
                }

                query = new List<Customer>();

                foreach (var project in projects)
                {
                    query.Add(project.Customer);
                }
            }
            else
            {
                var task = await _unit.Customers.GetAsync();
                query = task.ToList();
            }

            return query;
        }

        public bool CanModifyEmployee(UserRoleModel _userClaims, Employee employee)
        {
            if (_userClaims.Role == "admin" || employee.Id == _userClaims.UserId) return true;
            return false;            
        }

        public async Task<List<Member>> GetAuthorizedMembers(UserRoleModel _userClaims)
        {
            List<Member> query;

            
            if (_userClaims.Role == "user")
            {
                //Gets team memebers?
                var task = await _unit.Members.GetAsync(x => x.Team.Members.Any(y => y.Employee.Id == _userClaims.UserId));
                query = task.ToList();
            }
            else
            {
                //also gets team members?
                query = queryService.GetTeamMembers(_userClaims.UserId);
            }

            return query;
        }

        public bool CanGetMember(UserRoleModel userClaims, Member member)
        {
            if (userClaims.Role == "user" && !member.Team.Members.Any(x => x.Employee.Id == userClaims.UserId))
            {
                return false;
            }
            return true;
        }
        public bool CanPostMember(UserRoleModel userClaims, Member member)
        {
            Team team = _unit.Teams.Get(member.Team.Id);
            if (userClaims.Role == "lead" && !team.Members.Any(x => x.Employee.Id == userClaims.UserId))
            {
                return false;
            }
            return true;
        }
        public async Task<List<Project>> GetAuthorizedProjects(UserRoleModel userClaims)
        {
            List<Project> query = new List<Project>();

            if (userClaims.Role == "lead")
            {
                var task = await _unit.Projects.GetAsync(x => x.Team.Members.Any(y => y.Employee.Id == userClaims.UserId));
                query = task.ToList();
            }
            else
            {
                var task = await _unit.Projects.GetAsync();
                query = task.ToList();
            }

            return query;
        }
        public bool CanGetProject(UserRoleModel userClaims, Project project)
        {
            if (userClaims.Role == "lead" && !project.Team.Members.Any(x => x.Employee.Id == userClaims.UserId))
            {
                return false;
            }
            return true;
        }

        public async Task<List<Team>> GetAuthorizedTeams(UserRoleModel userClaims)
        {
            List<Team> query = new List<Team>();
            if (userClaims.Role == "user" || userClaims.Role == "lead")
            {
                var task = await _unit.Teams.GetAsync(x => x.Members.Any(y => y.Employee.Id == userClaims.UserId));
                query = task.ToList();
            }
            else
            {
                var task = await _unit.Teams.GetAsync();
                query = task.ToList();
            }

            return query;
        }
        public bool CanGetTeam(UserRoleModel userClaims, Team team)
        {
            if (userClaims.Role == "user" || (userClaims.Role == "lead" && !team.Members.Any(x => x.Employee.Id == userClaims.UserId)))
            {
                return false;
            }
            return true;
        }

    }
}
