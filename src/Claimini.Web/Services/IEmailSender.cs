// <copyright file="IEmailSender.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Web.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for Email
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Send an Email
        /// </summary>
        /// <param name="email">The email address to send to</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="message">The message to send</param>
        /// <returns>The task representing teh operation</returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
