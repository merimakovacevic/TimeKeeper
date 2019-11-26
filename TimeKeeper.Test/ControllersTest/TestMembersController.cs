﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestMembersController : BaseTestDatabase
    {
        [Test, Order(1)]
        public void GetAllMembers()
        {
            var controller = new MembersController(unit.Context);

            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<MemberModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(6, value.Count); //there are 6 Members in the test database
        }

        [Test, Order(2)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void GetMemberById(int id)
        {
            var controller = new MembersController(unit.Context);

            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as MemberModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(3)]
        public void GetMemberByWrongId()
        {
            int id = 40; //Member with id 40 doesn't exist in the test database
            var controller = new MembersController(unit.Context);

            var response = controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(4)]
        public void InsertMember()
        {
            var controller = new MembersController(unit.Context);

            Member member = new Member
            {
                Team=unit.Teams.Get(1),
                Role=unit.Roles.Get(1),
                Employee=unit.Employees.Get(1),
                Status=unit.MemberStatuses.Get(1)
            };

            var response = controller.Post(member) as ObjectResult;
            var value = response.Value as MemberModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(7, value.Id);//Id of the new Member will be 7
        }

        [Test, Order(5)]
        public void ChangeMembersRole()
        {
            var controller = new MembersController(unit.Context);
            int id = 3;//Try to change the Member with id 3
            int roleId = 1;

            Member member = new Member
            {
                Id=id,
                Team = unit.Teams.Get(1),
                Role = unit.Roles.Get(roleId),
                Employee = unit.Employees.Get(1),
                Status = unit.MemberStatuses.Get(1)
            };

            var response = controller.Put(id, member) as ObjectResult;
            var value = response.Value as MemberModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(roleId, value.Role.Id);
        }

        [Test, Order(6)]
        public void ChangeMembersStatus()
        {
            var controller = new MembersController(unit.Context);
            int id = 3;//Try to change the Member with id 3
            int statusId = 1;

            Member member = new Member
            {
                Id = id,
                Team = unit.Teams.Get(1),
                Role = unit.Roles.Get(1),
                Employee = unit.Employees.Get(1),
                Status = unit.MemberStatuses.Get(statusId)
            };

            var response = controller.Put(id, member) as ObjectResult;
            var value = response.Value as MemberModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(statusId, value.Status.Id);
        }

        [Test, Order(7)]
        public void ChangeMemberWithWrongId()
        {
            var controller = new MembersController(unit.Context);
            int id = 40;//Try to change the Member with id (doesn't exist)

            Member member = new Member
            {
                Id = id,
                Team = unit.Teams.Get(1),
                Role = unit.Roles.Get(1),
                Employee = unit.Employees.Get(1),
                Status = unit.MemberStatuses.Get(1)
            };

            var response = controller.Put(id, member) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(8)]
        public void DeleteMember()
        {
            var controller = new MembersController(unit.Context);
            int id = 3;//Try to delete the member with id 3

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(9)]
        public void DeleteMemberWithWrongId()
        {
            var controller = new MembersController(unit.Context);
            int id = 40;//Try to delete the project with id (doesn't exist)

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
