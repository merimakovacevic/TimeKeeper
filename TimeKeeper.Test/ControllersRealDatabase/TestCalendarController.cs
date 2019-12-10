using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TimeKeeper.API.Controllers;
using TimeKeeper.BLL.Utilities;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.Test.ControllersRealDatabase
{
    [TestFixture]
    public class TestCalendarController : TestBaseRealDatabase
    {
        /*This method is for manual creation of the assertion employee time models.
         * The calculated hours assigned were calculated using queries in PostgreSQL*/
        private List<EmployeeTimeModel> CreateEmployeeTimeModels()
        {
            List<EmployeeTimeModel> employeeTimes = new List<EmployeeTimeModel>();
            List<string> dayTypes = unit.DayTypes.Get().Select(x => x.Name).ToList();
            //
            //ASSERT EMPLOYEE NR. 1 - id nr 2 - William Brown
            //
            int firstEmployeeId = 2;

            //This test employee has only 12 work hours in the test database
            EmployeeTimeModel firstEmployee = new EmployeeTimeModel(dayTypes);
            firstEmployee.Employee = unit.Employees.Get(firstEmployeeId).Master();
            //SetHourTypes(firstEmployee.HourTypes);

            firstEmployee.HourTypes["Workday"] = 120;
            firstEmployee.HourTypes["Holiday"] = 16;
            firstEmployee.HourTypes["Vacation"] = 40;
            //6 actual weekend days in the calendar = 48 hours
            /*There are in fact 22 day entries in the Calendar table, plus the 8 weekend days for january 2018 which aren't saved in the database, 
             * should make a total of 30 days entered. This should mean that there is 1 missing entry (31 total days for january).
             * But, 2 of those 8 weekend days are entered as holidays, which ARE saved in the database. 
             * This means that there are actually 3 missing entries = 24 hours*/
             //Technically, holidays/vacations aren't paid if they happen to be on a weekend, and thus should not be saved!
            firstEmployee.HourTypes["Missing entries"] = 24;
            firstEmployee.TotalHours = 200;
            //Total hours + Weekends for the month = 248

            firstEmployee.PaidTimeOff = firstEmployee.HourTypes["Holiday"] + firstEmployee.HourTypes["Vacation"];

            employeeTimes.Add(firstEmployee);
            //
            //ASSERT EMPLOYEE NR. 2 - Id nr 41 - Laura Parker
            //
            int secondEmployeeId = 41;
            EmployeeTimeModel secondEmployee = new EmployeeTimeModel(dayTypes);
            secondEmployee.Employee = unit.Employees.Get(secondEmployeeId).Master();
            //SetHourTypes(secondEmployee.HourTypes);

            secondEmployee.HourTypes["Workday"] = 215;
            secondEmployee.HourTypes["Holiday"] = 0;
            secondEmployee.HourTypes["Vacation"] = 0;
            //4 actual weekend days in the calendar = 32 hours
            /*There are in fact 22 day entries in the Calendar table, plus the 8 weekend days for january 2018 which aren't saved in the database, 
             * should make a total of 30 days entered. This should mean that there is 1 missing entry (31 total days for january).
             * But, 2 of those 8 weekend days are entered as holidays, which ARE saved in the database. 
             * This means that there are actually 3 missing entries = 24 hours*/
            //Technically, holidays/vacations aren't paid if they happen to be on a weekend, and thus should not be saved!
            secondEmployee.HourTypes["Missing entries"] = 0;
            secondEmployee.TotalHours = 215;
            //Total hours + Weekends for the month = 247

            secondEmployee.Overtime = 24 + 15; //Weekend hours = 15
            secondEmployee.PaidTimeOff = secondEmployee.HourTypes["Holiday"] + secondEmployee.HourTypes["Vacation"];

            employeeTimes.Add(secondEmployee);
            return employeeTimes;
        }


        //a new test needs to be written for Dasbhoard and Reports controllers
       /* [Test, Order(1)]
        [TestCase(2, 2018, 1)]
        [TestCase(41, 2019, 4)]
        public void GetPersonalReport(int employeeId, int year, int month)
        {
            var controller = new ReportsController(unit.Context);

            var response = controller.GetPersonalReport(employeeId, year, month) as ObjectResult;
            var value = response.Value as EmployeeTimeModel;
            List<EmployeeTimeModel> employees = CreateEmployeeTimeModels();
            EmployeeTimeModel assertEmployee = employees.FirstOrDefault(x => x.Employee.Id == employeeId);

            Assert.AreEqual(200, response.StatusCode);
            foreach (KeyValuePair<string, decimal> hourType in value.HourTypes)
            {
                Assert.AreEqual(assertEmployee.HourTypes[hourType.Key], hourType.Value);
            }
            //The sum total hours field should be equal to the sum of all other fields
            Assert.AreEqual(value.TotalHours, value.HourTypes.Sum(x => x.Value));
            Assert.AreEqual(assertEmployee.Overtime, value.Overtime);
            Assert.AreEqual(assertEmployee.PaidTimeOff, value.PaidTimeOff);          
        }*/
    }
}
