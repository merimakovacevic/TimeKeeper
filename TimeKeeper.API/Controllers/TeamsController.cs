using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var result = unit.Teams.Get().Select(t=> new { Id=t.Id, Name=t.Name,
                                                            Members=t.Members.Select(m=> new{m.Employee.FirstName, m.Role.Name}).ToList()}).ToList();
            return Ok(result);
        }
        
    }
}