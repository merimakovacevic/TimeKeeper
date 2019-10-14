using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class DayType: BaseStatus
    {
        public DayType()
        {
            Calendar = new List<Day>();
        }
        public IList<Day> Calendar { get; set; }
    }
}
