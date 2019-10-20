using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class CustomersRepository: Repository<Customer>
    {
        public CustomersRepository(TimeKeeperContext context) : base(context) { }

        public override void Update(Customer customer, int id)
        {
            Customer old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(customer);
                old.Status = customer.Status;
                old.HomeAddress = customer.HomeAddress;
            }
        }
    }
}
