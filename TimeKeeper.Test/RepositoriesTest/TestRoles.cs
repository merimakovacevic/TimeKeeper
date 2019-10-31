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

        
        [Test, Order(4)]
        public void InsertRole()
        {
            Role role = new Role
            {
                HourlyPrice=20,
                MonthlyPrice=2000
            };
            unit.Roles.Insert(role);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(6, role.Id);//id of the new role will be 6
        }

        [Test, Order(5)]
        public void ChangeRolesName()
        {
            int id = 2;//Try to change the role with id
            string name = "Backend developer";
            Role role = new Role
            {
                Id = id,
                Name=name
            };
            unit.Roles.Update(role, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(name, role.Name);
        }


        [Test, Order(7)]
        public void ChangeRoleWithWrongId()
        {
            int id = 40;//Try to change the role with id (doesn't exist)
            Role role = new Role
            {
                Id = id,
                HourlyPrice=20
            };
            unit.Roles.Update(role, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
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
        
    }
}
