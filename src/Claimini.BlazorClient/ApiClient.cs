using System.Net.Http;
using System.Threading.Tasks;
using Claimini.BlazorClient.Dto;
using Microsoft.AspNetCore.Blazor;

namespace Claimini.BlazorClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private static readonly string CustomerApiUri = "/api/customers";

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomerDto[]> GetCustomers()
        {
            var customers = await this.httpClient.GetJsonAsync<CustomerDto[]>(CustomerApiUri);
            return customers;
        }

        public async Task<CustomerDto> GetCustomer(int id)
        {
            var customer = await this.httpClient.GetJsonAsync<CustomerDto>($"{CustomerApiUri}/{id}");
            return customer;
        }

        public async Task<CustomerDto> PostCustomer(CustomerDto customer)
        {
            var returnedCustomer = await this.httpClient.PostJsonAsync<CustomerDto>(CustomerApiUri, customer);
            return returnedCustomer;
        }

        public async Task<CustomerDto> PutCustomer(CustomerDto customer)
        {
            var returnedCustomer = await this.httpClient.PutJsonAsync<CustomerDto>($"{CustomerApiUri}/{customer.Id}", customer);
            return returnedCustomer;
        }
    }
}
