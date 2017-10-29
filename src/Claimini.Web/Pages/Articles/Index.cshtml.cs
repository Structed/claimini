// <copyright file="Index.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Articles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Article Index Model
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly Claimini.Web.Data.ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexModel"/> class.
        /// </summary>
        /// <param name="context">The DB Context</param>
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the Article Model
        /// </summary>
        public IList<Article> Article { get; set; }

        /// <summary>
        /// GET Request Handler
        /// </summary>
        /// <returns>Shows a list of Articles</returns>
        public async Task OnGetAsync()
        {
            this.Article = await this.context.Article.ToListAsync();
        }
    }
}
