using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        private UnitOfWork unit;
        public TeamsController(TimeKeeperContext context)
        {
            unit = new UnitOfWork(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = unit.Teams.Get().ToList().Select(x => x.Create()).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = unit.Teams.Get(id);
            return Ok(result.Create());
        }
    }
}