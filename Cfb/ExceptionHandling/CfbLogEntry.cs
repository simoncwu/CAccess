using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Cfb.ExceptionHandling
{
	/// <summary>
	/// Represents a log message. Contains the common properties that are required for all log messages.
	/// </summary>
	public class CfbLogEntry : LogEntry
	{
		/// <summary>
		/// Event number or identifier.
		/// </summary>
		public new CfbEventID EventId
		{
			get
			{
				switch (base.EventId)
				{
					case (int)CfbEventID.EmailFailure:
						return CfbEventID.EmailFailure;
					case (int)CfbEventID.Informational:
						return CfbEventID.Informational;
					default:
						return CfbEventID.None;
				}
			}
			set { base.EventId = (int)value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CfbLogEntry"/> class.
		/// </summary>
		public CfbLogEntry()
		{
		}

		/// <summary>
		/// Creates a new instance of the <see cref="CfbLogEntry"/> class with a full set of constructor parameters.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Collection of category names used to route the log entry to a one or more sinks.</param>
		/// <param name="priority">Only messages must be above the minimum priority are processed.</param>
		/// <param name="eventId">Event number or identifier.</param>
		/// <param name="severity">Log entry severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
		/// <param name="title">Additional description of the log entry message.</param>
		/// <param name="properties">Dictionary of key/value pairs to record.</param>
		public CfbLogEntry(object message, ICollection<string> categories, int priority, CfbEventID eventId, TraceEventType severity, string title, IDictionary<string, object> properties)
			: base(message, categories, priority, (int)eventId, severity, title, properties)
		{
		}

		/// <summary>
		/// Creates a new instance of the <see cref="CfbLogEntry"/> class with a full set of constructor parameters.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
		/// <param name="priority">Only messages must be above the minimum priority are processed.</param>
		/// <param name="eventId">Event number or identifier.</param>
		/// <param name="severity">Log entry severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
		/// <param name="title">Additional description of the log entry message.</param>
		/// <param name="properties">Dictionary of key/value pairs to record.</param>
		public CfbLogEntry(object message, string category, int priority, CfbEventID eventId, TraceEventType severity, string title, IDictionary<string, object> properties)
			: base(message, category, priority, (int)eventId, severity, title, properties)
		{
		}
	}
}
