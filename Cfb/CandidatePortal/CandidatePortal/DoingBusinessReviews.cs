using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A collection of Doing Business reviews.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class DoingBusinessReviews
	{
		/// <summary>
		/// The inner collection of Doing Business reviews.
		/// </summary>
		[DataMember(Name = "Reviews")]
		private readonly CPCalendarItemCollection<DoingBusinessReview> _reviews;

		/// <summary>
		/// Gets the inner collection of Doing Business reviews.
		/// </summary>
		public CPCalendarItemCollection<DoingBusinessReview> Reviews
		{
			get { return _reviews; }
		}

		/// <summary>
		/// A collection of Doing Business review response deadlines.
		/// </summary>
		[DataMember(Name = "ResponseDeadlines")]
		private readonly DbrResponseDeadlines _responseDeadlines;

		/// <summary>
		/// Gets a collection of Doing Business review response deadlines.
		/// </summary>
		public DbrResponseDeadlines ResponseDeadlines
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
		/// Initializes a new, empty collection of <see cref="DoingBusinessReview"/> records.
		/// </summary>
		internal DoingBusinessReviews()
		{
			_reviews = new CPCalendarItemCollection<DoingBusinessReview>();
			_responseDeadlines = new DbrResponseDeadlines();
		}

		/// <summary>
		/// Retrieves a collection of all completed Doing Business reviews on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Doing Business reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all completed Doing Business reviews for the specified candidate and election cycle.</returns>
		public static DoingBusinessReviews GetDoingBusinessReviews(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetDoingBusinessReviews(candidateID, electionCycle);
		}
	}
}
