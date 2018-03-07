using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Tracking details for a Post Election Audit Initial Documentation Request Inadequate Response event for a single candidate and election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(IdrInadequateDeadline))]
	public class IdrInadequateEvent : InadequateEventBase
	{
		/// <summary>
		/// Gets the type of post election audit report or letter.
		/// </summary>
		public override AuditReportType AuditReportType
		{
			get { return this.IsAdditional ? AuditReportType.IdrAdditionalInadequateResponse : AuditReportType.IdrInadequateResponse; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdrInadequateEvent"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		public IdrInadequateEvent(DateTime date)
			: this(date, false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdrInadequateEvent"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		/// <param name="isAdditional">true to create an additional inadequate event; otherwise, false to create the original event.</param>
		public IdrInadequateEvent(DateTime date, bool isAdditional)
			: base(date, isAdditional ? CPCalendarItemType.IdrAdditionalInadequateEvent : CPCalendarItemType.IdrInadequateEvent)
		{
			this.Title = Properties.CPCalendarItemResources.IdrIRTitle;
			this.Description = Properties.CPCalendarItemResources.IdrIRDescription;
		}

		/// <summary>
		/// Retrieves the Post Election Initial Documentation Request Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response event is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Initial Documentation Request Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		public static IdrInadequateEvent GetIdrInadequateEvent(string candidateID, string electionCycle, bool published)
		{
			return CPProviders.DataProvider.GetIdrInadequateEvent(candidateID, electionCycle, published);
		}
	}
}
