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

namespace TimeKeeper.API.Controllers
{
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
     
        /// <summary>
        /// This method is used for login
        /// </summary>
        /// <returns status="200">OK</returns>
        /// <returns status="404">NotFound</returns>
        /// <returns status="400">BadRequest</returns>
        [HttpGet]
        [Route("login")]
        [Authorize]
        public IActionResult Login()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var accessToken = HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result;
                    var response = new
                    {
                        Id = User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString(),
                        Name = User.Claims.FirstOrDefault(c => c.Type == "given_name").Value.ToString(),
                        Role = User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString(),
                        accessToken //Bearer {accessToken}
                    };
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method is used for logout
        /// </summary>
        /// <returns></returns>
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