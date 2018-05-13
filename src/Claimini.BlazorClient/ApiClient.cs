using System.Net.Http;
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
            var returnedCustomer = await this.httpClient.PostJsonAsync<CustomerDto>("/api/customers", customer);
            return returnedCustomer;
        }
    }
}
