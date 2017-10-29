// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Represents an Invoice, which contains <see cref="InvoiceItem"/>s
    /// and is associated to a <see cref="Customer"/>
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Gets or sets the Id of the Invoice
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets uNIX Timestamp (seconds) at which the Invoice was created
        /// </summary>
        [Required]
        public long CreatedInstant { get; set; }

        /// <summary>
        /// Gets or sets uNIX Timestamp (seconds) at which the Invoice was paid
        /// </summary>
        public long PaidInstant { get; set; }

        /// <summary>
        /// Gets or sets the Items of the Invoice
        /// </summary>
        [Required]
        public IList<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Gets the total price of the Invoice
        /// </summary>
        public decimal PriceTotal => this.Items.Sum(invoiceItem => invoiceItem.PriceTotal);
    }
}
