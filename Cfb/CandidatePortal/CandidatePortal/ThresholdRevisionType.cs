using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration representing the various kinds of <see cref="ThresholdRevision"/> objects.
	/// </summary>
	public enum ThresholdRevisionType : byte
	{
		/// <summary>
		/// The initial threshold revision issued upon a completed statement review.
		/// </summary>
		Initial,
		/// <summary>
		/// The revised threshold revision issued upon a completed review of a statement review response.
		/// </summary>
		Revised,
		/// <summary>
		/// A cumulative summary of threshold status information to date.
		/// </summary>
		Summary
	}
}
