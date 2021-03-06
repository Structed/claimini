// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using Claimini.Api.Services;
using Claimini.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Claimini.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Articles")]
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;

        public ArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        // GET: api/Articles
        [HttpGet]
        public IEnumerable<Article> Get()
        {
            IEnumerable<Article> articles = this.articleService.FindAll();
            //return new ObjectResult(articles);
            return articles;
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Article article = this.articleService.Find(id);

            return new ObjectResult(article);
        }

        // POST: api/Articles
        [HttpPost]
        public IActionResult Post([FromBody]CreateArticleDto articleDto)
        {
            if (this.ModelState.IsValid)
            {
                Article article = this.articleService.Create(articleDto);
                return new ObjectResult(article);
            }

            return new ObjectResult(this.ModelState);
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Article article)
        {
            if (this.ModelState.IsValid == false)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            article.Id = id;
            this.articleService.Update(article);

            return new OkObjectResult(article);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.articleService.Delete(id);
        }
    }
}
