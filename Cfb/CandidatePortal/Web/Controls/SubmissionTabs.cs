using System;
using System.Collections.Specialized;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// Control for displaying tabs for each of a candidate's submission document categories.
	/// </summary>
	public sealed class SubmissionTabs : ContentTabs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SubmissionTabs"/> class.
		/// </summary>
		public SubmissionTabs()
		{
		}

		/// <summary>
		/// Retrieves information regarding the submission tabs that are available for the current candidate context.
		/// </summary>
		/// <returns>A <see cref="NameValueCollection"/> containing mappings of tab names to destination page URLs for all submission types available within the current candidate context.</returns>
		public override NameValueCollection GetAvailableTabs()
		{
			NameValueCollection c = new NameValueCollection();
			if (CPProfile.HasFilerRegistrations)
				c.Add("Filer Registration", PageUrlManager.FilerRegistrationsPageUrl);
			if (CPProfile.HasCsmartIdsRequests)
				c.Add("C-SMART/IDS", PageUrlManager.CsmartIdsRequestsPageUrl);
			if (CPProfile.HasDisclosureStatements)
				c.Add("Disclosure Statement", PageUrlManager.DisclosureStatmentsPageUrl);
			if (CPProfile.HasCertifications)
				c.Add("Certification", PageUrlManager.CertificationsPageUrl);
			if (CPProfile.HasCoibReceipts)
				c.Add("COIB Receipt", PageUrlManager.CoibReceiptsPageUrl);
			if (CPProfile.HasPreElectionDisclosures)
				c.Add("Daily Pre-Election", PageUrlManager.PreElectionPageUrl);
			if (CPProfile.HasStatementsOfNeed)
				c.Add("Statement of Need", PageUrlManager.StatementsOfNeedPageUrl);
			if (CPProfile.HasTerminations)
				c.Add("Termination", PageUrlManager.TerminationPageUrl);
			return c;
		}
	}
}
