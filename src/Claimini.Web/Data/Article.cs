// <copyright file="Article.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Data
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an Article, which can be added as a <see cref="InvoiceItem"/>
    /// to an <see cref="Invoice"/>
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Gets or sets the Id of the Article
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default price of the Article
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the UNIX Timestamp (seconds) the Article was created
        /// </summary>
        [Required]
        public long CreatedTimestamp { get; set; }
    }
}
