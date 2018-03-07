using System;
using System.Linq;
using Cfb.CandidatePortal.SharePoint.Controls;
using Cfb.CandidatePortal.Web;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.WebApplication.Calendar
{
    public partial class Default : CPPage, ISecurePage
    {
        /// <summary>
        /// The currently selected date.
        /// </summary>
        private string _dateSelection;

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            this.CalendarView.ViewType = "Month";
            if (!Page.IsPostBack || !this.CalendarView.EnableViewState)
            {
                this.CalendarView.DataSource = from i in CPProfile.CalendarEvents
                                               select i.ToSPCalendarItem();
                this.CalendarView.DataBind();
            }
            _dateSelection = Request.QueryString["CalendarDate"];
            base.OnLoad(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            DateTime selectedDate;
            if (!string.IsNullOrEmpty(_dateSelection) && DateTime.TryParse(Server.UrlDecode(_dateSelection), out selectedDate))
            {
                this.CalendarView.SelectedDate = _dateSelection;
            }
            else
            {
                selectedDate = DateTime.Today;
                this.NavDate.Text = null;
            }
            this.PreviousDateLink.NavigateUrl = GetDateNavigationUrl(selectedDate.AddMonths(-1));
            this.NextDateLink.NavigateUrl = GetDateNavigationUrl(selectedDate.AddMonths(1));
            this.HeaderDate.Text = selectedDate.ToString("MMMM yyyy");
            base.OnPreRender(e);
        }

        /// <summary>
        /// Fires when the date navigation button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected void NavigateToDate(object sender, EventArgs e)
        {
            _dateSelection = this.NavDate.Text;
        }

        /// <summary>
        /// Retrieves a URL for navigating to a date in the calendar.
        /// </summary>
        /// <param name="date">The target navigation date.</param>
        /// <returns>A URL for viewing the calendar for the specified date.</returns>
        private string GetDateNavigationUrl(DateTime date)
        {
            return Page.AppRelativeVirtualPath + string.Format("?CalendarDate={0}%2F{1}%2F{2}", date.Month, date.Day, date.Year);
        }
    }
}