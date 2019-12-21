using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;

namespace TimeKeeper.DAL.Repositories
{
    public class EmployeesRepository: Repository<Employee>
    {
        public EmployeesRepository(TimeKeeperContext context): base(context) { }

        public override void Insert(Employee employee)
        {
            employee.Build(_context);
            base.Insert(employee);
        }

        public override void Update(Employee employee, int id)
        {
            Employee old = Get(id);
            ValidateUpdate(employee, id);

            if (old != null)
            {
                employee.Build(_context);
                _context.Entry(old).CurrentValues.SetValues(employee);
                old.Update(employee);
            }
            else throw new ArgumentNullException();
        }

        public override void Delete(int id)
        {
            Employee old = Get(id);

            if (!old.CanDelete())
            {
                Services.ThrowChildrenPresentException();
            }

            Delete(old);
        }
    }


}
