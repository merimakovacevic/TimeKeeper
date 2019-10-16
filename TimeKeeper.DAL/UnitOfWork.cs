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
        private Repository<Project, int> _projects;
        private Repository<PricingStatus, int> _pricingstatuses;
        private Repository<Customer, int> _customers;
        private Repository<CustomerStatus, int> _customerstatuses;
        private Repository<Day, int> _calendar;
        private Repository<DayType, int> _daytypes;
        private Repository<Employee, int> _employees;
        private Repository<EmployeePosition, int> _employeepositions;
        private Repository<EmploymentStatus, int> _employmentstatuses;
        private Repository<JobDetail, int> _tasks;
        private Repository<Member, int> _members;
        private Repository<ProjectStatus, int> _projectstatuses;
        private Repository<Role, int> _roles;
        private Repository<Team, int> _teams;

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
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
