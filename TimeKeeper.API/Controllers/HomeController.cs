using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeKeeper.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("Welcome to the world of time keepers!");
        }
        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return Ok("It seems you don't have access to this page!");
        }
    }
}