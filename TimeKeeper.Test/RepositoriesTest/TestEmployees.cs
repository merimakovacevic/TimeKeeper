using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
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
        [TestCase(58, "Helen")]
        [TestCase(67, "Dorothy")]
        [TestCase(101, "Jennifer")]
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
            Assert.AreEqual(1, employee.Id);//id of the new employee will be 1
        }

        [Test, Order(5)]
        public void ChangeEmployeesName()
        {
            int id = 58;//Try to change the employee with id
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
        public void DeleteEmployee()
        {
            int id = 101;//Try to delete the employee with id

            unit.Employees.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
        }

        [Test, Order(8)]
        public void DeleteEmployeeWithWrongId()
        {
            int id = 40;//Try to delete the employee with id (doesn't exist)

            unit.Employees.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
    }
}
