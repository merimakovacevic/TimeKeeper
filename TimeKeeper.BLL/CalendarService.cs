using System;
using System.Collections.Generic;
using System.Linq;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.Utility.Factory;
using TimeKeeper.DTO;
using TimeKeeper.BLL.Utilities;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using TimeKeeper.DAL;

namespace TimeKeeper.BLL
{
    public class CalendarService : BLLBaseService
    {
        protected MasterModel future;
        protected MasterModel empty;
        protected MasterModel weekend;
        protected MasterModel na;
        protected StoredProcedureService storedProcedureService;

        public CalendarService(UnitOfWork unit) : base(unit)
        {
            future = new MasterModel { Id = 10, Name = "Future" };
            empty = new MasterModel { Id = 11, Name = "Empty" };
            weekend = new MasterModel { Id = 12, Name = "Weekend" };
            na = new MasterModel { Id = 13, Name = "N/A" };
            storedProcedureService = new StoredProcedureService(unit);
        }                                        

        public List<DayModel> GetEmployeeMonth(int employeeId, int year, int month)
        {
            List<DayModel> employeeDays = GetEmployeeCalendar(employeeId, year, month);
            return FillEmployeeCalendar(employeeDays, _unit.Employees.Get(employeeId), year, month);
        }

        public List<DayModel> GetEmployeeMonth(Employee employee, int year, int month)
        {
            List<DayModel> employeeDays = GetEmployeeCalendar(employee, year, month);
            return FillEmployeeCalendar(employeeDays, employee, year, month);
        }

        private List<DayModel> FillEmployeeCalendar(List<DayModel> employeeDays, Employee employee, int year, int month)
        {
            List<DayModel> calendar = GetEmptyEmployeeCalendar(employee, year, month);
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
        public List<DayModel> GetOrderedEmployeeCalendar(Employee employee, int year, int month)
        {
            //Add validaiton!
            return employee.Calendar.Where(x => x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).OrderBy(x => x.Date).ToList();
        }
        public List<DayModel> GetEmployeeCalendar(int empId, int year, int month)
        {
            //Add validaiton!
            return _unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).ToList();
        }

        public List<DayModel> GetEmptyEmployeeCalendar(Employee employee, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!Validator.ValidateMonth(year, month)) throw new Exception("Invalid data! Check year and month");
                       
            DateTime day = new DateTime(year, month, 1);
            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    Employee = employee.Master(),
                    Date = day,
                    DayType = empty
                };

                if (day.IsWeekend()) newDay.DayType = weekend;
                if (day > DateTime.Today) newDay.DayType = future;
                if (day < employee.BeginDate || (employee.EndDate != new DateTime(1, 1, 1) && employee.EndDate != null && day > employee.EndDate)) newDay.DayType = na;

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

        public decimal GetBradfordFactor(int employeeId, int year)
        {
            //List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
            List<DateTime> sickDays = storedProcedureService.GetStoredProcedure<DateTime>("SickDays", new int[] { employeeId, year });
            //an absence instance are any number of consecutive absence days. 3 consecutive absence days make an instance.
            int absenceInstances = 0;
            int absenceDays = 0;
            //calendar = calendar.OrderBy(x => x.Date).ToList();

            //Bradford factor calculates only dates until the present day, because the calendar in argument returns the whole period
            //absenceDays = calendar.Where(x => x.DayType.Name == "Sick" && x.Date < DateTime.Now).Count();
            absenceDays = sickDays.Count();

            for (int i = 0; i < sickDays.Count; i++)
            {              
                if (i == 0) absenceInstances++;
                else
                {
                    double dateDiff = (sickDays[i].Date - sickDays[i - 1].Date).TotalDays;
                    if (dateDiff > 1)
                    {
                        //checks if the timespan insn't a weekend
                        if (dateDiff != 2)
                        {
                            absenceInstances++;
                        }
                        else if(sickDays[i].DayOfWeek == DayOfWeek.Friday)
                        {
                            absenceInstances++;
                        }
                    }
                }
                
            }
            return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
        }

        //public List<DayModel> GetEmployeeCalendar(Employee employee, int year, int month, int team)
        //{
        //    //Add validaiton!
        //    int em
        //    var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = $"select * from TeamTimeEmployee({team},{year},{month})";
        //    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
        //    DbDataReader sql = cmd.ExecuteReader();
        //    List<TeamTimeRawModel> rawData = new List<TeamTimeRawModel>();
        //    if (sql.HasRows)
        //    {
        //        while (sql.Read())
        //        {
        //            rawData.Add();
        //        }
        //    }
        //}

    }
}
