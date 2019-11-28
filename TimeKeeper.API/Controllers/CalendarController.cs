using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
namespace TimeKeeper.API.Controllers
{
    //[Authorize]
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
        /// This method returns all days
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
        public IActionResult Get(int id)
        {
            try
            {
                Day day = Unit.Calendar.Get(id);
                Logger.Info($"Try to get day with {id}");

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
        public IActionResult Post([FromBody] Day day)
        {
            try
            {
                Unit.Calendar.Insert(day);
                Unit.Save();
                Logger.Info($"Day {day.Date} added with id {day.Id}");
                return Ok(day.Create());
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
        public IActionResult Put(int id, [FromBody] Day day)
        {
            try
            {
                Logger.Info($"Attempt to update day with id {id}");
                Unit.Calendar.Update(day, id);
                Unit.Save();

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
        public IActionResult Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete day with id {id}");
                Unit.Calendar.Delete(id);
                Unit.Save();

                Logger.Info($"Deleted day with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpGet("team-time-tracking/{teamId}/{year}/{month}")]
        public IActionResult GetTimeTracking(int teamId, int year, int month)
        {
            try
            {
                return Ok(calendarService.GetTeamMonthReport(teamId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("employee-time-tracking/{employeeId}/{year}/{month}")]
        public IActionResult GetPersonalReport(int employeeId, int year, int month)
        {
            try
            {
                return Ok(calendarService.GetEmployeeMonthReport(employeeId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("personal-dashboard-month/{employeeId}/{year}/{month}")]
        public IActionResult GetPersonalMonthDashboard(int employeeId, int year, int month)
        {
            try
            {
                return Ok(calendarService.GetEmployeeMonthDashboard(employeeId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("personal-dashboard-year/{employeeId}/{year}")]
        public IActionResult GetPersonalYearDashboard(int employeeId, int year, int month)
        {
            try
            {
                return Ok(calendarService.GetEmployeeYearDashboard(employeeId, year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("monthly-overview/{year}/{month}")]
        public IActionResult GetMonthlyOverview(int year, int month)
        {
            try
            {
                return Ok(calendarService.GetMonthlyOverview(year, month));
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("project-history/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetProjectHistory(int projectId)
        {
            try
            {
                Logger.Info($"Try to get project history for project with id:{projectId}");
                return Ok(calendarService.GetProjectHistoryModel(projectId));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("projects-annual/{year}")]
        public IActionResult AnnualProjectOverview(int year)
        {
            try
            {
                return Ok(calendarService.GetTotalAnnualOverview(year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}