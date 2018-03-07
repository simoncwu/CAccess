using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A collection of compliance visits.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class ComplianceVisits
	{
		/// <summary>
		/// The inner collection of compliance visits.
		/// </summary>
		[DataMember(Name = "Visits")]
		private readonly CPCalendarItemCollection<ComplianceVisit> _visits;

		/// <summary>
		/// Gets the inner collection of compliance visits.
		/// </summary>
		public CPCalendarItemCollection<ComplianceVisit> Visits
		{
			get { return _visits; }
		}

		/// <summary>
		/// A collection of compliance visit response deadlines.
		/// </summary>
		[DataMember(Name = "ResponseDeadlines")]
		private readonly CVResponseDeadlines _responseDeadlines;

		/// <summary>
		/// Gets a collection of compliance visit response deadlines.
		/// </summary>
		public CVResponseDeadlines ResponseDeadlines
		{
			get { return _responseDeadlines; }
		}

		/// <summary>
		/// Gets a collection of upcoming compliance visit appointments.
		/// </summary>
		public IEnumerable<ComplianceVisit> UpcomingVisits
		{
			get { return from v in _visits where v.IsUpcoming select v; }
		}

		/// <summary>
		/// Gets the number of visits actually contained in the collection.
		/// </summary>
		public int Count
		{
			get { return _visits.Count; }
		}

		/// <summary>
		/// Initializes a new collection of <see cref="ComplianceVisit"/> records.
		/// </summary>
		public ComplianceVisits()
		{
			_visits = new CPCalendarItemCollection<ComplianceVisit>();
			_responseDeadlines = new CVResponseDeadlines();
		}

		/// <summary>
		/// Retrieves a collection of upcoming compliance visit response deadlines.
		/// </summary>
		public IEnumerable<CVResponseDeadline> GetUpcomingDeadlines()
		{
			return from d in _responseDeadlines where d.IsUpcoming select d;
		}

		/// <summary>
		/// Retrieves all compliance visits on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose compliance visits are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A <see cref="ComplianceVisits"/> collection of all compliance visits on record for the specified candidate and election cycle.</returns>
		public static ComplianceVisits GetComplianceVisits(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetComplianceVisits(candidateID, electionCycle);
		}
	}
}
