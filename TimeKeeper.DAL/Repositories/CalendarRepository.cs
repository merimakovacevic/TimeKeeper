using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class CalendarRepository: Repository<Day>
    {
        public CalendarRepository(TimeKeeperContext context) : base(context) { }

        public override void Insert(Day day)
        {
            day.Build(_context);
            base.Insert(day);
        }
        public override void Update(Day day, int id)
        {
            Day old = Get(id);
            ValidateUpdate(day, id);

            if (old != null)
            {
                day.Build(_context);
                _context.Entry(old).CurrentValues.SetValues(day);
                old.Update(day);
            }
        }

        public override void Delete(int id)
        {
            Day old = Get(id);

            if (!old.CanDelete())
            {
                Services.ThrowChildrenPresentException();
            }

            Delete(old);
        }
    }
}
