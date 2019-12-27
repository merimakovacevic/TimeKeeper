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
using System.Threading.Tasks;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    public class TestTeamsController : TestBaseTestDatabase
    {

        [Test, Order(1)]
        public async Task GetAllTeams()
        {
            var controller = new TeamsController(unit.Context);

            var response = await controller.GetAll() as ObjectResult;
            var value = response.Value as List<TeamModel>;

            Assert.AreEqual(200, response.StatusCode); 
            Assert.AreEqual(3, value.Count); //there are 3 teams in the test database
        }

        [Test, Order(1)]
        [TestCase(1, "Delta")]
        [TestCase(2, "Tango")]
        [TestCase(3, "Yankee")]
        public async Task GetTeamById(int id, string name)
        {
            var controller = new TeamsController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;
            var value = response.Value as TeamModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(2)]
        public async Task GetTeamByWrongId()
        {
            int id = 40; //Team with id 4 doesn't exist in the test database
            var controller = new TeamsController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(3)]
        public async Task InsertTeam()
        {
            var controller = new TeamsController(unit.Context);

            Team team = new Team
            {
                Name = "Delta"
            };

            var response = await controller.Post(team) as ObjectResult;
            var value = response.Value as TeamModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(4, value.Id);//Id of the new team will be 4
        }

        [Test, Order(4)]
        public async Task ChangeTeamName()
        {
            var controller = new TeamsController(unit.Context);
            int id = 3;//Try to change the team with id 3

            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };

            var response = await controller.Put(id, team) as ObjectResult;
            var value = response.Value as TeamModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Zulu", value.Name);
        }

        [Test, Order(5)]
        public async Task ChangeTeamWithWrongId()
        {
            var controller = new TeamsController(unit.Context);
            int id = 40;//Try to change the team with id (doesn't exist)

            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };

            var response =await controller.Put(id, team) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public async Task DeleteTeam()
        {
            var controller = new TeamsController(unit.Context);
            int id = 3;//Try to delete the team with id 3

            var response = await controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(7)]
        public async Task DeleteTeamWithWrongId()
        {
            var controller = new TeamsController(unit.Context);
            int id = 40;//Try to delete the team with id (doesn't exist)

            var response = await controller.Delete(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
