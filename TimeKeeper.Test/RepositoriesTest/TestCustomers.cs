using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestCustomers : TestBase
    {
        [Test, Order(1)]
        public void GetAllCustomers()
        {
            //Act
            int customersCount = unit.Customers.Get().Count();

            //Assert
            Assert.AreEqual(2, customersCount); //there are 2 customers in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "ImageNet Consulting")]
        [TestCase(2, "Big Data Scoring")]
        public void GetCustomerById(int id, string name)
        {
            var result = unit.Customers.Get(id);
            Assert.AreEqual(result.Name, name);
        }

        [Test, Order(3)]
        public void GetCustomerByWrongId()
        {
            int id = 40; //Customer with id doesn't exist in the test database
            var result = unit.Customers.Get(id);
            Assert.IsNull(result);
        }

        [Test, Order(4)]
        public void InsertCustomer()
        {
            Address homeAddress = new Address { City = "Sarajevo" };
            Customer customer = new Customer
            {
                Name = "Test Customer",
                HomeAddress = homeAddress
            };
            unit.Customers.Insert(customer);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(3, customer.Id);//id of the new customer will be 3
        }

        [Test, Order(5)]
        public void ChangeCustomersName()
        {
            int id = 2;//Try to change the customer with id
            Customer customer = new Customer
            {
                Id = id,
                Name = "Test Customer"
            };
            unit.Customers.Update(customer, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Test Customer", customer.Name);
        }


        [Test, Order(6)]
        public void ChangeCustomersTown()
        {
            int id = 2;//Try to change the customer with id
            Address homeAddress = new Address
            {
                City = "Sarajevo"
            };
            Customer customer = new Customer
            {
                Id = id,
                HomeAddress = homeAddress
            };
            unit.Customers.Update(customer, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Sarajevo", customer.HomeAddress.City);
        }

        [Test, Order(7)]
        public void ChangeCustomerWithWrongId()
        {
            int id = 40;//Try to change the customer with id (doesn't exist)
            Customer customer = new Customer
            {
                Id = id,
                Name = "Test Customer"
            };
            unit.Customers.Update(customer, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void ChangeCustomerStatus()
        {
            int id = 2;//Try to change the customer with id
            int statusId = 1; //new status Id

            Customer customer = new Customer
            {
                Id = id,
                Status = unit.CustomerStatuses.Get(statusId)
            };

            unit.Customers.Update(customer, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(statusId, customer.Status.Id);
        }

        [Test, Order(9)]
        public void DeleteCustomer()
        {
            int id = 2;//Try to delete the customer with id

            unit.Customers.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
        }

        [Test, Order(10)]
        public void DeleteCustomerWithWrongId()
        {
            int id = 40;//Try to delete the customer with id (doesn't exist)

            unit.Customers.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
    }
}
