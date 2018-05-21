using System.Collections.Generic;
using System.Linq;

namespace Claimini.Shared
{
    public class InvoiceFullDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Customer for whom the Invoice was created for
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the UNIX Timestamp (seconds) at which the Invoice was created
        /// </summary>
        public long CreatedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the UNIX Timestamp (seconds) at which the Invoice was paid
        /// </summary>
        public long PaidTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the Items of the Invoice
        /// </summary>
        public IList<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Gets the total price of the Invoice
        /// </summary>
        public decimal PriceTotal => this.Items.Sum(invoiceItem => invoiceItem.PriceTotal);
    }
}
