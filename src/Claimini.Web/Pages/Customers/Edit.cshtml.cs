// <copyright file="Edit.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Customers
{
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The Customer Edit Model
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditModel"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        public EditModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Customer
        /// </summary>
        [BindProperty]
        public Customer Customer { get; set; }

        /// <summary>
        /// GET Request Handler
        /// </summary>
        /// <param name="id">The id of the Customer</param>
        /// <returns>The page, displaying the customer for edit</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Customer = await this.context.Customer.SingleOrDefaultAsync(m => m.Id == id);

            if (this.Customer == null)
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

            this.context.Attach(this.Customer).State = EntityState.Modified;

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
