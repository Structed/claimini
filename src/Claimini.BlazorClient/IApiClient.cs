using System.Threading.Tasks;
using Claimini.Shared;

namespace Claimini.BlazorClient
{
    public interface IApiClient
    {
        Task<Customer[]> GetCustomers();

        Task<Customer> GetCustomer(int id);

        Task<Customer> PostCustomer(Customer customer);

        Task<Customer> PutCustomer(Customer customer);
    }
}
