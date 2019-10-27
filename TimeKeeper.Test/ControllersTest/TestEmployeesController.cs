using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestEmployeesController : TestBase
    {
        [Test, Order(1)]
        public void GetAllEmployees()
        {
            var controller = new EmployeesController(unit.Context);

            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<EmployeeModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(6, value.Count); //there are 6 employees in the test database
        }

        [Test, Order(1)]
        [TestCase(1, "Helen Carter")]
        [TestCase(2, "Dorothy Green")]
        [TestCase(3, "Sharon Phillips")]
        [TestCase(4, "Karen Gonzalez")]
        [TestCase(5, "Betty Nelson")]
        [TestCase(6, "Jennifer Wright")]
        public void GetEmployeeById(int id, string name)
        {
            var controller = new EmployeesController(unit.Context);

            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(2)]
        public void GetEmployeeByWrongId()
        {
            int id = 40; //Employee with id 40 doesn't exist in the test database
            var controller = new EmployeesController(unit.Context);

            var response = controller.Get(id) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(3)]
        public void InsertEmployee()
        {
            var controller = new EmployeesController(unit.Context);

            Employee employee = new Employee
            {
                FirstName="Sule",
                LastName="Sule",
                Position=unit.EmployeePositions.Get(1),
                Status=unit.EmploymentStatuses.Get(1)
            };

            var response = controller.Post(employee) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(7, value.Id);//Id of the new employee will be 4
        }

        [Test, Order(4)]
        public void ChangeEmployeeName()
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

            var response = controller.Put(id, employee) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Sule", value.FirstName);
            Assert.AreEqual("Sule", value.LastName);
        }

        [Test, Order(5)]
        public void ChangeEmployeeWithWrongId()
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

            var response = controller.Put(id, employee) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public void DeleteEmployee()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 3;//Try to delete the employee with id 3

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(7)]
        public void DeleteEmployeeWithWrongId()
        {
            var controller = new EmployeesController(unit.Context);
            int id = 40;//Try to delete the employee with id (doesn't exist)

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
