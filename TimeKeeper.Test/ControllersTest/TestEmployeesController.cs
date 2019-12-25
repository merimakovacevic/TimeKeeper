using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.API.Controllers;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestEmployeesController : TestBaseTestDatabase
    {
        [Test, Order(1)]
        public async Task GetAllEmployees()
        {
            var controller = new EmployeesController(unit.Context);

            var response = await controller.GetAll() as ObjectResult;
            var value = response.Value as List<EmployeeModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(6, value.Count); //there are 6 employees in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "Helen Carter")]
        [TestCase(2, "Dorothy Green")]
        [TestCase(3, "Sharon Phillips")]
        [TestCase(4, "Karen Gonzalez")]
        [TestCase(5, "Betty Nelson")]
        [TestCase(6, "Jennifer Wright")]
        public async Task GetEmployeeById(int id, string name)
        {
            var controller = new EmployeesController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(3)]
        public async Task GetEmployeeByWrongId()
        {
            int id = 40; //Employee with id 40 doesn't exist in the test database
            var controller = new EmployeesController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(4)]
        public async Task InsertEmployee()
        {
            var controller = new EmployeesController(unit.Context);

            Employee employee = new Employee
            {
                FirstName = "Sule",
                LastName = "Sule",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };

            var response = await controller.Post(employee) as ObjectResult;
            var value = response.Value as EmployeeModel;

            //Check if the user was added as well
            User user = unit.Users.Get(employee.Id);

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(7, value.Id);//Id of the new employee will be 7
            Assert.AreEqual(7, user.Id);//Id of the new user will be 7
            Assert.AreEqual("sulesu", user.Username);
        }

        [Test, Order(5)]
        public async Task ChangeEmployeeName()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 3;//Try to change the employee with id 3

            Employee employee = new Employee
            {
                Id=id,
                FirstName = "Sule",
                LastName = "Sule",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };

            var response = await controller.Put(id, employee) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Sule", value.FirstName);
            Assert.AreEqual("Sule", value.LastName);
        }

        [Test, Order(6)]
        public async Task ChangeEmployeeWithWrongId()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 40;//Try to change the employee with id (doesn't exist)

            Employee employee = new Employee
            {
                Id = id,
                FirstName = "Sule",
                LastName = "Sule",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };

            var response = await controller.Put(id, employee) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(7)]
        public async Task ChangeEmployeeStatus()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 2;//Try to change the employee with id
            int statusId = 1;

            Employee employee = new Employee
            {
                Id = id,
                FirstName = "Sule",
                LastName = "Sule",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(statusId)
            };

            var response = await controller.Put(id, employee) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(statusId, value.Status.Id);
        }

        [Test, Order(8)]
        public async Task ChangeEmployeeEndDate()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 2;//Try to change the employee with id
            DateTime endDate = DateTime.Now;

            Employee employee = new Employee
            {
                Id = id,
                FirstName = "Sule",
                LastName = "Sule",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1),
                EndDate=endDate
            };

            var response = await controller.Put(id, employee) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(endDate, value.EndDate);
        }

        [Test, Order(9)]
        public async Task DeleteEmployee()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 3;//Try to delete the employee with id 3

            var response = await controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(10)]
        public async Task DeleteEmployeeWithWrongId()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 40;//Try to delete the employee with id (doesn't exist)

            var response = await controller.Delete(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
