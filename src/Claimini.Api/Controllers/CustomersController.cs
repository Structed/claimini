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
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Customer customer = this.customerService.Get(id);
            return new OkObjectResult(customer);
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (customer.Id != 0)
            {
                this.ModelState.AddModelError("Id", "Id must not be set");
            }

            if (this.ModelState.IsValid == false)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            customer = this.customerService.Create(customer);
            return new OkObjectResult(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (this.ModelState.IsValid == false)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            customer.Id = id;
            this.customerService.Update(customer);

            return new OkObjectResult(customer);
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
