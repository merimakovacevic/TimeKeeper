using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestRoles : TestBaseTestDatabase
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
            var ex = Assert.Throws<ArgumentException>(() => unit.Roles.Get(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
        }

        
        [Test, Order(4)]
        public void InsertRole()
        {
            Role role = new Role
            {
                HourlyPrice = 20,
                MonthlyPrice = 2000
            };
            unit.Roles.Insert(role);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(6, role.Id);//id of the new role will be 6
        }

        [Test, Order(5)]
        public void ChangeRolesName()
        {
            //Try to change the role with id
            int id = 2;
            string name = "Backend developer";
            Role role = new Role
            {
                Id = id,
                Name = name
            };
            unit.Roles.Update(role, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(name, role.Name);
        }


        [Test, Order(6)]
        public void ChangeNonExistingRole()
        {
            //Try to change the role with id (doesn't exist)
            int id = 40;
            Role role = new Role
            {
                Id = id,
                HourlyPrice = 20
            };

            var ex = Assert.Throws<ArgumentException>(() => unit.Roles.Update(role, id));            
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(7)]
        public void ChangeRoleWithWrongId()
        {
            //Try to change the role with a wrong id argument in update method
            int id = 1;
            int wrongId = 2;

            Role role = new Role
            {
                Id = id,
                HourlyPrice = 20
            };

            var ex = Assert.Throws<ArgumentException>(() => unit.Roles.Update(role, wrongId));
            Assert.AreEqual(ex.Message, $"Error! Id of the sent object: {role.Id} and id in url: {wrongId} do not match");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void DeleteRoleWithChildren()
        {
            int id = 2;//Try to delete the role with id

            var ex = Assert.Throws<Exception>(() => unit.Roles.Delete(id));
            Assert.AreEqual(ex.Message, "Object cannot be deleted because child objects are present");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(9)]
        public void DeleteRoleWithWrongId()
        {
            int id = 40;//Try to delete the role with id (doesn't exist)

            var ex = Assert.Throws<ArgumentException>(() => unit.Roles.Delete(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");            
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(10)]
        public void DeleteRole()
        {
            int id = 2;//Try to delete the role with id

            Role role = unit.Roles.Get(id);

            //first all child entities need to be deleted
            //this list will be used for iteration
            List<Member> roleMembers = role.Members.ToList();

            foreach(Member member in roleMembers)
            {
                //role.Members.Remove(member);
                unit.Members.Delete(member);                
            }

            unit.Roles.Delete(id);

            int numberOfChanges = unit.Save();
            //Two child entities and one parent entity will be deleted, making it 3 changes
            Assert.AreEqual(3, numberOfChanges);
        }

    }
}
