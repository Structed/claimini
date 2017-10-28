using System.ComponentModel.DataAnnotations;

namespace Claimini.Web.Data
{
    /// <summary>
    /// Represents an item of an invoice
    /// </summary>
    public class InvoiceItem
    {
        /// <summary>
        /// The Id of the Invoice Item
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// The Article to which this item relates
        /// </summary>
        [Required]
        public Article Article { get; set; }

        /// <summary>
        /// The quantity of the given item
        /// </summary>
        [Required]
        public uint Quantity { get; set; }

        /// <summary>
        /// The price of a single item at the Instant the Invoice was created
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// The VAT percentage
        /// <example>i.e. 0.19m in Germany</example>
        /// </summary>
        [Required]
        public decimal VatPercentage { get; set; } = 0.19m;

        /// <summary>
        /// Calculated total price of the items
        /// </summary>
        public decimal PriceTotal => this.Price * this.Quantity;
    }
}
