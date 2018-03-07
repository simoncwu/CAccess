using System;
using System.Collections.Generic;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different types of calendar items supported.
	/// </summary>
	public enum CPCalendarItemType : byte
	{
		/// <summary>
		/// A generic calendar item with only basic properties.
		/// </summary>
		GenericItem,
		/// <summary>
		/// A statement review date.
		/// </summary>
		StatementReview,
		/// <summary>
		/// A compliance visit appointment.
		/// </summary>
		ComplianceVisit,
		/// <summary>
		/// A due date for a response to a statement review.
		/// </summary>
		SRResponseDeadline,
		/// <summary>
		/// A due date for a response to a compliance deadline.
		/// </summary>
		CVResponseDeadline,
		/// <summary>
		/// A filing date.
		/// </summary>
		FilingDeadline,
		/// <summary>
		/// A Doing Business review.
		/// </summary>
		DoingBusinessReview,
		/// <summary>
		/// A due date for a response to a Doing Business review.
		/// </summary>
		DbrResponseDeadline,
		/// <summary>
		/// The date the post election audit Initial Document Request is sent.
		/// </summary>
		InitialDocumentRequest,
		/// <summary>
		/// The due date for the response to the post election audit Initial Document Request.
		/// </summary>
		IdrResponseDeadline,
		/// <summary>
		/// The date of the post election audit IDR Inadequate Response event.
		/// </summary>
		IdrInadequateEvent,
		/// <summary>
		/// The due date for the response to the post election audit IDR Inadequate Response event.
		/// </summary>
		IdrInadequateDeadline,
		/// <summary>
		/// The date of an additional post election audit IDR Inadequate Response event.
		/// </summary>
		IdrAdditionalInadequateEvent,
		/// <summary>
		/// The due date for the response to an additional post election IDR Inadequate Response event.
		/// </summary>
		IdrAdditionalInadequateDeadline,
		/// <summary>
		/// The date the Draft Audit Report is sent.
		/// </summary>
		DraftAuditReport,
		/// <summary>
		/// The due date for the response to the Draft Audit Report.
		/// </summary>
		DarResponseDeadline,
		/// <summary>
		/// The date the post election audit Draft Audit Report inadequate response letter is sent.
		/// </summary>
		DarInadequateEvent,
		/// <summary>
		/// The due date for response to the post election Draft Audit Report inadequate response letter.
		/// </summary>
		DarInadequateDeadline,
		/// <summary>
		/// The date of an additional post election audit DAR Inadequate Response event.
		/// </summary>
		DarAdditionalInadequateEvent,
		/// <summary>
		/// The due date for the response to an additional post election DAR Inadequate Response event.
		/// </summary>
		DarAdditionalInadequateDeadline,
		/// <summary>
		/// The date the Final Audit Report is sent.
		/// </summary>
		FinalAuditReport,
		/// <summary>
		/// The date a post election tolling event starts.
		/// </summary>
		TollingEvent
	}
}
