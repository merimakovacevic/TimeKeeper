using System;
using System.Collections.Generic;
using System.Linq;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.Utility.Factory;
using TimeKeeper.DTO;
using TimeKeeper.BLL.Utilities;

namespace TimeKeeper.BLL
{
    public class CalendarService : BLLBaseService
    {
        public CalendarService(UnitOfWork unit) : base(unit)  {  }                                        

        public List<DayModel> GetEmployeeMonth(int employeeId, int year, int month)
        {
            List<DayModel> employeeDays = GetEmployeeCalendar(employeeId, year, month);
            return FillEmployeeCalendar(employeeDays, employeeId, year, month);
        }

        public List<DayModel> GetEmployeeMonth(Employee employee, int year, int month)
        {
            List<DayModel> employeeDays = GetEmployeeCalendar(employee, year, month);
            return FillEmployeeCalendar(employeeDays, employee.Id, year, month);
        }

        private List<DayModel> FillEmployeeCalendar(List<DayModel> employeeDays, int employeeId, int year, int month)
        {
            List<DayModel> calendar = GetEmptyEmployeeCalendar(employeeId, year, month);
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
        public List<DayModel> GetEmployeeCalendar(Employee employee, int year, int month)
        {
            //Add validaiton!
            return employee.Calendar.Where(x => x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).ToList();
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
            Employee employee = _unit.Employees.Get(employeeId);
            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    Employee = employee.Master(),
                    Date = day,
                    DayType = empty.Master()
                };

                if (day.IsWeekend()) newDay.DayType = weekend.Master();
                if (day > DateTime.Today) newDay.DayType = future.Master();
                if (day < employee.BeginDate || (employee.EndDate != new DateTime(1, 1, 1) && employee.EndDate != null && day > employee.EndDate)) newDay.DayType = na.Master();

                calendar.Add(newDay);
                day = day.AddDays(1);
            }

            return calendar;
        }


        /*
        private List<DayModel> GetEmptyEmployeeCalendar(int employeeId, int year, int month)
        {
            List<DayModel> calendar = GetEmptyGenericCalendar(year, month);
            Employee employee = _unit.Employees.Get(employeeId);
            DayType na = new DayType { Id = 13, Name = "N/A" };

            foreach(DayModel day in calendar)
            {
                day.Employee = employee.Master();
                if (day.Date < employee.BeginDate || (employee.EndDate != new DateTime(1, 1, 1) && employee.EndDate != null && day.Date > employee.EndDate)) day.DayType = na.Master();
            }

            return calendar;
        }

        protected List<DayModel> GetEmptyGenericCalendar(int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!Validator.ValidateMonth(year, month)) throw new Exception("Invalid data! Check year and month");

            DayType future = new DayType { Id = 10, Name = "Future" };
            DayType empty = new DayType { Id = 11, Name = "Empty" };
            DayType weekend = new DayType { Id = 12, Name = "Weekend" };
            DayType na = new DayType { Id = 13, Name = "N/A" };

            DateTime day = new DateTime(year, month, 1);

            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    //The Employee will be added when filling the generic calendar afterwards
                    Employee = null,
                    Date = day,
                    DayType = empty.Master()
                };

                if (day.IsWeekend()) newDay.DayType = weekend.Master();
                if (day > DateTime.Today) newDay.DayType = future.Master();

                calendar.Add(newDay);
                day = day.AddDays(1);
            }

            return calendar;
        }*/

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
