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
        public CustomersController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method returns all customers
        /// </summary>
        /// <returns>All customers</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Logger.Info($"Try to fetch all customers");
                return Ok(Unit.Customers.Get().OrderBy(x => x.Name).ToList().Select(x => x.Create()).ToList());
            }
            catch(Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// This method returns customer with specified id
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <returns>customer with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                Logger.Info($"Try to fetch customer with id {id}");
                Customer customer = Unit.Customers.Get(id);
                if(customer == null)
                {
                    Logger.Error($"Customer with id {id} cannot be found");
                    return NotFound();
                }
                else
                {
                    return Ok(customer.Create());
                }
            }
            catch(Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This method inserts a new customer
        /// </summary>
        /// <param name="customer">New customer that will be inserted</param>
        /// <returns>Model of inserted customer</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                customer.Status = Unit.CustomerStatuses.Get(customer.Status.Id);

                Unit.Customers.Insert(customer);
                Unit.Save();
                Logger.Info($"Customer {customer.Name} added with id {customer.Id}");
                return Ok(customer.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This method updates data for customer with specified id
        /// </summary>
        /// <param name="id">Id of customer that will be updated</param>
        /// <param name="customer">Data that comes from frontend</param>
        /// <returns>Customer with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                customer.Status = Unit.CustomerStatuses.Get(customer.Status.Id);
                Unit.Customers.Update(customer, id);
                int numberOfChanges = Unit.Save();
               Logger.Info($"Attempt to update customer with id {id}");

                if (numberOfChanges == 0)
                {
                    Logger.Error($"Customer with id {id} cannot be found");
                    return NotFound();
                }

                Logger.Info($"Customer {customer.Name} with id {customer.Id} updated");
                return Ok(customer.Create());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This method deletes customer with specified id
        /// </summary>
        /// <param name="id">Id of customer that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Customers.Delete(id);
                int numberOfChanges = Unit.Save();
                Logger.Info($"Attempt to delete customer with id {id}");
                if (numberOfChanges == 0)
                {
                    Logger.Error($"Customer with id {id} cannot be found");
                    return NotFound();
                }

                Logger.Info($"Customer with id {id} deleted");
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}