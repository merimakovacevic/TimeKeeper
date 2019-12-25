using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.API.Controllers;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestCustomersController : TestBaseTestDatabase
    {
        [Test, Order(1)]
        [TestCase(1, 5)]
        public async Task GetAllCustomers(int page, int pageSize)
        {
            var controller = new CustomersController(unit.Context);

            var response = await controller.GetAll(page, pageSize) as ObjectResult;
            var value = response.Value as List<CustomerModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(2, value.Count()); //there are 2 customers in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "ImageNet Consulting")]
        [TestCase(2, "Big Data Scoring")]
        public async Task GetCustomerById(int id, string name)
        {
            var controller = new CustomersController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(name, value.Name);
        }

        [Test, Order(3)]
        public async Task GetCustomerByWrongId()
        {
            var controller = new CustomersController(unit.Context);
            int id = 40; //Customer with id doesn't exist in the test database

            var response = await controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }


        [Test, Order(4)]
        public async Task InsertCustomer()
        {
            var controller = new CustomersController(unit.Context);

            Address homeAddress = new Address { City = "Sarajevo" };
            Customer customer = new Customer
            {
                Name = "Test Customer",
                HomeAddress = homeAddress,
                Status = unit.CustomerStatuses.Get(1)
            };

            var response = await controller.Post(customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(3, value.Id);//id of the new customer will be 3
        }

        [Test, Order(5)]
        public async Task ChangeCustomersName()
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

            var response = await controller.Put(id, customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Test Customer", value.Name);
        }


        [Test, Order(6)]
        public async Task ChangeCustomersTown()
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

            var response = await controller.Put(id, customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Sarajevo", value.HomeAddress.City);
        }

        [Test, Order(7)]
        public async Task ChangeCustomerWithWrongId()
        {
            var controller = new CustomersController(unit.Context);
            int id = 40;//Try to change the customer with id (doesn't exist)
            Address homeAddress = new Address { City = "Sarajevo" };
            Customer customer = new Customer
            {
                Id = id,
                Name = "Test Customer",
                HomeAddress = homeAddress,
                Status = unit.CustomerStatuses.Get(1)
            };

            var response = await controller.Put(id, customer) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        [Test, Order(8)]
        public async Task ChangeCustomerStatus()
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

            var response = await controller.Put(id, customer) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(statusId, value.Status.Id);
        }

        [Test, Order(9)]
        public async Task DeleteCustomer()
        {
            var controller = new CustomersController(unit.Context);
            int id = 2;//Try to delete the customer with id

            var response = await controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);
        }

        [Test, Order(10)]
        public async Task DeleteCustomerWithWrongId()
        {
            var controller = new CustomersController(unit.Context);
            int id = 40;//Try to delete the customer with id (doesn't exist)

            var response = await controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
