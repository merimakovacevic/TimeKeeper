using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class EmployeesRepository: Repository<Employee>
    {
        public EmployeesRepository(TimeKeeperContext context): base(context) { }

        public override void Insert(Employee employee)
        {
            Build(employee);
            base.Insert(employee);
        }

        private void Build(Employee employee)
        {
            employee.Status = _context.EmploymentStatuses.Find(employee.Status.Id);
            employee.Position = _context.EmployeePositions.Find(employee.Position.Id);
        }

        public override void Update(Employee employee, int id)
        {
            Employee old = Get(id);
            ValidateUpdate(employee, id);

            if (old != null)
            {
                Build(employee);
                _context.Entry(old).CurrentValues.SetValues(employee);
                old.Position = employee.Position;
                old.Status = employee.Status;
            }
            else throw new ArgumentNullException();
        }
        public override void Delete(int id)
        {
            Employee old = Get(id);

            if (old.Calendar.Count != 0 || old.Members.Count != 0)
            {
                Services.ThrowChildrenPresentException();
            }

            Delete(old);
        }
    }


}
