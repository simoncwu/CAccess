using Cfb.ExceptionHandling.Properties;

namespace Cfb.ExceptionHandling
{
	/// <summary>
	/// Provides access to category names for CFB log entries.
	/// </summary>
	public sealed class CfbLogCategory
	{
		/// <summary>
		/// Category name for data access events.
		/// </summary>
		public static readonly string Data = Resources.DataAccessCategory;

		/// <summary>
		/// Category name for debugging.
		/// </summary>
		public static readonly string Debug = Resources.DebugCategory;

		/// <summary>
		/// Category name for e-mail events.
		/// </summary>
		public static readonly string Email = Resources.EmailCategory;

		/// <summary>
		/// Category name for tracing.
		/// </summary>
		public static readonly string Trace = Resources.TraceCategory;

		/// <summary>
		/// Category name for web service events.
		/// </summary>
		public static readonly string WebService = Resources.WebServiceCategory;

		/// <summary>
		/// Private constructor to prevent class instantiation.
		/// </summary>
		private CfbLogCategory() { }
	}
}
