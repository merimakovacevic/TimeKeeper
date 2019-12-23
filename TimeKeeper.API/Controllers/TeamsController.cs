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
using Newtonsoft.Json;
using TimeKeeper.API.Services;
using TimeKeeper.BLL;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.LOG;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseController
    {
        private PaginationService<Team> _pagination;
        public TeamsController(TimeKeeperContext context) : base(context)
        {
            _pagination = new PaginationService<Team>();
        }


        /// <summary>
        /// This method returns all teams from a selected page, given the page size
        /// </summary>
        /// <returns>All teams from a page</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "AdminLeadOrMember")]
<<<<<<< HEAD
        public IActionResult GetAll(int page = 1, int pageSize = 10)
=======
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
>>>>>>> dev
        {
            try
            {
                Logger.Info($"Try to fetch ${pageSize} teams from page ${page}");

                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");
                Tuple<PaginationModel, List<Team>> teamsPagination;
                List<Team> query;

                if(userRole == "user" || userRole == "lead")
                {
                    var task = await Unit.Teams.GetAsync(x => x.Members.Any(y => y.Employee.Id == userId));
                    query = task.ToList();                    
                }
                else
                {
                    var task = await Unit.Teams.GetAsync();
                    query = task.ToList();
                }

                teamsPagination = _pagination.CreatePagination(page, pageSize, query);
                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(teamsPagination.Item1));
                return Ok(teamsPagination.Item2.Select(x => x.Create()).ToList());
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
        [Authorize(Policy = "AdminLeadOrMember")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int id)
        {
            try {
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                Logger.Info($"Try to get team with {id}");
                Team team = await Unit.Teams.GetAsync(id);
                if (team == null)
                {
                    return NotFound($"Requested resource with {id} does not exist");
                }
                if (userRole == "user" || (userRole == "lead" && !team.Members.Any(x => x.Employee.Id == userId)))
                {
                    return Unauthorized();
                }
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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] Team team)
        {
            try
            {
                Unit.Teams.InsertAsync(team);
                await Unit.SaveAsync();
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
        [Authorize(Policy = "IsAdmin")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromBody] Team team)
        {
            try
            {
                Unit.Teams.UpdateAsync(team, id);
                await Unit.SaveAsync();

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete team with id {id}");
                Unit.Teams.DeleteAsync(id);
                await Unit.SaveAsync();

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