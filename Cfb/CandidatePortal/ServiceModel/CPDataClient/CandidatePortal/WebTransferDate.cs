using System;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to web transfer dates by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a web transfer date for a specific election cycle.
		/// </summary>
		/// <returns>A web transfer date for a specific election cycle.</returns>
		public DateTime? GetWebTransferDate(string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetWebTransferDate(electionCycle); }
		}
	}
}