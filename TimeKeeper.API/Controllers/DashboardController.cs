using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.BLL;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        protected DashboardService dashboardService;
        public DashboardController(TimeKeeperContext context) : base(context)
        {
            dashboardService = new DashboardService(Unit);
        }

        [HttpGet("personal-dashboard/{employeeId}/{year}/{month}")]
        public IActionResult GetPersonalMonthDashboard(int employeeId, int year, int month)
        {
            try
            {
                return Ok(dashboardService.GetEmployeeDashboard(employeeId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("personal-dashboard/{employeeId}/{year}")]
        public IActionResult GetPersonalYearDashboard(int employeeId, int year)
        {
            try
            {
                return Ok(dashboardService.GetEmployeeDashboard(employeeId, year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("team-dashboard/{teamId}/{year}/{month}")]
        public IActionResult GetTeamDashboard(int teamId, int year, int month)
        {
            try
            {
                return Ok(dashboardService.GetTeamDashboard(teamId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("admin-dashboard/{year}/{month}")]
        public IActionResult GetAdminDashboard(int year, int month)
        {
            try
            {
                return Ok(dashboardService.GetAdminDashboardInfo(year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}