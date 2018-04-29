namespace Claimini.BlazorClient.Dto
{
    /// <summary>
    /// Represents a Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the Id of the Customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the Customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Street Address
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets an additional, optional address field
        /// </summary>
        public string StreetAddressAdditional { get; set; }

        /// <summary>
        /// Gets or sets the ZIP Code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state in which the customer resides
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the Country in which the Customer resides
        /// </summary>
        public string Country { get; set; }
    }
}
