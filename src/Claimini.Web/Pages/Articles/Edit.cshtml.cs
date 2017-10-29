// <copyright file="Edit.cshtml.cs" company="Johannes Ebner">
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
    /// Article Edit Model
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditModel"/> class.
        /// </summary>
        /// <param name="context">The DB Context</param>
        public EditModel(Claimini.Web.Data.ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the article Model
        /// </summary>
        [BindProperty]
        public Article Article { get; set; }

        /// <summary>
        /// GET Request Handler
        /// </summary>
        /// <param name="id">The Article's Id</param>
        /// <returns>The Edit page</returns>
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
        /// Saves the Article edits
        /// </summary>
        /// <returns>The Article Index Page or the Edit page on error</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.context.Attach(this.Article).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // TODO: Handle exception
            }

            return this.RedirectToPage("./Index");
        }
    }
}
