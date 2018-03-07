using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
    /// <summary>
    /// A Post Election Audit tolling event.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class TollingEvent : CPCalendarItem, ITollingEvent
    {
        #region ITollingEvent Members

        /// <summary>
        /// Gets or sets the type of tolling letter associated with the event.
        /// </summary>
        [DataMember]
        public TollingLetter TollingLetter { get; set; }

        /// <summary>
        /// Gets the type of tolling event.
        /// </summary>
        public TollingEventType TollingEventType
        {
            get { return this.TollingLetter != null && this.TollingLetter.TypeCode.IsExtensionType ? TollingEventType.Extension : TollingEventType.OutstandingResponse; }
        }

        /// <summary>
        /// Gets or sets the start of the tolling period.
        /// </summary>
        [DataMember]
        public DateTime TollingStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end of the tolling period.
        /// </summary>
        [DataMember]
        public DateTime? TollingEndDate { get; set; }

        /// <summary>
        /// Gets or sets the deadline for the response to the tolling event.
        /// </summary>
        [DataMember]
        public DateTime? TollingDueDate { get; set; }

        /// <summary>
        /// Gets or sets the reason the tolling event ended.
        /// </summary>
        [DataMember]
        public TollingEndReason TollingEndReason { get; set; }

        /// <summary>
        /// Gets or sets the tolling event number.
        /// </summary>
        [DataMember]
        public int TollingEventNumber { get; set; }

        /// <summary>
        /// Gets whether or not the tolling event is an extension event.
        /// </summary>
        public bool IsExtension
        {
            get { return this.TollingLetter != null && this.TollingLetter.IsExtension; }
        }

        /// <summary>
        /// Gets whether or not the tolling event is an inadequate response event.
        /// </summary>
        public bool IsInadequateResponse
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the number of the reference tolling event to which this event applies.
        /// </summary>
        [DataMember]
        public int ReferenceEventNumber { get; set; }

        #endregion

        /// <summary>
        /// Gets whether or not the tolling period has ended.
        /// </summary>
        public bool TollingEnded { get { return this.TollingEndDate.HasValue; } }

        /// <summary>
        /// Creates a new instance of the <see cref="TollingEvent"/> class.
        /// </summary>
        /// <param name="startDate">The start date of the tolling event.</param>
        /// <param name="endDate">The end date of the tolling event.</param>
        public TollingEvent(DateTime startDate, DateTime endDate)
            : base(startDate, endDate, CPCalendarItemType.TollingEvent)
        {
        }
    }
}
