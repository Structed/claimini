using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
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

        public void SetAuthorizationHeader(string token)
        {
            Guard.Against.NullOrWhiteSpace(token, nameof(token));
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task RegisterUser(TokenViewModel viewModel)
        {
            await this.httpClient.PostJsonAsync("/api/token/registration", viewModel);
        }

        public async Task<string> Login(TokenViewModel viewModel)
        {
            var tokenResponse = await this.httpClient.PostJsonAsync<TokenResponse>("/api/token/login", viewModel);
            this.SetAuthorizationHeader(tokenResponse.BearerToken);
            return tokenResponse.BearerToken;
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

        public async Task<Article> PostArticle(Article article)
        {
            return await this.httpClient.PostJsonAsync<Article>(ArticlesApiUri, article);
        }

        public async Task<Article> PutArticle(Article article)
        {
            return await this.httpClient.PutJsonAsync<Article>($"{ArticlesApiUri}/{article.Id}", article);
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

        public async Task<byte[]> GetInvoicePdf(string id)
        {
            var bytes = await this.httpClient.GetByteArrayAsync($"{InvoicesApiUri}/{id}/pdf");
            return bytes;
        }
        
        public async Task<string> GetInvoicePdfBas64(string id)
        {
            var bytes = await this.GetInvoicePdf(id);
            return Convert.ToBase64String(bytes);
        }

        public async Task<List<Article>> GetArticles()
        {
            return await this.httpClient.GetJsonAsync<List<Article>>($"{ArticlesApiUri}");
        }

        public async Task<Article> GetArticle(int id)
        {
            return await this.httpClient.GetJsonAsync<Article>($"{ArticlesApiUri}/{id}");
        }

        public async Task<InvoiceFullDto> PostInvoice(InvoiceDto invoice)
        {
            var invoiceFullDto = await this.httpClient.PostJsonAsync<InvoiceFullDto>(InvoicesApiUri, invoice);
            return invoiceFullDto;
        }
    }
}
