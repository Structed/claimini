using System.Threading.Tasks;
using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient
{
    public interface IApiClient
    {
        Task<CustomerDto[]> GetCustomers();

        Task<CustomerDto> GetCustomer(int id);

        Task<CustomerDto> PostCustomer(CustomerDto customer);
    }
}
