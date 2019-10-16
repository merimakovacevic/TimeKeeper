using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Repositories;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly TimeKeeperContext _context;

        private IRepository<Project, int> _projects;
        private IRepository<PricingStatus, int> _pricingStatuses;
        private IRepository<Customer, int> _customers;
        private IRepository<CustomerStatus, int> _customerStatuses;
        private IRepository<Day, int> _calendar;
        private IRepository<DayType, int> _dayTypes;
        private IRepository<Employee, int> _employees;
        private IRepository<EmployeePosition, int> _employeePositions;
        private IRepository<EmploymentStatus, int> _employmentStatuses;
        private IRepository<JobDetail, int> _tasks;
        private IRepository<Member, int> _members;
        private IRepository<ProjectStatus, int> _projectStatuses;
        private IRepository<Role, int> _roles;
        private IRepository<Team, int> _teams;

        //Will the context injection here be necessary?
        public UnitOfWork(TimeKeeperContext context=null)
        {
            if (context != null)
            {
                _context = context;
            }
            else
            {
                _context = new TimeKeeperContext();
            }
        }


        //public IRepository<Customer> Customers => _customers ?? (_customers = new CustomersRepository(_context));
        public IRepository<Customer, int> Customers => _customers ?? (_customers = new Repository<Customer, int>(_context));
        public IRepository<CustomerStatus, int> CustomerStatuses => _customerStatuses ?? (_customerStatuses = new Repository<CustomerStatus, int>(_context));
        //public IRepository<Day> Calendar => _calendar ?? (_calendar = new CalendarRepository(_context));
        public IRepository<Day, int> Calendar => _calendar ?? (_calendar = new Repository<Day, int>(_context));
        public IRepository<DayType, int> DayTypes => _dayTypes ?? (_dayTypes = new Repository<DayType, int>(_context));
       // public IRepository<Employee> Employees => _employees ?? (_employees = new EmployeesRepository(_context));
        public IRepository<Employee, int> Employees => _employees ?? (_employees = new Repository<Employee, int>(_context));
        public IRepository<EmployeePosition, int> EmployeePositions => _employeePositions ?? (_employeePositions = new Repository<EmployeePosition, int>(_context));
        public IRepository<EmploymentStatus, int> EmploymentStatuses => _employmentStatuses ?? (_employmentStatuses = new Repository<EmploymentStatus, int>(_context));
        public IRepository<JobDetail, int> Tasks => _tasks ?? (_tasks = new Repository<JobDetail, int>(_context));
        //public IRepository<Member> Members => _members ?? (_members = new MembersRepository(_context));
        public IRepository<Member, int> Members => _members ?? (_members = new Repository<Member, int>(_context));
        public IRepository<PricingStatus, int> PricingStatuses => _pricingStatuses ?? (_pricingStatuses = new Repository<PricingStatus, int>(_context));
        //public IRepository<Project> Projects => _projects ?? (_projects = new ProjectsRepository(_context));
        public IRepository<Project, int> Projects => _projects ?? (_projects = new Repository<Project, int>(_context));
        public IRepository<ProjectStatus, int> ProjectStatuses => _projectStatuses ?? (_projectStatuses = new Repository<ProjectStatus, int>(_context));
        public IRepository<Role, int> Roles => _roles ?? (_roles = new Repository<Role, int>(_context));
        public IRepository<Team, int> Teams => _teams ?? (_teams = new Repository<Team, int>(_context));



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
