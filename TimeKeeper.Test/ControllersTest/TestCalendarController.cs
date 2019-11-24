using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestCalendarController : BaseTestDatabase
    {
        [Test, Order(1)]
        [TestCase(3, 2019, 2, 3, 17, 0, 0, 8)]
        [TestCase(2, 2019, 6, 1, 19, 0, 0, 10)]
        /*Although this employee should have 28 N/A days in total (we are testing a month after her EndDate), 
         * there are 2 recorded working days in this month for her*/
        [TestCase(6, 2018, 2, 2, 0, 26, 0, 0)]
        /*This test should return 31 N/A days, because it is related to the month and year before employee's BeginDate*/
        [TestCase(5, 2015, 3, 0, 0, 31, 0, 0)]
        /*This test should return 31 future days, because it is related to the month and year after the present month and year*/
        [TestCase(5, 2020, 3, 0, 0, 0, 31, 0)]
        public void GetEmployeesMonthCalendar(int employeeId, int year, int month, int taskDays, int empty, int na, int future, int weekend)
        {
            var controller = new CalendarController(unit.Context);

            var response = controller.Get(employeeId, year, month) as ObjectResult;
            var value = response.Value as List<DayModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(taskDays, value.Count(x => x.DayType.Id <= 7));
            Assert.AreEqual(empty, value.Count(x => x.DayType.Name == "Empty"));
            Assert.AreEqual(na, value.Count(x => x.DayType.Name == "N/A"));
            Assert.AreEqual(future, value.Count(x => x.DayType.Name == "Future"));
            Assert.AreEqual(weekend, value.Count(x => x.DayType.Name == "Weekend"));
        }

        private List<EmployeeTimeModel> CreateTeamReport(int teamId)
        {
            List<EmployeeTimeModel> employeeTimes = new List<EmployeeTimeModel>();
            List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            employeeTimes = unit.Teams.Get(teamId).Members.Select(x => x.Employee.CreateTimeModel()).ToList();

            foreach (EmployeeTimeModel employeeTime in employeeTimes)
            {
                //
                employeeTime.HourTypes.SetHourTypes(unit);
                //SetHourTypes(employeeTime.HourTypes);
                employeeTime.TotalHours = 160;
            }

            employeeTimes.FirstOrDefault(x => x.Employee.Id == 5).HourTypes["Workday"] = 16;
            employeeTimes.FirstOrDefault(x => x.Employee.Id == 5).HourTypes["Missing entries"] = 18 * 8;
            employeeTimes.FirstOrDefault(x => x.Employee.Id == 1).HourTypes["Workday"] = 8;
            employeeTimes.FirstOrDefault(x => x.Employee.Id == 1).HourTypes["Missing entries"] = 19 * 8;
            employeeTimes.FirstOrDefault(x => x.Employee.Id == 4).HourTypes["Workday"] = 16;
            employeeTimes.FirstOrDefault(x => x.Employee.Id == 4).HourTypes["Missing entries"] = 18 * 8;
            /*
            employeeTimes.Where(x => x.Employee.Id == 5).Select(x => x.HourTypes["Workday"] = 16);
            employeeTimes.Where(x => x.Employee.Id == 5).Select(x => x.HourTypes["Missing entries"] = 18 * 8);
            employeeTimes.Where(x => x.Employee.Id == 1).Select(x => x.HourTypes["Workday"] = 8);
            employeeTimes.Where(x => x.Employee.Id == 1).Select(x => x.HourTypes["Missing entries"] = 19 * 8);
            employeeTimes.Where(x => x.Employee.Id == 4).Select(x => x.HourTypes["Workday"] = 16);
            employeeTimes.Where(x => x.Employee.Id == 4).Select(x => x.HourTypes["Missing entries"] = 18 * 8);*/

            return employeeTimes;
        }

        [Test, Order(2)]
        [TestCase(3, 2019, 6)]
        public void GetTeamTimeTracking(int teamId, int year, int month)
        {
            var controller = new CalendarController(unit.Context);
            var response = controller.GetTimeTracking(teamId, year, month) as ObjectResult;
            var value = response.Value as List<EmployeeTimeModel>;

            //this dictionary is only used for iteration through it's keys
            Dictionary<string, decimal> hourTypes = new Dictionary<string, decimal>();
            hourTypes.SetHourTypes(unit);
            //SetHourTypes(hourTypes);

            List<EmployeeTimeModel> employeeTimes = CreateTeamReport(teamId);

            Assert.AreEqual(200, response.StatusCode);
            foreach (KeyValuePair<string, decimal> hourType in hourTypes)
            {
                Assert.AreEqual(value.Sum(x => x.HourTypes[hourType.Key]), employeeTimes.Sum(x => x.HourTypes[hourType.Key]));
            }
            Assert.AreEqual(value.Sum(x => x.TotalHours), employeeTimes.Sum(x => x.TotalHours));
            Assert.AreEqual(value.Sum(x => x.PaidTimeOff), employeeTimes.Sum(x => x.PaidTimeOff));
            Assert.AreEqual(value.Sum(x => x.Overtime), employeeTimes.Sum(x => x.Overtime));
        }

        [Test, Order(2)]
        public void GetDayById()
        {
            int id = 3;
            int employeeId = 3;
            var controller = new CalendarController(unit.Context);

            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as DayModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(employeeId, value.Employee.Id);
            Assert.AreEqual(1, value.JobDetails.Count());
        }

        [Test, Order(3)]
        public void GetNonExistingDay()
        {
            int id = 1000;
            var controller = new CalendarController(unit.Context);

            var response = controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(4)]
        public void InsertDay()
        {
            var controller = new CalendarController(unit.Context);

            Day day = new Day
            {
                Employee = unit.Employees.Get(1),
                DayType = unit.DayTypes.Get(1),
                Date = DateTime.Now
            };

            var response = controller.Post(day) as ObjectResult;
            var value = response.Value as DayModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(101, value.Id);//id of the new day will be 3
        }

        [Test, Order(5)]
        public void ChangeDay()
        {
            //Try to change day with id 2
            var controller = new CalendarController(unit.Context);
            int id = 2;

            Day day = new Day
            {
                Id = id,
                Employee = unit.Employees.Get(1),
                DayType = unit.DayTypes.Get(1),
                Date = DateTime.Now
            };

            var response = controller.Put(id, day) as ObjectResult;
            var value = response.Value as DayModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, value.Employee.Id);//id of the new day will be 3
        }

        [Test, Order(6)]
        public void ChangeNonExistingDay()
        {
            //Try to change day with id
            var controller = new CalendarController(unit.Context);
            int id = 1000;

            Day day = new Day
            {
                Id = id,
                Employee = unit.Employees.Get(1),
                DayType = unit.DayTypes.Get(1),
                Date = DateTime.Now
            };

            var response = controller.Put(id, day) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public void ChangeDayWithWrongId()
        {
            //Try to change day with id
            var controller = new CalendarController(unit.Context);
            int id = 2;
            int wrongId = 3;

            Day day = new Day
            {
                Id = id,
                Employee = unit.Employees.Get(1),
                DayType = unit.DayTypes.Get(1),
                Date = DateTime.Now
            };

            var response = controller.Put(wrongId, day) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        [Test, Order(7)]
        public void DeleteDay()
        {
            //Try to delete day with id
            var controller = new CalendarController(unit.Context);
            int id = 2;

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);
        }

        [Test, Order(8)]
        public void DeleteNonExistingDay()
        {
            //Try to delete day with id
            var controller = new CalendarController(unit.Context);
            int id = 1000;

            var response = controller.Delete(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
