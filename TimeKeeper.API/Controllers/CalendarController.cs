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
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        public CalendarController(TimeKeeperContext context) : base(context) { }

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
                Employee emp = Unit.Employees.Get(employeeId);

                if (emp == null)
                {
                    Logger.Error($"Employee with id {employeeId} cannot be found");
                    return NotFound("Task not found");
                }                

                return Ok(emp.Calendar.Where(x => x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()));
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
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
                //This is only neccessary if there will be an employee in the route
                /*
                Employee emp = Unit.Employees.Get(employeeId);
               // Log.LogInformation($"Try to get employee with {employeeId}");
                if (emp == null)
                {
                    Logger.Error($"Employee with id {employeeId} cannot be found");
                    return NotFound("Employee not found");
                }*/

                Day day = Unit.Calendar.Get(id);

                Logger.Info($"Try to get day with {id}");
                if (day == null)
                {
                    Logger.Error($"Day with id {id} cannot be found");
                    return NotFound("Day not found");
                }
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
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
                day.Employee = Unit.Employees.Get(day.Employee.Id);
                day.DayType = Unit.DayTypes.Get(day.DayType.Id);

                Unit.Calendar.Insert(day);
                Unit.Save();
                Logger.Info($"Day {day.Date} added with id {day.Id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                return BadRequest(ex);
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
                day.Employee = Unit.Employees.Get(day.Employee.Id);
                day.DayType = Unit.DayTypes.Get(day.DayType.Id);

                Unit.Calendar.Update(day, id);
                int numberOfChanges = Unit.Save();
                Logger.Info($"Attempt to update day with id {id}");

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Day with id {id} cannot be found");
                    return NotFound();
                }

                Logger.Info($"Changed day with id {id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
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
                    Logger.Info($"Attempt to delete day with id {id}");
                    return NotFound();
                }
                Logger.Info($"Deleted day with id {id}");
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