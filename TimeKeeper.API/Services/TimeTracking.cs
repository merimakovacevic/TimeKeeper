using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Services
{
    public static class TimeTracking
    {
        public static decimal GetHoursByDayType(DayType dayType, Employee employee, int year, int month)
        {
            decimal totalHours = 0;
            List<Day> days = employee.Calendar.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();

            foreach(Day day in days)
            {
                if(day.DayType == dayType)
                {
                    totalHours += day.TotalHours;
                }
            }

            return totalHours;
        }
    }
}
