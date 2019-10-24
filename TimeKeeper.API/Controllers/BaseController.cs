using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UnitOfWork Unit;
        //protected static ILogger<BaseController> Log;
        public BaseController(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
            //Log = log;
        }
    }
}