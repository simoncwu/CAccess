using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A history of a candidate's public funds determinations.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class PublicFundsHistory : IEnumerable<PublicFundsDetermination>
	{
		/// <summary>
		/// A collection of a candidate's public funds determinations.
		/// </summary>
		[DataMember(Name = "Determinations")]
		private readonly List<PublicFundsDetermination> _determinations;

		/// <summary>
		/// Gets a collection of a candidate's public funds determinations.
		/// </summary>
		public List<PublicFundsDetermination> Determinations
		{
			get { return _determinations; }
		}

		/// <summary>
		/// Gets or sets the determination at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the determination to get or set.</param>
		/// <returns>The determination at the specified index.</returns>
		public PublicFundsDetermination this[int index]
		{
			get { return _determinations[index]; }
			set { _determinations[index] = value; }
		}

		/// <summary>
		/// Gets the total number of payments made to the candidate.
		/// </summary>
		public int PaymentCount
		{
			get { return _determinations.Where(d => d.PaymentIssued).Count(); }
		}

		/// <summary>
		/// Gets the total amount paid to the candidate, in U.S. dollars and cents.
		/// </summary>
		public decimal TotalPaymentAmount
		{
			get { return _determinations.Where(d => d.PaymentIssued).Sum(d => d.PaymentAmount); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PublicFundsHistory"/> class that is empty and has the specified initial capacity.
		/// </summary>
		/// <param name="capacity">The number of elements that the new history can initially store.</param>
		public PublicFundsHistory(int capacity)
		{
			_determinations = new List<PublicFundsDetermination>(capacity);
		}

		#region IEnumerable<FundsDetermination> Members

		/// <summary>
		/// Returns an enumerator that iterates through the determination history.
		/// </summary>
		/// <returns>An <see cref="IEnumerator{T}"/> that can be used to iterate through the history.</returns>
		public IEnumerator<PublicFundsDetermination> GetEnumerator()
		{
			return _determinations.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <summary>
		/// Returns an enumerator that iterates through the determination history.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the history.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _determinations.GetEnumerator();
		}

		#endregion

				/// <summary>
		/// Retrieves a history of public funds determinations for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose public funds determination history is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A history of public funds determinations for the specified candidate and election cycle.</returns>
		public static PublicFundsHistory GetPublicFundsHistory(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetPublicFundsHistory(candidateID, electionCycle);
		}
	}
}
