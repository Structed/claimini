using System.Threading.Tasks;
using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient
{
    public interface IApiClient
    {
        Task<Dto.Customer[]> GetCustomers();
    }
}