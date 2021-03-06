// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace Claimini.Shared
{
    public class InvoiceDto
    {
        public int CustomerId { get; set; }

        public IEnumerable<InvoiceItemDto> InvoiceItems { get; set; }

        public bool IsValid()
        {
            return this.CustomerId > 0 && this.InvoiceItems.Any();
        }
    }
}
