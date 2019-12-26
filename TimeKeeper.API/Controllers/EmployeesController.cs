using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using Newtonsoft.Json;
using TimeKeeper.BLL;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        private PaginationService<Employee> _pagination;

        public EmployeesController(TimeKeeperContext context) : base(context)
        {
            _pagination = new PaginationService<Employee>();
        }

        /// <summary>
        /// This method returns all employees from a selected page, given the page size
        /// </summary>
        /// <returns>All employees from a page</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 100)
        {
            try
            {
                Logger.Info($"Try to fetch ${pageSize} employees from page ${page}");
                var task = await Unit.Employees.GetAsync();
                var query = task.ToList();

                Tuple<PaginationModel, List<Employee>> employeesPagination = _pagination.CreatePagination(page, pageSize, query);

                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(employeesPagination.Item1));
                return Ok(employeesPagination.Item2.ToList().Select(x => x.Create()).ToList());

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                //var query = await Unit.Employees.GetAsync();
                //return Ok(query.Select(x => x.Create()).ToList());
                DateTime start = DateTime.Now;
                var query = await Unit.Employees.GetAsync();
                var result = query.Select(x => x.Create()).ToList();
                DateTime final = DateTime.Now;
                return Ok(new { dif = final - start, result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Logger.Info($"Try to fetch employee with id {id}");
                Employee employee = await Unit.Employees.GetAsync(id);
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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            try
            {
                await Unit.Employees.InsertAsync(employee);
                await Unit.SaveAsync();

                //User insertion is coupled to employee insertion
                User user = employee.CreateUser();

                await Unit.Users.InsertAsync(user);
                await Unit.SaveAsync();

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromBody] Employee employee)
        {
            try
            {
                Logger.Info($"Attempt to update employee with id {id}");
                if (!resourceAccess.CanWriteEmployee(GetUserClaims(), employee)) return Unauthorized();

                await Unit.Employees.UpdateAsync(employee, id);
                await Unit.SaveAsync();
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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete employee with id {id}");
                await Unit.Employees.DeleteAsync(id);
                await Unit.SaveAsync();

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