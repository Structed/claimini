// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using Claimini.Api.Data;
using Claimini.Api.Repository.Pdf;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Claimini.Api.Repository
{
    public class PdfRepository
    {
        public void WriteInvoicePdf(Invoice invoice, PdfWriter writer, string backgroundImagePath = @"./static/TestBackground.png")
        {
            // Set up document
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            Image backgroundImage = LoadBackgroundImage(backgroundImagePath);

            // Register Event Handlers
            RegisterEventHandlers(pdf, backgroundImage);

            document.SetMargins(20, 20, 20, 20);
            document.SetFontSize(12);

            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
            var bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            // Add Content
            AddItemTable(document, invoice, bold, font);

            // Finih up the document
            document.Close();
        }

        private static Image LoadBackgroundImage(string backgroundImagePath)
        {
            var imageData = ImageDataFactory.Create(backgroundImagePath);
            var backgroundImage = new Image(imageData);
            return backgroundImage;
        }

        private static void RegisterEventHandlers(PdfDocument pdf, Image backgroundImage)
        {
            // Adds a background Image to every new page
            BackgroundEventHandler handler = new BackgroundEventHandler(backgroundImage);
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
        }

        private static void AddItemTable(Document document, Invoice invoice, PdfFont bold, PdfFont font)
        {
            var table = new Table(new float[] {3, 1, 2, 2, 2}); // Relative widths to each other - 3 is 3x as wide as 1.
            table.UseAllAvailableWidth();

            AddTableHeader(table, bold);

            foreach (InvoiceItem item in invoice.Items)
            {
                AddTableRow(table, item, font);
            }

            var noBorderStyle = new Style().SetBorder(Border.NO_BORDER);

            Cell totalPriceLabelCell = new Cell(0, 4)
                .Add(new Paragraph($"Total Price:")
                    .SetFont(bold))
                .SetTextAlignment(TextAlignment.RIGHT)
                .AddStyle(noBorderStyle);
            table.AddCell(totalPriceLabelCell);

            Cell totalPriceCell = CreateCell(invoice.PriceTotal.ToString("C"), bold, TextAlignment.RIGHT).AddStyle(noBorderStyle);
            table.AddCell(totalPriceCell);

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

        private static Cell CreateCell(string text, PdfFont font, TextAlignment? textAlignment = null)
        {
            var cell = new Cell().Add(new Paragraph(text).SetFont(font)).SetTextAlignment(textAlignment);
            return cell;
        }

        private static void AddTableRow(Table table, InvoiceItem item, PdfFont font)
        {
            table.AddCell(CreateCell(item.Article.Name, font));
            table.AddCell(CreateCell(item.Quantity.ToString(), font, TextAlignment.RIGHT));
            table.AddCell(CreateCell(item.Price.ToString("C"), font, TextAlignment.RIGHT));
            table.AddCell(CreateCell(item.VatPercentage.ToString("P"), font, TextAlignment.RIGHT));
            table.AddCell(CreateCell(item.PriceTotal.ToString("C"), font, TextAlignment.RIGHT));
        }
    }
}
