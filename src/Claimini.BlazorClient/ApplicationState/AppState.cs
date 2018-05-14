using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient
{
    public class AppState
    {
        public string Location { get; set; }

        public CustomerDto[] Customers { get; set; }

        public CustomerDto SelectedCustomerIndex { get; set; }
    }
}
