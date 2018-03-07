using System;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to extension request data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a specific extension request.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate who requested the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension was requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review for which the extension was requested.</param>
		/// <param name="iteration">The iteration of the extension request.</param>
		/// <returns>An <see cref="ExtensionRequest"/> representing the specified extension request if found; otherwise, false.</returns>
		public ExtensionRequest GetExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, byte iteration)
		{
			using (DataClient client = new DataClient()) { return client.GetExtensionRequest(candidateID, electionCycle, type, reviewNumber, iteration); }
		}

		/// <summary>
		/// Creates a new extension request and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate requesting the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension is being requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review for which the extension is being requested.</param>
		/// <param name="date">The date of the extension request.</param>
		/// <param name="requestedDueDate">The requested extension due date.</param>
		/// <param name="reason">The reason for the extension.</param>
		/// <returns>A new <see cref="ExtensionRequest"/> object if the extension request was added successfully; otherwise, null.</returns>
		public ExtensionRequest AddExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, DateTime date, DateTime requestedDueDate, string reason)
		{
			using (DataClient client = new DataClient()) { return client.AddExtensionRequest(candidateID, electionCycle, type, reviewNumber, date, requestedDueDate, reason); }
		}
	}
}