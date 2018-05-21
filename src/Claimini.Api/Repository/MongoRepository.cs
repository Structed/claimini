// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Claimini.Api.Data;
using Claimini.Shared;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Claimini.Api.Repository
{
    internal class MongoRepository<T> : IMongoRepository<T> where T : class, IMongoDocument, new()
    {
        private readonly IMongoCollection<T> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{T}" /> class.
        /// </summary>
        /// <param name="mongoCollection">The settings.</param>
        public MongoRepository(IMongoCollection<T> mongoCollection)
        {
            this.collection = mongoCollection;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.collection
                .Find(_ => true)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync(int currentPage, int pageSize)
        {
            return await this.collection
                .Find(_ => true)
                .Skip((currentPage - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            return await this.collection
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.collection
                .Find(predicate)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public T Get(string id)
        {
            return this.collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;
        }

        /// <inheritdoc/>
        public async Task InsertAsync(T model)
        {
            await this.collection.InsertOneAsync(model);
        }

        /// <inheritdoc/>
        public async Task<T> Update(string id, T model)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(s => s.Id, model.Id);
            await this.collection.ReplaceOneAsync(filter, model);

            return await this.GetAsync(id);
        }
    }
}
