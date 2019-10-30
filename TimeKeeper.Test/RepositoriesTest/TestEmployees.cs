﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestEmployees: TestBase
    {
        [Test, Order(1)]
        public void GetAllEmployees()
        {
            //Act
            int employeesCount = unit.Employees.Get().Count();

            //Assert
            Assert.AreEqual(6, employeesCount); //there are 6 employees in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "Helen")]
        [TestCase(2, "Dorothy")]
        [TestCase(6, "Jennifer")]
        public void GetEmployeeById(int id, string firstName)
        {
            var result = unit.Employees.Get(id);
            Assert.AreEqual(result.FirstName, firstName);
        }

        [Test, Order(3)]
        public void GetEmployeeByWrongId()
        {
            int id = 40; //Employee with id 4 0doesn't exist in the test database
            var result = unit.Employees.Get(id);
            Assert.IsNull(result);
        }

        [Test, Order(4)]
        public void InsertEmployee()
        {
            Employee employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe"
            };
            unit.Employees.Insert(employee);
            int numberOfChanges = unit.Save();

            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(7, employee.Id);//id of the new employee will be 1
        }

        [Test, Order(5)]
        public void ChangeEmployeesName()
        {
            int id = 2;//Try to change the employee with id
            Employee employee = new Employee
            {
                Id = id,
                FirstName = "Jane",
                LastName = "Doe"
            };
            unit.Employees.Update(employee, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Jane", employee.FirstName);
            Assert.AreEqual("Doe", employee.LastName);
        }

        [Test, Order(6)]
        public void ChangeEmployeeWithWrongId()
        {
            int id = 40;//Try to change the employee with id (doesn't exist)
            Employee employee = new Employee
            {
                Id = id,
                FirstName = "John"
            };
            unit.Employees.Update(employee, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(7)]
        public void ChangeEmployeeStatus()
        {
            int id = 2;//Try to change the employee with id
            Employee employee = new Employee
            {
                Id = id
            };
            employee.Status = unit.EmploymentStatuses.Get(3);
            unit.Employees.Update(employee, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(3, employee.Status.Id);
        }

        [Test, Order(8)]
        public void ChangeEmployeeEndDate()
        {
            int id = 2;//Try to change the employee with id
            DateTime endDate = new DateTime(2019, 10, 23);
            Employee employee = new Employee
            {
                Id = id,
                EndDate = endDate
            };
            unit.Employees.Update(employee, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(endDate, employee.EndDate);
        }

        [Test, Order(9)]
        public void DeleteEmployee()
        {
            int id = 2;//Try to delete the employee with id

            unit.Employees.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
        }

        [Test, Order(10)]
        public void DeleteEmployeeWithWrongId()
        {
            int id = 40;//Try to delete the employee with id (doesn't exist)

            unit.Employees.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
    }
}
