using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A collection of <see cref="SRResponseDeadline"/> objects.
	/// </summary>
	[CollectionDataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class SRResponseDeadlines : CPCalendarItemCollection<SRResponseDeadline>
	{
		/// <summary>
		/// Initializes a new collection of <see cref="SRResponseDeadline"/> records.
		/// </summary>
		public SRResponseDeadlines()
		{
		}
	}
}
