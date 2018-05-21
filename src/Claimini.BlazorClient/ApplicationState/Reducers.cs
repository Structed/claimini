using System;
using System.Linq;
using BlazorRedux;
using Claimini.Shared;

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
                SelectedCustomer = SelectedCustomerReducer(state.SelectedCustomer, state.Customers, action),
            };
        }

        private static Customer SelectedCustomerReducer(Customer stateSelectedCustomer, Customer[] stateCustomers, IAction action)
        {
            switch (action)
            {
                case Actions.SelectCustomerAction selectCustomerReducer:
                    return stateCustomers.First(e => e.Id == selectCustomerReducer.CustomerId);
                default:
                    return stateSelectedCustomer;
            }
        }

        public static Customer[] CustomersReducer(Customer[] stateCustomers, IAction action)
        {
            switch (action)
            {
                case Actions.ReceiveCustomersAction customersAction:
                    return customersAction.Customers;
                case Actions.UpdateCustomerAction customersAction:
                    int index = stateCustomers.ToList().FindIndex(e => e.Id == customersAction.Customer.Id);
                    Customer[] clone = (Customer[])stateCustomers.Clone();
                    clone[index] = customersAction.Customer;
                    return clone;
                default:
                    return stateCustomers;
            }
        }
    }
}
