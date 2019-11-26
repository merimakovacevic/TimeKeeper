﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private PaginationService<Project> _pagination;
        public ProjectsController(TimeKeeperContext context) : base(context)
        {
            _pagination = new PaginationService<Project>();
        }

        /// <summary>
        /// This method returns all projects rom a selected page, given the page size
        /// </summary>
        /// <returns>All projects from a page</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAll(int page = 1, int pageSize = 5)
        {
            try
            {
                Logger.Info($"Try to get all Projects");
                Tuple<PaginationModel, List<Project>> projectsPagination;
                List<Project> query;

                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value.ToString());
                string role = User.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString();

                if (role == "admin" || role == "lead")
                {
                    query = Unit.Projects.Get().ToList();
                }
                else
                {
                    query = Unit.Projects.Get(x => x.Team.Members.Any(y => y.Employee.Id == userId)).ToList();                    
                }

                projectsPagination = _pagination.CreatePagination(page, pageSize, query);
                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(projectsPagination.Item1));
                return Ok(projectsPagination.Item2.Select(x => x.Create()).ToList());

            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsMemberOnProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            try
            {
                Logger.Info($"Try to fetch project with id {id}");
                Project project = Unit.Projects.Get(id);                

                return Ok(project.Create());                
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Project project)
        {
            try
            {
                Unit.Projects.Insert(project);
                Unit.Save();

                Logger.Info($"Project {project.Name} added with id {project.Id}");
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Project project)
        {
            try
            {
                Logger.Info($"Attempt to update project with id {id}");
                Unit.Projects.Update(project, id);

                Unit.Save();

                Logger.Info($"Project {project.Name} with id {project.Id} updated");
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete project with id {id}");
                Unit.Projects.Delete(id);
                Unit.Save();

                Logger.Info($"Project with id {id} deleted");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
