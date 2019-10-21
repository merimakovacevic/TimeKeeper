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
    [Route("api/tasks/{taskId}/calendar")]
    [ApiController]
    public class CalendarController : BaseController
    {
        public CalendarController(TimeKeeperContext context, ILogger<TeamsController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get(int taskId)
        {
            try
            {
                JobDetailModel task = Unit.Tasks.Get(taskId).Create();
                if (task == null)
                {
                    Log.LogError($"Task with id {taskId} cannot be found");
                    return NotFound("Task not found");
                }
                return Ok(task.Day);
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int taskId, int id)
        {
            try
            {
                JobDetail task = Unit.Tasks.Get(taskId);
                Log.LogInformation($"Try to get task with {taskId}");
                if (task == null)
                {
                    Log.LogError($"Task with id {taskId} cannot be found");
                    return NotFound("Job not found");
                }
                DayModel day = task.Day.Create();
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

        [HttpPost]
        public IActionResult Post([FromBody] Day day, int taskId)
        {
            try
            {
                day.Employee = Unit.Employees.Get(day.Employee.Id);
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Day day)
        {
            try
            {
                day.Employee = Unit.Employees.Get(day.Employee.Id);
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