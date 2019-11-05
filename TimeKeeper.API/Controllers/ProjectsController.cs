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
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        public ProjectsController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method returns all projects
        /// </summary>
        /// <returns>All projects</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Logger.Info($"Try to fetch all projects");
                return Ok(Unit.Projects.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method returns project with specified id
        /// </summary>
        /// <param name="id">Id of project</param>
        /// <returns>project with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            try
            {
                Project project = Unit.Projects.Get(id);
                Logger.Info($"Try to fetch project with id {id}");

                if (project == null)
                {
                    Logger.Error($"Project with id {id} cannot be found");
                    return NotFound();
                }
                else
                {
                    return Ok(project.Create());
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method inserts a new project
        /// </summary>
        /// <param name="project">New project that will be inserted</param>
        /// <returns>Model of inserted project</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Project project)
        {
            try
            {
                project.Team = Unit.Teams.Get(project.Team.Id);
                project.Customer = Unit.Customers.Get(project.Customer.Id);
                project.Status = Unit.ProjectStatuses.Get(project.Status.Id);
                project.Pricing = Unit.PricingStatuses.Get(project.Pricing.Id);

                Unit.Projects.Insert(project);
                Unit.Save();

                Logger.Info($"Project {project.Name} added with id {project.Id}");
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method updates data for project with specified id
        /// </summary>
        /// <param name="id">Id of project that will be updated</param>
        /// <param name="project">Data that comes from frontend</param>
        /// <returns>project with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Project project)
        {
            try
            {
                project.Team = Unit.Teams.Get(project.Team.Id);
                project.Customer = Unit.Customers.Get(project.Customer.Id);
                project.Status = Unit.ProjectStatuses.Get(project.Status.Id);
                project.Pricing = Unit.PricingStatuses.Get(project.Pricing.Id);

                Unit.Projects.Update(project, id);

                int numberOfChanges = Unit.Save();
                Logger.Info($"Attempt to update project with id {id}");

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Project with id {id} cannot be found");
                    return NotFound();
                }

                Logger.Info($"Project {project.Name} with id {project.Id} updated");
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method deletes project with specified id
        /// </summary>
        /// <param name="id">Id of project that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Projects.Delete(id);

                int numberOfChanges = Unit.Save();
                Logger.Info($"Attempt to delete project with id {id}");

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Project with id {id} cannot be found");
                    return NotFound();
                }

                Logger.Info($"Project with id {id} deleted");
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex);
            }
        }
    }
}