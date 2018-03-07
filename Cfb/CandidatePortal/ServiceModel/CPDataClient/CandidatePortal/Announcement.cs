using System;
using System.Collections.Generic;
using Cfb.CandidatePortal.SharePoint;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to announcement data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a collection of unexpired announcements filtered by election cycle.
		/// </summary>
		/// <param name="electionCycle">The election cycle to filter results by, or an empty string to retrieve announcements across all elections.</param>
		/// <returns>A collection of unexpired announcements filtered by the election cycle specified.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="electionCycle"/> is null.</exception>
		public IEnumerable<Announcement> GetAnnouncements(string electionCycle)
		{
			return AnnouncementsClient.GetAnnouncements(electionCycle);
		}

		/// <summary>
		/// Retrieves an announcement by unique ID.
		/// </summary>
		/// <param name="id">The unique ID of the announcement to retrieve.</param>
		/// <returns>The announcement matching the ID specified if found; otherwise, null.</returns>
		public Announcement GetAnnouncement(string id)
		{
			return AnnouncementsClient.GetAnnouncement(id);
		}

		/// <summary>
		/// Retrieves a filing date announcement by statement number and election cycle.
		/// </summary>
		/// <param name="statementNumber">The filing statement number to search for.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The announcement matching the filing statement number and election cycle specified if found; otherwise, null.</returns>
		public Announcement GetFilingDateAnnouncement(byte statementNumber, string electionCycle)
		{
			return AnnouncementsClient.GetFilingDateAnnouncement(statementNumber, electionCycle);
		}
	}
}