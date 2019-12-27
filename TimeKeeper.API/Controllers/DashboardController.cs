using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.BLL;
using TimeKeeper.BLL.DashboardServices;
using TimeKeeper.DAL;
using TimeKeeper.DTO.ReportModels.CompanyDashboard;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        protected PersonalDashboard personalDashboard;
        protected TeamDashboard teamDashboard;
        protected CompanyDashboard companyDashboard;
        public DashboardController(TimeKeeperContext context) : base(context)
        {
            personalDashboard = new PersonalDashboard(Unit);
            teamDashboard = new TeamDashboard(Unit);
            companyDashboard = new CompanyDashboard(Unit);
        }

        [HttpGet("personal/{employeeId}/{year}/{month}")]
        public IActionResult GetPersonalMonthDashboard(int employeeId, int year, int month)
        {
            try
            {
                if (!resourceAccess.CanGetPersonalDashboard(GetUserClaims(), employeeId)) return Unauthorized();
                return Ok(personalDashboard.GetPersonalDashboardStored(employeeId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        //[HttpGet("personal/{employeeId}/{year}")]
        //public IActionResult GetPersonalYearDashboard(int employeeId, int year)
        //{
        //    try
        //    {
        //        return Ok(dashboardService.GetEmployeeDashboard(employeeId, year));
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleException(ex);
        //    }
        //}

        [Authorize(Policy = "AdminOrLeader")]
        [HttpGet("team/{teamId}/{year}/{month}")]
        public IActionResult GetTeamDashboard(int teamId, int year, int month)
        {
            try
            {
                if (!resourceAccess.CanGetTeamReports(GetUserClaims(), teamId)) return Unauthorized();
                return Ok(teamDashboard.GetTeamDashboardStored(teamId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("company/{year}/{month}")]
        public IActionResult GetCompanyDashboard(int year, int month)
        {
            try
            {
                DateTime start = DateTime.Now;
                CompanyDashboardModel dashboard = companyDashboard.GetCompanyDashboard(year, month);
                DateTime end = DateTime.Now;
                return Ok(new { (end - start).TotalMilliseconds, dashboard});
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}