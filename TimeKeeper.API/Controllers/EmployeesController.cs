using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.Utility.Services;
using TimeKeeper.API.Services;
using Newtonsoft.Json;
using TimeKeeper.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TimeKeeper.API.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        PaginationService<Employee> Pagination;
        public EmployeesController(TimeKeeperContext context) : base(context) {
            Pagination = new PaginationService<Employee>();
        }

        /// <summary>
        /// This method returns all employees
        /// </summary>
        /// <returns>All employees</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAll(int page = 1, int pageSize=10)
        {
            try
            {
                Logger.Info($"Try to fetch all employees");

                Tuple < PaginationModel, List<Employee>> employeesPagination = Pagination.CreatePagination(page, pageSize, Unit.Employees.Get() as DbSet<Employee>);
                
                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(employeesPagination.Item1));
                return Ok(employeesPagination.Item2.ToList().Select(x => x.Create()).ToList());
                    
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns employee with specified id
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>employee with specified id</returns>
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
                Logger.Info($"Try to fetch employee with id {id}");
                Employee employee = Unit.Employees.Get(id);
                 return Ok(employee.Create());                
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method inserts a new employee
        /// </summary>
        /// <param name="employee">New employee that will be inserted</param>
        /// <returns>Model of inserted employee</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                //employee.Status = Unit.EmploymentStatuses.Get(employee.Status.Id);
                //employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Insert(employee);
                Unit.Save();

                //User insertion is coupled to employee insertion
                User user = employee.CreateUser();

                Unit.Users.Insert(user);
                Unit.Save();

                Logger.Info($"Employee {employee.FirstName} {employee.LastName} added with id {employee.Id}");
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method updates data for employee with specified id
        /// </summary>
        /// <param name="id">Id of employee that will be updated</param>
        /// <param name="employee">Data that comes from frontend</param>
        /// <returns>employee with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "IsEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                //employee.Status = Unit.EmploymentStatuses.Get(employee.Status.Id);
                //employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Logger.Info($"Attempt to update employee with id {id}");
                Unit.Employees.Update(employee, id);
                Unit.Save();

                /*int numberOfChanges = Unit.Save();
                Logger.Info($"Attempt to update employee with id {id}");

                if (numberOfChanges == 0)
                {
                   Logger.Error($"Employee with id {id} cannot be found");
                    return NotFound();
                }*/
                Logger.Info($"Employee {employee.FirstName} {employee.LastName} with id {employee.Id} updated");
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method deletes employee with specified id
        /// </summary>
        /// <param name="id">Id of employee that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete employee with id {id}");
                Unit.Employees.Delete(id);
                Unit.Save();

                /*int numberOfChanges = Unit.Save();
                Logger.Info($"Attempt to delete employee with id {id}");

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Employee with id {id} cannot be found");
                    return NotFound();
                }*/

                Logger.Info($"Employee with id {id} deleted");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}