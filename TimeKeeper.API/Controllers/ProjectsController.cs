using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        public ProjectsController(TimeKeeperContext context, ILogger<ProjectsController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Projects.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Project project = Unit.Projects.Get(id);
                if (project == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(project.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
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
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
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

                if(numberOfChanges == 0)
                {
                    return NotFound();
                }

                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Projects.Delete(id);

                int numberOfChanges = Unit.Save();

                if (numberOfChanges == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}