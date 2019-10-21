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
        public TasksController(TimeKeeperContext context, ILogger<TasksController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            var result = Unit.Tasks.Get().ToList().Select(x => x.Create()).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = Unit.Tasks.Get(id);
            return Ok(result.Create());
        }
    }
}