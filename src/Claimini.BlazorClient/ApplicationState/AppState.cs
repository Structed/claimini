using Claimini.BlazorClient.Dto;

namespace Claimini.BlazorClient.ApplicationState
{
    public class AppState
    {
        public string Location { get; set; }

        public CustomerDto[] Customers { get; set; }

        public CustomerDto SelectedCustomer { get; set; }
    }
}
