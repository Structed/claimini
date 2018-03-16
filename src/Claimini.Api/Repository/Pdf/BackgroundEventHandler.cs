// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;

namespace Claimini.Api.Repository.Pdf
{
    public class BackgroundEventHandler : IEventHandler
    {
        private readonly Image image;

        public BackgroundEventHandler(Image image)
        {
            this.image = image;
        }

        public void HandleEvent(Event docEvent)
        {
            this.AddBackgroundImage(docEvent);
        }

        private void AddBackgroundImage(Event docEvent)
        {
            var documentEvent = docEvent as PdfDocumentEvent;
            PdfDocument pdfDocument = documentEvent?.GetDocument();
            PdfPage page = documentEvent?.GetPage();
            PdfCanvas pdfCanvas = new PdfCanvas(page?.NewContentStreamBefore(), page?.GetResources(), pdfDocument);
            Rectangle area = page?.GetPageSize();
            var canvas = new Canvas(pdfCanvas, pdfDocument, area);
            canvas.Add(this.image);
        }
    }
}
