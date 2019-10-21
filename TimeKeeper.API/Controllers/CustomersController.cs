using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        public CustomersController(TimeKeeperContext context, ILogger<CustomersController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Customers.Get().OrderBy(x => x.Name).ToList().Select(x => x.Create()).ToList());
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
                Customer customer = Unit.Customers.Get(id);
                if(customer == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(customer.Create());
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                customer.Status = Unit.CustomerStatuses.Get(customer.Status.Id);
                Unit.Customers.Insert(customer);
                Unit.Save();
                return Ok(customer.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                customer.Status = Unit.CustomerStatuses.Get(customer.Status.Id);
                Unit.Customers.Update(customer, id);
                int numberOfChanges = Unit.Save();

                if(numberOfChanges == 0)
                {
                    return NotFound();
                }

                return Ok(customer.Create());
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
                Unit.Customers.Delete(id);
                int numberOfChanges = Unit.Save();

                if(numberOfChanges == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}