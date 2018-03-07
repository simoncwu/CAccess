using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A collection of <see cref="CVResponseDeadline"/> objects.
	/// </summary>
	[CollectionDataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CVResponseDeadlines : CPCalendarItemCollection<CVResponseDeadline>
	{
		/// <summary>
		/// Initializes a new collection of <see cref="CVResponseDeadline"/> records.
		/// </summary>
		public CVResponseDeadlines()
		{
		}
	}
}
