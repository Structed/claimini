using System.Collections.Generic;
using Claimini.Api.Data;
using Claimini.Shared;

namespace Claimini.Api.Services
{
    public interface ICustomerService
    {
        Customer Create(Customer customer);

        Customer Get(int id);

        IEnumerable<Customer> GetAll();

        bool Delete(int id);

        Customer Update(Customer customer);
    }
}
