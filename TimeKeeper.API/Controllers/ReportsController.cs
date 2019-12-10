using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.BLL.ReportServices;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : BaseController
    {

        protected MonthlyOverview monthlyOverview;
        protected AnnualOverview annualOverview;
        protected ProjectHistory projectHistory;
        protected TimeTracking timeTracking;
        public ReportsController(TimeKeeperContext context) : base(context)
        {
            monthlyOverview = new MonthlyOverview(Unit);
            annualOverview = new AnnualOverview(Unit);
            projectHistory = new ProjectHistory(Unit);
            timeTracking = new TimeTracking(Unit);
        }

        [HttpGet("team-time-tracking/{teamId}/{year}/{month}")]
        public IActionResult GetTimeTracking(int teamId, int year, int month)
        {
            try
            {
                return Ok(timeTracking.GetTeamMonthReport(teamId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("monthly-overview/{year}/{month}")]
        public IActionResult GetMonthlyOverview(int year, int month)
        {
            try
            {
                return Ok(monthlyOverview.GetMonthlyOverview(year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("project-history/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetProjectHistory(int projectId)
        {
            try
            {
                Logger.Info($"Try to get project history for project with id:{projectId}");
                return Ok(projectHistory.GetProjectHistoryModel(projectId));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("project-history/{projectId}/{employeeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetMonthlyProjectHistory(int projectId, int employeeId)
        {
            try
            {
                Logger.Info($"Try to get project monthly project history for project with id:{projectId} and employee with id:{employeeId}");
                return Ok(projectHistory.GetMonthlyProjectHistory(projectId, employeeId));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("annual-overview/{year}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AnnualProjectOverview(int year)
        {
            try
            {
                return Ok(annualOverview.GetAnnualOverview(year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("annual-overview-stored/{year}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetStoredAnnual(int year)
        {
            try
            {
                DateTime start = DateTime.Now;
                var ar = annualOverview.GetStored(year);
                DateTime final = DateTime.Now;
                return Ok(new {dif=final-start, ar });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("monthly-overview-stored/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetStoredMonthly(int year, int month)
        {
            try
            {
                DateTime start = DateTime.Now;
                var ar = monthlyOverview.GetStored(year, month);
                DateTime final = DateTime.Now;
                return Ok(new { dif = final - start, ar });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}