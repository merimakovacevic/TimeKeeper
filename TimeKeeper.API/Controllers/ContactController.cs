using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.LOG;
using TimeKeeper.Mail.Services;
using TimeKeeper.BLL;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase //will not inherit from BaseController, the mails won't be saved in the database
    {
        public LoggerService Logger = new LoggerService();

        [HttpPost]
        public IActionResult PostEmail([FromBody] MailModel mail)
        {
            try
            {
                string mailTo = mail.Email; 
                string subject = $"Contact request from {mail.Name}";
                string body = $"{mail.Message}, {mail.Phone}";
                MailService.Send(mailTo, subject, body);
                return Ok();
            }
            catch(Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}