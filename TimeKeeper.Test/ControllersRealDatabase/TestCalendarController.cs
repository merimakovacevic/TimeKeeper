using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersRealDatabase
{
    [TestFixture]
    public class TestCalendarController : BaseRealDatabase
    {
        private EmployeeTimeModel CreateEmployeeTimeModels(int employeeId)
        {
            List<DayType> dayTypesInMemory = unit.DayTypes.Get().ToList();
            dayTypesInMemory.Add(new DayType { Id = 10, Name = "Future" });
            dayTypesInMemory.Add(new DayType { Id = 11, Name = "Empty" });
            dayTypesInMemory.Add(new DayType { Id = 12, Name = "Weekend" });
            dayTypesInMemory.Add(new DayType { Id = 13, Name = "N/A" });           

            //This test employee has only 12 work hours in the test database
            EmployeeTimeModel assertEmployee = unit.Employees.Get(employeeId).CreateTimeModel();
            foreach (DayType dayType in dayTypesInMemory)
            {
                assertEmployee.HourTypes.Add(dayType.Name, 0);
            }

            assertEmployee.HourTypes["Workday"] = 120;
            assertEmployee.HourTypes["Holiday"] = 16;
            assertEmployee.HourTypes["Vacation"] = 40;
            /*There are in fact 22 day entries in the Calendar table, plus the 8 weekend days for january 2018 which aren't saved in the database, 
             * should make a total of 30 days entered. This should mean that there is 1 missing entry (31 total days for january).
             * But, 2 of those 8 weekend days are entered as holidays, which ARE saved in the database. 
             * This means that there are actually 3 missing entries = 24 hours*/
            assertEmployee.HourTypes.Add("Missing entries", 24);
            assertEmployee.HourTypes.Add("Total hours", 176);

            assertEmployee.PaidTimeOff = assertEmployee.HourTypes["Holiday"] + assertEmployee.HourTypes["Vacation"];

            return assertEmployee;
        }


        [Test, Order(1)]
        [TestCase(2, 2018, 1)]
        public void GetPersonalReport(int employeeId, int year, int month)
        {
            var controller = new CalendarController(unit.Context);

            var response = controller.GetPersonalReport(employeeId, year, month) as ObjectResult;
            var value = response.Value as EmployeeTimeModel;
            EmployeeTimeModel assertEmployee = CreateEmployeeTimeModels(employeeId);

            Assert.AreEqual(200, response.StatusCode);
            foreach (KeyValuePair<string, decimal> hourType in value.HourTypes)
            {
                Assert.AreEqual(assertEmployee.HourTypes[hourType.Key], hourType.Value);
            }
            Assert.AreEqual(assertEmployee.Overtime, value.Overtime);
            Assert.AreEqual(assertEmployee.PaidTimeOff, value.PaidTimeOff);          
        }
    }
}
