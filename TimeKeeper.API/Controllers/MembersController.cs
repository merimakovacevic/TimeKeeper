using System;
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
using Newtonsoft.Json;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : BaseController
    {
        protected QueryService queryService;
        private PaginationService<Member> _pagination;
        public MembersController(TimeKeeperContext context) : base(context)
        {
            queryService = new QueryService(Unit);
            _pagination = new PaginationService<Member>();
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
        [Authorize(Policy = "AdminLeadOrMember")]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            try
            {
                Tuple<PaginationModel, List<Member>> membersPagination;
                List<Member> query = await resourceAccess.GetAuthorizedMembers(GetUserClaims());

                membersPagination = _pagination.CreatePagination(page, pageSize, query);
                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(membersPagination));
                return Ok(membersPagination.Item2.Select(x => x.Create()).ToList());
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
        [Authorize(Policy = "AdminLeadOrMember")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Logger.Info($"Try to get member with {id}");
                Member member = await Unit.Members.GetAsync(id);

                if (!resourceAccess.CanReadMember(GetUserClaims(), member)) return Unauthorized();                
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
        [Authorize(Policy = "AdminOrLeader")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] Member member)
        {
            try
            {
                Logger.Info("Trying to post new member");
                if (!resourceAccess.CanWriteMember(GetUserClaims(), member)) return Unauthorized();
                await Unit.Members.InsertAsync(member);
                await Unit.SaveAsync();
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
        [Authorize(Policy = "AdminOrLeader")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromBody] Member member)
        {
            try
            {
                await Unit.Members.UpdateAsync(member, id);
                await Unit.SaveAsync();

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
        [Authorize(Policy = "AdminOrLeader")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete team with id {id}");
                Unit.Members.Delete(id); 
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
