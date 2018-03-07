// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Claimini.Api.Data
{
    /// <summary>
    /// Represents an Invoice, which contains <see cref="InvoiceItem"/>s
    /// and is associated to a <see cref="Customer"/>
    /// </summary>
    [NotMapped]
    public class Invoice : IMongoDocument
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonId]
        [JsonIgnore]
        [NotMapped]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Gets or sets the Customer for whom the Invoice was created for
        /// </summary>
        [Required]
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the UNIX Timestamp (seconds) at which the Invoice was created
        /// </summary>
        [Required]
        public long CreatedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the UNIX Timestamp (seconds) at which the Invoice was paid
        /// </summary>
        public long PaidTimestamp { get; set; }

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
