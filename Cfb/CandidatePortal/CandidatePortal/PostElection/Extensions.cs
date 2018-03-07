using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Extensions to classes in the <see cref="PostElection"/> namespace.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Determines whether or not a tolling event is an extension for a post election audit report.
		/// </summary>
		/// <param name="evt">The <see cref="ITollingEvent"/> to analyze.</param>
		/// <param name="report">The post election audit report to compare against.</param>
		/// <returns>true if the tolling event is an extension for the post election audit report specified; otherwise, false.</returns>
		public static bool IsExtensionFor(this ITollingEvent evt, AuditReportBase report)
		{
			// check basic properties
			if (report == null || evt == null || !evt.IsExtension || evt.TollingLetter == null)
				return false;

			// check tolling source and, where applicable, reference event number
			switch (report.AuditReportType)
			{
				case AuditReportType.InitialDocumentationRequest:
					return evt.TollingLetter.HasIdrScope;
				case AuditReportType.IdrInadequateResponse:
				case AuditReportType.IdrAdditionalInadequateResponse:
					IdrInadequateEvent idrInadequate = report as IdrInadequateEvent;
					return idrInadequate != null && idrInadequate.TollingEventNumber == evt.ReferenceEventNumber && evt.TollingLetter.HasIdrInadequateScope;
				case AuditReportType.DraftAuditReport:
					return evt.TollingLetter.HasDarScope;
				case AuditReportType.DarInadequateResponse:
				case AuditReportType.DarAdditionalInadequateResponse:
					DarInadequateEvent darInadequate = report as DarInadequateEvent;
					return darInadequate != null && darInadequate.TollingEventNumber == evt.ReferenceEventNumber && evt.TollingLetter.HasDarInadequateScope;
				default:
					return false;
			}
		}

		/// <summary>
		/// Gets the number of tolling days incurred to date by a tolling event.
		/// </summary>
		/// <param name="evt">The <see cref="ITollingEvent"/> to analyze.</param>
		/// <returns>The number of tolling days incurred to date by the specified tolling event.</returns>
		public static int GetTollingDays(this ITollingEvent evt)
		{
			int count = (evt.TollingEndDate ?? DateTime.Today).Date.Subtract(evt.TollingStartDate.Date).Days;
			if (!evt.TollingEndDate.HasValue || evt.TollingEndReason == TollingEndReason.Extension || evt.TollingEndReason == TollingEndReason.NoResponse || evt.TollingEndReason == TollingEndReason.AgreementExpired)
				count++;
			return count > 0 ? count : 0;
		}
	}
}
