using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : BaseController
    {
        public MasterController(TimeKeeperContext context, ILogger<MasterController> log) : base(context, log) { }

        [HttpGet("teams")]
        public IActionResult GetTeams() => Ok(Unit.Teams.Get().Select(x => x.Master()).ToList());

        [HttpGet("roles")]
        public IActionResult GetRoles() => Ok(Unit.Roles.Get().Select(x => x.Master()).ToList());

        [HttpGet("customers")]
        public IActionResult GetCustomers() => Ok(Unit.Customers.Get().Select(x => x.Master()).ToList());

        [HttpGet("projects")]
        public IActionResult GetProjects() => Ok(Unit.Projects.Get().Select(x => x.Master()).ToList());

        [HttpGet("employees")]
        public IActionResult GetEmployees() => Ok(Unit.Employees.Get().Select(x => x.Master()).ToList());

        [HttpGet("employee-positions")]
        public IActionResult GetEmployeePositions() => Ok(Unit.EmployeePositions.Get().Select(x => x.Master()).ToList());

        [HttpGet("employment-statuses")]
        public IActionResult GetEmploymentStatuses() => Ok(Unit.EmploymentStatuses.Get().Select(x => x.Master()).ToList());

        [HttpGet("customer-statuses")]
        public IActionResult GetCustomerStatuses() => Ok(Unit.CustomerStatuses.Get().Select(x => x.Master()).ToList());

        [HttpGet("pricing-statuses")]
        public IActionResult GetPricingStatuses() => Ok(Unit.PricingStatuses.Get().Select(x => x.Master()).ToList());

        [HttpGet("project-statuses")]
        public IActionResult GetProjectStatuses() => Ok(Unit.ProjectStatuses.Get().Select(x => x.Master()).ToList());

        [HttpGet("member-statuses")]
        public IActionResult GetMemberStatuses() => Ok(Unit.MemberStatuses.Get().Select(x => x.Master()).ToList());
    }
}