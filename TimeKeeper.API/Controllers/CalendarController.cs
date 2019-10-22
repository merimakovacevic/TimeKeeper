using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/employees/{employeeId}/calendar")]
    [ApiController]
    public class CalendarController : BaseController
    {
        public CalendarController(TimeKeeperContext context, ILogger<TeamsController> log) : base(context, log) { }

        /// <summary>
        /// This method returns all days
        /// </summary>
        /// <param name="employeeId">Id of employee who owns the day</param>
        /// <returns>All days</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int employeeId)
        {
            try
            {
                EmployeeModel emp = Unit.Employees.Get(employeeId).Create();
                if (emp == null)
                {
                    Log.LogError($"Employee with id {employeeId} cannot be found");
                    return NotFound("Task not found");
                }
                return Ok(emp.Calendar);
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest();
            }
        }

        /// <summary>
        /// This method returns day with specified id
        /// </summary>
        /// <param name="id">Id of day</param>
        /// <param name="employeeId">Id of employee who owns the day</param>
        /// <returns>day with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int employeeId, int id)
        {
            try
            {
                Employee emp = Unit.Employees.Get(employeeId);
                Log.LogInformation($"Try to get employee with {employeeId}");
                if (emp == null)
                {
                    Log.LogError($"Employee with id {employeeId} cannot be found");
                    return NotFound("Employee not found");
                }
                DayModel day = emp.Calendar.FirstOrDefault(x => x.Id == id).Create();
                Log.LogInformation($"Try to get day with {id}");
                if (day == null)
                {
                    Log.LogError($"Day with id {id} cannot be found");
                    return NotFound("Day not found");
                }
                return Ok(day);
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest();
            }
        }

        /// <summary>
        /// This method inserts a new day
        /// </summary>
        /// <param name="day">New day that will be inserted</param>
        /// <param name="employeeId">Id of employee who owns the day</param>
        /// <returns>Model of inserted day</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Day day, int employeeId)
        {
            try
            {
                day.Employee = Unit.Employees.Get(employeeId);
                day.DayType = Unit.DayTypes.Get(day.DayType.Id);

                Unit.Calendar.Insert(day);
                Unit.Save();
                Log.LogInformation($"Day {day.Date} added with id {day.Id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method updates data for day with specified id
        /// </summary>
        /// <param name="id">Id of day that will be updated</param>
        /// <param name="day">Data that comes from frontend</param>
        /// <param name="employeeId">Id of employee who owns the day</param>
        /// <returns>Team with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Day day, int employeeId)
        {
            try
            {
                day.Employee = Unit.Employees.Get(employeeId);
                day.DayType = Unit.DayTypes.Get(day.DayType.Id);

                Unit.Calendar.Update(day, id);
                Log.LogInformation($"Changed day with id {id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
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
                Unit.Calendar.Delete(id);

                int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    Log.LogInformation($"Attempt to delete day with id {id}");
                    return NotFound();
                }
                Log.LogInformation($"Deleted day with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
    }
}