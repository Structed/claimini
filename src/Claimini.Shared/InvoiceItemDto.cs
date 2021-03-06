// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

namespace Claimini.Shared
{
    public class InvoiceItemDto : IInvoiceItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
    }
}
