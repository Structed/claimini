using System.Collections.Generic;
using Claimini.Api.Data;
using Claimini.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claimini.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Customer> customers = this.customerService.GetAll();
            return new OkObjectResult(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            Customer customer = this.customerService.Get(id);
            return new OkObjectResult(customer);
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            customer = this.customerService.Create(customer);
            return new OkObjectResult(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (this.customerService.Delete(id))
            {
                return new NoContentResult();
            }

            return new NotFoundObjectResult(id);
        }
    }
}
