using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.LOG;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseController
    {
        public TeamsController(TimeKeeperContext context) : base(context) { }


        /// <summary>
        /// This method returns all teams
        /// </summary>
        /// <returns>All teams</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                if(userRole == "admin")
                {
                    return Ok(Unit.Teams.Get().ToList().Select(x => x.Create()).ToList());
                }
                else
                {
                    var query = Unit.Teams.Get(x => x.Members.Any(y => y.Employee.Id == userId));
                    return Ok(query.ToList().Select(x => x.Create()).ToList());//without the first ToList(), we will have a lazy loading exception?
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns team with specified id
        /// </summary>
        /// <param name="id">Id of team</param>
        /// <returns>Team with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "IsMemberInTeam")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try {

                Logger.Info($"Try to get team with {id}");
                Team team = Unit.Teams.Get(id);

                return Ok(team.Create());

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method inserts a new team
        /// </summary>
        /// <param name="team">New team that will be inserted</param>
        /// <returns>Model of inserted team</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        //[Authorize(Policy = "IsAdmin")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Team team)
        {
            try
            {
                Unit.Teams.Insert(team);
                Unit.Save();
                Logger.Info($"Team {team.Name} added with id {team.Id}");
                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method updates data for team with specified id
        /// </summary>
        /// <param name="id">Id of team that will be updated</param>
        /// <param name="team">Data that comes from frontend</param>
        /// <returns>Team with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        //[Authorize(Policy = "IsAdmin")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Team team)
        {
            try
            {
                Unit.Teams.Update(team, id);
                Unit.Save();

                /*int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Team with {id} not found");
                    return NotFound();
                }*/
                Logger.Info($"Changed team with id {id}");
                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method deletes team with specified id
        /// </summary>
        /// <param name="id">Id of team that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        //[Authorize(Policy = "IsAdmin")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete team with id {id}");
                Unit.Teams.Delete(id);
                Unit.Save();

                /*int numberOfChanges = Unit.Save();
                
                if (numberOfChanges == 0)
                {
                    Logger.Error($"Attempt to delete team with id {id}");
                    return NotFound();
                }*/
                Logger.Info($"Deleted team with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}