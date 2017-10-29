// <copyright file="Details.cshtml.cs" company="Johannes Ebner">
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
    /// The Details Model
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsModel"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        public DetailsModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Customer model
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        ///  GET Request Handler
        /// </summary>
        /// <param name="id">The Id of the Customer</param>
        /// <returns>The detail page of the Customer, if found</returns>
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
    }
}
