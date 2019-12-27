using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(TimeKeeperContext context) : base(context)
        {
        }

        //[HttpGet("password")]
        //public IActionResult GetUsersAndUpdate()
        //{
        //    var query = Unit.Users.Get();
        //    foreach (User user in query)
        //    {
        //        user.Password = user.Username.HashWith(user.Password);
        //        Unit.Context.Entry(user).CurrentValues.SetValues(user);
        //    }
        //    Unit.Save();
        //    return Ok();
        //}

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var query = Unit.Users.Get();
                return Ok(query.Select(x => x.Create()).ToList().OrderBy(x => x.Name));
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
                User user = Unit.Users.Get(id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                Unit.Users.Insert(user);
                Unit.Save();
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User user, int id)
        {
            try
            {
                Unit.Users.Update(user, id);
                Unit.Save();
                return Ok(user.Create());
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
                Unit.Users.Delete(id);
                Unit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                User user = Access.Check(model.Username, model.Password);
                if (user != null)
                {
                    string token = Access.GetToken(user);
                    return Ok(new { user = user.Create(), token });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}