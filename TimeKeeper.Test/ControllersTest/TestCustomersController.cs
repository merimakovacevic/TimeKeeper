using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestCustomersController : BaseTestDatabase
    {
        [Test, Order(1)]
        public void GetAllCustomers()
        {
            var controller = new CustomersController(unit.Context);

            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<CustomerModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(2, value.Count()); //there are 2 customers in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "ImageNet Consulting")]
        [TestCase(2, "Big Data Scoring")]
        public void GetCustomerById(int id, string name)
        {
            var controller = new CustomersController(unit.Context);

            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(name, value.Name);
        }

        [Test, Order(3)]
        public void GetCustomerByWrongId()
        {
            var controller = new CustomersController(unit.Context);
            int id = 40; //Customer with id doesn't exist in the test database

            var response = controller.Get(id) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }


        [Test, Order(4)]
        public void InsertCustomer()
        {
            var controller = new CustomersController(unit.Context);

            Address homeAddress = new Address { City = "Sarajevo" };
            Customer customer = new Customer
            {
                Name = "Test Customer",
                HomeAddress = homeAddress,
                Status = unit.CustomerStatuses.Get(1)
            };

            var response = controller.Post(customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(3, value.Id);//id of the new customer will be 3
        }

        [Test, Order(5)]
        public void ChangeCustomersName()
        {
            var controller = new CustomersController(unit.Context);
            int id = 2;//Try to change the customer with id

            Address homeAddress = new Address
            {
                City = "Sarajevo"
            };
            Customer customer = new Customer
            {
                Id = id,
                Name = "Test Customer",
                Status = unit.CustomerStatuses.Get(1),
                HomeAddress = homeAddress
            };

            var response = controller.Put(id, customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Test Customer", value.Name);
        }


        [Test, Order(6)]
        public void ChangeCustomersTown()
        {
            var controller = new CustomersController(unit.Context);
            int id = 2;//Try to change the customer with id

            Address homeAddress = new Address
            {
                City = "Sarajevo"
            };
            Customer customer = new Customer
            {
                Id = id,
                HomeAddress = homeAddress,
                Status = unit.CustomerStatuses.Get(1)
            };

            var response = controller.Put(id, customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Sarajevo", value.HomeAddress.City);
        }

        [Test, Order(7)]
        public void ChangeCustomerWithWrongId()
        {
            var controller = new CustomersController(unit.Context);
            int id = 40;//Try to change the customer with id (doesn't exist)

            Customer customer = new Customer
            {
                Id = id,
                Name = "Test Customer",
                Status = unit.CustomerStatuses.Get(1)
            };

            var response = controller.Put(id, customer) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(8)]
        public void ChangeCustomerStatus()
        {
            var controller = new CustomersController(unit.Context);
            int id = 2;//Try to change the customer with id
            int statusId = 1; //new status Id

            Address homeAddress = new Address
            {
                City = "Sarajevo"
            };
            Customer customer = new Customer
            {
                Id = id,
                Status = unit.CustomerStatuses.Get(statusId),
                HomeAddress = homeAddress
            };

            var response = controller.Put(id, customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(statusId, value.Status.Id);
        }

        [Test, Order(9)]
        public void DeleteCustomer()
        {
            var controller = new CustomersController(unit.Context);
            int id = 2;//Try to delete the customer with id

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);
        }

        [Test, Order(10)]
        public void DeleteCustomerWithWrongId()
        {
            var controller = new CustomersController(unit.Context);
            int id = 40;//Try to delete the customer with id (doesn't exist)

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
