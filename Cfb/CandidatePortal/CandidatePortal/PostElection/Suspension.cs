using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// A Post Election Audit suspension.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class Suspension
	{
		/// <summary>
		/// The date of the suspension.
		/// </summary>
		[DataMember(Name = "Date")]
		private readonly DateTime _date;

		/// <summary>
		/// Gets the date of the suspension.
		/// </summary>
		public DateTime Date
		{
			get { return _date; }
		}

		/// <summary>
		/// Gets or sets the name of the suspender.
		/// </summary>
		[DataMember]
		public string SuspenderName { get; set; }

		/// <summary>
		/// Gets or sets the reason for the suspension.
		/// </summary>
		[DataMember]
		public SuspensionReason SuspensionReason { get; set; }

		/// <summary>
		/// Creates a new instance of the <see cref="Suspension"/> class.
		/// </summary>
		/// <param name="date">The date of the suspension</param>
		public Suspension(DateTime date)
		{
			_date = date;
		}

		/// <summary>
		/// Retrieves Post Election Audit suspension information for a specfic candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose suspension information is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>Suspension information matching the specified criteria if found; otherwise, null.</returns>
		public static Suspension GetSuspension(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetSuspension(candidateID, electionCycle);
		}
	}
}
