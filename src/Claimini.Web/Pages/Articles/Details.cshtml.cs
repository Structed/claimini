// <copyright file="Details.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Articles
{
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Article Details Model
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsModel"/> class.
        /// </summary>
        /// <param name="context">The DB context</param>
        public DetailsModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Article model
        /// </summary>
        public Article Article { get; set; }

        /// <summary>
        /// GET Request Handler
        /// Shows the Article Details Page
        /// </summary>
        /// <param name="id">The Id of the article</param>
        /// <returns>Shows the Article Details Page</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Article = await this.context.Article.SingleOrDefaultAsync(m => m.Id == id);

            if (this.Article == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }
    }
}
