using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the types of audit review response extensions.
	/// </summary>
	public enum ExtensionType : byte
	{
		/// <summary>
		/// An extension for a response to a statement review.
		/// </summary>
		StatementReview,
		/// <summary>
		/// An extension for a response to a compliance visit.
		/// </summary>
		ComplianceVisitReview,
		/// <summary>
		/// An extension for a response to a doing business review.
		/// </summary>
		DoingBusinessReview
	}
}
