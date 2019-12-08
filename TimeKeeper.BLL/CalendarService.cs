using System;
using System.Collections.Generic;
using System.Linq;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.Utility.Factory;
using TimeKeeper.DTO;

namespace TimeKeeper.BLL
{
    public class CalendarService : BLLBaseService
    {
        public CalendarService(UnitOfWork unit) : base(unit)
        {

        }                                        

        public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        {
            List<DayModel> calendar = GetEmptyEmployeeCalendar(empId, year, month);
            List<DayModel> employeeDays = GetEmployeeCalendar(empId, year, month);

            foreach (var d in employeeDays)
            {
                calendar[d.Date.Day - 1] = d;
            }
            return calendar;
        }     

        public List<DayModel> GetEmployeeCalendar(int employeeId, int year)
        {
            //Add validaiton!
            return _unit.Calendar.Get(x => x.Employee.Id == employeeId && x.Date.Year == year).Select(x => x.Create()).ToList();
        }
        public List<DayModel> GetEmployeeCalendar(int empId, int year, int month)
        {
            //Add validaiton!
            return _unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).ToList();
        }
        public List<DayModel> GetEmptyEmployeeCalendar(int employeeId, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!Validator.ValidateMonth(year, month)) throw new Exception("Invalid data! Check year and month");

            DayType future = new DayType { Id = 10, Name = "Future" };
            DayType empty = new DayType { Id = 11, Name = "Empty" };
            DayType weekend = new DayType { Id = 12, Name = "Weekend" };
            DayType na = new DayType { Id = 13, Name = "N/A" };

            DateTime day = new DateTime(year, month, 1);
            Employee emp = _unit.Employees.Get(employeeId);
            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    Employee = emp.Master(),
                    Date = day,
                    DayType = empty.Master()
                };

                if (day.IsWeekend()) newDay.DayType = weekend.Master();
                if (day > DateTime.Today) newDay.DayType = future.Master();
                if (day < emp.BeginDate || (emp.EndDate != new DateTime(1, 1, 1) && emp.EndDate != null && day > emp.EndDate)) newDay.DayType = na.Master();

                calendar.Add(newDay);
                day = day.AddDays(1);
            }

            return calendar;
        }

        public int GetYearlyWorkingDays(int year)
        {
            int workingDays = 0;
            for (int i = 1; i <= 12; i++)
            {
                workingDays += GetMonthlyWorkingDays(year, i);
            }

            return workingDays;
        }
        public int GetMonthlyWorkingDays(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workingDays = 0;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime thisDay = new DateTime(year, month, i);
                if (!thisDay.IsWeekend() && thisDay < DateTime.Now)
                {
                    workingDays++;
                }
            }

            return workingDays;
        }

        public int GetMonthlyWorkingDays(Employee employee, int year, int month)
        {
            //this method counts only days until the present day
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workingDays = 0;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime thisDay = new DateTime(year, month, i);
                if (!thisDay.IsWeekend() && thisDay < DateTime.Now && employee.BeginDate < thisDay && (employee.EndDate != new DateTime(1, 1, 1) && employee.EndDate != null && thisDay > employee.EndDate))
                {
                    workingDays++;
                }
            }

            return workingDays;
        }   
    }
}
