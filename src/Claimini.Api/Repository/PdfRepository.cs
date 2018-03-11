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
        public void WriteInvoicePdf(Invoice invoice, string destinationPath)
        {
            // Set up document
            var writer = new PdfWriter(destinationPath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.SetMargins(20, 20, 20, 20);

            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
            var bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            // Add Content
            AddItemTable(document, invoice, bold, font);

            // Finih up the document
            document.Close();
        }

        private static void AddItemTable(Document document, Invoice invoice, PdfFont bold, PdfFont font)
        {
            var table = new Table(new float[] {3, 1, 2, 2, 2});
            table.UseAllAvailableWidth();

            AddTableHeader(table, bold);

            foreach (InvoiceItem item in invoice.Items)
            {
                AddTableRow(table, item);
            }


            document.Add(table);
        }

        private static void AddTableHeader(Table table, PdfFont font)
        {
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.Article), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.Quantity), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.Price), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.VatPercentage), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.PriceTotal), font));
        }

        private static Cell CreateCell(string text, PdfFont font)
        {
            return new Cell().Add(new Paragraph(text).SetFont(font));
        }

        private static void AddTableRow(Table table, InvoiceItem item)
        {
            table.AddCell(item.Article.Name);
            table.AddCell(item.Quantity.ToString());
            table.AddCell(item.Price.ToString());
            table.AddCell(item.VatPercentage.ToString());
            table.AddCell(item.PriceTotal.ToString());
        }
    }
}
