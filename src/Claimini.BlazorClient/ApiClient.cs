using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Claimini.Shared;
using Microsoft.AspNetCore.Blazor;

namespace Claimini.BlazorClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private static readonly string CustomerApiUri = "/api/customers";
        private static readonly string ArticlesApiUri = "/api/articles";
        private static readonly string InvoicesApiUri = "/api/invoices";

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customers = await this.httpClient.GetJsonAsync<List<Customer>>(CustomerApiUri);
            return customers;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await this.httpClient.GetJsonAsync<Customer>($"{CustomerApiUri}/{id}");
            return customer;
        }

        public async Task<Customer> PostCustomer(Customer customer)
        {
            var returnedCustomer = await this.httpClient.PostJsonAsync<Customer>(CustomerApiUri, customer);
            return returnedCustomer;
        }

        public async Task<Customer> PutCustomer(Customer customer)
        {
            var returnedCustomer = await this.httpClient.PutJsonAsync<Customer>($"{CustomerApiUri}/{customer.Id}", customer);
            return returnedCustomer;
        }

        public async Task<List<InvoiceFullDto>> GetInvoices()
        {
            List <InvoiceFullDto> invoices = await this.httpClient.GetJsonAsync<List<InvoiceFullDto>>(InvoicesApiUri);
            return invoices;
        }

        public async Task<InvoiceFullDto> GetInvoice(string id)
        {
            return await this.httpClient.GetJsonAsync<InvoiceFullDto>($"{InvoicesApiUri}/{id}");
        }

        public async Task<List<Article>> GetArticles()
        {
            return await this.httpClient.GetJsonAsync<List<Article>>($"{ArticlesApiUri}");
        }
    }
}
