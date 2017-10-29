// <copyright file="Disable2fa.cshtml.cs" company="Johannes Ebner">
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
    /// The Disable 2FA Model
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<Disable2faModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Disable2faModel"/> class.
        /// </summary>
        /// <param name="userManager">The user manager</param>
        /// <param name="logger">The logger</param>
        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILogger<Disable2faModel> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// GET REquest Handler
        /// </summary>
        /// <returns>This page</returns>
        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!await this.userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new ApplicationException($"Cannot disable 2FA for user with ID '{this.userManager.GetUserId(this.User)}' as it's not currently enabled.");
            }

            return this.Page();
        }

        /// <summary>
        /// POST Request Handler
        /// Disables 2-factor-auth for the current user
        /// </summary>
        /// <returns>TwoFactorAuthentication Index Page</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            // ReSharper disable once InconsistentNaming
            var disable2faResult = await this.userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred disabling 2FA for user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", this.userManager.GetUserId(this.User));

            return this.RedirectToPage("./TwoFactorAuthentication");
        }
    }
}
