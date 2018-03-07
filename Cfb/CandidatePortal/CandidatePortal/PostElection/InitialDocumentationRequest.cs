using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Tracking details for a Post Election Audit Initial Documentation Request for a single candidate and election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(IdrResponseDeadline))]
	public class InitialDocumentationRequest : AuditReportBase
	{
		/// <summary>
		/// Gets the type of post election audit report or letter.
		/// </summary>
		public override AuditReportType AuditReportType
		{
			get { return AuditReportType.InitialDocumentationRequest; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InitialDocumentationRequest"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		public InitialDocumentationRequest(DateTime date)
			: base(date, CPCalendarItemType.InitialDocumentRequest)
		{
			this.Title = Properties.CPCalendarItemResources.IdrTitle;
			this.Description = Properties.CPCalendarItemResources.IdrDescription;
		}

		/// <summary>
		/// Retrieves the Post Election Audit Initial Documentation Request for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The Post Election Initial Documentation Request for the candidate and election cycle specified if found; otherwise, null.</returns>
		public static InitialDocumentationRequest GetInitialDocumentationRequest(string candidateID, string electionCycle)
		{
			return GetInitialDocumentationRequest(candidateID, electionCycle, true);
		}

		/// <summary>
		/// Retrieves the Post Election Audit Initial Documentation Request for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Initial Documentation Request for the candidate and election cycle specified if found; otherwise, null.</returns>
		public static InitialDocumentationRequest GetInitialDocumentationRequest(string candidateID, string electionCycle, bool published)
		{
			return CPProviders.DataProvider.GetInitialDocumentationRequest(candidateID, electionCycle, published);
		}

		/// <summary>
		/// Counts the number of Draft Audit Report tolling days incurred by a candidate for a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The number of DAR tolling days incurred that match the criteria specified.</returns>
		public static int CountDarTollingDaysIncurred(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.CountTollingDaysIncurred(candidateID, electionCycle, true);
		}
	}
}
