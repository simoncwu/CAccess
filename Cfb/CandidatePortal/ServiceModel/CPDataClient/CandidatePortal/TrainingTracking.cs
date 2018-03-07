using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to Doing Business data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves the current training tracking status for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose training tracking status is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The current training tracking status for the specified candidate and election cycle.</returns>
		public TrainingStatus GetTrainingStatus(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetTrainingStatus(candidateID, electionCycle); }
		}
	}
}