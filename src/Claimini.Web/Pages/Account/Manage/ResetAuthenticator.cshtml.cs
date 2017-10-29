// <copyright file="ResetAuthenticator.cshtml.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Pages.Account.Manage
{
    using System;
    using System.Threading.Tasks;
    using Claimini.Web.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Reset Authentication Model
    /// </summary>
    public class ResetAuthenticatorModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<ResetAuthenticatorModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetAuthenticatorModel"/> class.
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="logger">Logger</param>
        public ResetAuthenticatorModel(
            UserManager<ApplicationUser> userManager,
            ILogger<ResetAuthenticatorModel> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// GET Request Handler
        /// Displays the user to reset the authenticator for
        /// </summary>
        /// <returns>The page</returns>
        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.Page();
        }

        /// <summary>
        /// POST Request Handler
        /// </summary>
        /// <returns>EnableAuthenticator Page</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.userManager.SetTwoFactorEnabledAsync(user, false);
            await this.userManager.ResetAuthenticatorKeyAsync(user);
            this.logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

            return this.RedirectToPage("./EnableAuthenticator");
        }
    }
}
