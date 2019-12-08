using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.DTO;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestTeamsController : TestBaseTestDatabase
    {

        [Test, Order(1)]
        public void GetAllTeams()
        {
            var controller = new TeamsController(unit.Context);

            var response = controller.GetAll() as ObjectResult;
            var value = response.Value as List<TeamModel>;

            Assert.AreEqual(200, response.StatusCode); 
            Assert.AreEqual(3, value.Count); //there are 3 teams in the test database
        }

        [Test, Order(1)]
        [TestCase(1, "Delta")]
        [TestCase(2, "Tango")]
        [TestCase(3, "Yankee")]
        public void GetTeamById(int id, string name)
        {
            var controller = new TeamsController(unit.Context);

            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as TeamModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(2)]
        public void GetTeamByWrongId()
        {
            int id = 40; //Team with id 4 doesn't exist in the test database
            var controller = new TeamsController(unit.Context);

            var response = controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(3)]
        public void InsertTeam()
        {
            var controller = new TeamsController(unit.Context);

            Team team = new Team
            {
                Name = "Delta"
            };

            var response = controller.Post(team) as ObjectResult;
            var value = response.Value as TeamModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(4, value.Id);//Id of the new team will be 4
        }

        [Test, Order(4)]
        public void ChangeTeamName()
        {
            var controller = new TeamsController(unit.Context);
            int id = 3;//Try to change the team with id 3

            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };

            var response = controller.Put(id, team) as ObjectResult;
            var value = response.Value as TeamModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Zulu", value.Name);
        }

        [Test, Order(5)]
        public void ChangeTeamWithWrongId()
        {
            var controller = new TeamsController(unit.Context);
            int id = 40;//Try to change the team with id (doesn't exist)

            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };

            var response = controller.Put(id, team) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public void DeleteTeam()
        {
            var controller = new TeamsController(unit.Context);
            int id = 3;//Try to delete the team with id 3

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(7)]
        public void DeleteTeamWithWrongId()
        {
            var controller = new TeamsController(unit.Context);
            int id = 40;//Try to delete the team with id (doesn't exist)

            var response = controller.Delete(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
