// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Claimini.Api.Data;

namespace Claimini.Api.Repository
{
    public interface IMongoRepository<T> where T : class, IMongoDocument, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(int currentPage, int pageSize);

        Task<T> GetAsync(string id);

        Task<T> FindByAsync(Expression<Func<T, bool>> predicate);

        T Get(string id);

        Task InsertAsync(T model);

        Task<T> Update(string id, T model);
    }
}
