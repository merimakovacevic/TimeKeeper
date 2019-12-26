using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.DTO;
using TimeKeeper.Domain.Entities;
using System.Threading.Tasks;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    class TestProjectsController : TestBaseTestDatabase
    {
        [Test, Order(1)]
        public async Task GetAllProjects()
        {
            var controller = new ProjectsController(unit.Context);

            var response = await controller.GetAll() as ObjectResult;
            var value = response.Value as List<ProjectModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(3, value.Count); //there are 3 projects in the test database
        }

        [Test, Order(1)]
        [TestCase(1, "Titanic Data Set")]
        [TestCase(2, "Loan Prediction")]
        [TestCase(3, "Image Net")]
        public async Task GetProjectById(int id, string name)
        {
            var controller = new ProjectsController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(2)]
        public async Task GetProjectByWrongId()
        {
            int id = 40; //project with id 4 doesn't exist in the test database
            var controller = new ProjectsController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(3)]
        public async Task InsertProject()
        {
            var controller = new ProjectsController(unit.Context);

            Project project = new Project
            {
                Name = "Web app",
                Team = unit.Teams.Get(1),
                Customer = unit.Customers.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1)
            };

            var response = await controller.Post(project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(4, value.Id);//Id of the new project will be 4
        }

        [Test, Order(4)]
        public async Task ChangeProjectName()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 3;//Try to change the project with id 3

            Project project = new Project
            {
                Id = id,
                Name = "Web app",
                Team=unit.Teams.Get(1),
                Customer=unit.Customers.Get(1),
                Status=unit.ProjectStatuses.Get(1),
                Pricing=unit.PricingStatuses.Get(1)
            };

            var response = await controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Web app", value.Name);
        }

        [Test, Order(5)]
        public async Task ChangeProjectWithWrongId()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 40;//Try to change the project with id (doesn't exist)

            Project project = new Project
            {
                Id = id,
                Name = "Web app",
                Team = unit.Teams.Get(1),
                Customer = unit.Customers.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1)
            };

            var response = await controller.Put(id, project) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public async Task ChangeProjectStatus()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 2;//Try to change the project with id
            int statusId = 1; //new status Id

            Project project = new Project
            {
                Id=id,
                Name = "Web app",
                Team = unit.Teams.Get(1),
                Customer = unit.Customers.Get(1),
                Status = unit.ProjectStatuses.Get(statusId),
                Pricing = unit.PricingStatuses.Get(1)
            };

            var response = await controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(statusId, value.Status.Id);
        }
        [Test, Order(7)]
        public async Task ChangeProjectsTeam()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 2;//Try to change the project with id
            int teamId = 2;

            Project project = new Project
            {
                Id = id,
                Name = "Web app",
                Team = unit.Teams.Get(teamId),
                Customer = unit.Customers.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1)
            };

            var response = await controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(teamId, value.Team.Id);
        }
        [Test, Order(8)]
        public async Task ChangeProjectsEndDate()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 2;//Try to change the project with id
            DateTime endDate = DateTime.Now;

            Project project = new Project
            {
                Id = id,
                Name = "Web app",
                Team = unit.Teams.Get(1),
                Customer = unit.Customers.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.PricingStatuses.Get(1),
                EndDate=endDate
            };

            var response = await controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(endDate, value.EndDate);
        }

        [Test, Order(9)]
        public async Task DeleteProject()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 3;//Try to delete the project with id 3

            var response = await controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(10)]
        public async Task DeleteProjectWithWrongId()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 40;//Try to delete the project with id (doesn't exist)

            var response = await controller.Delete(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
