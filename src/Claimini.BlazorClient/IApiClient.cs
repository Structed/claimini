using System.Threading.Tasks;
using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient
{
    public interface IApiClient
    {
        Task<CustomerDto[]> GetCustomers();

        Task<CustomerDto> PostCustomer(CustomerDto customer);
    }
}
