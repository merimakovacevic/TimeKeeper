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
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : BaseController
    {
        public MembersController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method returns all members
        /// </summary>
        /// <returns>All members</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {  
                //This is only neccessary if there is a team in the route
                /*
                TeamModel team = Unit.Teams.Get(teamId).Create();
                if (team == null)
                {
                    //Log.LogError($"Team with id {teamId} cannot be found");
                    return NotFound("Team not found");
                }*/
                return Ok(Unit.Members.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch(Exception ex)
            {
                //Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method returns member with specified id
        /// </summary>
        /// <param name="id">Id of day</param>
        /// <returns>member with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                /*
                Team team = Unit.Teams.Get(teamId);
                //Log.LogInformation($"Try to get team with {teamId}");
                if (team == null)
                {
                    //Log.LogError($"Team with id {teamId} cannot be found");
                    return NotFound("Team not found");
                }*/

                Member member = Unit.Members.Get(id);

                if (member == null)
                {
                    //Log.LogError($"Member with id {id} cannout be found");
                    return NotFound();
                }
                return Ok(member.Create());
            }
            catch(Exception ex)
            {
                //Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method inserts a new member
        /// </summary>
        /// <param name="member">New member that will be inserted</param>
        /// <returns>Model of inserted member</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Member member)
        {
            try
            {
                member.Team = Unit.Teams.Get(member.Team.Id);
                member.Employee = Unit.Employees.Get(member.Employee.Id);
                member.Role = Unit.Roles.Get(member.Role.Id);
                member.Status = Unit.MemberStatuses.Get(member.Status.Id);

                Unit.Members.Insert(member);
                Unit.Save();
                //Log.LogInformation($"Member {member.Employee.FirstName} added with id {member.Id}");
                return Ok(member.Create());
            }
            catch (Exception ex)
            {
                //Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method updates data for member with specified id
        /// </summary>
        /// <param name="id">Id of member that will be updated</param>
        /// <param name="member">Data that comes from frontend</param>
        /// <returns>member with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Member member)
        {
            try
            {
                member.Team = Unit.Teams.Get(member.Team.Id);
                member.Employee = Unit.Employees.Get(member.Employee.Id);
                member.Role = Unit.Roles.Get(member.Role.Id);
                member.Status = Unit.MemberStatuses.Get(member.Status.Id);

                Unit.Members.Update(member, id);

                int numberOfChanges = Unit.Save();

                if(numberOfChanges == 0)
                {
                    //Log.LogError($"Member with id {id} cannout be found");
                    return NotFound();
                }

                //Log.LogInformation($"Changed member with id {id}");

                return Ok(member.Create());
            
            }
            catch (Exception ex)
            {
                //Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method deletes member with specified id
        /// </summary>
        /// <param name="id">Id of member that has to be deleted</param>
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
                Unit.Members.Delete(id);
                //Log.LogInformation($"Attempt to delete team with id {id}");
                int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    //Log.LogInformation($"Member with id {id} not found");
                    return NotFound();
                }
                //Log.LogInformation($"Deleted team with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                //Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
    }
}
