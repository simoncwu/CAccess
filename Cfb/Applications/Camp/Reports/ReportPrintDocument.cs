using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WinForms;

namespace Cfb.Camp.Reports
{
	/// <summary>
	/// A derived <see cref="PrintDocument"/> for printing <see cref="ServerReport"/> and <see cref="LocalReport"/> reports without requiring a <see cref="Reportviewer"/>.
	/// </summary>
	public class ReportPrintDocument : PrintDocument
	{
		/// <summary>
		/// The zero-based index of the current page being printed.
		/// </summary>
		private int _currentPage;

		/// <summary>
		/// A collection of data streams representing the rendered output of all report pages.
		/// </summary>
		private List<Stream> _pages;

		/// <summary>
		/// A server report to print.
		/// </summary>
		private readonly ServerReport _serverReport;

		/// <summary>
		/// A local report to print.
		/// </summary>
		private readonly LocalReport _localReport;

		/// <summary>
		/// The page settings for the report.
		/// </summary>
		private readonly ReportPageSettings _pageSettings;

		/// <summary>
		/// An XML string that contains the device-specific content that is required by the report rendering extension.
		/// </summary>
		private readonly string _deviceInfo;

		/// <summary>
		/// Whether or not the report has been rendered.
		/// </summary>
		private bool _rendered;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportPrintDocument"/> class.
		/// </summary>
		/// <param name="serverReport">A <see cref="ServerReport"/> to print.</param>
		public ReportPrintDocument(ServerReport serverReport)
			: this(serverReport as Report)
		{
			_serverReport = serverReport;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportPrintDocument"/> class.
		/// </summary>
		/// <param name="localReport">A <see cref="LocalReport"/> to print.</param>
		public ReportPrintDocument(LocalReport localReport)
			: this(localReport as Report)
		{
			_localReport = localReport;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportPrintDocument"/> class.
		/// </summary>
		/// <param name="report">The <see cref="Report"/> to print.</param>
		private ReportPrintDocument(Report report)
		{
			_rendered = false;
			_pageSettings = report.GetDefaultPageSettings();
			_deviceInfo = CreateEmfDeviceInfo();
			_pages = new List<Stream>();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="Component"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				foreach (Stream s in _pages)
				{
					s.Dispose();
				}
				_pages.Clear();
			}
		}

		/// <summary>
		/// Raises the <see cref="PrintDocument.BeginPrint"/> event. It is called after the <see cref="PrintDocument.Print"/> method is called and before the first page of the document prints.
		/// </summary>
		/// <param name="e">A <see cref="PrintEventArgs"/> that contains the event data.</param>
		protected override void OnBeginPrint(PrintEventArgs e)
		{
			base.OnBeginPrint(e);
			_currentPage = 0;
			if (!_rendered)
				RenderReport();
		}

		/// <summary>
		/// Raises the <see cref="PrintDocument.PrintPage"/> event. It is called before a page prints.
		/// </summary>
		/// <param name="e">A <see cref="PrintPageEventArgs"/> that contains the event data.</param>
		protected override void OnPrintPage(PrintPageEventArgs e)
		{
			base.OnPrintPage(e);
			Stream pageData = _pages[_currentPage++];
			pageData.Position = 0;
			// print each page by loading it into a Metafile and drawing it as an image
			using (Metafile pageMetafile = new Metafile(pageData))
			{
				Rectangle bounds = e.PageBounds;
				PageSettings pageSettings = e.PageSettings;
				// Draw a white background for the report
				//e.Graphics.FillRectangle(Brushes.White, adjustedRect);
				e.Graphics.DrawImage(pageMetafile, bounds.Left - pageSettings.HardMarginX, bounds.Top - pageSettings.HardMarginY, bounds.Width, bounds.Height);
				e.HasMorePages = _currentPage < _pages.Count;
			}
		}

		/// <summary>
		/// Raises the <see cref="PrintDocument.QueryPageSettings"/> event. It is called immediately before each PrintPage event.
		/// </summary>
		/// <param name="e">A <see cref="QueryPageSettingsEventArgs"/> that contains the event data.</param>
		protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
		{
			base.OnQueryPageSettings(e);
			PageSettings settings = new PageSettings();
			settings.PaperSize = _pageSettings.PaperSize;
			settings.Margins = _pageSettings.Margins;
			e.PageSettings = settings;
		}

		/// <summary>
		/// Renders the report associated with this print document.
		/// </summary>
		public void RenderReport()
		{
			RenderReport(_localReport);
			RenderReport(_serverReport);
			_rendered = true;
		}

		/// <summary>
		/// Renders a server report.
		/// </summary>
		/// <param name="serverReport">The server report to render.</param>
		private void RenderReport(ServerReport serverReport)
		{
			if (serverReport == null) return;
			_pages.Clear();

			// Generating Image renderer pages a la carte can be expensive. To generate page 2, the server needs to recalculate and discard page 1.
			// Using PersistStreams causes the server to generate all pages in the background while returning after page 1 is complete.
			NameValueCollection firstPageParameters = new NameValueCollection();
			firstPageParameters.Add("rs:PersistStreams", "True");
			// GetNextStream returns the next page in the sequence from the background process started by PersistStreams.
			NameValueCollection nonFirstPageParameters = new NameValueCollection();
			nonFirstPageParameters.Add("rs:GetNextStream", "True");

			// render and store streams
			string mimeType, fileExtension;
			Stream pageStream = serverReport.Render("IMAGE", _deviceInfo, firstPageParameters, out mimeType, out fileExtension);
			// The server returns an empty stream when moving beyond the last page.
			while (pageStream.Length > 0)
			{
				_pages.Add(pageStream);
				pageStream = serverReport.Render("IMAGE", _deviceInfo, nonFirstPageParameters, out mimeType, out fileExtension);
			}
		}

		/// <summary>
		/// Renders a local report.
		/// </summary>
		/// <param name="localReport">The local report to render.</param>
		private void RenderReport(LocalReport localReport)
		{
			if (localReport == null) return;
			_pages.Clear();
			Warning[] warnings;
			localReport.Render("IMAGE", _deviceInfo, LocalReportCreateStreamCallback, out warnings);
		}

		/// <summary>
		/// Provides a new <see cref="Stream"/> for report rendering.
		/// </summary>
		/// <param name="name">The name of the stream.</param>
		/// <param name="extension">The file name extension to use if a file stream is being created.</param>
		/// <param name="encoding">An <see cref="Encoding"/> enumerator value specifying the character encoding of the stream. This may be null if the stream does not contain characters.</param>
		/// <param name="mimeType">A <see cref="String"/> containing the MIME type of the stream.</param>
		/// <param name="willSeek">A <see cref="Boolean"/> value indicating whether the stream needs to support seeking. If the value is false, the stream will be forward-only and will be sent to the client in chunks as it is created. If the value is true, the stream may be written in any order.</param>
		/// <returns>A <see cref="Stream"/> object to which the report rendering processes can write data.</returns>
		private Stream LocalReportCreateStreamCallback(string name, string extension, Encoding encoding, string mimeType, bool willSeek)
		{
			MemoryStream stream = new MemoryStream();
			_pages.Add(stream);
			return stream;
		}

		/// <summary>
		/// Generates the device-specific content XML required by the report rendering extension.
		/// </summary>
		/// <returns>An XML string containing the device-specific content required by the report rendering extension.</returns>
		private string CreateEmfDeviceInfo()
		{
			PaperSize paperSize = _pageSettings.PaperSize;
			Margins margins = _pageSettings.Margins;
			// The device info string defines the page range to print as well as the size of the page.
			// A start and end page of 0 means generate all pages.
			return string.Format(
				CultureInfo.InvariantCulture,
@"<DeviceInfo>
	<OutputFormat>emf</OutputFormat>
	<StartPage>0</StartPage>
	<EndPage>0</EndPage>
	<MarginTop>{0}</MarginTop>
	<MarginLeft>{1}</MarginLeft>
	<MarginRight>{2}</MarginRight>
	<MarginBottom>{3}</MarginBottom>
	<PageHeight>{4}</PageHeight>
	<PageWidth>{5}</PageWidth>
</DeviceInfo>",
				FormatForDeviceInfo(margins.Top),
				FormatForDeviceInfo(margins.Left),
				FormatForDeviceInfo(margins.Right),
				FormatForDeviceInfo(margins.Bottom),
				FormatForDeviceInfo(paperSize.Height),
				FormatForDeviceInfo(paperSize.Width));
		}

		/// <summary>
		/// Formats a page setting dimensions for device-specific rendering content XML.
		/// </summary>
		/// <param name="dimension">A page setting dimension in hundredths of an inch.</param>
		/// <returns>The value of <paramref name="dimension"/> as a <see cref="String"/> appropriate for device-specific rendering content XML.</returns>
		private string FormatForDeviceInfo(int dimension)
		{
			return string.Format("{0}in", dimension / 100.0);
		}
	}
}
