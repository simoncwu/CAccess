using System.Collections.Specialized;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Defines methods for reading and passing values via a query string.
	/// </summary>
	public interface IQueryStringAware
	{
		/// <summary>
		/// Serializes properties to a query string.
		/// </summary>
		/// <returns>A <see cref="NameValueCollection"/> query string containing properties as values.</returns>
		NameValueCollection GetQueryString();

		/// <summary>
		/// Parses and sets properties from values in a query string.
		/// </summary>
		/// <param name="queryString">The query string to parse.</param>
		void ParseQueryString(NameValueCollection queryString);
	}
}
