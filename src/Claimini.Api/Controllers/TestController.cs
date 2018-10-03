using System;
using System.Collections.Generic;
using Claimini.Api.Services;
using Claimini.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Claimini.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private readonly IArticleService articleService;

        public TestController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet("random-article")]
        public IActionResult CreateRandomArticles()
        {
            var articles = new List<Article>();

            for (int i = 12; i <= 20; i++)
            {
                string name = $"Article_{i}";
                decimal price = (decimal) new Random().NextDouble();

                var createArticleDto = new CreateArticleDto
                {
                    Name = name,
                    Price = price
                };
                var article = articleService.Create(createArticleDto);
                articles.Add(article);
            }

            return new ObjectResult(articles);
        }
    }
}
