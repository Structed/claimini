// <copyright file="Article.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claimini.Shared
{
    /// <summary>
    /// Represents an Article, which can be added as a <see cref="Invoice"/>
    /// to an <see cref="InvoiceItem"/>
    /// </summary>
    public class Article : IInvoiceItem
    {
        /// <summary>
        /// Gets or sets the Id of the Article
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 51)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default price of the Article
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the Tax (VAT) percentage
        /// </summary>
        [Required]
        public decimal TaxPercentage { get; set; }

        /// <summary>
        /// Gets or sets the UNIX Timestamp (seconds) the Article was created
        /// </summary>
        [Required]
        public long CreatedTimestamp { get; set; }

        [NotMapped]
        public int Quantity { get; set; }

        public decimal TaxValue => this.Price * this.TaxPercentage;

        public decimal NetPrice => this.Price + this.TaxValue;

        public decimal TotalPrice => this.Price * Quantity;

        public decimal TotalTaxValue => this.TotalPrice * this.TaxPercentage;

        public decimal TotalNetPrice => this.Price * Quantity + this.TotalTaxValue;
    }
}
