// <copyright file="Delete.cshtml.cs" company="Johannes Ebner">
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
        /// Gets or sets the Invoice model
        /// </summary>
        [BindProperty]
        public Invoice Invoice { get; set; }

        /// <summary>
        /// GET REquest handler
        /// </summary>
        /// <param name="id">The Id of the Invoice</param>
        /// <returns>The deletion confirmation page, not found otherwise</returns>
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
        /// <param name="id">The I</param>
        /// <returns>The index page, if the Invoice was deleted. Not found otherwise.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Invoice = await this.context.Invoice.FindAsync(id);

            if (this.Invoice != null)
            {
                this.context.Invoice.Remove(this.Invoice);
                await this.context.SaveChangesAsync();
            }

            return this.RedirectToPage("./Index");
        }
    }
}
