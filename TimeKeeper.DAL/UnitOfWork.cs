using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Repositories;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL
{
    public class UnitOfWork : IDisposable
    {
        protected TimeKeeperContext _context;

        private IRepository<Project> _projects;
        private IRepository<PricingStatus> _pricingStatuses;
        private IRepository<Customer> _customers;
        private IRepository<CustomerStatus> _customerStatuses;
        private IRepository<Day> _calendar;
        private IRepository<DayType> _dayTypes;
        private IRepository<Employee> _employees;
        private IRepository<EmployeePosition> _employeePositions;
        private IRepository<EmploymentStatus> _employmentStatuses;
        private IRepository<JobDetail> _tasks;
        private IRepository<Member> _members;
        private IRepository<MemberStatus> _memberStatuses;
        private IRepository<ProjectStatus> _projectStatuses;
        private IRepository<Role> _roles;
        private IRepository<Team> _teams;
        private IRepository<User> _users;

        //Will the context injection here be necessary?
        //It is ensured that the context is always instantiated, so a default argument in UnitOfWork contstructor is not neccessary
        public UnitOfWork(TimeKeeperContext context)
        {
            _context = context;
        }

        public TimeKeeperContext Context => _context;

        public IRepository<Customer> Customers => _customers ?? (_customers = new CustomersRepository(_context));
        public IRepository<CustomerStatus> CustomerStatuses => _customerStatuses ?? (_customerStatuses = new Repository<CustomerStatus>(_context));
        public IRepository<Day> Calendar => _calendar ?? (_calendar = new CalendarRepository(_context));
        public IRepository<DayType> DayTypes => _dayTypes ?? (_dayTypes = new Repository<DayType>(_context));
        public IRepository<Employee> Employees => _employees ?? (_employees = new EmployeesRepository(_context));
        public IRepository<EmployeePosition> EmployeePositions => _employeePositions ?? (_employeePositions = new Repository<EmployeePosition>(_context));
        public IRepository<EmploymentStatus> EmploymentStatuses => _employmentStatuses ?? (_employmentStatuses = new Repository<EmploymentStatus>(_context));
        public IRepository<JobDetail> Tasks => _tasks ?? (_tasks = new TasksRepository(_context));
        public IRepository<Member> Members => _members ?? (_members = new MembersRepository(_context));
        public IRepository<MemberStatus> MemberStatuses => _memberStatuses ?? (_memberStatuses = new Repository<MemberStatus>(_context));
        public IRepository<PricingStatus> PricingStatuses => _pricingStatuses ?? (_pricingStatuses = new Repository<PricingStatus>(_context));
        public IRepository<Project> Projects => _projects ?? (_projects = new ProjectsRepository(_context));
        public IRepository<ProjectStatus> ProjectStatuses => _projectStatuses ?? (_projectStatuses = new Repository<ProjectStatus>(_context));
        public IRepository<Role> Roles => _roles ?? (_roles = new RolesRepository(_context));
        public IRepository<Team> Teams => _teams ?? (_teams = new TeamsRepository(_context));
        public IRepository<User> Users => _users ?? (_users = new Repository<User>(_context));


        public int Save() => _context.SaveChanges();

        public async Task<int> SaveAsync() { return await _context.SaveChangesAsync(); }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
