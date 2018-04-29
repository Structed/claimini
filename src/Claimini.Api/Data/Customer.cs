// <copyright file="Customer.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace Claimini.Api.Data
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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Street Address
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets an additional, optional address field
        /// </summary>
        [Display(Name = "Additional Street Address")]
        [StringLength(50)]
        public string StreetAddressAdditional { get; set; }

        /// <summary>
        /// Gets or sets the ZIP Code
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(10)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state in which the customer resides
        /// </summary>
        [StringLength(20)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the Country in which the Customer resides
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
    }
}
