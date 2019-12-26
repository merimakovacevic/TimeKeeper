using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DAL;
using TimeKeeper.BLL;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : BaseController
    {
        public MasterController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All teams (master model)</returns>
        [HttpGet("teams")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTeams()
        {
            var query = await resourceAccess.GetAuthorizedTeams(GetUserClaims());
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All roles (master model)</returns>
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var query = await Unit.Roles.GetAsync();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All customers (master model)</returns>
        [Authorize(Policy = "AdminOrLeader")]
        [HttpGet("customers")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCustomers()
        {
            var query = await resourceAccess.GetAuthorizedCustomers(GetUserClaims());
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All projects (master model)</returns>
        [HttpGet("projects")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProjects()
        {
            var query = await resourceAccess.GetAuthorizedProjectsMaster(GetUserClaims());
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All employees (master model)</returns>
        [HttpGet("employees")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetEmployees()
        {
            var query = await Unit.Employees.GetAsync();
            return Ok(query.ToList().Select(x => x.Master()).ToList());
            //Added aditional ToList() before the Select method, to solve Detached objects lazy loading Warning
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All employee positions (master model)</returns>
        [HttpGet("employee-positions")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetEmployeePositions()
        {
            var query = await Unit.EmployeePositions.GetAsync();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All employment statuses (master model)</returns>
        [HttpGet("employment-statuses")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetEmploymentStatuses()
        {
            var query = await Unit.EmploymentStatuses.GetAsync();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All customer statuses (master model)</returns>
        [HttpGet("customer-statuses")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCustomerStatuses()
        {
            var query = await Unit.CustomerStatuses.GetAsync();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All pricing statuses (master model)</returns>
        [HttpGet("pricing-statuses")]
        public async Task<IActionResult> GetPricingStatuses()
        {
            var query = await Unit.PricingStatuses.GetAsync();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All project statuses (master model)</returns>
        [HttpGet("project-statuses")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProjectStatuses()
        {
            var query = await Unit.ProjectStatuses.GetAsync();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All member statuses (master model)</returns>
        [HttpGet("member-statuses")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetMemberStatuses()
        {
            var query = await Unit.MemberStatuses.GetAsync();
            return Ok(query.Select(x => x.Master()).ToList());
        }
    }
}