using System.ComponentModel.DataAnnotations;

namespace Claimini.Web.Data
{
    /// <summary>
    /// Represents an Article, which can be added as a <see cref="InvoiceItem"/>
    /// to an <see cref="Invoice"/>
    /// </summary>
    public class Article
    {
        /// <summary>
        /// The Id of the Article
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The default price of the Article
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// The UNIX Timestamp (seconds) the Article was created
        /// </summary>
        [Required]
        public long CreatedTimestamp { get; set; }
    }
}
