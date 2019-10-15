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
        private Repository<Project> _projects;
        private Repository<PricingStatus> _pricingstatuses;
        private Repository<Customer> _customers;
        private Repository<CustomerStatus> _customerstatuses;
        private Repository<Day> _calendar;
        private Repository<DayType> _daytypes;
        private Repository<Employee> _employees;
        private Repository<EmployeePosition> _employeepositions;
        private Repository<EmploymentStatus> _employmentstatuses;
        private Repository<JobDetail> _tasks;
        private Repository<Member> _members;
        private Repository<ProjectStatus> _projectstatuses;
        private Repository<Role> _roles;
        private Repository<Team> _teams;

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
