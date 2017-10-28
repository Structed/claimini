using System.ComponentModel.DataAnnotations;

namespace Claimini.Web.Data
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }


        [Display(Name = "Additional Street Address")]
        [StringLength(50)]
        public string StreetAddressAdditional { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [Required]
        [StringLength(30)]
        public string Country { get; set; }
    }
}
