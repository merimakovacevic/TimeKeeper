using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    public class TestCalendar: TestBaseTestDatabase
    {
        [Test, Order(1)]
        public void GetAllDays()
        {
            //Act
            int daysCount = unit.Calendar.Get().Count();

            //Assert
            Assert.AreEqual(100, daysCount); //there are 2 customers in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "2018-03-23 00:00:00")]
        [TestCase(2, "2018-04-23 00:00:00")]
        public void GetDayById(int id, DateTime date)
        {
            var result = unit.Calendar.Get(id);
            Assert.AreEqual(result.Date, date);
        }

        [Test, Order(3)]
        public void GetDayByWrongId()
        {
            int id = 140; //Day with id doesn't exist in the test database
            var ex = Assert.Throws<ArgumentException>(() => unit.Calendar.Get(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
        }

        [Test, Order(4)]
        public void InsertDay()
        {
            //Employee employee = unit.Employees.Get(1);
            //DayType dayType = unit.DayTypes.Get(1);
            Day day = new Day
            {
                Date=DateTime.Now,
                Employee= unit.Employees.Get(1),
                DayType= unit.DayTypes.Get(1)
            };
            unit.Calendar.Insert(day);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(101, day.Id);//id of the new day will be 101
        }

        [Test, Order(5)]
        public void ChangeDaysDate()
        {
            int id = 2;//Try to change the day with id
            DateTime date = new DateTime(2019, 11, 30);
            Day day = new Day
            {
                Id=id,
                Date = date,
                Employee=unit.Employees.Get(6),
                DayType=unit.DayTypes.Get(1)
            };
            unit.Calendar.Update(day, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(date, day.Date);
        }

        [Test, Order(6)]
        public void ChangeDaysEmployee()
        {
            int id = 2;
            Day day = new Day
            {
                Id = id,
                Employee = unit.Employees.Get(2),
                DayType=unit.DayTypes.Get(1)
            };
            unit.Calendar.Update(day, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(2, day.Employee.Id);
        }

        [Test, Order(7)]
        public void ChangeDaysDayType()
        {
            int id = 2;
            Day day = new Day
            {
                Id = id,
                Employee=unit.Employees.Get(6),
                DayType = unit.DayTypes.Get(2)
            };
            unit.Calendar.Update(day, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(2, day.DayType.Id);
        }

        [Test, Order(8)]
        public void ChangeDayWithWrongId()
        {
            //Try to change the day with a wrong id argument in update method
            int id = 1;
            int wrongId = 2;
            Day day = new Day
            {
                Id = id,
                Date = DateTime.Now,
                Employee=unit.Employees.Get(4),
                DayType=unit.DayTypes.Get(2)
            };

            var ex = Assert.Throws<ArgumentException>(() => unit.Calendar.Update(day, wrongId));
            Assert.AreEqual($"Error! Id of the sent object: {day.Id} and id in url: {wrongId} do not match", ex.Message);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(9)]
        public void DeleteDayWithChildren()
        {
            int id = 2;//Try to delete the customer with id, exception will be thrown

            var ex = Assert.Throws<Exception>(() => unit.Calendar.Delete(id));
            Assert.AreEqual(ex.Message, "Object cannot be deleted because child objects are present");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(10)]
        public void DeleteDayWithWrongId()
        {
            int id = 140;//Try to delete the customer with id (doesn't exist)

            var ex = Assert.Throws<ArgumentException>(() => unit.Calendar.Delete(id));
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
    }
}
