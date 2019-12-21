using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeKeeper.BLL;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : BaseController
    {
        protected CalendarService calendarService;
        public MobileController(TimeKeeperContext context) :base(context)
        {
            calendarService = new CalendarService(Unit);
        }

        [HttpGet("customers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllCustomers()
        {
            try
            {
                return Ok(Unit.Customers.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("employees")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllEmployees()
        {
            try
            {
                return Ok(Unit.Employees.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("projects")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllProjects()
        {
            try
            {
                return Ok(Unit.Projects.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("roles")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllRoles()
        {
            try
            {
                return Ok(Unit.Roles.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("teams")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllTeams()
        {
            try
            {
                return Ok(Unit.Teams.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("calendar/{employeeId}/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetMonthCalendar(int employeeId, int year, int month)
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

        [HttpPut("employee/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] EmployeeMobileModel employee)
        {
            try
            {
                Employee emp = Unit.Employees.Get(employee.Id);
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.Phone = employee.Phone;
                emp.Email = employee.Email;
                emp.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Update(emp, id);
                Unit.Save();

                return Ok(emp.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}