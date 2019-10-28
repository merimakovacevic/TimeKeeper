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
        public IActionResult Get()
        {
            try
            {
                Logger.Info($"Try to get all roles");
                return Ok(Unit.Roles.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch(Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
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
        public IActionResult Get(int id)
        {
            try
            {
                Logger.Info($"Try to get roles with {id}");
                Role role = Unit.Roles.Get(id);
                if (role == null)
                {
                    Logger.Error($"Role with id {id} cannot be found");
                    return NotFound();
                }
                else
                {
                    return Ok(role.Create());
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
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
        public IActionResult Post([FromBody] Role role)
        {
            try
            {
                Unit.Roles.Insert(role);
                Unit.Save();
                Logger.Info($"Role added with id {role.Id}");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
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
        public IActionResult Put(int id, [FromBody] Role role)
        {
            try
            {
                Unit.Roles.Update(role, id);
                int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Role with {id} not found");
                    return NotFound();
                }
                Logger.Info($"Changed role with id {id}");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
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
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Roles.Delete(id);

                int numberOfChanges = Unit.Save();
                Logger.Info($"Attempt to delete role with id {id}");
                if (numberOfChanges == 0)
                {
                    Logger.Info($"Attempt to delete role with id {id}");
                    return NotFound();
                }
                Logger.Info($"Deleted role with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}