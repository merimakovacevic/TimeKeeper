using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseController
    {
        public TeamsController(TimeKeeperContext context, ILogger<TeamsController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Teams.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Log.LogInformation($"Try to get team with {id}");
                Team team = Unit.Teams.Get(id);
                if (team == null)
                {
                    Log.LogError($"There is no team with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(team.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
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
                Log.LogInformation($"Team {team.Name} added with id {team.Id}");
                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
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
                    Log.LogError($"Team with {id} not found");
                    return NotFound();
                }
                Log.LogInformation($"Changed team with id {id}");
                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
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
                    Log.LogInformation($"Attempt to delete team with id {id}");
                    return NotFound();
                }
                Log.LogInformation($"Deleted team with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
    }
}