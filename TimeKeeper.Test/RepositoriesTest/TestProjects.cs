using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestProjects : TestBaseTestDatabase
    {
        [Test, Order(1)]
        public void GetAllProjects()
        {
            //Act
            int projectsCount = unit.Projects.Get().Count();

            //Assert
            Assert.AreEqual(3, projectsCount); //there are 3 projects in the test database
        }

        [Test, Order(2)]
        [TestCase(1, "Titanic Data Set")]
        [TestCase(3, "Image Net")]
        public void GetProjectById(int id, string name)
        {
            var result = unit.Projects.Get(id);
            Assert.AreEqual(result.Name, name);
        }

        [Test, Order(3)]
        public void GetProjectByWrongId()
        {
            //Project with id doesn't exist in the test database
            int id = 40; 
            var ex = Assert.Throws<ArgumentException>(() => unit.Projects.Get(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
        }


        [Test, Order(4)]
        public void InsertProject()
        {
            Project project = new Project
            {
                Name = "Test Project",
                Team = unit.Teams.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1),
                Customer = unit.Customers.Get(1)
            };
            unit.Projects.Insert(project);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(4, project.Id);//id of the new project will be 4
        }

        [Test, Order(5)]
        public void ChangeProjectsName()
        {
            int id = 2;//Try to change the project with id
            Project project = new Project
            {
                Id = id,
                Name = "Test Project",
                Team = unit.Teams.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1),
                Customer = unit.Customers.Get(1)
            };
            unit.Projects.Update(project, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Test Project", project.Name);
        }
               

        [Test, Order(6)]
        public void ChangeNonExistingProject()
        {
            //Try to change the project with id (doesn't exist)
            int id = 40;
            Project project = new Project
            {
                Id = id,
                Name = "Test Project",
                Team = unit.Teams.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1),
                Customer = unit.Customers.Get(1)
            };
            var ex = Assert.Throws<ArgumentException>(() => unit.Projects.Update(project, id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(7)]
        public void ChangeProjectWithWrongId()
        {
            //Try to change the employee with a wrong id argument in update method
            int id = 1;
            int wrongId = 2;

            Project project = new Project
            {
                Id = id,
                Name = "Test Project",
                Team = unit.Teams.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1),
                Customer = unit.Customers.Get(1)
            };
            var ex = Assert.Throws<ArgumentException>(() => unit.Projects.Update(project, wrongId));
            Assert.AreEqual(ex.Message, $"Error! Id of the sent object: {project.Id} and id in url: {wrongId} do not match");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void ChangeProjectStatus()
        {
            int id = 2;//Try to change the project with id
            int statusId = 4; //new status Id
            Project project = new Project
            {
                Id = id,
                Name = "Test Project",
                Team = unit.Teams.Get(1),
                Status = unit.ProjectStatuses.Get(statusId),
                Pricing = unit.PricingStatuses.Get(1),
                Customer = unit.Customers.Get(1)
            };

            unit.Projects.Update(project, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(statusId, project.Status.Id);
        }

        [Test, Order(9)]
        public void ChangeProjectTeam()
        {
            int id = 2;//Try to change the project with id
            Project project = new Project
            {
                Id = id,
                Name = "Test Project",
                Team = unit.Teams.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1),
                Customer = unit.Customers.Get(1)
            };
            int teamId = 1; //new status Id
            project.Team = unit.Teams.Get(teamId);
            unit.Projects.Update(project, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(teamId, project.Team.Id);
        }

        [Test, Order(10)]
        public void ChangeProjectEndDate()
        {
            DateTime endDate = new DateTime(2019, 10, 23);
            int id = 2;//Try to change the project with id
            Project project = new Project
            {
                Id = id,
                Name = "Test Project",
                Team = unit.Teams.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1),
                Customer = unit.Customers.Get(1),
                EndDate = endDate
            };
            unit.Projects.Update(project, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(endDate, project.EndDate);
        }

        [Test, Order(11)]
        public void DeleteProjectWithChildren()
        {
            int id = 2;//Try to delete the project with id
            
            var ex = Assert.Throws<Exception>(() => unit.Projects.Delete(id));
            Assert.AreEqual(ex.Message, "Object cannot be deleted because child objects are present");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(12)]
        public void DeleteProjectWithWrongId()
        {
            int id = 40;//Try to delete the project with id (doesn't exist)

            var ex = Assert.Throws<ArgumentException>(() => unit.Projects.Delete(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");            
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(13)]
        public void DeleteProject()
        {
            int id = 2;//Try to delete the project with id

            Project project = unit.Projects.Get(2);
            //first all child entities will have to be deleted
            //this list will be used for iteration only
            List<JobDetail> projectTasks = project.Tasks.ToList();
            foreach(JobDetail task in projectTasks)
            {
                //project.Tasks.Remove(task);
                unit.Tasks.Delete(task);
            }

            unit.Save();

            unit.Projects.Delete(id);
            int numberOfChanges = unit.Save();
            //67 child entities and 1 parent entitiy will be deleted, making it 68 changes
            Assert.AreEqual(68, numberOfChanges);
        }
    }
}
