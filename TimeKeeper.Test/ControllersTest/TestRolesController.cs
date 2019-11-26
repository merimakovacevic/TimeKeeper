using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class TestRolesController: BaseTestDatabase
    {
        [Test, Order(1)]
        public void GetAllRoles()
        {
            var controller = new RolesController(unit.Context);

            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<RoleModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(5, value.Count); //there are 5 roles in the test database
        }

        [Test, Order(1)]
        [TestCase(1, "Project Manager")]
        [TestCase(2, "Quality Assurance Engineer")]
        [TestCase(3, "Software Developer")]
        [TestCase(4, "Team Lead")]
        [TestCase(5, "UI / UX Designer")]
        public void GetRoleById(int id, string name)
        {
            var controller = new RolesController(unit.Context);

            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as RoleModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(2)]
        public void GetRoleByWrongId()
        {
            int id = 40; //Role with id 40 doesn't exist in the test database
            var controller = new RolesController(unit.Context);

            var response = controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(3)]
        public void InsertRole()
        {
            var controller = new RolesController(unit.Context);

            Role role = new Role
            {
                Name = "Backend developer"
            };

            var response = controller.Post(role) as ObjectResult;
            var value = response.Value as RoleModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(6, value.Id);//Id of the new Role will be 6
        }

        [Test, Order(4)]
        public void ChangeRoleName()
        {
            var controller = new RolesController(unit.Context);
            int id = 3;//Try to change the Role with id 3

            Role role = new Role
            {
                Id = id,
                Name = "Backend developer"
            };

            var response = controller.Put(id, role) as ObjectResult;
            var value = response.Value as RoleModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Backend developer", value.Name);
        }

        [Test, Order(5)]
        public void ChangeRoleWithWrongId()
        {
            var controller = new RolesController(unit.Context);
            int id = 40;//Try to change the Role with id (doesn't exist)

            Role role = new Role
            {
                Id = id,
                Name = "Backend developer"
            };

            var response = controller.Put(id, role) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public void DeleteRole()
        {
            var controller = new RolesController(unit.Context);
            int id = 3;//Try to delete the Role with id 3

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(7)]
        public void DeleteRoleWithWrongId()
        {
            var controller = new RolesController(unit.Context);
            int id = 40;//Try to delete the Role with id (doesn't exist)

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
