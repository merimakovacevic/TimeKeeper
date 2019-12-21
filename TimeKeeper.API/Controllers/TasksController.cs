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
        public IActionResult Get(int page = 1, int pageSize = 10)
        {
            try
            {
                Logger.Info($"Try to fetch ${pageSize} projects from page ${page}");
                Tuple<PaginationModel, List<JobDetail>> tasksPagination;
                List<JobDetail> query;

                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                if (userRole == "lead")
                {
                    query = Unit.Tasks.Get(x => x.Project.Team.Members.Any(y => y.Employee.Id == userId)).ToList();
                }
                else if (userRole == "user")
                {
                    query = Unit.Tasks.Get(x => x.Day.Employee.Id == userId).ToList();
                }
                else
                {
                    query = Unit.Tasks.Get().ToList();
                }

                tasksPagination = _pagination.CreatePagination(page, pageSize, query);
                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(tasksPagination.Item1));
                return Ok(tasksPagination.Item2.Select(x => x.Create()).ToList());
            }
            catch(Exception ex)
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
        public IActionResult Get(int id)
        {
            try
            {
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                Logger.Info($"Try to get task with {id}");
                var task = Unit.Tasks.Get(id);
                if (task == null)
                {
                    return NotFound($"Requested resource with {id} does not exist");
                }
                if ((userRole == "lead" && !(task.Project.Team.Members.Any(x => x.Employee.Id == userId))) ||
                    (userRole == "user" && !(task.Day.Employee.Id == userId)))
                {
                    return Unauthorized();
                }
                return Ok(task.Create());
            }
            catch(Exception ex)
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
        public IActionResult Post([FromBody] JobDetail jobDetail)
        {
            try
            {
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                Logger.Info($"Task for employee {jobDetail.Day.Employee.FullName}, day {jobDetail.Day.Date} added with id {jobDetail.Id}");
                if (userRole == "lead" && !jobDetail.Project.Team.Members.Any(x => x.Employee.Id == userId) ||
                    userRole == "user" && !(jobDetail.Day.Employee.Id == userId))
                {
                    return Unauthorized();
                }
                Unit.Tasks.Insert(jobDetail);
                Unit.Save();
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
        public IActionResult Put(int id, [FromBody] JobDetail jobDetail)
        {
            try
            {
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                Logger.Info($"Changed task with id {id}");
                if (userRole == "lead" && !jobDetail.Project.Team.Members.Any(x => x.Employee.Id == userId) ||
                    userRole == "user" && !(jobDetail.Day.Employee.Id == userId))
                {
                    return Unauthorized();
                }
                Unit.Tasks.Update(jobDetail, id);
                Unit.Save();
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
        [Authorize(Policy = "IsAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete task with id {id}");
                Unit.Tasks.Delete(id);
                Unit.Save();

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
