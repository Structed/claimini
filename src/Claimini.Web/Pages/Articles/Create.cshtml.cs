// <copyright file="Create.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Articles
{
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Article Create Model
    /// </summary>
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateModel"/> class.
        /// </summary>
        /// <param name="context">The DB context</param>
        public CreateModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Article Model
        /// </summary>
        [BindProperty]
        public Article Article { get; set; }

        /// <summary>
        /// GET REquest Handler
        /// </summary>
        /// <returns>Shows the create page</returns>
        public IActionResult OnGet()
        {
            return this.Page();
        }

        /// <summary>
        /// POST Request Handler
        /// Creates a new Article
        /// </summary>
        /// <returns>This or index page</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.context.Article.Add(this.Article);
            await this.context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}
