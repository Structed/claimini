using System;
using System.Collections.Generic;
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
                BearerToken = BearerTokenReducer(state.BearerToken, action),
                Customers = CustomersReducer(state.Customers, action),
                SelectedCustomer = SelectedCustomerReducer(state.SelectedCustomer, state.Customers, action),
                Invoices = InvoicesReducer(state.Invoices, action),
                SelectedInvoice = SelectedInvoiceReducer(state.SelectedInvoice, action),
                Articles = ArticlesReducer(state.Articles, action),
            };
        }

        private static string BearerTokenReducer(string bearerToken, IAction action)
        {
            switch (action)
            {
                case Actions.ReceiveBearerTokenAction receiveBearerTokenAction:
                    return receiveBearerTokenAction.BearerToken;
                default:
                    return bearerToken;
            }
        }

        private static List<Article> ArticlesReducer(List<Article> stateArticles, IAction action)
        {
            switch (action)
            {
                case Actions.ReceiveArticlesAction customersAction:
                    return customersAction.Articles;
                //case Actions.UpdateCustomerAction customersAction:
                //    int index = stateArticles.ToList().FindIndex(e => e.Id == customersAction.Customer.Id);
                //    List<Article> clone = stateArticles.ToList(); // Shallow copy!
                //    clone[index] = customersAction.Customer;
                //    return clone;
                default:
                    return stateArticles;
            }
        }

        private static Customer SelectedCustomerReducer(Customer stateSelectedCustomer, List<Customer> stateCustomers, IAction action)
        {
            switch (action)
            {
                case Actions.SelectCustomerAction selectCustomerReducer:
                    return stateCustomers.First(e => e.Id == selectCustomerReducer.CustomerId);
                default:
                    return stateSelectedCustomer;
            }
        }

        public static List<Customer> CustomersReducer(List<Customer> stateCustomers, IAction action)
        {
            switch (action)
            {
                case Actions.ReceiveCustomersAction customersAction:
                    return customersAction.Customers;
                case Actions.UpdateCustomerAction customersAction:
                    int index = stateCustomers.ToList().FindIndex(e => e.Id == customersAction.Customer.Id);
                    List<Customer> clone = stateCustomers.ToList(); // Shallow copy!
                    clone[index] = customersAction.Customer;
                    return clone;
                default:
                    return stateCustomers;
            }
        }

        public static InvoiceFullDto SelectedInvoiceReducer(InvoiceFullDto stateInvoice, IAction action)
        {
            switch (action)
            {
                case Actions.SelectInvoiceAction invoiceAction:
                    return invoiceAction.SelectedInvoice;
                default:
                    return stateInvoice;
            }
        }
        
        public static List<InvoiceFullDto> InvoicesReducer(List<InvoiceFullDto> stateInvoices, IAction action)
        {
            switch (action)
            {
                case Actions.ReceiveInvoicesAction invoicesAction:
                    return invoicesAction.Invoices;
                //case Actions.UpdateCustomerAction customersAction:
                //    int index = stateInvoices.ToList().FindIndex(e => e.Id == customersAction.Customer.Id);
                //    List<Customer> clone = (List<Customer>)stateInvoices.Clone();
                //    clone[index] = customersAction.Customer;
                //    return clone;
                default:
                    return stateInvoices;
            }
        }
    }
}
