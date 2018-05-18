using System.Threading.Tasks;
using BlazorRedux;
using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient.ApplicationState
{
    public class ActionCreators
    {
        public static async Task LoadCustomers(Dispatcher<IAction> dispatch, IApiClient apiClient)
        {
            CustomerDto[] customers = await apiClient.GetCustomers();
            dispatch(new Actions.ReceiveCustomersAction()
            {
                Customers = customers
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
