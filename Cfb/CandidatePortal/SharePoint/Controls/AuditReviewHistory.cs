using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Web;
using Microsoft.SharePoint.WebControls;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
    /// <summary>
    /// A custom web server control for displaying audit review history.
    /// </summary>
    public abstract class AuditReviewHistory<T> : GroupGridView where T : AuditReviewBase
    {
        #region Primary key names for identifying audit review records

        private const string CommitteeKeyName = "CommitteeID";

        private const string ReviewNumberKeyName = "Number";

        #endregion

        #region Property values for audit review number column

        private readonly string _numberHeaderText;
        private readonly string _numberCssClass;

        #endregion

        #region Property values for review sent column

        private readonly string _sentHeaderText;
        private readonly string _sentCssClass;
        private readonly string _sentNullDisplayText;

        #endregion

        /// <summary>
        /// Gets the format for response received status.
        /// </summary>
        protected string ResponseReceivedFormat
        {
            get { return Properties.Resources.ResponseReceivedFormat; }
        }

        /// <summary>
        /// Gets the text for indicating that a response has not yet been received.
        /// </summary>
        protected string ResponseNotReceivedText
        {
            get { return Properties.Resources.ResponseNotReceivedText; }
        }

        /// <summary>
        /// Gets the text for indicating that no response is required.
        /// </summary>
        protected string NoResponseRequiredText
        {
            get { return Properties.Resources.NoResponseRequiredText; }
        }

        /// <summary>
        /// Gets the format for dates.
        /// </summary>
        protected string DateFormat
        {
            get { return Properties.Resources.DateFormat; }
        }

        /// <summary>
        /// Gets or sets the collection from which the control retrieves its list of audit reviews.
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [Description("The collection of audit reviews to display.")]
        public new IEnumerable<T> DataSource
        {
            get { return base.DataSource as IEnumerable<T>; }
            set { base.DataSource = value; }
        }

        /// <summary>
        /// Gets or sets the collection of IDs for matching audit review CMO messages.
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [Description("The collection of IDs for matching audit review CMO messages.")]
        public IDictionary<byte, string> MessageIDDataSource { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditReviewHistory{T}"/> control.
        /// </summary>
        internal AuditReviewHistory()
        {
            this.CssClass += " auditGrid";
            this.EmptyDataText = Properties.Resources.EmptyDataText;

            // set member values based on audit review type
            Type t = typeof(T);
            if (t == typeof(ComplianceVisit))
            {
                _numberHeaderText = "Visit";
                _numberCssClass = "cp-VisitNumber";
                _sentHeaderText = "Letter Sent";
                _sentCssClass = "cp-LetterSent";
                _sentNullDisplayText = Properties.Resources.NoLetterSentText;
            }
            else if (t == typeof(StatementReview))
            {
                _numberHeaderText = "Statement";
                _numberCssClass = "cp-StatementNumber";
                _sentHeaderText = "Letter Sent";
                _sentCssClass = "cp-LetterSent";
                _sentNullDisplayText = Properties.Resources.NoLetterSentText;
            }
            else
            {
                _numberHeaderText = "Statement";
                _numberCssClass = "cp-StatementNumber";
                _sentHeaderText = "Review Sent";
                _sentCssClass = "cp-ReviewSent";
                _sentNullDisplayText = Properties.Resources.NoReviewSentText;
            }
            this.DataKeyNames = new[] { CommitteeKeyName, ReviewNumberKeyName };
        }

        /// <summary>
        /// Raises the <see cref="GridView.OnInit"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs args)
        {
            base.OnInit(args);
            this.CssClass += " cp-AuditReviewHistory";
        }

        /// <summary>
        /// Creates the control hierarchy that is used to render a composite data-bound control.
        /// </summary>
        protected override void CreateChildControls()
        {
            BoundField field;

            // review number
            field = new BoundField()
            {
                DataField = "Number",
                HeaderText = _numberHeaderText
            };
            field.HeaderStyle.CssClass = field.ItemStyle.CssClass = _numberCssClass;
            this.Columns.Add(field);

            // compliance visit appointment
            if (typeof(T) == typeof(ComplianceVisit))
            {
                field = new BoundField()
                {
                    DataField = "Date",
                    DataFormatString = this.DateFormat,
                    HeaderText = "Appointment",
                    HtmlEncode = false
                };
                field.HeaderStyle.CssClass = field.ItemStyle.CssClass = "cp-Appointment";
                this.Columns.Add(field);
            }

            // sent date
            field = new BoundField()
            {
                DataField = "SentDate",
                DataFormatString = this.DateFormat,
                HeaderText = _sentHeaderText,
                HtmlEncode = false,
                NullDisplayText = _sentNullDisplayText
            };
            field.HeaderStyle.CssClass = field.ItemStyle.CssClass = _sentCssClass;
            this.Columns.Add(field);

            // response due date
            field = new BoundField()
            {
                DataField = "ResponseDeadline",
                DataFormatString = this.DateFormat,
                HeaderText = "Response Due",
                HtmlEncode = false,
                NullDisplayText = this.NoResponseRequiredText
            };
            field.HeaderStyle.CssClass = field.ItemStyle.CssClass = "cp-ResponseDue";
            this.Columns.Add(field);

            // response status
            field = new BoundField()
            {
                DataField = "ResponseReceivedDate",
                DataFormatString = this.DateFormat,
                HeaderText = "Response Status",
                NullDisplayText = this.NoResponseRequiredText
            };
            field.HeaderStyle.CssClass = field.ItemStyle.CssClass = "cp-ResponseStatus";
            this.Columns.Add(field);

            // view review
            field = new BoundField();
            field.HeaderStyle.CssClass = field.ItemStyle.CssClass = "cp-DownloadLink";
            this.Columns.Add(field);
        }

        /// <summary>
        /// Rebinds the <see cref="AuditReviewHistory{T}"/> control to its data after the DataMember, DataSource, or DatasourceID property is changed.
        /// </summary>
        protected override void OnDataPropertyChanged()
        {
            if (this.DataSource.GroupBy(t => t.CommitteeID).Count() > 1)
                this.GroupBy("Committee", "CommitteeID", "CommitteeName");
            base.OnDataPropertyChanged();
        }

        /// <summary>
        /// Raises the <see cref="GridView.RowDataBound"/> event.
        /// </summary>
        /// <param name="e">A <see cref="GridViewRowEventArgs"/> that contains the event data.</param>
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DoingBusinessReview dbr = e.Row.DataItem as DoingBusinessReview;
                StatementReview sr = e.Row.DataItem as StatementReview;
                ComplianceVisit cv = e.Row.DataItem as ComplianceVisit;
                AuditReviewBase review = e.Row.DataItem as AuditReviewBase;
                int responseReceivedColumnIndex = cv != null ? 3 : 2;
                int numColumnIndex = 0;
                if (this.AllowGrouping)
                {
                    responseReceivedColumnIndex++;
                    numColumnIndex++;
                }

                // number details
                if (dbr != null)
                {
                    e.Row.Cells[numColumnIndex].Text = string.Format("{0} ({1:d})", dbr.Number, dbr.Statement.DueDate);
                }
                else if (sr != null)
                {
                    e.Row.Cells[numColumnIndex].Text = string.Format("{0} ({1:d})", sr.Number, sr.Statement.DueDate);
                }
                if (review != null)
                {
                    if (review.ResponseRequired)
                    {
                        // response deadline
                        ResponseDeadlineBase deadline = review.ResponseDeadline;
                        if (deadline.ExtensionGranted)
                        {
                            if (deadline.ExtensionDate.HasValue)
                            {
                                e.Row.Cells[responseReceivedColumnIndex].Text = string.Format("{0:MM/dd/yyyy}<br />Originally due {1:MM/dd/yyyy}<br />Extension granted {2:MM/dd/yyyy}", deadline.Date, deadline.OriginalDueDate, deadline.ExtensionDate);
                            }
                            else
                            {
                                e.Row.Cells[responseReceivedColumnIndex].Text = string.Format("{0:d}<br />Originally due {1:d}<br />Extensions granted: {2}", deadline.Date, deadline.OriginalDueDate, deadline.ExtensionNumber);
                            }
                        }
                        else
                        {
                            e.Row.Cells[responseReceivedColumnIndex].Text = string.Format(this.DateFormat, deadline.Date);
                        }

                        // response status
                        e.Row.Cells[responseReceivedColumnIndex + 1].Text = review.ResponseReceived ? (dbr != null ? Properties.Resources.ResponseReceivedText : string.Format(this.ResponseReceivedFormat, review.ResponseReceivedDate)) : this.ResponseNotReceivedText;

                        // response extension - disabled indefinitely
                        //string.Format("{0} <a href=\"{1}\" title=\"Request Response Extension\">(Request Extension)</a>", this.ResponseNotReceivedText, PageUrlManager.GetExtensionRequestUrl(ExtensionType.StatementReview, sr.StatementNumber));
                        //string.Format("{0} {1}", this.ResponseNotReceivedText, MakeExtensionRequestLink(ExtensionType.ComplianceVisitReview, cv.Number));
                    }
                    else
                    {
                        e.Row.Cells[responseReceivedColumnIndex].Text = e.Row.Cells[responseReceivedColumnIndex + 1].Text = this.NoResponseRequiredText;
                    }

                    // add link to view CMO message if available
                    string messageID;
                    if (this.MessageIDDataSource != null && this.MessageIDDataSource.TryGetValue(review.Number, out messageID))
                        e.Row.Cells[responseReceivedColumnIndex + 2].Text = string.Format("<a class=\"pdf-file\" href=\"{0}\" title=\"{1}\">{1}</a>", PageUrlManager.GetMessageUrl(messageID), "View Review");
                }
            }
        }

        /// <summary>
        /// Finds the item specified by the current <see cref="HttpRequest"/> context and, if found, selects it.
        /// </summary>
        protected override void SelectHttpRequestedItem()
        {
            char? id = Page.Request.GetCommitteeID();
            byte? number = Page.Request.GetReviewNumber();
            if (!id.HasValue || !number.HasValue)
                return;
            foreach (SPGridViewRow row in this.Rows)
            {
                DataKey key = this.DataKeys[row.RowIndex];
                if (key != null)
                {
                    if (id.Value.ToString().Equals(key.Values[CommitteeKeyName].ToString(), StringComparison.InvariantCultureIgnoreCase) && number.Value == (byte)key.Values[ReviewNumberKeyName])
                    {
                        this.SelectedIndex = row.RowIndex;
                        return;
                    }
                }
            }
        }
    }
}
