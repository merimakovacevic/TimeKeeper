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
        private UnitOfWork unit;
        private QueryService queryService;
        public ResouceAccessHandler(UnitOfWork unit)
        {
            this.unit = unit;
        }
        public bool CanReadOrWriteTask(UserRoleModel userClaims, JobDetail newTask)
        {
            Project project = unit.Projects.Get(newTask.Project.Id);
            Day day = unit.Calendar.Get(newTask.Day.Id);

            if (userClaims.Role == "lead" && !project.Team.Members.Any(x => x.Employee.Id == userClaims.UserId) ||
                userClaims.Role == "user" && !(day.Employee.Id == userClaims.UserId))
            {
                return false;
            }

            return true;
        }

        public async Task<List<JobDetail>> GetAuthorizedTasks(UserRoleModel userClaims)
        {
            List<JobDetail> query;

            if (userClaims.Role == "lead")
            {
                var task = await unit.Tasks.GetAsync(x => x.Project.Team.Members.Any(y => y.Employee.Id == userClaims.UserId));
                query = task.ToList();
            }
            else if (userClaims.Role == "user")
            {
                var task = await unit.Tasks.GetAsync(x => x.Day.Employee.Id == userClaims.UserId);
                query = task.ToList();
            }
            else
            {
                var task = await unit.Tasks.GetAsync();
                query = task.ToList();
            }

            return query;
        }

        public bool CanReadDay(UserRoleModel userClaims, Day day)
        {
            if (userClaims.Role == "lead" && !day.JobDetails.Any(x => x.Project.Team.Members.Any(y => y.Employee.Id == userClaims.UserId)) ||
                    userClaims.Role == "user" && !day.JobDetails.Any(x => x.Project.Team.Members.Any(y => y.Employee.Id == userClaims.UserId)))
            {
                return false;
            }

            return true;
        }
        public bool CanWriteDay(UserRoleModel userClaims, Day newDay)
        {
            Day day = unit.Calendar.Get(newDay.Id);
            if (userClaims.Role != "admin" && !(day.Employee.Id == userClaims.UserId))
            {
                return false;
            }
            return true;
        }

        public async Task<List<Customer>> GetAuthorizedCustomers(UserRoleModel userClaims)
        {
            List<Customer> query;

            if (userClaims.Role == "lead")
            {
                var employee = await unit.Employees.GetAsync(userClaims.UserId);
                var teams = employee.Members.GroupBy(x => x.Team.Id).Select(y => y.Key).ToList();
                List<Project> projects = new List<Project>();

                foreach (var team in teams)
                {
                    projects.AddRange(unit.Projects.Get(x => x.Team.Id == team));
                }

                query = new List<Customer>();

                foreach (var project in projects)
                {
                    query.Add(project.Customer);
                }
            }
            else
            {
                var task = await unit.Customers.GetAsync();
                query = task.ToList();
            }

            return query;
        }

        public bool CanWriteEmployee(UserRoleModel userClaims, Employee employee)
        {
            if (userClaims.Role == "admin" || employee.Id == userClaims.UserId) return true;
            return false;            
        }

        public async Task<List<Member>> GetAuthorizedMembers(UserRoleModel userClaims)
        {
            List<Member> query;

            
            if (userClaims.Role == "user")
            {
                //Gets team memebers?
                var task = await unit.Members.GetAsync(x => x.Team.Members.Any(y => y.Employee.Id == userClaims.UserId));
                query = task.ToList();
            }
            else
            {
                //also gets team members?
                query = queryService.GetTeamMembers(userClaims.UserId);
            }

            return query;
        }

        public bool CanReadMember(UserRoleModel userClaims, Member member)
        {
            if (userClaims.Role == "user" && !member.Team.Members.Any(x => x.Employee.Id == userClaims.UserId))
            {
                return false;
            }
            return true;
        }
        public bool CanWriteMember(UserRoleModel userClaims, Member member)
        {
            Team team = unit.Teams.Get(member.Team.Id);
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
                var task = await unit.Projects.GetAsync(x => x.Team.Members.Any(y => y.Employee.Id == userClaims.UserId));
                query = task.ToList();
            }
            else
            {
                var task = await unit.Projects.GetAsync();
                query = task.ToList();
            }

            return query;
        }
        public bool CanReadProject(UserRoleModel userClaims, Project project)
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
                var task = await unit.Teams.GetAsync(x => x.Members.Any(y => y.Employee.Id == userClaims.UserId));
                query = task.ToList();
            }
            else
            {
                var task = await unit.Teams.GetAsync();
                query = task.ToList();
            }

            return query;
        }
        public bool CanReadTeam(UserRoleModel userClaims, Team team)
        {
            if (userClaims.Role == "user" || (userClaims.Role == "lead" && !team.Members.Any(x => x.Employee.Id == userClaims.UserId)))
            {
                return false;
            }
            return true;
        }

    }
}
