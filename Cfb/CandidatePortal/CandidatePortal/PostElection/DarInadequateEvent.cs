using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Tracking details for a Post Election Audit Draft Audit Report Inadequate Response event for a single candidate and election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(DarInadequateDeadline))]
	public class DarInadequateEvent : InadequateEventBase
	{
		/// <summary>
		/// Gets the type of post election audit report or letter.
		/// </summary>
		public override AuditReportType AuditReportType
		{
			get { return this.IsAdditional ? AuditReportType.DarAdditionalInadequateResponse : AuditReportType.DarInadequateResponse; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DarInadequateEvent"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		public DarInadequateEvent(DateTime date)
			: this(date, false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DarInadequateEvent"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		/// <param name="isAdditional">true to create an additional inadequate event; otherwise, false to create the original event.</param>
		public DarInadequateEvent(DateTime date, bool isAdditional)
			: base(date, isAdditional ? CPCalendarItemType.DarAdditionalInadequateEvent : CPCalendarItemType.DarInadequateEvent)
		{
			this.Title = Properties.CPCalendarItemResources.DarIRTitle;
			this.Description = Properties.CPCalendarItemResources.DarIRDescription;
		}

		/// <summary>
		/// Retrieves the Post Election Draft Audit Report Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		public static DarInadequateEvent GetDarInadequateEvent(string candidateID, string electionCycle, bool published)
		{
			return CPProviders.DataProvider.GetDarInadequateEvent(candidateID, electionCycle, published);
		}
	}
}
