using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<Dto.Customer[]> GetCustomers()
        {
            var customers = await this.httpClient.GetJsonAsync<Dto.Customer[]>("/api/customers");
            return customers;
        }
    }
}
