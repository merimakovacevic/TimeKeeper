using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    class CustomersRepository: Repository<Customer>
    {
        public CustomersRepository(TimeKeeperContext context) : base(context) { }

        public override void Insert(Customer customer)
        {
            customer.Build(_context);
            base.Insert(customer);
        }

        public override void Update(Customer customer, int id)
        {
            Customer old = Get(id);
            ValidateUpdate(customer, id);

            if (old != null)
            {
                customer.Build(_context);              
                _context.Entry(old).CurrentValues.SetValues(customer);
                old.Update(customer);
            }
        }

        public override void Delete(int id)
        {
            Customer old = Get(id);

            if (!old.CanDelete())
            {
                Services.ThrowChildrenPresentException();
            }

            Delete(old);
        }
    }
}
