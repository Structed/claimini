using System;
using BlazorRedux;
using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient.ApplicationState
{
    public class Reducers
    {
        public static AppState RootReducer(AppState state, IAction action)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            return new AppState
            {
                Location = Location.Reducer(state.Location, action),
                Customers = CustomersReducer(state.Customers, action),
                SelectedCustomerIndex = SelectedCustomerIndexReducer(state.SelectedCustomerIndex, action)
            };
        }

        private static CustomerDto SelectedCustomerIndexReducer(CustomerDto stateSelectedCustomerIndex, IAction action)
        {
            switch (action)
            {
                case Actions.SelectCustomerReducer selectCustomerReducer:
                    return selectCustomerReducer.SelectedCustomer;
                default:
                    return stateSelectedCustomerIndex;
            }
        }

        public static CustomerDto[] CustomersReducer(CustomerDto[] stateCustomers, IAction action)
        {
            switch (action)
            {
                case Actions.ReceiveCustomersAction customersAction:
                    return customersAction.Customers;
                default:
                    return stateCustomers;
            }
        }
    }
}
