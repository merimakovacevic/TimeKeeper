using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Repositories;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test
{
    [TestFixture]
    public class TestTeams: TestBase
    {

        [Test, Order(1)]
        
        public void GetAllTeams()
        {
            //Act
            int teamsCount = unit.Teams.Get().Count();

            //Assert
            Assert.AreEqual(3, teamsCount); //there are 3 teams in the test database
        }

        [Test, Order(1)]
        [TestCase(1, "Delta")]
        [TestCase(2, "Tango")]
        [TestCase(3, "Yankee")]
        public void GetTeamById(int id, string name)
        {
            var result = unit.Teams.Get(id);
            Assert.AreEqual(result.Name, name);
        }

        [Test, Order(2)]
        public void GetTeamByWrongId()
        {
            int id = 40; //Team with id 4 doesn't exist in the test database
            var result = unit.Teams.Get(id);
            Assert.IsNull(result);
        }

        [Test, Order(3)]
        public void InsertTeam()
        {
            Team team = new Team
            {
                Name = "Delta"
            };
            unit.Teams.Insert(team);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(4, team.Id);//id of the new book will be 4
        }

        [Test, Order(4)]
        public void ChangeTeamName()
        {
            int id = 3;//Try to change the team with id 3
            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };
            unit.Teams.Update(team, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Zulu", team.Name);
        }

        [Test, Order(5)]
        public void ChangeTeamWithWrongId()
        {
            int id = 40;//Try to change the team with id (doesn't exist)
            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };
            unit.Teams.Update(team, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(6)]
        public void DeleteTeam()
        {
            int id = 3;//Try to delete the team with id 3

            unit.Teams.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
        }

        [Test, Order(7)]
        public void DeleteTeamWithWrongId()
        {
            int id = 40;//Try to delete the team with id (doesn't exist)

            unit.Teams.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
    }
}
