using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.BLL;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        public RolesController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method returns all roles
        /// </summary>
        /// <returns>All roles</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                Logger.Info($"Try to get all roles");
                var query = await Unit.Roles.GetAsync();
                return Ok(query.ToList().Select(x => x.Create()).ToList());
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns role with specified id
        /// </summary>
        /// <param name="id">Id of role</param>
        /// <returns>Role with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Logger.Info($"Try to get roles with {id}");
                Role role = await Unit.Roles.GetAsync(id);

                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method inserts a new role
        /// </summary>
        /// <param name="role">New role that will be inserted</param>
        /// <returns>Model of inserted role</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody] Role role)
        {
            try
            {
                await Unit.Roles.InsertAsync(role);
                await Unit.SaveAsync();
                Logger.Info($"Role added with id {role.Id}");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method updates data for role with specified id
        /// </summary>
        /// <param name="id">Id of role that will be updated</param>
        /// <param name="role">Data that comes from frontend</param>
        /// <returns>Role with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put(int id, [FromBody] Role role)
        {
            try
            {
                await Unit.Roles.UpdateAsync(role, id);
                await Unit.SaveAsync();

                Logger.Info($"Changed role with id {id}");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method deletes role with specified id
        /// </summary>
        /// <param name="id">Id of role that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete role with id {id}");
                await Unit.Roles.DeleteAsync(id);
                await Unit.SaveAsync();

                Logger.Info($"Deleted role with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}