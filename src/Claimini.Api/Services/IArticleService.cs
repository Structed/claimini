// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using Claimini.Api.Data;
using Claimini.Api.Data.Dto;

namespace Claimini.Api.Services
{
    public interface IArticleService
    {
        Article Create(CreateArticleDto articleDto);

        Article Find(int id);

        IEnumerable<Article> FindAll();

        void Delete(int id);
    }
}
