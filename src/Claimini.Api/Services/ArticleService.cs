// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using Claimini.Api.Repository;
using Claimini.Shared;
using Microsoft.EntityFrameworkCore;

namespace Claimini.Api.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Article> articleRepository;

        public ArticleService(IUnitOfWork unitOfWork, IRepository<Article> articleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.articleRepository = articleRepository;
        }

        public Article Create(CreateArticleDto articleDto)
        {
            Article article = new Article
            {
                CreatedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Name = articleDto.Name,
                Price = articleDto.Price,
                TaxPercentage = articleDto.TaxPercentage
            };

            if (articleRepository.Exists(a => a.Name == article.Name))
            {
                throw new DuplicateEntryException(nameof(article.Name), article.Name);
            };

            EntityState state = articleRepository.Update(article);
            if (state == EntityState.Added)
            {
                unitOfWork.Commit();
            }

            return article;
        }

        public Article Find(int id)
        {
            return this.articleRepository.Get(id);
        }

        public IEnumerable<Article> FindAll()
        {
            return this.articleRepository.GetAll();
        }

        public void Delete(int id)
        {
            var article = new Article{Id = id};
            this.articleRepository.Delete(article);
            this.unitOfWork.Commit();
        }

        public Article Update(Article article)
        {
            this.articleRepository.Update(article);
            this.unitOfWork.Commit();

            return article;
        }
    }
}
