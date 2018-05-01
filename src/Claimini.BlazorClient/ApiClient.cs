using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Claimini.BlazorClient.Dto;
using Microsoft.AspNetCore.Blazor;

namespace Claimini.BlazorClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomerDto[]> GetCustomers()
        {
            var customers = await this.httpClient.GetJsonAsync<CustomerDto[]>("/api/customers");
            return customers;
        }

        public async Task<CustomerDto> PostCustomer(CustomerDto customer)
        {
            HttpContent content = new ObjectContent<CustomerDto>(customer, new JsonMediaTypeFormatter());
            HttpResponseMessage result = await this.httpClient.PostAsync("/api/customers", content);
            var returnedCustomer = await result.Content.ReadAsAsync<CustomerDto>();
            Console.WriteLine($"Customer saved with ID {returnedCustomer.Id}");
            return returnedCustomer;
        }
    }
}
