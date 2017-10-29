// <copyright file="Create.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Customers
{
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// The Customer Create Model
    /// </summary>
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateModel"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        public CreateModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Customer model
        /// </summary>
        [BindProperty]
        public Customer Customer { get; set; }

        /// <summary>
        ///  GET Request Handler
        /// </summary>
        /// <returns>The Create Page</returns>
        public IActionResult OnGet()
        {
            return this.Page();
        }

        /// <summary>
        /// POST Request handler
        /// </summary>
        /// <returns>The index page, if the Customer was successfully created. The create page if the data was not valid.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.context.Customer.Add(this.Customer);
            await this.context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}
