using System;
using System.Collections.Generic;
using System.Linq;
using Claimini.Shared;

namespace Claimini.BlazorClient.ApplicationState
{
    public class AppState
    {
        public string Location { get; set; }

        public List<InvoiceFullDto> Invoices { get; set; }

        public Customer[] Customers { get; set; }

        public Customer SelectedCustomer { get; set; }

        public Customer GetCustomerById(int customerId)
        {
            Customer customerDto = this.Customers.FirstOrDefault(x => x.Id == customerId);
            if (customerDto == null || customerDto.Equals(default(Customer)))
            {
                // TODO: Load customer
                Console.WriteLine("Should load customer here");
            }

            return customerDto;
        }
    }
}
