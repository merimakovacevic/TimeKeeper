using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    public class TestTasks : TestBaseTestDatabase
    {
        [Test, Order(1)]
        public void GetAllTasks()
        {
            //Act
            int tasksCount = unit.Tasks.Get().Count();

            //Assert
            Assert.AreEqual(97, tasksCount); //there are 97 tasks in the test database
        }


        [Test, Order(2)]
        [TestCase(2, "iOS Sprint 20")]
        [TestCase(4, "Sprint 23 web testing")]
        public void GetTaskById(int id, string name)
        {
            var result = unit.Tasks.Get(id);
            Assert.AreEqual(result.Description, name);
        }

        [Test, Order(3)]
        public void GetTaskByWrongId()
        {
            int id = 140; //Task with id doesn't exist in the test database
            var ex = Assert.Throws<ArgumentException>(() => unit.Tasks.Get(id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
        }

        [Test, Order(4)]
        public void InsertTask()
        {
            JobDetail task = new JobDetail
            {
                Day = unit.Calendar.Get(1),
                Project = unit.Projects.Get(1)
            };
            unit.Tasks.Insert(task);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(98, task.Id);//id of the new task will be 98
        }

        [Test, Order(5)]
        public void ChangeTaskDescription()
        {
            int id = 2;//Try to change the task with id
            JobDetail task = new JobDetail
            {
                Id=id,
                Day = unit.Calendar.Get(1),
                Project = unit.Projects.Get(1),
                Description="Unit tests for TimeKeeper project"
            };
            unit.Tasks.Update(task, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Unit tests for TimeKeeper project", task.Description);
        }

        [Test, Order(6)]
        public void ChangeTaskProject()
        {
            int id = 2;//Try to change the project with id
            JobDetail task = new JobDetail
            {
                Id = id,
                Day = unit.Calendar.Get(75),
                Project = unit.Projects.Get(1)
            };
            unit.Tasks.Update(task, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual("Titanic Data Set", task.Project.Name);
        }

        [Test, Order(7)]
        public void ChangeNonExistingTask()
        {
            //Try to change the task with id (doesn't exist)
            int id = 140;
            JobDetail task = new JobDetail
            {
                Id = id,
                Day = unit.Calendar.Get(75),
                Project = unit.Projects.Get(2)
            };

            var ex = Assert.Throws<ArgumentException>(() => unit.Tasks.Update(task, id));
            Assert.AreEqual(ex.Message, $"There is no object with id: {id} in the database");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void ChangeTaskWithWrongId()
        {
            //Try to change the task with a wrong id argument in update method
            int id = 1;
            int wrongId = 2;
            JobDetail task = new JobDetail
            {
                Id = id,
                Day = unit.Calendar.Get(75),
                Project = unit.Projects.Get(2)
            };

            var ex = Assert.Throws<ArgumentException>(() => unit.Tasks.Update(task, wrongId));
            Assert.AreEqual(ex.Message, $"Error! Id of the sent object: {task.Id} and id in url: {wrongId} do not match");
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(11)]
        public void DeleteTaskWithWrongId()
        {
            int id = 140;//Try to delete the task with id (doesn't exist)

            var ex = Assert.Throws<ArgumentException>(() => unit.Tasks.Delete(id));
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(12)]
        public void DeleteTask()
        {
            int id = 2;//Try to delete the task with id

            unit.Tasks.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
        }
    }
}
