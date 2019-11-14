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
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(TimeKeeperContext context) : base(context) { }

        [HttpGet]
        [Route("api/users")]
        public IActionResult Get()
        {
            try
            {
                var currentUser = HttpContext.User as ClaimsPrincipal;
                List<string> claims = new List<string>();

                foreach (Claim claim in currentUser.Claims)
                {
                    claims.Add(claim.Value);
                }

                var users = Unit.Users.Get().ToList();
                return Ok(new { claims, users });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }  
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                User control = Unit.Users.Get(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();

                if (control == null) return NotFound();
                byte[] bytes = Encoding.ASCII.GetBytes($"{control.Username}:{control.Password}");
                string base64 = Convert.ToBase64String(bytes);
                return Ok(new
                {
                    control.Id,
                    control.Name,
                    control.Role,
                    base64
                });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [AllowAnonymous]
        [Route("api/logout")]
        [HttpGet]
        public async Task Logout()//asynchronous method that doesn't return a value. Task<IActionResult> returns a value
        {
            if(User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync("Cookies");
                await HttpContext.SignOutAsync("oidc");
            }
        }

    }
}