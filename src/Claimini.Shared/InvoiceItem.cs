// <copyright file="InvoiceItem.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace Claimini.Shared
{
    /// <summary>
    /// Represents an item of an invoice
    /// </summary>
    public class InvoiceItem
    {
        /// <summary>
        /// Gets or sets the Id of the Invoice Item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Article to which this item relates
        /// </summary>
        [Required]
        public Article Article { get; set; }

        /// <summary>
        /// Gets or sets the price of a single item at the Instant the Invoice was created
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the VAT percentage
        /// <example>i.e. 0.19m in Germany</example>
        /// </summary>
        [Required]
        public decimal VatPercentage { get; set; } = 0.19m;

        /// <summary>
        /// Gets or sets the quantity of the given item
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets calculated total price of the items
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal PriceTotal => this.Price * this.Quantity;
    }
}
