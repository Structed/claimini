using System.ComponentModel.DataAnnotations;

namespace Claimini.Web.Data
{
    /// <summary>
    /// Represents a Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The Id of the Customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Customer
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// The Street Address
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// An additional, optional address field
        /// </summary>
        [Display(Name = "Additional Street Address")]
        [StringLength(50)]
        public string StreetAddressAdditional { get; set; }

        /// <summary>
        /// The ZIP Code
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        /// <summary>
        /// The state in which the customer resides
        /// </summary>
        [StringLength(20)]
        public string State { get; set; }

        /// <summary>
        /// The Country in which the Customer resides
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
    }
}
