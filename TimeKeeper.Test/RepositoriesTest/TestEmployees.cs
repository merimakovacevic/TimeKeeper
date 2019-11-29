using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestEmployees: TestBaseTestDatabase
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
            int id = 40; //Employee with id 40 doesn't exist in the test database

            var ex = Assert.Throws<ArgumentException>(() => unit.Employees.Get(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");            
        }

        [Test, Order(4)]
        public void InsertEmployee()
        {
            Employee employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
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
                LastName = "Doe",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };
            unit.Employees.Update(employee, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Jane", employee.FirstName);
            Assert.AreEqual("Doe", employee.LastName);
        }

        [Test, Order(6)]
        public void ChangeNonExistingEmployee()
        {
            int id = 40;//Try to change the employee with id (doesn't exist)
            Employee employee = new Employee
            {
                Id = id,
                FirstName = "John",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };           

            var ex = Assert.Throws<ArgumentException>(() => unit.Employees.Update(employee, id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(7)]
        public void ChangeEmployeeWithWrongId()
        {
            //Try to change the employee with a wrong id argument in update method
            int id = 1;
            int wrongId = 2;
            Employee employee = new Employee
            {
                Id = id,
                FirstName = "John",
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };

            var ex = Assert.Throws<ArgumentException>(() => unit.Employees.Update(employee, wrongId));
            Assert.AreEqual(ex.Message, $"Error! Id of the sent object: {employee.Id} and id in url: {wrongId} do not match");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void ChangeEmployeeStatus()
        {
            //Try to change the employee with id
            int id = 2;
            Employee employee = new Employee
            {
                Id = id,
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };
            employee.Status = unit.EmploymentStatuses.Get(3);
            unit.Employees.Update(employee, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(3, employee.Status.Id);
        }

        [Test, Order(9)]
        public void ChangeEmployeeEndDate()
        {
            //Try to change the employee with id
            int id = 2;
            DateTime endDate = new DateTime(2019, 10, 23);
            Employee employee = new Employee
            {
                Id = id,
                EndDate = endDate,
                Position = unit.EmployeePositions.Get(1),
                Status = unit.EmploymentStatuses.Get(1)
            };
            unit.Employees.Update(employee, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(endDate, employee.EndDate);
        }

        [Test, Order(10)]
        public void DeleteEmployeeWithChildren()
        {
            //Try to delete the employee with id
            int id = 2;            

            var ex = Assert.Throws<Exception>(() => unit.Employees.Delete(id));
            Assert.AreEqual(ex.Message, "Object cannot be deleted because child objects are present");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(11)]
        public void DeleteEmployeeWithWrongId()
        {
            //Try to delete the employee with id (doesn't exist) 
            int id = 40;           

            var ex = Assert.Throws<ArgumentException>(() => unit.Employees.Delete(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(12)]
        public void DeleteEmployee()
        {
            //Try to delete the employee with id
            int id = 2;

            Employee employee = unit.Employees.Get(id);
            //This list will be used for iteration only
            List<Day> employeeCalendar = employee.Calendar.ToList();

            foreach(Day day in employeeCalendar)
            {
                //employee.Calendar.Remove(day);
                unit.Calendar.Delete(day);
            }

            int numberOfChanges = unit.Save();
            unit.Employees.Delete(id);

            //Two child entities and one parent entity will be deleted, making it 3 changes
            Assert.AreEqual(3, numberOfChanges);
        }
    }
}
