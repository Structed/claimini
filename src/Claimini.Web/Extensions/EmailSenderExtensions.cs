// <copyright file="EmailSenderExtensions.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

// ReSharper disable once CheckNamespace
namespace Claimini.Web.Services
{
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    /// <summary>
    /// An extension to send emails
    /// </summary>
    public static class EmailSenderExtensions
    {
        /// <summary>
        /// Asynchronously sends an Email Confirmation
        /// </summary>
        /// <param name="emailSender">The sender of the email</param>
        /// <param name="email">The email address to send the mail to</param>
        /// <param name="link">The confirmation link to place in the email</param>
        /// <returns>A Task, representing the sending</returns>
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(link)}'>clicking here</a>.");
        }

        /// <summary>
        /// Asynchronously sends a password reset Email
        /// </summary>
        /// <param name="emailSender">The sender of the email</param>
        /// <param name="email">The email address to send the mail to</param>
        /// <param name="callbackUrl">The callback link to include in the email</param>
        /// <returns>A Task, representing the sending</returns>
        public static Task SendResetPasswordAsync(this IEmailSender emailSender, string email, string callbackUrl)
        {
            return emailSender.SendEmailAsync(
                email,
                "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }
    }
}
