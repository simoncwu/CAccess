using System;
using System.Linq;
using System.Web.UI;
using Cfb.CandidatePortal.SharePoint.Controls;
using Microsoft.SharePoint.WebControls;

namespace Cfb.CandidatePortal.Web.WebApplication.Controls
{
    public partial class CPCalendarView : UserControl
    {
        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // set up the calendar
            this.CalendarView.ViewType = "Month";
            string day = Request.QueryString["CalendarDate"];
            DateTime selected = DateTime.Today;
            if (!string.IsNullOrEmpty(day))
            {
                if (DateTime.TryParse(Server.UrlDecode(day), out selected))
                {
                    this.CalendarView.SelectedDate = day;
                }
            }
            this.PreviousDateLink.NavigateUrl = string.Format(@"javascript:MoveToViewDate('{0:M\\u002\fd\\u002\fyyyy}', null);", selected.AddMonths(-1));
            this.NextDateLink.NavigateUrl = string.Format(@"javascript:MoveToViewDate('{0:M\\u002\fd\\u002\fyyyy}', null);", selected.AddMonths(1));
            this.HeaderDate.Text = selected.ToString("MMMM yyyy");
            this.CalendarView.DataSource = from i in CPProfile.CalendarEvents
                                           select i.ToSPCalendarItem();
            this.CalendarView.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangeDate(object sender, EventArgs e)
        {
            if (this.DateTimeControl.IsValid)
            {
                DateTime selected = this.DateTimeControl.SelectedDate;
                Response.Redirect(Page.AppRelativeVirtualPath + string.Format("?CalendarDate={0}%2F{1}%2F{2}&CalendarPeriod=Day", selected.Month, selected.Day, selected.Year));
            }
            else
            {
                this.DateTimeControl.ClearSelection();
            }
        }
    }
}