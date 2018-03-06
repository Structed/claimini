// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Api.Configuration
{
    /// <summary>
    /// Configuration options for MongoDb
    /// </summary>
    public class MongoConfiguration
    {
        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        /// <value>
        /// The connection string
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the database
        /// </summary>
        /// <value>
        /// The connection string
        /// </value>
        public string Database { get; set; }
    }
}
