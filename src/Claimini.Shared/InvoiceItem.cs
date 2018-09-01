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
        /// Gets or sets the Article to which this item relates
        /// </summary>
        [Required]
        public Article Article { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the given item
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets calculated total price of the items
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal PriceTotal => this.Article.Price * this.Quantity;
    }
}
