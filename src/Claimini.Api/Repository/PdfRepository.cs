// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using Claimini.Api.Data;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Claimini.Api.Repository
{
    public class PdfRepository
    {
        public void WritePdf(Invoice invoice, string destinationPath)
        {
            var writer = new PdfWriter(destinationPath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.SetMargins(20, 20, 20, 20);

            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
            var bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            var table = new Table(new float[] { 3, 1, 2, 2, 2 });
            table.UseAllAvailableWidth();


            AddTableHeader(table, bold);

            foreach (InvoiceItem item in invoice.Items)
            {
                AddTableRow(table, item, font);
            }


            document.Add(table);
            document.Close();
        }

        private static void AddTableHeader(Table table, PdfFont bold)
        {
            table.AddHeaderCell(nameof(InvoiceItem.Article)).SetFont(bold);
            table.AddHeaderCell(nameof(InvoiceItem.Quantity)).SetFont(bold);
            table.AddHeaderCell(nameof(InvoiceItem.Price)).SetFont(bold);
            table.AddHeaderCell(nameof(InvoiceItem.VatPercentage)).SetFont(bold);
            table.AddHeaderCell(nameof(InvoiceItem.PriceTotal)).SetFont(bold);
        }

        private static void AddTableRow(Table table, InvoiceItem item, PdfFont font)
        {
            table.AddCell(item.Article.Name).SetFont(font);
            table.AddCell(item.Quantity.ToString()).SetFont(font);
            table.AddCell(item.Price.ToString()).SetFont(font);
            table.AddCell(item.VatPercentage.ToString()).SetFont(font);
            table.AddCell(item.PriceTotal.ToString()).SetFont(font);
        }
    }
}
