using System;
using System.Collections.Specialized;
using System.Web.UI;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// Control for displaying tabs for each of a candidate's compliance item categories.
	/// </summary>
	[ToolboxData("<{0}:AuditTabs runat=server></{0}:AuditTabs>")]
	public sealed class AuditTabs : ContentTabs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuditTabs"/> control.
		/// </summary>
		public AuditTabs()
		{
		}

		/// <summary>
		/// Retrieves information regarding the tabs that are available for the current candidate context.
		/// </summary>
		/// <returns>A <see cref="NameValueCollection"/> containing mappings of tab names to destination page URLs for all tabs to be rendered.</returns>
		public override NameValueCollection GetAvailableTabs()
		{
			NameValueCollection c = new NameValueCollection();
			if (CPProfile.HasStatementReviews)
				c.Add("Statement Reviews", PageUrlManager.StatementReviewsPageUrl);
			if (CPProfile.HasDoingBusinessReviews)
				c.Add("Doing Business", PageUrlManager.DoingBusinessPageUrl);
			if (CPProfile.HasComplianceVisits)
				c.Add("Compliance Visits", PageUrlManager.ComplianceVisitsPageUrl);
			if (CPProfile.HasThresholdHistory)
				c.Add("Threshold Status", PageUrlManager.ThresholdPageUrl);
			if (CPProfile.HasPublicFundsHistory)
				c.Add("Public Funds", PageUrlManager.PublicFundsPageUrl);
			if (CPProfile.HasPostElectionAudit)
				c.Add("Post Election", PageUrlManager.PostElectionPageUrl);
			return c;
		}
	}
}
