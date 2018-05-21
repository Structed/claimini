using BlazorRedux;
using Claimini.Shared;

namespace Claimini.BlazorClient.ApplicationState
{
    public class Actions
    {
        public class ReceiveCustomersAction : IAction
        {
            public Customer[] Customers { get; set; }
        }

        public class UpdateCustomerAction : IAction
        {
            public Customer Customer { get; set; }
        }

        public class SelectCustomerAction : IAction
        {
            public int CustomerId;
        }
    }
}
