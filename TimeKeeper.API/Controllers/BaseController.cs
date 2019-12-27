using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Authorization;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.DTO;
using TimeKeeper.LOG;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly UnitOfWork Unit;
        public readonly LoggerService Logger = new LoggerService();
        protected AccessHandler Access;
        protected ResouceAccessHandler resourceAccess;

        public BaseController(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
            Access = new AccessHandler(Unit);
            resourceAccess = new ResouceAccessHandler(Unit);
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
                return BadRequest(exception.Message);
            }
        }

        [NonAction]
        public string GetUserClaim(string claimType)
        {
            return User.Claims.FirstOrDefault(x => x.Type == claimType).Value.ToString();
        }

        [NonAction]
        public UserRoleModel GetUserClaims()
        {
            return new UserRoleModel
            {
                UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value.ToString()),
                Role = User.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString()
            };     
        }
    }
}