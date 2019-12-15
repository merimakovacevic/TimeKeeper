﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using Microsoft.Extensions.Logging;
using TimeKeeper.BLL;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;
using Microsoft.AspNetCore.Authorization;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : BaseController
    {
        protected QueryService queryService;
        public MembersController(TimeKeeperContext context) : base(context)
        {
            queryService = new QueryService(Unit);
        }

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
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                if (userRole == "admin")
                {
                    Logger.Info("Try to get all members");
                    return Ok(Unit.Members.Get().ToList().Select(x => x.Create()).ToList());
                }
                else
                {
                    return Ok(queryService.GetEmployeeTeamMembers(userId));
                }

            }
            catch(Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsMember")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                Logger.Info($"Try to get member with {id}");
                Member member = Unit.Members.Get(id);

                return Ok(member.Create());
            }
            catch(Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsMember")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Member member)
        {
            try
            {
                Unit.Members.Insert(member);
                Unit.Save();
                Logger.Info($"Member {member.Employee.FirstName} added with id {member.Id}");
                return Ok(member.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsMember")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Member member)
        {
            try
            {
                Unit.Members.Update(member, id);
                Unit.Save();

                Logger.Info($"Changed member with id {id}");
                return Ok(member.Create());
            
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete team with id {id}");
                Unit.Members.Delete(id); 
                Unit.Save();

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
