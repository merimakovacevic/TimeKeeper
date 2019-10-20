using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseController
    {
        public TeamsController(TimeKeeperContext context) : base(context) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Teams.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Team team = Unit.Teams.Get(id);
                if (team == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(team.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Team team)
        {
            try
            {
                Unit.Teams.Insert(team);
                Unit.Save();
                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Team team)
        {
            try
            {
                Unit.Teams.Update(team, id);
                int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    return NotFound();
                }

                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Teams.Delete(id);

                int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}