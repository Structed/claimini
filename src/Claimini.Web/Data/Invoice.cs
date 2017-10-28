using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Claimini.Web.Data
{
    /// <summary>
    /// Represents an Invoice, which contains <see cref="InvoiceItem"/>s
    /// and is associated to a <see cref="Customer"/>
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// The Id of the Invoice
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// UNIX Timestamp (seconds) at which the Invoice was created
        /// </summary>
        [Required]
        public long CreatedInstant { get; set; }

        /// <summary>
        /// UNIX Timestamp (seconds) at which the Invoice was paid
        /// </summary>
        public long PaidInstant { get; set; }

        /// <summary>
        /// The Items of the Invoice
        /// </summary>
        [Required]
        public IList<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Gets the total price of the Invoice
        /// </summary>
        public decimal PriceTotal => this.Items.Sum(invoiceItem => invoiceItem.PriceTotal);
    }
}
