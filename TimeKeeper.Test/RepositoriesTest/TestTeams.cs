using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Repositories;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestTeams: TestBaseTestDatabase
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
            var ex = Assert.Throws<ArgumentException>(() => unit.Teams.Get(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
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
            Assert.AreEqual(4, team.Id);//id of the new team will be 4
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
        public void ChangeNonExistingTeam()
        {
            //Try to change the team with id (doesn't exist)
            int id = 40;
            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };
            var ex = Assert.Throws<ArgumentException>(() => unit.Teams.Update(team, id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
        [Test, Order(6)]
        public void ChangeTeamWithWrongId()
        {
            //Try to change the team with a wrong id argument in update method
            int id = 1;
            int wrongId = 2;

            Team team = new Team
            {
                Id = id,
                Name = "Zulu"
            };
            var ex = Assert.Throws<ArgumentException>(() => unit.Teams.Update(team, wrongId));
            Assert.AreEqual(ex.Message, $"Error! Id of the sent object: {team.Id} and id in url: {wrongId} do not match");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(7)]
        public void DeleteTeamWithChildren()
        {
            //Try to delete the team with id 3
            int id = 3;

            var ex = Assert.Throws<Exception>(() => unit.Teams.Delete(id));
            Assert.AreEqual(ex.Message, "Object cannot be deleted because child objects are present");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void DeleteNonExistingTeam()
        {
            //Try to delete the team with id (doesn't exist)
            int id = 40;

            var ex = Assert.Throws<ArgumentException>(() => unit.Teams.Delete(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");

            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(9)]
        public void DeleteTeam()
        {
            //Try to delete the team with id 3
            int id = 3;
            Team team = unit.Teams.Get(id);

            //first all child entities need to be deleted
            //these lists will be used for iteration
            List<Member> teamMembers = team.Members.ToList();
            List<Project> teamProjects = team.Projects.ToList();

            foreach(Member member in teamMembers)
            {
                //team.Members.Remove(member);
                unit.Members.Delete(member);
            }

            foreach(Project project in teamProjects)
            {
                //team.Projects.Remove(project);
                unit.Projects.Delete(project);
            }

            unit.Save();

            unit.Teams.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
    }
}
