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
    [Authorize(AuthenticationSchemes = "TokenAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : BaseController
    {
        public MobileController(TimeKeeperContext context) :base(context)
        {
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
    }
}