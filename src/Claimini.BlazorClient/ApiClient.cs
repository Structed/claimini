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

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Customer[]> GetCustomers()
        {
            var customers = await this.httpClient.GetJsonAsync<Customer[]>(CustomerApiUri);
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
    }
}
