// <copyright file="Create.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Invoices
{
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <inheritdoc />
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
        /// GET Request Handler
        /// </summary>
        /// <returns>The Create page</returns>
        public IActionResult OnGet()
        {
            return this.Page();
        }

        /// <summary>
        /// Gets or sets the Invoice model
        /// </summary>
        [BindProperty]
        public Invoice Invoice { get; set; }

        /// <summary>
        /// POST Request Handler
        /// </summary>
        /// <returns>The index page, if the Invoice was successfully created. The create page if the data was not valid.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.context.Invoice.Add(this.Invoice);
            await this.context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}
