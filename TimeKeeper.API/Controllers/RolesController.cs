using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        public RolesController(TimeKeeperContext context) : base(context) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Roles.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Role role = Unit.Roles.Get(id);
                if (role == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(role.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}