using System;
using System.Collections.Generic;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Printing;
using BlueYonder.Companion.Client.DataModel;
using BlueYonder.Companion.Client.Views;

namespace BlueYonder.Companion.Client.Helpers
{
    public class PrinterJob : IDisposable
    {
        private const double ApplicationContentMarginLeft = 0.075;
        private const double ApplicationContentMarginTop = 0.03;

        private readonly Page _page;
        private readonly PrintJobType _type;
        private readonly Reservation _reservationForPrinting;
        private readonly List<UIElement> _printPages;
        private readonly PrintDocument _printDocument;
        private readonly IPrintDocumentSource _printDocumentSource;

        public Canvas PrintingRoot
        {
            get { return _page.FindName("printingRoot") as Canvas; }
        }

        public PrinterJob(Page page, PrintJobType type, Reservation reservation)
        {
            _page = page;
            _reservationForPrinting = reservation;
            _type = type;

            _printDocument = new PrintDocument();
            _printDocument.Paginate += CreatePrintPages;
            _printDocument.GetPreviewPage += GetPrintPreviewPage;
            _printDocument.AddPages += AddPrintPages;
            _printDocumentSource = _printDocument.DocumentSource;

            _printPages = new List<UIElement>();

            PrintManager.GetForCurrentView().PrintTaskRequested += PrintTaskRequested;
        }

        private void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
        {
            PrintTask printTask = e.Request.CreatePrintTask("BlueYonder", sourceRequested => sourceRequested.SetSource(_printDocumentSource));

            printTask.Options.DisplayedOptions.Clear();
            printTask.Options.DisplayedOptions.Add(StandardPrintTaskOptions.Copies);
            printTask.Options.DisplayedOptions.Add(StandardPrintTaskOptions.Orientation);
            printTask.Options.DisplayedOptions.Add(StandardPrintTaskOptions.ColorMode);
        }

        private void CreatePrintPages(object sender, PaginateEventArgs e)
        {
            // Clear the cache of preview pages 
            _printPages.Clear();

            // Clear the printing root of preview pages
            PrintingRoot.Children.Clear();

            PrintTaskOptions printingOptions = e.PrintTaskOptions;

            // Get the page description to deterimine how big the page is
            PrintPageDescription pageDescription = printingOptions.GetPageDescription(0);

            AddPrintPages(pageDescription);

            // Report the number of preview pages created
            _printDocument.SetPreviewPageCount(_printPages.Count, PreviewPageCountType.Intermediate);
        }

        private void AddPrintPages(PrintPageDescription printPageDescription)
        {
            if (_type == PrintJobType.BoardingPass)
                CreateBoardingPassPage(printPageDescription, _reservationForPrinting);

            else if (_type == PrintJobType.Receipt)
                CreateReceiptPages(printPageDescription, _reservationForPrinting, null);
        }

        private void CreateBoardingPassPage(PrintPageDescription printPageDescription, Reservation reservation)
        {
            var page = new BoardingPassPreviewPage();
            var rootElement = (FrameworkElement)page.FindName("PrintRoot");

            PreparePrintPage(printPageDescription, reservation, page, rootElement);
        }

        private void CreateReceiptPages(PrintPageDescription printPageDescription, Reservation reservation, RichTextBlockOverflow previousOverflowElement)
        {
            FrameworkElement page;
            if (previousOverflowElement == null)
            {
                page = new ReceiptPreviewPage();
            }
            else
            {
                page = new ReceiptContinuationPreviewPage(previousOverflowElement);
            }

            PreparePrintPage(printPageDescription, reservation, page, page);

            // Find the last text container and see if the content is overflowing
            var overflowContainer = (RichTextBlockOverflow)page.FindName("continuationPageLinkedContainer");
            while (overflowContainer.HasOverflowContent)
            {
                CreateReceiptPages(printPageDescription, reservation, overflowContainer);
            }
        }

        private void PreparePrintPage(PrintPageDescription printPageDescription, Reservation reservation, FrameworkElement page, FrameworkElement rootElement)
        {
            page.DataContext = reservation;

            rootElement.Width = printPageDescription.PageSize.Width;
            rootElement.Height = printPageDescription.PageSize.Height;

            double marginWidth = Math.Max(printPageDescription.PageSize.Width - printPageDescription.ImageableRect.Width,
                printPageDescription.PageSize.Width * ApplicationContentMarginLeft * 2);

            double marginHeight = Math.Max(printPageDescription.PageSize.Height - printPageDescription.ImageableRect.Height,
                printPageDescription.PageSize.Height * ApplicationContentMarginTop * 2);

            // Set content size based on the given margins
            var contentContainer = (FrameworkElement) page.FindName("ContentContainer");
            contentContainer.Width = printPageDescription.PageSize.Width - marginWidth;
            contentContainer.Height = printPageDescription.PageSize.Height - marginHeight;

            // Set content margins
            contentContainer.SetValue(Canvas.LeftProperty, marginWidth / 2);
            contentContainer.SetValue(Canvas.TopProperty, marginHeight / 2);

            page.UpdateLayout();

            _printPages.Add(page);
            PrintingRoot.Children.Add(page);
            PrintingRoot.InvalidateMeasure();
            PrintingRoot.UpdateLayout();
        }

        private void GetPrintPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            _printDocument.SetPreviewPage(e.PageNumber, _printPages[e.PageNumber - 1]);
        }

        private void AddPrintPages(object sender, AddPagesEventArgs e)
        {
            foreach (UIElement page in _printPages)
            {
                _printDocument.AddPage(page);
            }

            // Indicate that all of the print pages have been provided
            _printDocument.AddPagesComplete();
        }

        public void Dispose()
        {
            PrintManager.GetForCurrentView().PrintTaskRequested -= PrintTaskRequested;
        }
    }
}
