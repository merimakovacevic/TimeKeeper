using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Models;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace TimeKeeper.API.Controllers
{
    [Route("api/teams/{teamId}/members")]
    [ApiController]
    public class MembersController : BaseController
    {
        public MembersController(TimeKeeperContext context, ILogger<TeamsController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get(int teamId)
        {
            try
            {
                TeamModel team = Unit.Teams.Get(teamId).Create();
                if (team == null)
                {
                    Log.LogError($"Team with id {teamId} cannot be found");
                    return NotFound("Team not found");
                }
                return Ok(team.Members);
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int teamId, int id)
        {
            try
            {
                Team team = Unit.Teams.Get(teamId);
                Log.LogInformation($"Try to get team with {teamId}");
                if (team == null)
                {
                    Log.LogError($"Team with id {teamId} cannot be found");
                    return NotFound("Team not found");
                }
                MemberModel member = team.Members.FirstOrDefault(x => x.Id == id).Create();
                if (member == null)
                {
                    Log.LogError($"Member with id {id} cannout be found");
                    return NotFound("Member not found");
                }
                return Ok(member);
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Member member, int teamId)
        {
            try
            {
                member.Team = Unit.Teams.Get(teamId);
                member.Employee = Unit.Employees.Get(member.Employee.Id);
                member.Role = Unit.Roles.Get(member.Role.Id);

                Unit.Members.Insert(member);
                Unit.Save();
                Log.LogInformation($"Member {member.Employee.FirstName} added with id {member.Id}");
                return Ok(member.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Member member, int teamId)
        {
            try
            {
                member.Team = Unit.Teams.Get(teamId);
                member.Employee = Unit.Employees.Get(member.Employee.Id);
                member.Role = Unit.Roles.Get(member.Role.Id);

                Unit.Members.Update(member, id);
                Log.LogInformation($"Changed member with id {id}");

                return Ok(member.Create());
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
                Unit.Members.Delete(id);

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