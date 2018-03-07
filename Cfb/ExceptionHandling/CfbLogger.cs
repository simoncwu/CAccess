using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Cfb.ExceptionHandling
{
	/// <summary>
	/// Utility for writing log entries.
	/// </summary>
	public sealed class CfbLogger
	{
		/// <summary>
		/// Private constructor to prevent class instantiation.
		/// </summary>
		private CfbLogger() { }

		/// <summary>
		/// Writes a new log entry.
		/// </summary>
		/// <param name="entry">The log entry to write.</param>
		public static void Write(CfbLogEntry entry)
		{
			if (Logger.IsLoggingEnabled() && Logger.ShouldLog(entry))
			{
				Logger.Write(entry);
			}
		}
	}
}
