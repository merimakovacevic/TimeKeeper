using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        public EmployeesController(TimeKeeperContext context, ILogger<EmployeesController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Log.LogInformation($"Try to fetch all employees");
                return Ok(Unit.Employees.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Log.LogInformation($"Try to fetch employee with id {id}");
                Employee employee = Unit.Employees.Get(id);
                if (employee == null)
                {
                    Log.LogError($"Employee with id {id} cannot be found");
                    return NotFound();
                }
                else
                {
                    return Ok(employee.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                employee.Status = Unit.EmploymentStatuses.Get(employee.Status.Id);
                employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Insert(employee);
                Unit.Save();
                Log.LogInformation($"Employee {employee.FirstName} {employee.LastName} added with id {employee.Id}");
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                employee.Status = Unit.EmploymentStatuses.Get(employee.Status.Id);
                employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Update(employee, id);

                int numberOfChanges = Unit.Save();
                Log.LogInformation($"Attempt to update employee with id {id}");

                if (numberOfChanges == 0)
                {
                    Log.LogError($"Employee with id {id} cannot be found");
                    return NotFound();
                }
                Log.LogInformation($"Employee {employee.FirstName} {employee.LastName} with id {employee.Id} updated");
                return Ok(employee.Create());
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
                Unit.Employees.Delete(id);

                int numberOfChanges = Unit.Save();
                Log.LogInformation($"Attempt to delete employee with id {id}");

                if (numberOfChanges == 0)
                {
                    Log.LogError($"Employee with id {id} cannot be found");
                    return NotFound();
                }

                Log.LogInformation($"Employee with id {id} deleted");
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