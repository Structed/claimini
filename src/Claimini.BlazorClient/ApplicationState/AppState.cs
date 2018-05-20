using System;
using System.Linq;
using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient.ApplicationState
{
    public class AppState
    {
        public string Location { get; set; }

        public CustomerDto[] Customers { get; set; }

        public CustomerDto SelectedCustomer { get; set; }

        public CustomerDto GetCustomerById(int customerId)
        {
            CustomerDto customerDto = this.Customers.FirstOrDefault(x => x.Id == customerId);
            if (customerDto == null || customerDto.Equals(default(CustomerDto)))
            {
                // TODO: Load customer
                Console.WriteLine("Should load customer here");
            }

            return customerDto;
        }
    }
}
