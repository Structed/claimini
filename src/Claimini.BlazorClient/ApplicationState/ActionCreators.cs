using System.Threading.Tasks;
using BlazorRedux;
using Claimini.Shared;

namespace Claimini.BlazorClient.ApplicationState
{
    public class ActionCreators
    {
        public static async Task LoadCustomers(Dispatcher<IAction> dispatch, IApiClient apiClient)
        {
            Customer[] customers = await apiClient.GetCustomers();
            dispatch(new Actions.ReceiveCustomersAction()
            {
                Customers = customers
            });
        }

        public static async Task UpdateCustomer(Dispatcher<IAction> dispatch, IApiClient apiClient, Customer customer)
        {
            customer = await apiClient.PutCustomer(customer);
            dispatch(new Actions.UpdateCustomerAction()
            {
                Customer = customer
            });
        }

        public static void SelectCustomer(Dispatcher<IAction> dispatch, int customerId)
        {
            dispatch(new Actions.SelectCustomerAction()
            {
                CustomerId = customerId
            });
        }
    }
}
