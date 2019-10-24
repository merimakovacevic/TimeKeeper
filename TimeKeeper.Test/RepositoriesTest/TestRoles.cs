using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestRoles : TestBase
    {
        [Test, Order(1)]
        public void GetAllRoles()
        {
            //Act
            int rolesCount = unit.Roles.Get().Count();

            //Assert
            Assert.AreEqual(5, rolesCount); //there are 5 roles in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "Project Manager")]
        [TestCase(2, "Quality Assurance Engineer")]
        public void GetRoleById(int id, string name)
        {
            var result = unit.Roles.Get(id);
            Assert.AreEqual(result.Name, name);
        }

        [Test, Order(3)]
        public void GetRoleByWrongId()
        {
            int id = 40; //Role with id doesn't exist in the test database
            var result = unit.Roles.Get(id);
            Assert.IsNull(result);
        }

        /*
        [Test, Order(4)]
        public void InsertRole()
        {
            Address homeAddress = new Address { City = "Sarajevo" };
            Role role = new Role
            {
                Name = "Test Role",
                HomeAddress = homeAddress
            };
            unit.Roles.Insert(role);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(3, role.Id);//id of the new role will be 3
        }

        [Test, Order(5)]
        public void ChangeRolesName()
        {
            int id = 2;//Try to change the role with id
            Role role = new Role
            {
                Id = id,
                Name = "Test Role"
            };
            unit.Roles.Update(role, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Test Role", role.Name);
        }


        [Test, Order(6)]
        public void ChangeRolesTown()
        {
            int id = 2;//Try to change the role with id
            Address homeAddress = new Address
            {
                City = "Sarajevo"
            };
            Role role = new Role
            {
                Id = id,
                HomeAddress = homeAddress
            };
            unit.Roles.Update(role, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Sarajevo", role.HomeAddress.City);
        }

        [Test, Order(7)]
        public void ChangeRoleWithWrongId()
        {
            int id = 40;//Try to change the role with id (doesn't exist)
            Role role = new Role
            {
                Id = id,
                Name = "Test Role"
            };
            unit.Roles.Update(role, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void ChangeRoleStatus()
        {
            int id = 2;//Try to change the role with id
            Role role = new Role
            {
                Id = id
            };
            int statusId = 1; //new status Id
            role.Status = unit.RoleStatuses.Get(statusId);
            unit.Roles.Update(role, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(statusId, role.Status.Id);
        }

        [Test, Order(9)]
        public void DeleteRole()
        {
            int id = 2;//Try to delete the role with id

            unit.Roles.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
        }

        [Test, Order(10)]
        public void DeleteRoleWithWrongId()
        {
            int id = 40;//Try to delete the role with id (doesn't exist)

            unit.Roles.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
        */
    }
}
