using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying the training history for a single course.
    /// </summary>
    [ToolboxData("<{0}:TrainingCourseHistoryControl runat=server></{0}:TrainingCourseHistoryControl>")]
    public class TrainingCourseHistoryControl : WebControl
    {
        // default property values
        private const string DefaultCompliantIndicatorText = "*";
        private const string DefaultEmptyDataText = "No records found for the current election cycle.";
        private const bool DefaultEmptyVisible = false;

        /// <summary>
        /// A collection of training sessions to display.
        /// </summary>
        private IEnumerable<TrainingSession> _sessions;

        /// <summary>
        /// Gets or sets the text to display when the data source does not contain any records.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultEmptyDataText)]
        [Description("The text to display when the data source does not contain any records.")]
        [Localizable(true)]
        public string EmptyDataText { get; set; }

        /// <summary>
        /// Gets or sets the path to an image to display to indicate "Yes" in a column.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display to indicate \"Yes\" in a column.")]
        [UrlProperty("*.gif;*.png")]
        public string YesImageUrl { get; set; }

        /// <summary>
        /// Gets or sets whether or not the control should be visible when the data source does not contain any records.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultEmptyVisible)]
        [Description("Whether or not the control should be visible when the data source does not contain any records.")]
        public bool EmptyVisible { get; set; }

        /// <summary>
        /// Gets or sets the path to an image to display to indicate "No" in a column.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display to indicate \"No\" in a column.")]
        [UrlProperty("*.gif;*.png")]
        public string NoImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of course sessions to display. An emtpy value will display all courses.
        /// </summary>
        [Category("Data")]
        [Description("The type of course sessions to display. An empty value will display all courses.")]
        public TrainingCourseType CourseType { get; set; }

        /// <summary>
        /// Gets or sets the text to display to indicate that a trainee counts towards the campaign's compliance status.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultCompliantIndicatorText)]
        [Description("The text to display to indicate that a trainee counts towards the campaign's compliance status.")]
        [Localizable(true)]
        public string CompliantIndicatorText { get; set; }

        /// <summary>
        /// Gets or sets the training status data source.
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        public TrainingStatus DataSource { get; set; }

        /// <summary>
        /// Gets whether or not the control contains any sessions.
        /// </summary>
        public bool HasSessions
        {
            get { return _sessions != null && _sessions.Any(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingCourseHistoryControl"/> control.
        /// </summary>
        public TrainingCourseHistoryControl()
        {
            this.CompliantIndicatorText = DefaultCompliantIndicatorText;
            this.EmptyDataText = DefaultEmptyDataText;
            this.EmptyVisible = DefaultEmptyVisible;
        }

        /// <summary>
        /// Raises the <see cref="DataBinding"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnDataBinding(EventArgs e)
        {
            var ds = this.DataSource;
            if (ds != null)
                _sessions = (this.CourseType == TrainingCourseType.Unknown ? ds.Sessions.Sessions.Values.ToList() : ds.Sessions[this.CourseType]);
            if (_sessions == null || _sessions.Count() == 0)
                this.Visible = this.EmptyVisible;
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // control container
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-ThresholdHistoryControl");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // control label
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-TrainingCourse ms-listviewtable");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-gb");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "8");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-gb");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-bold ms-descriptiontext");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("Course : {0}", this.CourseType == TrainingCourseType.Unknown ? "All Training Courses" : CPConvert.ToString(this.CourseType));
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders the HTML closing tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (!this.HasSessions)
            {
                if (this.EmptyVisible)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "8");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "emptydata");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(this.EmptyDataText);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                return;
            }

            // header row
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-viewheadertr");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "sidemargin compliant");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "trainee");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Trainee Name");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "relation");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Relationship");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "date");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Course Date");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "attcomp");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Attended");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "attcomp");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Completed");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "sidemargin");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.RenderEndTag();
            writer.RenderEndTag();

            // session rows
            var ds = this.DataSource;
            var transferDate = CPProviders.DataProvider.GetWebTransferDate(CPProfile.ElectionCycle) ?? DateTime.Today;
            foreach (var trainee in ds.Trainees.Values.OrderBy(t => t.FormalName))
            {
                foreach (var registration in from r in ds.Registrations.Registrations
                                             join s in _sessions on r.SessionID equals s.ID
                                             where r.TraineeID == trainee.ID
                                             orderby s.Date descending
                                             select r)
                {
                    var session = _sessions.Where(s => s.ID == registration.SessionID).First();
                    bool undetermined = session.Date >= transferDate.Date.AddDays(-1); // undetermined until a day after the last transfer
                    if (session.Date > DateTime.Now)
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "upcoming");
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    // margin
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "sidemargin compliant");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!undetermined && ds.ComplianceAchievedBy(registration))
                        writer.Write(this.CompliantIndicatorText);
                    writer.RenderEndTag();
                    // trainee name
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "trainee");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(trainee.GetFullName(true));
                    writer.RenderEndTag();
                    // relationship
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "relation");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(CPConvert.ToString(trainee.CampaignRelationship));
                    writer.RenderEndTag();
                    // course date
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "date");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(session.Date.ToShortDateString());
                    writer.RenderEndTag();
                    // attendance status
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "attcomp");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!undetermined)
                        RenderPassFail(writer, registration.Attended);
                    writer.RenderEndTag();
                    // completion status
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "attcomp");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!undetermined)
                        RenderPassFail(writer, registration.Completed);
                    writer.RenderEndTag();
                    // margin
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "sidemargin");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
            }
        }

        /// <summary>
        /// Renders a visual indicator representing a pass/fail status.
        /// </summary>
        /// <param name="pass">Wether or not to render a pass status instead of a fail status.</param>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        private void RenderPassFail(HtmlTextWriter writer, bool pass = false)
        {
            string imageUrl = pass ? this.YesImageUrl : this.NoImageUrl;
            string status = pass ? "Yes" : "No";
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                writer.Write(status);
                return;
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "16");
            writer.AddAttribute(HtmlTextWriterAttribute.Height, "16");
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, status);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, Page.ResolveUrl(imageUrl));
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
        }
    }
}
