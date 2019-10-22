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

        [HttpGet]
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

        [HttpGet("{id}")]
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
                Day day = emp.Calendar.FirstOrDefault(x => x.Id == id);
                Log.LogInformation($"Try to get day with {id}");
                if (day == null)
                {
                    Log.LogError($"Day with id {id} cannot be found");
                    return NotFound("Day not found");
                }
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Day day, int employeeId)
        {
            try
            {                
                day.Employee = Unit.Employees.Get(employeeId);
                day.DayType = Unit.DayTypes.Get(day.DayType.Id);

                Unit.Calendar.Insert(day);
                Unit.Save();//Exception is thrown: duplicate key value violates unique constraint "PK_Calendar"
                Log.LogInformation($"Day {day.Date} added with id {day.Id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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