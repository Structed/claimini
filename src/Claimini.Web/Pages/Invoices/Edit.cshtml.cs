// <copyright file="Edit.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Invoices
{
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditModel"/> class.
        /// </summary>
        /// <param name="context">The DB Context</param>
        public EditModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Invoice Model
        /// </summary>
        [BindProperty]
        public Invoice Invoice { get; set; }

        /// <summary>
        /// GET Request Handler
        /// </summary>
        /// <param name="id">The id of the Invoice</param>
        /// <returns>The page, displaying the Invoice for edit</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Invoice = await this.context.Invoice.SingleOrDefaultAsync(m => m.Id == id);

            if (this.Invoice == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }

        /// <summary>
        /// POST Request Handler
        /// </summary>
        /// <returns>The detail page, if edit was successful. Else, the edit page again</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.context.Attach(this.Invoice).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // TODO: handle exception
            }

            return this.RedirectToPage("./Index");
        }
    }
}
