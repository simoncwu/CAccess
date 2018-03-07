using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A collection of statement reviews.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class StatementReviews
	{
		/// <summary>
		/// The inner collection of statement reviews.
		/// </summary>
		[DataMember(Name = "Reviews")]
		private readonly CPCalendarItemCollection<StatementReview> _reviews;

		/// <summary>
		/// Gets the inner collection of statement reviews.
		/// </summary>
		public CPCalendarItemCollection<StatementReview> Reviews
		{
			get { return _reviews; }
		}

		/// <summary>
		/// A collection of statement review response deadlines.
		/// </summary>
		[DataMember(Name = "ResponseDeadlines")]
		private readonly SRResponseDeadlines _responseDeadlines;

		/// <summary>
		/// Gets a collection of statement review response deadlines.
		/// </summary>
		public SRResponseDeadlines ResponseDeadlines
		{
			get { return _responseDeadlines; }
		}

		/// <summary>
		/// Gets the number of reviews actually contained in the collection.
		/// </summary>
		public int Count
		{
			get { return _reviews.Count; }
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="StatementReview"/> records.
		/// </summary>
		internal StatementReviews()
		{
			_reviews = new CPCalendarItemCollection<StatementReview>();
			_responseDeadlines = new SRResponseDeadlines();
		}

		/// <summary>
		/// Gets a collection of upcoming statement review response deadlines.
		/// </summary>
		public IEnumerable<SRResponseDeadline> GetUpcomingDeadlines()
		{
			return from d in _responseDeadlines where d.IsUpcoming select d;
		}

		/// <summary>
		/// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
		public static StatementReviews GetStatementReviews(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetStatementReviews(candidateID, electionCycle);
		}
	}
}
