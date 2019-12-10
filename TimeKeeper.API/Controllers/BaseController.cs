using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.LOG;

namespace TimeKeeper.API.Controllers
{
    //[Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly UnitOfWork Unit;
        public readonly LoggerService Logger = new LoggerService();
        protected AccessHandler Access;
        
        public BaseController(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
            Access = new AccessHandler(Unit);
        }

        [NonAction]
        public IActionResult HandleException(Exception exception)
        {
            
            if (exception is ArgumentException)
            {
                Logger.Error(exception.Message);
                return NotFound(exception.Message);
            }                
            else
            {
                Logger.Fatal(exception);
                return BadRequest(exception);
            }            
        }

        [NonAction]
        public string GetUserClaim(string claimType)
        {
            return User.Claims.FirstOrDefault(x => x.Type == claimType).Value.ToString();
        }
    }
}
