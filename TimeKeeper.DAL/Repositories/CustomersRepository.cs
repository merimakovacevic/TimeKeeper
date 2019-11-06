using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class CustomersRepository: Repository<Customer>
    {
        public CustomersRepository(TimeKeeperContext context) : base(context) { }

        private void Build(Customer customer)
        {
            customer.Status = _context.CustomerStatuses.Find(customer.Status.Id);
        }

        public override void Insert(Customer customer)
        {
            Build(customer);
            base.Insert(customer);
        }

        public override void Update(Customer customer, int id)
        {
            Customer old = Get(id);

            if (old != null)
            {
                Build(customer);
                _context.Entry(old).CurrentValues.SetValues(customer);
                old.Status = customer.Status;
                old.HomeAddress = customer.HomeAddress;
            }
        }

        public override void Delete(int id)
        {
            Customer old = Get(id);

            if (old.Projects.Count != 0)
                throw new Exception("Object cannot be deleted because child objects are present");

            Delete(old);
        }
    }
}
