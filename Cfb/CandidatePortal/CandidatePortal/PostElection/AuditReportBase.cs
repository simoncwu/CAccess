using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Provides the abstract base class for tracking the details of a Post Election Audit Report for a single candidate and election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public abstract class AuditReportBase : CPCalendarItem
	{
		/// <summary>
		/// Gets the type of post election audit report or letter.
		/// </summary>
		public abstract AuditReportType AuditReportType { get; }

		/// <summary>
		/// A collection of tolling events for the post election audit report or letter.
		/// </summary>
		private TollingEventCollection _tollingEvents;

		/// <summary>
		/// Gets a collection of tolling events for the post election audit report or letter.
		/// </summary>
		public TollingEventCollection TollingEvents
		{
			get { return _tollingEvents ?? (_tollingEvents = new TollingEventCollection()); }
		}

		/// <summary>
		/// A collection of extension tolling events for the post election audit report or letter.
		/// </summary>
		private TollingEventCollection _extensions;

		/// <summary>
		/// Gets a collection of extension tolling events for the post election audit report or letter.
		/// </summary>
		public TollingEventCollection Extensions
		{
			get { return _extensions ?? (_extensions = new TollingEventCollection()); }
		}

		/// <summary>
		/// Gets the number of tolling days that affect issuance of the post election audit report.
		/// </summary>
		public int TollingDaysIncurred { get { return this.TollingEvents.Sum(t => t.GetTollingDays()); } }

		/// <summary>
		/// Gets whether or not there are any tolling events for the post election audit report or letter.
		/// </summary>
		public bool HasTolling
		{
			get { return this.TollingEvents != null && this.TollingEvents.Count > 0; }
		}

		/// <summary>
		/// Gets the due date for the response to the audit report, if required.
		/// </summary>
		public DateTime? DueDate
		{
			get { return this.ResponseDeadline == null ? null : this.ResponseDeadline.Date as DateTime?; }
		}

		/// <summary>
		/// Gets or sets the date the report was first sent.
		/// </summary>
		public DateTime SentDate
		{
			get { return this.EndDate; }
			set { this.StartDate = this.EndDate = value; }
		}

		/// <summary>
		/// Gets the date the response to the audit report was actually received.
		/// </summary>
		public DateTime? ResponseReceivedDate
		{
			get { return this.ResponseDeadline == null ? null : this.ResponseDeadline.ResponseReceivedDate; }
		}

		/// <summary>
		/// Gets or sets the date the audit report response was submitted.
		/// </summary>
		[DataMember]
		public DateTime? ResponseSubmittedDate { get; set; }

		/// <summary>
		/// Gets the official date of the response to the audit report.
		/// </summary>
		public DateTime? ResponseDate
		{
			get { return this.ResponseSubmittedDate.HasValue ? this.ResponseSubmittedDate : this.ResponseReceivedDate; }
		}

		/// <summary>
		/// Gets or sets the date the campaign received the report, if available from a delivery receipt confirmation.
		/// </summary>
		[DataMember]
		public DateTime? ReceiptDate { get; set; }

		/// <summary>
		/// Gets or sets the date a second report was sent, if available.
		/// </summary>
		[DataMember]
		public DateTime? SecondSentDate { get; set; }

		/// <summary>
		/// Gets or sets the date the campaign received a second report, if available from a delivery receipt confirmation.
		/// </summary>
		[DataMember]
		public DateTime? SecondReceiptDate { get; set; }

		/// <summary>
		/// Gets whether or not a second request was sent for the report.
		/// </summary>
		public bool IsSecondRequest
		{
			get { return this.SecondSentDate.HasValue; }
		}

		/// <summary>
		/// Gets or sets the deadline for the response to the audit report, if requested.
		/// </summary>
		[DataMember]
		public ResponseDeadlineBase ResponseDeadline { get; set; }

		/// <summary>
		/// Gets whether or not a response to the audit report was received.
		/// </summary>
		public bool ResponseReceived
		{
			get { return this.ResponseDate.HasValue; }
		}

		/// <summary>
		/// Gets the latest response deadline for the report, or its inadequate response event.
		/// </summary>
		public ResponseDeadlineBase LastReponseDeadline
		{
			get
			{
				if (this.InadequateNotice != null && this.InadequateNotice.ResponseDeadline != null)
					return this.InadequateNotice.ResponseDeadline;
				else if (this.ResponseDeadline != null)
					return this.ResponseDeadline;
				else
					return null;
			}
		}

		/// <summary>
		/// Gets the latest extension date in effect for the report.
		/// </summary>
		public DateTime? ExtensionDate
		{
			get { return this.Extensions == null || this.Extensions.Count == 0 ? null : (DateTime?)_extensions.Max(e => e.TollingDueDate.Value); }
		}

		/// <summary>
		/// Gets whether or not the report is an inadequate response notice.
		/// </summary>
		public bool IsInadequateNotice
		{
			get
			{
				switch (this.AuditReportType)
				{
					case AuditReportType.IdrInadequateResponse:
					case AuditReportType.IdrAdditionalInadequateResponse:
					case AuditReportType.DarInadequateResponse:
					case AuditReportType.DarAdditionalInadequateResponse:
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Gets the most recent inadequate response event for the report.
		/// </summary>
		public InadequateEventBase InadequateNotice
		{
			get
			{
				Func<ITollingEvent, bool> HasInadequateScope = delegate(ITollingEvent t)
				{
					if (t == null || t.TollingLetter == null)
						return false;
					switch (this.AuditReportType)
					{
						case AuditReportType.InitialDocumentationRequest:
							return t.TollingLetter.HasIdrInadequateScope;
						case AuditReportType.DraftAuditReport:
							return t.TollingLetter.HasDarInadequateScope;
						default:
							return false;
					}
				};
				var matches = from t in this.TollingEvents
							  where HasInadequateScope(t) && t.IsInadequateResponse
							  select t;
				return matches.OrderByDescending(t => t.TollingDueDate).FirstOrDefault() as InadequateEventBase;
			}
		}

		/// <summary>
		/// Initializes a new <see cref="AuditReportBase"/> for a specific date and type.
		/// </summary>
		/// <param name="date">The date the report was sent.</param>
		/// <param name="type">The type of calendar event.</param>
		internal AuditReportBase(DateTime date, CPCalendarItemType type)
			: base(date.Date, type)
		{
		}

		/// <summary>
		/// Retrieves a <see cref="CPCalendarItem"/> representing the second sent instance of the report.
		/// </summary>
		/// <returns>A <see cref="CPCalendarItem"/> representing the second sent instance of the report if one was sent; otherwise, null.</returns>
		public CPCalendarItem GetSecondSentCalendarItem()
		{
			if (this.SecondSentDate.HasValue)
			{
				return new CPCalendarItem(this.SecondSentDate.Value, this.CalendarItemType);
			}
			return null;
		}

		/// <summary>
		/// Populates the audit report with a collection of tolling events.
		/// </summary>
		/// <param name="events">The collection of events to load into the audit report.</param>
		public void LoadTollingEvents(ICollection<ITollingEvent> events)
		{
			if (events != null)
			{
				foreach (var evt in events)
				{
					if (evt != null)
					{
						this.TollingEvents.Add(evt);
						if (evt.IsExtensionFor(this))
						{
							this.Extensions.Add(evt);
						}
					}
				}
				DateTime? dueDate = this.ExtensionDate;
				ResponseDeadlineBase deadline = this.ResponseDeadline;
				if (dueDate.HasValue && deadline != null && dueDate.Value != deadline.Date)
				{
					deadline.GrantExtension(dueDate.Value, null, null);
				}
			}
		}

		/// <summary>
		/// Retrieves a collection of post election audit events for display in a calendar.
		/// </summary>
		/// <returns>A collection of post election audit events for display in a calendar.</returns>
		public IEnumerable<CPCalendarItem> GetCalendarEvents()
		{
			List<CPCalendarItem> items = new List<CPCalendarItem>();
			ResponseDeadlineBase deadline = this.ResponseDeadline;
			if (deadline != null)
				items.Add(deadline);
			InadequateEventBase inadequate = this.InadequateNotice;
			if (inadequate != null)
				items.AddRange(inadequate.GetCalendarEvents());
			return items;
		}
	}
}
