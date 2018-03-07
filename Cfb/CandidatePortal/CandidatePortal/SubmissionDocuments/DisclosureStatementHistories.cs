using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's complete campaign filing disclosure statement history.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class DisclosureStatementHistories
	{
		/// <summary>
		/// The inner collection of disclosure statement submissions, grouped by committee ID and statement number.
		/// </summary>
		[DataMember(Name = "Submissions")]
		private readonly SortedDictionary<string, DisclosureStatementHistory> _submissions;

		/// <summary>
		/// The inner collection of disclosure statement submissions, grouped by committee ID and statement number.
		/// </summary>
		public SortedDictionary<string, DisclosureStatementHistory> Submissions
		{
			get { return _submissions; }
		}

		/// <summary>
		/// The numbers of the disclosure statements filed by the campaign, grouped by committee ID.
		/// </summary>
		[DataMember(Name = "CommitteeStatements")]
		private readonly Dictionary<char, Dictionary<byte, Statement>> _committeeStatements;

		/// <summary>
		/// Gets the numbers of the disclosure statements filed by the campaign, grouped by committee ID.
		/// </summary>
		public Dictionary<char, Dictionary<byte, Statement>> CommitteeStatements
		{
			get { return _committeeStatements; }
		}

		/// <summary>
		/// The names of the committees that have filed disclosure statements, indexed by committee ID.
		/// </summary>
		[DataMember(Name = "CommitteeNames")]
		private readonly Dictionary<char, string> _committeeNames;

		/// <summary>
		/// Gets the names of the committees that have filed disclosure statements, indexed by committee ID.
		/// </summary>
		public Dictionary<char, string> CommitteeNames
		{
			get { return _committeeNames; }
		}

		/// <summary>
		/// Gets the number of disclosure statements actually contained in the collection.
		/// </summary>
		public int Count
		{
			get { return _submissions.Count; }
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="DisclosureStatementHistory"/> records.
		/// </summary>
		public DisclosureStatementHistories()
		{
			_submissions = new SortedDictionary<string, DisclosureStatementHistory>();
			_committeeStatements = new Dictionary<char, Dictionary<byte, Statement>>();
			_committeeNames = new Dictionary<char, string>();
		}

		/// <summary>
		/// Combines a committee ID and statement number into a key for indexing a <see cref="DisclosureStatementHistories"/> collection.
		/// </summary>
		/// <param name="committeeID">A committee CFIS ID.</param>
		/// <param name="statementNumber">A disclosure statement number.</param>
		/// <returns>A key for indexing a <see cref="DisclosureStatementHistories"/> collection.</returns>
		public static string ToKey(char committeeID, byte statementNumber)
		{
			return string.Format("{0}{1}", committeeID, statementNumber);
		}

		/// <summary>
		/// Retrieves a generic collection of all filing disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filing disclosure statements on record for the specified candidate and election cycle.</returns>
		public static DisclosureStatementHistories GetDisclosureStatements(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetDisclosureStatements(candidateID, electionCycle);
		}
	}
}
