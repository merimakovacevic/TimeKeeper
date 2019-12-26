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
using Newtonsoft.Json;
using TimeKeeper.API.Authorization;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : BaseController
    {
        private PaginationService<JobDetail> _pagination;

        public TasksController(TimeKeeperContext context) : base(context)
        {
            _pagination = new PaginationService<JobDetail>();
        }

        /// <summary>
        /// This method returns all tasks
        /// </summary>
        /// <returns>All tasks</returns>
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
                Logger.Info($"Try to fetch ${pageSize} projects from page ${page}");
                //List<JobDetail> query = await GetAuthorizedTasks();
                List<JobDetail> query = await resourceAccess.GetAuthorizedTasks(GetUserClaims());

                Tuple<PaginationModel, List<JobDetail>> tasksPagination;
                tasksPagination = _pagination.CreatePagination(page, pageSize, query);

                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(tasksPagination.Item1));
                return Ok(tasksPagination.Item2.Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns a task with specified id
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
        [Authorize(Policy = "AdminLeadOrMember")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Logger.Info($"Try to get task with {id}");
                JobDetail task = await Unit.Tasks.GetAsync(id);

                if (!resourceAccess.HasRight(GetUserClaims(), task)) return Unauthorized();
                return Ok(task.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method inserts a new task
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <returns>Model of inserted task</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "AdminLeadOrOwner")]
        public async Task<IActionResult> Post([FromBody] JobDetail jobDetail)
        {
            try
            {
                if (!resourceAccess.HasRight(GetUserClaims(), jobDetail)) return Unauthorized();
                //This line will result in an null object reference exception, we cannot access properties of jobDetail.Day because they are null
                //Logger.Info($"Task for employee {jobDetail.Day.Employee.FullName}, day {jobDetail.Day.Date} added with id {jobDetail.Id}");
                await Unit.Tasks.InsertAsync(jobDetail);
                await Unit.SaveAsync();
                Logger.Info($"Task with id {jobDetail.Id} was added");
                return Ok(jobDetail.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method updates data for a task with the specified id
        /// </summary>
        /// <param name="id">Id of task that will be updated</param>
        /// <param name="jobDetail">Data that comes from frontend</param>
        /// <returns>Task model with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "AdminLeadOrOwner")]
        public async Task<IActionResult> Put(int id, [FromBody] JobDetail jobDetail)
        {
            try
            {
                if (!resourceAccess.HasRight(GetUserClaims(), jobDetail)) return Unauthorized();
                Logger.Info($"Modified task with id {id}");

                await Unit.Tasks.UpdateAsync(jobDetail, id);
                await Unit.SaveAsync();
                return Ok(jobDetail.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method deletes a task with the specified id
        /// </summary>
        /// <param name="id">Id of task that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "AdminLeadOrOwner")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete task with id {id}");
                JobDetail jobDetail = Unit.Tasks.Get(id);
                if (!resourceAccess.HasRight(GetUserClaims(), jobDetail)) return Unauthorized();
                await Unit.Tasks.DeleteAsync(id);
                await Unit.SaveAsync();

                Logger.Info($"Deleted task with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

    }
}