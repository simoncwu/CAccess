using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to Campaign Messages Online settings data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Creates a new settings entry for a candidate and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate to whom this preference applies.</param>
		/// <param name="isPaperless">Whether or not the candidate has a paperless preference.</param>
		/// <param name="username">The C-Access username of the updating user.</param>
		/// <returns>A new <see cref="CmoSettings"/> object if the settings were added successfully; otherwise, null.</returns>
		public CmoSettings AddCmoSettings(string candidateID, bool isPaperless, string username)
		{
			using (DataClient client = new DataClient())
			{
				return client.AddCmoSettings(candidateID, isPaperless, username);
			}
		}

		/// <summary>
		/// Gets the C-Access Campaign Message Online settings for a specific candidate.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate to query.</param>
		/// <returns>The specified candidate's Campaign Message Online settings if one exists; otherwise, null.</returns>
		public CmoSettings GetCmoSettings(string candidateID)
		{
			using (DataClient client = new DataClient())
			{
				return client.GetCmoSettings(candidateID);
			}
		}

		/// <summary>
		/// Updates this instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="settings">The settings to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		public bool Update(CmoSettings settings)
		{
			using (DataClient client = new DataClient())
			{
				return client.UpdateCmoSettings(settings);
			}
		}
	}
}