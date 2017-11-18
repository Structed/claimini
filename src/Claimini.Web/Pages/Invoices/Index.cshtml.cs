// <copyright file="Index.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Invoices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexModel"/> class.
        /// </summary>
        /// <param name="context">The DB Context</param>
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets a list of invoices
        /// </summary>
        public IList<Invoice> Invoice { get; set; }

        /// <summary>
        /// GET request handler
        /// </summary>
        /// <returns>The page</returns>
        public async Task OnGetAsync()
        {
            this.Invoice = await this.context.Invoice.ToListAsync();
        }
    }
}
