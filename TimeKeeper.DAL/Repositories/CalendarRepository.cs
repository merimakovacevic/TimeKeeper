using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class CalendarRepository: Repository<Day>
    {
        public CalendarRepository(TimeKeeperContext context) : base(context) { }
        public override void Update(Day day, int id)
        {
            Day old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(day);
                old.Employee = day.Employee;
                old.DayType = day.DayType;
            }
        }
    }
}
