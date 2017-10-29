// <copyright file="Contact.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <inheritdoc />
    public class ContactModel : PageModel
    {
        /// <summary>
        /// Gets or sets tHe message to display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// GET Request handler
        /// </summary>
        public void OnGet()
        {
            this.Message = "Your contact page.";
        }
    }
}
