using System;
using System.Collections.Generic;
using System.Linq;
using BlazorRedux;
using Claimini.Shared;
using Claimini.Shared.Extensions;
using MongoDB.Bson;

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
                Invoices = InvoicesReducer(state.Invoices, action),
                SelectedInvoice = SelectedInvoiceReducer(state.SelectedInvoice, action),
                Articles = ArticlesReducer(state.Articles, action),
            };
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
                    Console.WriteLine($"Reducing {typeof(Actions.ReceiveInvoicesAction)}");
                    Console.WriteLine($"Retrieved {invoicesAction.Invoices.Count}");
                    Console.WriteLine($"First Invoice Customer ID: {invoicesAction.Invoices.First().Customer.Id}");


                    foreach (var i in invoicesAction.Invoices)
                    {
                        Console.WriteLine($"Invoice: {i.Id}");
                        Console.WriteLine($"Customer of Invoice {i.Id}: {i.Customer.Id}: {i.Customer.Name}");
                        Console.WriteLine("Items:");
                        foreach (var item in i.Items)
                        {
                            Console.WriteLine($"{item.Article.Name}: {item.Quantity} @ {item.Price} = {item.PriceTotal}");
                        }
                    }


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
