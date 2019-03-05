// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Claimini.Api.Repository;
using Claimini.Api.Services;
using Claimini.Shared;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Claimini.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Invoices")]
    [Authorize]
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

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("{id}/pdf")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPdf(string id)
        {
            var invoice = this.invoiceService.GetInvoice(id);
            var pdfRepository = new PdfRepository();

            var filePath = Path.GetTempFileName();

            // If this is a non-null, non-empty list, the PDFs will be loaded as background for each page
            var extraPdfPaths = new List<string>
            {
                // You can get great templates for setup here: http://www.r9005.de/wissen/vorlagen-briefbogen.php
                //@"./static/DIN_5008_Form_B.pdf",   // http://www.r9005.de/bilder/wissen/Vorlage-Briefbogen-Form-B.pdf
                @"./static/Vorlage-Briefbogen-Form-B.pdf"   // http://www.r9005.de/bilder/wissen/Vorlage-Briefbogen-Form-B.pdf
            };

            // If a background image is set to a non-empty string, it will be loaded as background for each page
            var backgroundImagePath = @"./static/TestBackground.png";

            var writer = new PdfWriter(filePath);
            //pdfRepository.WriteInvoicePdf(invoice, writer, extraPdfPaths, backgroundImagePath);
            pdfRepository.WriteInvoicePdf(invoice, writer, extraPdfPaths);

            var response = await CreatePdfFileContentResult(filePath);
            return response;
        }

        private async Task<FileContentResult> CreatePdfFileContentResult(string filePath)
        {
            byte[] contents = await System.IO.File.ReadAllBytesAsync(filePath);
            FileContentResult response = File(contents, "application/pdf");
            return response;
        }
    }
}
