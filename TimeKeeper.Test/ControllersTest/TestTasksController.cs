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
    public class TestTasksController : TestBaseTestDatabase
    {
        [Test, Order(1)]
        public async Task GetAllTasks()
        {
            var controller = new TasksController(unit.Context);

            var response =await controller.Get() as ObjectResult;
            var value = response.Value as List<JobDetailModel>;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(97, value.Count); //there are 97 tasks in the test database
        }

        [Test, Order(1)]
        [TestCase(2, "iOS Sprint 20")]
        [TestCase(4, "Sprint 23 web testing")]
        [TestCase(5, "TS")]
        public async Task GetTaskById(int id, string description)
        {
            var controller = new TasksController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;
            var value = response.Value as JobDetailModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(id, value.Id);
        }

        [Test, Order(2)]
        public async Task GetTaskByWrongId()
        {
            int id = 140; //Task with id 140 doesn't exist in the test database
            var controller = new TasksController(unit.Context);

            var response = await controller.Get(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(3)]
        public async Task InsertTask()
        {
            var controller = new TasksController(unit.Context);

            JobDetail task = new JobDetail
            {
                Day = unit.Calendar.Get(1),
                Project = unit.Projects.Get(1)
            };

            var response = await controller.Post(task) as ObjectResult;
            var value = response.Value as JobDetailModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(98, value.Id);//Id of the new task will be 98
        }

        [Test, Order(4)]
        public async Task ChangeTaskHours()
        {
            var controller = new TasksController(unit.Context);
            int id = 3;//Try to change the task with id 3

            JobDetail task = new JobDetail
            {
                Id=id,
                Day = unit.Calendar.Get(1),
                Project = unit.Projects.Get(1),
                Hours = 7
            };

            var response = await controller.Put(id, task) as ObjectResult;
            var value = response.Value as JobDetailModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(7, value.Hours);
        }

        [Test, Order(5)]
        public async Task ChangeTaskWithWrongId()
        {
            var controller = new TasksController(unit.Context);
            int id = 140;//Try to change the task with id (doesn't exist)

            JobDetail task = new JobDetail
            {
                Id = id,
                Day = unit.Calendar.Get(1),
                Project = unit.Projects.Get(1),
                Hours = 7
            };

            var response = await controller.Put(id, task) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test, Order(6)]
        public async Task DeleteTask()
        {
            var controller = new TasksController(unit.Context);
            int id = 3;//Try to delete the task with id 3

            var response = await controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, response.StatusCode);

        }

        [Test, Order(7)]
        public async Task DeleteTaskWithWrongId()
        {
            var controller = new TasksController(unit.Context);
            int id = 140;//Try to delete the task with id (doesn't exist)

            var response =await controller.Delete(id) as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
