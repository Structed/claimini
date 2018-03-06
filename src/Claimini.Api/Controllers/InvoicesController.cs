using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claimini.Api.Data;
using Claimini.Api.Data.Dto;
using Claimini.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Claimini.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Invoices")]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        // GET: api/Invoices
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Invoices/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InvoiceDto invoiceDto)
        {
            if (this.ModelState.IsValid)
            {
                Invoice invoice = await invoiceService.CreateInvoice(invoiceDto);
                return new ObjectResult(invoice);
            }

            return new ObjectResult(this.ModelState);
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
