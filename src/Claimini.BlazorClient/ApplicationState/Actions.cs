using BlazorRedux;
using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient.ApplicationState
{
    public class Actions
    {
        public class ReceiveCustomersAction : IAction
        {
            public CustomerDto[] Customers { get; set; }
        }

        public class UpdateCustomerAction : IAction
        {
            public CustomerDto Customer { get; set; }
        }

        public class SelectCustomerAction : IAction
        {
            public int CustomerId;
        }
    }
}