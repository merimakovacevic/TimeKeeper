using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    //Will the route for this Controller require refactoring? Employees/{id}/Calendar/{id}/Tasks?
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
                var result = Unit.Tasks.Get().ToList().Select(x => x.Create()).ToList();
                Logger.Info($"Try to get all tasks");
                return Ok(result);
            }
            catch(Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method returns role with specified id
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
                var result = Unit.Tasks.Get(id);
                if (result == null)
                {
                    Logger.Error($"Task with id {id} cannot be found");
                    return NotFound();
                }
                return Ok(result.Create());
            }
            catch(Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex);
            }
        }
    }
}