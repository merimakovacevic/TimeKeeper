using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.BLL.ReportServices;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
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

        /// <summary>
        /// This methods return team time tracking for an employee
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("team-time-tracking/{teamId}/{year}/{month}")]
        public IActionResult GetTimeTracking(int teamId, int year, int month)
        {
            try
            {
                if (!resourceAccess.CanGetTeamReports(GetUserClaims(), teamId)) return Unauthorized();
                return Ok(timeTracking.GetTeamMonthReport(teamId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns monthly overview
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrLeader")]
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

        /// <summary>
        /// This method returns project history
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrLeader")]
        [HttpGet("project-history/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetProjectHistory(int projectId)
        {
            try
            {
                return Ok(projectHistory.GetProjectHistoryModel(projectId));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns project history 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrLeader")]
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

        /// <summary>
        /// This method returns annual overview
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrLeader")]
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

        /// <summary>
        /// This method returns annual overview for an employee
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrLeader")]
        [HttpGet("annual-overview-stored/{year}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetStoredAnnual(int year)
        {
            try
            {
                return Ok(annualOverview.GetStored(year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns monthly overview report using stored procedure
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrLeader")]
        [HttpGet("monthly-overview-stored/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetStoredMonthly(int year, int month)
        {
            try
            {
                return Ok(monthlyOverview.GetStored(year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns project history report using stored procedure
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOrLeader")]
        [HttpGet("project-history-stored/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetStoredProjectHistory(int projectId)
        {
            try
            {
                return Ok(projectHistory.GetStored(projectId));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}