// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using Claimini.Api.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Claimini.Api.Data
{
    /// <summary>
    /// Represents Database Context for 'RootConfigurationDb' NoSql database
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoConfiguration mongoConfigurationSettings;
        private readonly IMongoDatabase database;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbContext"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public MongoDbContext(MongoConfiguration settings)
        {
            this.mongoConfigurationSettings = settings;
            IMongoClient client = new MongoClient(this.mongoConfigurationSettings.ConnectionString);
            this.database = client.GetDatabase(this.mongoConfigurationSettings.Database);
        }

        /// <inheritdoc />
        public IMongoCollection<Invoice> Invoices
            => this.database.GetCollection<Invoice>("Invoice");
    }
}
