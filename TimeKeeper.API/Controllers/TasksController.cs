using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    //Will the route for this Controller require refactoring? Employees/{id}/Calendar/{id}/Tasks?
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : BaseController
    {
        public TasksController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method returns all tasks
        /// </summary>
        /// <returns>All tasks</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Logger.Info($"Try to get all tasks");
                var result = Unit.Tasks.Get().ToList().Select(x => x.Create()).ToList();                
                return Ok(result);
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns a task with specified id
        /// </summary>
        /// <param name="id">Id of role</param>
        /// <returns>Role with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            try
            {
                Logger.Info($"Try to get task with {id}");
                var task = Unit.Tasks.Get(id);

                /*if (result == null)
                {
                    Logger.Error($"Task with id {id} cannot be found");
                    return NotFound();
                }*/

                return Ok(task.Create());
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method inserts a new task
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <returns>Model of inserted task</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] JobDetail jobDetail)
        {
            try
            {
                //jobDetail.Day = Unit.Calendar.Get(jobDetail.Day.Id);
                //jobDetail.Project = Unit.Projects.Get(jobDetail.Project.Id);

                Unit.Tasks.Insert(jobDetail);
                Unit.Save();

                Logger.Info($"Task for employee {jobDetail.Day.Employee.FullName}, day {jobDetail.Day.Date} added with id {jobDetail.Id}");
                return Ok(jobDetail.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method updates data for a task with the specified id
        /// </summary>
        /// <param name="id">Id of task that will be updated</param>
        /// <param name="jobDetail">Data that comes from frontend</param>
        /// <returns>Task model with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] JobDetail jobDetail)
        {
            try
            {
                //jobDetail.Day = Unit.Calendar.Get(jobDetail.Day.Id);
                //jobDetail.Project = Unit.Projects.Get(jobDetail.Project.Id);

                Unit.Tasks.Update(jobDetail, id);
                Unit.Save();
                /*
                int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Task with {id} not found");
                    return NotFound();
                }*/
                Logger.Info($"Changed task with id {id}");
                return Ok(jobDetail.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method deletes a task with the specified id
        /// </summary>
        /// <param name="id">Id of task that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete task with id {id}");
                Unit.Tasks.Delete(id);
                Unit.Save();

                /*int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Task with id {id} not found");
                    return NotFound();
                }*/
                Logger.Info($"Deleted task with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}