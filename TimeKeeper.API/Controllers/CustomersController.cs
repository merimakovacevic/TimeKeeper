using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.BLL;
using TimeKeeper.DTO;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        private PaginationService<Customer> _pagination;
        public CustomersController(TimeKeeperContext context) : base(context)
        {
            _pagination = new PaginationService<Customer>();
        }

        /// <summary>
        /// This method returns all customers from a selected page, given the page size
        /// </summary>
        /// <returns>All customers from a page</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "AdminOrLeader")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 100)
        {
            try
            {
                Logger.Info($"Try to fetch ${pageSize} customers from page ${page}");

                Tuple<PaginationModel, List<Customer>> customersPagination;
                
                List<Customer> query;
                query = await resourceAccess.GetAuthorizedCustomers(GetUserClaims());

                customersPagination = _pagination.CreatePagination(page, pageSize, query);
                HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(customersPagination.Item1));
                return Ok(customersPagination.Item2.Select(x => x.Create()).ToList());
                //return Ok(Unit.Customers.Get().OrderBy(x => x.Name).ToList().Select(x => x.Create()).ToList());
            }
            catch(Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Logger.Info($"Try to fetch customer with id {id}");
                Customer customer = await Unit.Customers.GetAsync(id);

                return Ok(customer.Create());                
            }
            catch(Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            try
            {
                await Unit.Customers.InsertAsync(customer);
                await Unit.SaveAsync();
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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            try
            {
                Logger.Info($"Attempt to update customer with id {id}");
                await Unit.Customers.UpdateAsync(customer, id);
                await Unit.SaveAsync();

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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Logger.Info($"Attempt to delete customer with id {id}");
                await Unit.Customers.DeleteAsync(id);
                await Unit.SaveAsync();

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