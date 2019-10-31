using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;
using TimeKeeper.Utility.Services;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestUsers : TestBase
    {
        [Test, Order(1)]
        public void GetAllUsers()
        {
            int usersCount = unit.Users.Get().Count();

            Assert.AreEqual(6, usersCount); //there are 6 users in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "helenca")]
        [TestCase(2, "dorothygr")]
        [TestCase(6, "jenniferwr")]
        public void GetUserById(int id, string username)
        {
            var result = unit.Users.Get(id);
            Assert.AreEqual(result.Username, username);
        }

        [Test, Order(3)]
        public void GetUserByWrongId()
        {
            int id = 40; //User with id 40 doesn't exist in the test database
            var result = unit.Users.Get(id);
            Assert.IsNull(result);
        }

        [Test, Order(4)]
        public void InsertUser()
        {
            //A new user is inserted within the EmployeesRepository Insert method
            Employee employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe"
            };
            unit.Employees.Insert(employee);
            unit.Save();

            //User insertion is coupled to employee insertion
            User user = new User
            {
                Id = employee.Id,
                Name = employee.FullName,
                Username = employee.MakeUsername(),
                Password = "$ch00l",
                Role = "user"
            };

            unit.Users.Insert(user);

            int numberOfChanges = unit.Save();

            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(7, user.Id);//id of the new user will be 7
            Assert.AreEqual("johndo", user.Username);
        }
    }
}
