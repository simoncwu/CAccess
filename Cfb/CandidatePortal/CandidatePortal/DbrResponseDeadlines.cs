using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A collection of <see cref="DbrResponseDeadlines"/> objects.
	/// </summary>
	[CollectionDataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class DbrResponseDeadlines : CPCalendarItemCollection<DbrResponseDeadline>
	{
		/// <summary>
		/// Initializes a new collection of <see cref="DbrResponseDeadline"/> records.
		/// </summary>
		public DbrResponseDeadlines()
		{
		}
	}
}
