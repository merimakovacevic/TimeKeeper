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
        private IRepository<Project> _projects;
        private IRepository<PricingStatus> _pricingStatuses;
        private IRepository<Customer> _customers;
        private IRepository<CustomerStatus> _customerstatuses;
        private IRepository<Day> _calendar;
        private IRepository<DayType> _daytypes;
        private IRepository<Employee> _employees;
        private IRepository<EmployeePosition> _employeepositions;
        private IRepository<EmploymentStatus> _employmentstatuses;
        private IRepository<JobDetail> _tasks;
        private IRepository<Member> _members;
        private IRepository<ProjectStatus> _projectstatuses;
        private IRepository<Role> _roles;
        private IRepository<Team> _teams;

        //Will the context injection here necessary?
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
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
