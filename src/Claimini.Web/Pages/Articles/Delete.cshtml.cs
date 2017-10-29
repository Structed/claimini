// <copyright file="Delete.cshtml.cs" company="Johannes Ebner">
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
    /// Article Delete Model
    /// </summary>
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteModel"/> class.
        /// </summary>
        /// <param name="context">The DB Context</param>
        public DeleteModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Article Model
        /// </summary>
        [BindProperty]
        public Article Article { get; set; }

        /// <summary>
        /// GET Request Handler
        /// </summary>
        /// <param name="id">The Id of the Article</param>
        /// <returns>The confirmation or not found page</returns>
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

        /// <summary>
        /// POST Request Handler
        /// Deletes an Article
        /// </summary>
        /// <param name="id">The Id of the Article</param>
        /// <returns>Index or Not Found page</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Article = await this.context.Article.FindAsync(id);

            if (this.Article != null)
            {
                this.context.Article.Remove(this.Article);
                await this.context.SaveChangesAsync();
            }

            return this.RedirectToPage("./Index");
        }
    }
}
