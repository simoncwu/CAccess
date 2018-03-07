using System;
using System.Collections.Specialized;
using System.Web.UI;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// Control for displaying tabs for each of a candidate's compliance item categories.
    /// </summary>
    [ToolboxData("<{0}:ProfileTabs runat=server></{0}:ProfileTabs>")]
    public sealed class ProfileTabs : ContentTabs
    {
        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// Retrieves information regarding the tabs that are available for the current candidate context.
        /// </summary>
        /// <returns>A <see cref="NameValueCollection"/> containing mappings of tab names to destination page URLs for all tabs to be rendered.</returns>
        public override NameValueCollection GetAvailableTabs()
        {
            NameValueCollection c = new NameValueCollection();
            c.Add("Candidate Profile", PageUrlManager.CandidateProfilePageUrl);
            c.Add("Committee Profile", PageUrlManager.CommitteeProfilePageUrl);
            c.Add("Financial Summary", PageUrlManager.FinancialSummaryPageUrl);
            c.Add("Campaign Training", PageUrlManager.TrainingTrackingPageUrl);
            return c;
        }
    }
}
