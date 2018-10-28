using System;
using System.Collections.Generic;
using System.Linq;
using Claimini.Shared;

namespace Claimini.BlazorClient.ApplicationState
{
    public class AppState
    {
        public string Location { get; set; }

        public List<InvoiceFullDto> Invoices { get; set; }

        public InvoiceFullDto SelectedInvoice { get; set; }

        public List<Article> Articles { get; set; }

        public List<Customer> Customers { get; set; }

        public Customer SelectedCustomer { get; set; }

        public Customer GetCustomerById(int id)
        {
            Customer dto = this.Customers.FirstOrDefault(x => x.Id == id);
            if (dto == null || dto.Equals(default(Customer)))
            {
                // TODO: Load customer
                Console.WriteLine("Should load Customer here");
            }

            return dto;
        }

        public Article GetArticleById(int id)
        {
            Article dto = this.Articles.FirstOrDefault(x => x.Id == id);
            if (dto == null || dto.Equals(default(Article)))
            {
                // TODO: Load Article
                Console.WriteLine("Should load Article here");
            }

            return dto;
        }
    }
}
