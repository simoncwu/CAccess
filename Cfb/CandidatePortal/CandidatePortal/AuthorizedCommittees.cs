using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a candidate's authorized committees.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class AuthorizedCommittees
	{
		/// <summary>
		/// Gets the principal committe authorized by the candidate.
		/// </summary>
		public AuthorizedCommittee PrincipalCommittee
		{
			get { return _committees.Values.FirstOrDefault(c => c.IsPrincipal); }
		}

		/// <summary>
		/// Gets the collection of other non-principal committees authorized by the candidate.
		/// </summary>
		public IEnumerable<Committee> OtherCommittees
		{
			get { return _committees.Values.Where(c => !c.IsPrincipal); }
		}

		/// <summary>
		/// The complete collection of committees authorized by the candidate.
		/// </summary>
		[DataMember(Name = "Committees")]
		private Dictionary<char, AuthorizedCommittee> _committees;

		/// <summary>
		/// Gets the complete collection of committees authorized by the candidate.
		/// </summary>
		public Dictionary<char, AuthorizedCommittee> Committees
		{
			get { return _committees; }
		}

		/// <summary>
		/// Initializes a new, empty collection of committees authorized by the candidate with an initial capacity.
		/// </summary>
		/// <param name="capacity">The number of <see cref="AuthorizedCommittee"/> objects that the collection can initially store.</param>
		internal AuthorizedCommittees(int capacity)
		{
			_committees = new Dictionary<char, AuthorizedCommittee>(capacity);
		}

		/// <summary>
		/// Retrieves all authorized committees for a candidate in a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose authorized committees are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all authorized committees on record for the specified candidate and election cycle.</returns>
		public static AuthorizedCommittees GetAuthorizedCommittees(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetAuthorizedCommittees(candidateID, electionCycle);
		}
	}
}
