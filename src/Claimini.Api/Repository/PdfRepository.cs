// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using Claimini.Api.Repository.Pdf;
using Claimini.Api.Repository.Pdf.EventHandler;
using Claimini.Shared;
using iText.IO.Image;
using iText.Kernel.Colors;
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
        private PdfFont normalFont;
        private PdfFont boldFont;

        public void WriteInvoicePdf(Invoice invoice, PdfWriter writer, List<string> templatePdfPaths = null, string backgroundImagePath = "")
        {
            // Set up document
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // Register Event Handlers
            if (false == string.IsNullOrEmpty(backgroundImagePath))
            {
                Image backgroundImage = LoadBackgroundImage(backgroundImagePath);
                RegisterImageBackgroundEventHandler(pdf, backgroundImage);
            }

            if (templatePdfPaths != null && templatePdfPaths.Count > 0)
            {
                RegisterPdfBackgroundEventHandler(pdf, templatePdfPaths);
            }

            float marginBottom = PdfUserUnitUtils.MillimetersToPoints(40);
            float marginSides = PdfUserUnitUtils.MillimetersToPoints(20);
            document.SetMargins(0, marginSides, marginBottom, marginSides);
            document.SetFontSize(10);


            this.normalFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
            this.boldFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            // Add Content
            AddSenderAddress(document);
            AddMailingAddress(document, invoice);
            AddItemTable(document, invoice);

            // Finish up the document
            document.Close();
        }

        private static void AddSenderAddress(Document document)
        {
            var paragraph = new Paragraph("Test & Test * Bahnhofstraße 13337 * 80636 München");
            paragraph.SetTextAlignment(TextAlignment.CENTER);
            paragraph.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            paragraph.SetFontSize(8);

            float marginTop = PdfUserUnitUtils.MillimetersToPoints(45f);
            float width = PdfUserUnitUtils.MillimetersToPoints(85f);
            float height = PdfUserUnitUtils.MillimetersToPoints(5f);

            paragraph.SetMarginTop(marginTop);
            paragraph.SetHeight(height);
            paragraph.SetWidth(width);
            paragraph.SetMarginBottom(0f);
            paragraph.SetBackgroundColor(ColorConstants.GREEN); // Debug Coloring

            document.Add(paragraph);
        }

        private void AddMailingAddress(Document document, Invoice invoice)
        {
            float width = PdfUserUnitUtils.MillimetersToPoints(85f);
            float height = PdfUserUnitUtils.MillimetersToPoints(40f);
            float padding = PdfUserUnitUtils.MillimetersToPoints(5f);
            width -= padding * 2;
            height -= padding * 2;

            Div div = new Div();

            div.SetBackgroundColor(ColorConstants.MAGENTA); // Debug Coloring
            div.SetPadding(padding);
            div.SetWidth(width);
            div.SetHeight(height);
            div.SetFont(this.normalFont);
            div.SetFontSize(10f);

            var customer = invoice.Customer;

            div.Add(new Paragraph(customer.Name));
            div.Add(new Paragraph(customer.StreetAddress));
            div.Add(new Paragraph(customer.StreetAddressAdditional ?? ""));
            div.Add(new Paragraph(customer.State ?? ""));
            div.Add(new Paragraph(customer.ZipCode));
            div.Add(new Paragraph(customer.Country));

            var style = new Style();
            style.SetMargins(0, 0, 0, 0);
            foreach (var element in div.GetChildren())
            {
                if (element.GetType() == typeof(Paragraph)) {
                    ((Paragraph) element).AddStyle(style);
                }
            }

            document.Add(div);
        }

        private void AddItemTable(Document document, Invoice invoice)
        {
            var table = new Table(new float[] {3, 1, 2, 2, 2}); // Relative widths to each other - 3 is 3x as wide as 1.

            float marginTop = PdfUserUnitUtils.MillimetersToPoints(23.5f);
            table.SetMarginTop(marginTop);

            table.UseAllAvailableWidth();

            AddTableHeader(table, this.boldFont);

            foreach (InvoiceItem item in invoice.Items)
            {
                AddTableRow(table, item, this.normalFont);
            }

            AddTableFooter(invoice, this.boldFont, table);

            document.Add(table);
        }

        private static void AddTableHeader(Table table, PdfFont font)
        {
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.Article), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.Quantity), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.Article.Price), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.Article.TaxPercentage), font));
            table.AddHeaderCell(CreateCell(nameof(InvoiceItem.PriceTotal), font));
        }

        private static void AddTableRow(Table table, InvoiceItem item, PdfFont font)
        {
            table.AddCell(CreateCell(item.Article.Name, font));
            table.AddCell(CreateCell(item.Quantity.ToString(), font, TextAlignment.RIGHT));
            table.AddCell(CreateCell(item.Article.Price.ToString("C"), font, TextAlignment.RIGHT));
            table.AddCell(CreateCell(item.Article.TaxPercentage.ToString("P"), font, TextAlignment.RIGHT));
            table.AddCell(CreateCell(item.PriceTotal.ToString("C"), font, TextAlignment.RIGHT));
        }
        
        private static void AddTableFooter(Invoice invoice, PdfFont bold, Table table)
        {
            var noBorderStyle = new Style().SetBorder(Border.NO_BORDER);

            // Total Price
            string label = $"Total Price:";
            Cell totalPriceLabelCell = new Cell(0, 4)
                .Add(new Paragraph(label)
                    .SetFont(bold))
                .SetTextAlignment(TextAlignment.RIGHT)
                .AddStyle(noBorderStyle);
            table.AddCell(totalPriceLabelCell);

            Cell totalPriceCell = CreateCell(invoice.PriceTotal.ToString("C"), bold, TextAlignment.RIGHT).AddStyle(noBorderStyle);
            table.AddCell(totalPriceCell);
        }
        
        private static Cell CreateCell(string text, PdfFont font, TextAlignment? textAlignment = null)
        {
            var cell = new Cell().Add(new Paragraph(text).SetFont(font)).SetTextAlignment(textAlignment);
            return cell;
        }

        private static Image LoadBackgroundImage(string backgroundImagePath)
        {
            var imageData = ImageDataFactory.Create(backgroundImagePath);
            var backgroundImage = new Image(imageData);
            return backgroundImage;
        }

        private static void RegisterImageBackgroundEventHandler(PdfDocument pdf, Image backgroundImage)
        {
            // Adds a background Image to every new page
            var handler = new BackgroundImageEventHandler(backgroundImage);
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
        }

        private static void RegisterPdfBackgroundEventHandler(PdfDocument pdf, List<string> templatePdfPaths)
        {
            var handler = new PdfBackgroundEventHandler(templatePdfPaths);
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
        }
    }
}
