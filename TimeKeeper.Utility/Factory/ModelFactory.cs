using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels;

namespace TimeKeeper.Utility.Factory
{
    public static class ModelFactory
    {
        public static TeamModel Create(this Team team)
        {
            return new TeamModel
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                StatusActive = team.StatusActive,
                Members = team.Members.Select(x => x.Master("team")).ToList(),
                Projects = team.Projects.Select(x => x.Master()).ToList()
            };
        }

        public static EmployeeModel Create(this Employee employee)
        {
            return new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = employee.FullName,
                Email = employee.Email,
                Phone = employee.Phone,
                Position = employee.Position.Master(),
                Salary = employee.Salary,
                Birthday = employee.Birthday,
                BeginDate = employee.BeginDate,
                EndDate = employee.EndDate,
                Status = employee.Status.Master(),
                Members = employee.Members.Select(x => x.Master("role")).ToList(),
                Calendar = employee.Calendar.Select(x => x.Master()).ToList()
            };
        }

        public static CustomerModel Create(this Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                ContactName = customer.ContactName,
                EmailAddress = customer.EmailAddress,
                HomeAddress = new MasterModelAddress { Street = customer.HomeAddress.Street, City = customer.HomeAddress.City },
                Status = customer.Status.Master(),
                Projects = customer.Projects.Select(x => x.Master()).ToList()
            };
        }

        public static ProjectModel Create(this Project project)
        {
            return new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Team = project.Team.Master(),
                Customer = project.Customer.Master(),
                Amount = project.Amount,
                Status = project.Status.Master(),
                Pricing = project.Pricing.Master(),
                Tasks = project.Tasks.Select(x => x.Master()).ToList()
            };
        }

        public static RoleModel Create(this Role role)
        {
            return new RoleModel
            {
                Id = role.Id,
                Name = role.Name,
                HourlyPrice = role.HourlyPrice,
                MonthlyPrice = role.MonthlyPrice,
                Members = role.Members.Select(y => y.Master("role")).ToList()
            };
        }

        public static JobDetailModel Create(this JobDetail jobDetail)
        {
            return new JobDetailModel
            {
                Id = jobDetail.Id,
                Description = jobDetail.Description,
                Day = jobDetail.Day.Master(),
                Project = jobDetail.Project.Master(),
                Hours = jobDetail.Hours
            };
        }

        public static MemberModel Create(this Member member)
        {
            return new MemberModel
            {
                Id = member.Id,
                Team = member.Team.Master(),
                Employee = member.Employee.Master(),
                Role = member.Role.Master(),
                Status = member.Status.Master(),
                HoursWeekly = member.HoursWeekly
            };
        }

        public static DayModel Create(this Day day)
        {
            return new DayModel
            {
                Id = day.Id,
                Employee = day.Employee.Master(),
                Date = day.Date,
                DayType = day.DayType.Master(),
                TotalHours = day.TotalHours,
                JobDetails = day.JobDetails.Select(x => x.Create()).ToList()
            };
        }

        public static UserModel Create(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}