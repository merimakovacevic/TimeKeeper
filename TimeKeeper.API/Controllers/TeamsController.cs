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
        private UnitOfWork _unit;
        public TeamsController(TimeKeeperContext context)
        {
            _unit = new UnitOfWork();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _unit.Teams.Get().Select(t => new { t.Id, t.Name, Members = t.Members.Select(m => new { m.Employee.FirstName, m.Role.Name })
                                          .ToList() }).ToList();//Team Description property needs implementation
            return Ok();
        }
    }
}