// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claimini.Api.Data;
using Claimini.Api.Data.Dto;
using Claimini.Api.Repository;
using MongoDB.Bson.IO;

namespace Claimini.Api.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> CreateInvoice(InvoiceDto invoice);
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
            Customer customer = this.customerRepository.Get(invoiceDto.CustomerId);
            IEnumerable<Article> articles = this.articleRepository.FindBy(article => invoiceDto.InvoiceItems.Select(item => item.ArticleId).ToList().Contains(article.Id)).ToList();


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
    }
}
