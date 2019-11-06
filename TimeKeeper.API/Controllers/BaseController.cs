using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.DAL;
using TimeKeeper.LOG;

namespace TimeKeeper.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UnitOfWork Unit;
        public LoggerService Logger = new LoggerService();
        public BaseController(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }

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
    }
}