using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;
using TimeKeeper.Utility.Services;

namespace TimeKeeper.DAL.Repositories
{
    public class EmployeesRepository: Repository<Employee>
    {
        public EmployeesRepository(TimeKeeperContext context): base(context) { }
        /*
        public override Employee Get(int id)
        {
            Employee employee = base.Get(id);

            if (employee == null)
            {
                throw new ArgumentNullException("Employee not found");
            }
            else
            {
                return employee;
            }
        }*/

        public override void Update(Employee employee, int id)
        {
            Employee old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(employee);
                old.Position = employee.Position;
                old.Status = employee.Status;
            }
        }
    }


}
