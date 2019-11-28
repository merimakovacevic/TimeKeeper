using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestCustomers : TestBaseTestDatabase
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
            var ex = Assert.Throws<ArgumentException>(() => unit.Customers.Get(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
        }

        [Test, Order(4)]
        public void InsertCustomer()
        {
            Address homeAddress = new Address { City = "Sarajevo" };
            Customer customer = new Customer
            {
                Name = "Test Customer",
                HomeAddress = homeAddress,
                Status = unit.CustomerStatuses.Get(1)
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
                Name = "Test Customer",
                Status = unit.CustomerStatuses.Get(1)
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
                HomeAddress = homeAddress,
                Status = unit.CustomerStatuses.Get(1)
            };
            unit.Customers.Update(customer, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Sarajevo", customer.HomeAddress.City);
        }

        [Test, Order(7)]
        public void ChangeNonExistingCustomer()
        {
            //Try to change the customer with id (doesn't exist)
            int id = 40;
            Customer customer = new Customer
            {
                Id = id,
                Name = "Test Customer",
                Status = unit.CustomerStatuses.Get(1)
            };            

            var ex = Assert.Throws<ArgumentException>(() => unit.Customers.Update(customer, id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void ChangeCustomerWithWrongId()
        {
            //Try to change the customer with a wrong id argument in update method
            int id = 1;
            int wrongId = 2;
            Customer customer = new Customer
            {
                Id = id,
                Name = "Test Customer",
                Status = unit.CustomerStatuses.Get(1)
            };

            var ex = Assert.Throws<ArgumentException>(() => unit.Customers.Update(customer, wrongId));
            Assert.AreEqual(ex.Message, $"Error! Id of the sent object: {customer.Id} and id in url: {wrongId} do not match");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(9)]
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

        [Test, Order(10)]
        public void DeleteCustomerWithChildren()
        {
            int id = 2;//Try to delete the customer with id, exception will be thrown

            var ex = Assert.Throws<Exception>(() => unit.Customers.Delete(id));
            Assert.AreEqual(ex.Message, "Object cannot be deleted because child objects are present");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(11)]
        public void DeleteCustomerWithWrongId()
        {
            int id = 40;//Try to delete the customer with id (doesn't exist)

            var ex = Assert.Throws<ArgumentException>(() => unit.Customers.Delete(id));
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(12)]
        public void DeleteCustomer()
        {
            int id = 2;//Try to delete the customer with id
            Customer customer = unit.Customers.Get(id);

            //first all child entities need to be deleted
            //this list will be used for iteration
            List<Project> customerProjects = customer.Projects.ToList();

            foreach (Project project in customerProjects)
            {
                //customer.Projects.Remove(project);
                /*this Delete method wasn't overriden in the specific repository, 
                 * meaning that even if a child entity has child entities itself, it will still be deleted*/
                unit.Projects.Delete(project);
            }

            unit.Customers.Delete(id);
            int numberOfChanges = unit.Save();
            //Two child entities and one Customer will be deleted, making it 3 changes
            Assert.AreEqual(3, numberOfChanges);
        }
    }
}
