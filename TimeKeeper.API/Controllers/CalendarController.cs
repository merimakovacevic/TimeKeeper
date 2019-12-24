using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.BLL;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        protected CalendarService calendarService;

        public CalendarController(TimeKeeperContext context) : base(context)
        {
            calendarService = new CalendarService(Unit);
        }

        /// <summary>
        /// This method returns monthly calendar for an employee
        /// </summary>
        /// <param name="employeeId">Id of employee who owns the day</param>
        /// <param name="year">Year from the date</param>
        /// <param name="month">Month from the date</param>
        /// <returns>All days</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{employeeId}/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int employeeId, int year, int month)
        {
            try
            {
                return Ok(calendarService.GetEmployeeMonth(employeeId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns day with specified id
        /// </summary>
        /// <param name="id">Id of day</param>
        /// <returns>day with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "AdminLeadOrOwner")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                Logger.Info($"Try to get day with {id}");
                Day day = Unit.Calendar.Get(id);
                if (day == null)
                {
                    // make extension method and implement it
                    return NotFound($"Requested resource with {id} does not exist");
                }
                if (userRole == "admin" && !day.JobDetails.Any(x => x.Project.Team.Members.Any(y => y.Employee.Id == userId)) ||
                    userRole == "user" && !day.JobDetails.Any(x => x.Project.Team.Members.Any(y => y.Employee.Id == userId)))
                {
                    return Unauthorized();
                }
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method inserts a new day
        /// </summary>
        /// <param name="day">New day that will be inserted</param>
        /// <returns>Model of inserted day</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] Day day)
        {
            try
            {
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                Logger.Info("Try to insert new day");

                if (userRole != "admin" && !(day.Employee.Id == userId))
                {
                    return Unauthorized();
                }
                await Unit.Calendar.InsertAsync(day);
                await Unit.SaveAsync();
                Logger.Info($"Day {day.Date} added with id {day.Id}");
                Day createdDay = Unit.Calendar.Get(day.Id);
                return Ok(createdDay.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method updates data for day with specified id
        /// </summary>
        /// <param name="id">Id of day that will be updated</param>
        /// <param name="day">Data that comes from frontend</param>
        /// <returns>Team with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromBody] Day day)
        {
            try
            {
                Logger.Info($"Attempt to update day with id {id}");
                int userId = int.Parse(GetUserClaim("sub"));
                string userRole = GetUserClaim("role");

                if (userRole != "admin" && !(day.Employee.Id == userId))
                {
                    return Unauthorized();
                }
                Unit.Calendar.UpdateAsync(day, id);
                await Unit.SaveAsync();
                Logger.Info($"Changed day with id {id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method deletes day with specified id
        /// </summary>
        /// <param name="id">Id of day that has to be deleted</param>
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
                Logger.Info($"Attempt to delete day with id {id}");
                Unit.Calendar.DeleteAsync(id);
                await Unit.SaveAsync();

                Logger.Info($"Deleted day with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}