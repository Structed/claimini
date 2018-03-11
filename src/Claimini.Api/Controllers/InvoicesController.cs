// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Claimini.Api.Data;
using Claimini.Api.Data.Dto;
using Claimini.Api.Repository;
using Claimini.Api.Services;
using iText.Kernel.Pdf;
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
        public async Task<IEnumerable<Invoice>> Get()
        {
            return await this.invoiceService.GetAllAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            Invoice invoice = this.invoiceService.GetInvoice(id);
            return new ObjectResult(invoice);
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GetPdf(string id)
        {
            var invoice = this.invoiceService.GetInvoice(id);
            var pdfRepo = new PdfRepository();

            var filePath = Path.GetTempFileName();

            var writer = new PdfWriter(filePath);
            pdfRepo.WriteInvoicePdf(invoice, writer);

            byte[] contents = await System.IO.File.ReadAllBytesAsync(filePath);
            FileContentResult response = File(contents, "application/pdf");
            return response;
        }
    }
}
