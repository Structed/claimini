// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System.Collections.Generic;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;

namespace Claimini.Api.Repository.Pdf.EventHandler
{
    public class PdfBackgroundEventHandler : IEventHandler
    {
        private readonly List<string> backgroundPdfPaths;

        public PdfBackgroundEventHandler(List<string> backgroundPdfPaths)
        {
            this.backgroundPdfPaths = backgroundPdfPaths;
        }

        public void HandleEvent(Event docEvent)
        {
            this.AddPdfAsBackground(docEvent);
        }

        public void AddPdfAsBackground(Event docEvent)
        {
            var documentEvent = docEvent as PdfDocumentEvent;
            PdfDocument pdfDocument = documentEvent?.GetDocument();
            PdfPage page = documentEvent?.GetPage();
            PdfCanvas pdfCanvas = new PdfCanvas(page?.NewContentStreamBefore(), page?.GetResources(), pdfDocument);

            foreach (string path in this.backgroundPdfPaths)
            {
                var backgroundPdfDocument = new PdfDocument(new PdfReader(path));
                var pdfFormXObject = backgroundPdfDocument.GetFirstPage().CopyAsFormXObject(pdfDocument);
                pdfCanvas.AddXObject(pdfFormXObject, 0, 0);
                backgroundPdfDocument.Close();
            }
        }
    }
}
