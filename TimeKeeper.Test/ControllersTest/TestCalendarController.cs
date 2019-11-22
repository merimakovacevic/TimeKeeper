using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestCalendarController : TestBase
    {
        [Test, Order(1)]
        [TestCase(2, 2018, 1, 22, 1, 0, 0, 8)]
        public void GetEmployeesMonthCalendar(int employeeId, int year, int month, int taskDays, int empty, int na, int future, int weekend)
        {
            var controller = new CalendarController(unit.Context);

            var response = controller.Get(employeeId, year, month) as ObjectResult;
            var value = response.Value as List<Day>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(taskDays, value.Count(x => x.DayType.Id <= 7));
            Assert.AreEqual(empty, value.Count(x => x.DayType.Name == "Empty"));
            Assert.AreEqual(na, value.Count(x => x.DayType.Name == "N/A"));
            Assert.AreEqual(future, value.Count(x => x.DayType.Name == "Future"));
            Assert.AreEqual(weekend, value.Count(x => x.DayType.Name == "Weekend"));
        }
    }
}
