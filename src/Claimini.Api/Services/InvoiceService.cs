// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claimini.Api.Repository;
using Claimini.Shared;

namespace Claimini.Api.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> CreateInvoice(InvoiceDto invoice);

        Invoice GetInvoice(string id);

        Task<IEnumerable<Invoice>> GetAllAsync();
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IMongoRepository<Invoice> invoiceRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Article> articleRepository;

        public InvoiceService(IMongoRepository<Invoice> invoiceRepository, IRepository<Customer> customerRepository, IRepository<Article> articleRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.customerRepository = customerRepository;
            this.articleRepository = articleRepository;
        }

        public async Task<Invoice> CreateInvoice(InvoiceDto invoiceDto)
        {
            if (invoiceDto.CustomerId < 1)
            {
                throw new Exception("A Customer was not specified");
            }

            Customer customer = this.customerRepository.Get(invoiceDto.CustomerId);
            if (customer == null)
            {
                throw new Exception($"Customer with ID {invoiceDto.CustomerId} not found");
            }

            var articleIds = invoiceDto.InvoiceItems.Select(item => item.ArticleId).ToList();
            if (articleIds.Count < 1)
            {
                throw new Exception("Cannot create an Invoice with less than 1 article");
            }

            IEnumerable<Article> articles = this.articleRepository.FindBy(article => articleIds.Contains(article.Id)).ToList();


            List<InvoiceItem> invoiceItems = new List<InvoiceItem>(articles.Count());
            foreach (var article in articles)
            {
                InvoiceItemDto invoiceItemDto = invoiceDto.InvoiceItems.FirstOrDefault(dto => dto.ArticleId == article.Id);
                int quantity = invoiceItemDto?.Quantity ?? 0;

                InvoiceItem invoiceItem = new InvoiceItem
                {
                    Article = article,
                    Price = article.Price,
                    Quantity = quantity
                };
                invoiceItems.Add(invoiceItem);
            }

            Invoice invoice = new Invoice
            {
                Customer = customer,
                CreatedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Items = invoiceItems
            };

            await invoiceRepository.InsertAsync(invoice);
            return invoice;
        }

        public Invoice GetInvoice(string id)
        {
            return this.invoiceRepository.Get(id);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await this.invoiceRepository.GetAllAsync();
        }
    }
}
