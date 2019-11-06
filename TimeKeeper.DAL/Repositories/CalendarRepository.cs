using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class CalendarRepository: Repository<Day>
    {
        public CalendarRepository(TimeKeeperContext context) : base(context) { }
        private void Build(Day day)
        {
            day.Employee = _context.Employees.Find(day.Employee.Id);
            day.DayType = _context.DayTypes.Find(day.DayType.Id);
        }

        public override void Insert(Day day)
        {
            Build(day);
            base.Insert(day);
        }
        public override void Update(Day day, int id)
        {
            Day old = Get(id);

            if (old != null)
            {
                Build(day);
                _context.Entry(old).CurrentValues.SetValues(day);
                old.Employee = day.Employee;
                old.DayType = day.DayType;
            }
        }
    }
}
