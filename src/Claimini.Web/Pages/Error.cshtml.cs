// <copyright file="Error.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Error Model
    /// </summary>
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// Gets or sets tHe Request Id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the Request Id
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        /// <summary>
        /// GET Requests Handler
        /// </summary>
        public void OnGet()
        {
            this.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
        }
    }
}
