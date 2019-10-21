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
        public TasksController(TimeKeeperContext context, ILogger<TeamsController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = Unit.Tasks.Get().ToList().Select(x => x.Create()).ToList();
                Log.LogInformation($"Try to get all tasks");
                return Ok(result);
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Log.LogInformation($"Try to get task with {id}");
                var result = Unit.Tasks.Get(id);
                if (result == null)
                {
                    Log.LogError($"Task with id {id} cannot be found");
                    return NotFound();
                }
                return Ok(result.Create());
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
    }
}