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
        public IActionResult GetTeams() => Ok(Unit.Teams.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All roles (master model)</returns>
        [HttpGet("roles")]
        public IActionResult GetRoles() => Ok(Unit.Roles.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All customers (master model)</returns>
        [HttpGet("customers")]
        [ProducesResponseType(200)]
        public IActionResult GetCustomers() => Ok(Unit.Customers.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All projects (master model)</returns>
        [HttpGet("projects")]
        [ProducesResponseType(200)]
        public IActionResult GetProjects() => Ok(Unit.Projects.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All employees (master model)</returns>
        [HttpGet("employees")]
        [ProducesResponseType(200)]
        public IActionResult GetEmployees() => Ok(Unit.Employees.Get().ToList().Select(x => x.Master()).ToList()); //Added aditional ToList() before the Select method, to solve Detached objects lazy loading Warning

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All employee positions (master model)</returns>
        [HttpGet("employee-positions")]
        [ProducesResponseType(200)]
        public IActionResult GetEmployeePositions() => Ok(Unit.EmployeePositions.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All employment statuses (master model)</returns>
        [HttpGet("employment-statuses")]
        [ProducesResponseType(200)]
        public IActionResult GetEmploymentStatuses() => Ok(Unit.EmploymentStatuses.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All customer statuses (master model)</returns>
        [HttpGet("customer-statuses")]
        [ProducesResponseType(200)]
        public IActionResult GetCustomerStatuses() => Ok(Unit.CustomerStatuses.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All pricing statuses (master model)</returns>
        [HttpGet("pricing-statuses")]
        public IActionResult GetPricingStatuses() => Ok(Unit.PricingStatuses.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All project statuses (master model)</returns>
        [HttpGet("project-statuses")]
        [ProducesResponseType(200)]
        public IActionResult GetProjectStatuses() => Ok(Unit.ProjectStatuses.Get().Select(x => x.Master()).ToList());

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All member statuses (master model)</returns>
        [HttpGet("member-statuses")]
        [ProducesResponseType(200)]
        public IActionResult GetMemberStatuses() => Ok(Unit.MemberStatuses.Get().Select(x => x.Master()).ToList());     
    }
}