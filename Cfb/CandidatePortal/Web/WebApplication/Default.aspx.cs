using System;

namespace Cfb.CandidatePortal.Web.WebApplication
{
	public partial class Default : CPPage, ISecurePage
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			// candidate profile
			ActiveCandidate candidate = CPProfile.ActiveCandidate;
			_profileSummary.DataSource = candidate;
			_profileSummary.DataBind();

			// announcements list
			_announcementsList.DataSource = CPProviders.DataProvider.GetAnnouncements(CPProfile.ElectionCycle);
			_announcementsList.DataBind();

			//if (_pressReleasesLink.EnableViewState && !this.IsPostBack)
			//{
			//    if (_pressReleasesLink.Visible = candidate != null)
			//    {
			//        _pressReleasesLink.FirstName = candidate.FirstName;
			//        _pressReleasesLink.LastName = candidate.LastName;
			//        _pressReleasesLink.MiddleInitial = candidate.MiddleInitial;
			//    }
			//}
		}
	}
}