// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace Claimini.Shared
{
    /// <summary>
    /// Represents the data to create an <see cref="Article"/>
    /// </summary>
    public class CreateArticleDto
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default price of the Article
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
