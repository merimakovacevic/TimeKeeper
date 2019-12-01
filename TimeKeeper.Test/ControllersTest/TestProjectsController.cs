using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersTest
{
    [TestFixture]
    class TestProjectsController : TestBaseTestDatabase
    {
        [Test, Order(1)]
        public void GetAllProjects()
        {
            var controller = new ProjectsController(unit.Context);

            var response = controller.GetAll() as ObjectResult;
            var value = response.Value as List<ProjectModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(3, value.Count); //there are 3 projects in the test database
        }

        [Test, Order(1)]
        [TestCase(1, "Titanic Data Set")]
        [TestCase(2, "Loan Prediction")]
        [TestCase(3, "Image Net")]
        public void GetProjectById(int id, string name)
        {
            var controller = new ProjectsController(unit.Context);

            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(2)]
        public void GetProjectByWrongId()
        {
            int id = 40; //project with id 4 doesn't exist in the test database
            var controller = new ProjectsController(unit.Context);

            var response = controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(3)]
        public void InsertProject()
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

            var response = controller.Post(project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(4, value.Id);//Id of the new project will be 4
        }

        [Test, Order(4)]
        public void ChangeProjectName()
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

            var response = controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Web app", value.Name);
        }

        [Test, Order(5)]
        public void ChangeProjectWithWrongId()
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

            var response = controller.Put(id, project) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public void ChangeProjectStatus()
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

            var response = controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(statusId, value.Status.Id);
        }
        [Test, Order(7)]
        public void ChangeProjectsTeam()
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

            var response = controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(teamId, value.Team.Id);
        }
        [Test, Order(8)]
        public void ChangeProjectsEndDate()
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

            var response = controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(endDate, value.EndDate);
        }

        [Test, Order(9)]
        public void DeleteProject()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 3;//Try to delete the project with id 3

            var response = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(10)]
        public void DeleteProjectWithWrongId()
        {
            var controller = new ProjectsController(unit.Context);
            int id = 40;//Try to delete the project with id (doesn't exist)

            var response = controller.Delete(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
