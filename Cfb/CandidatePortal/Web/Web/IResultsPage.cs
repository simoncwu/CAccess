using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web
{
	/// <summary>
	/// Defines methods allowing web pages and controls to register and show error/result messages.
	/// </summary>
	public interface IResultsPage
	{
		/// <summary>
		/// Gets whether or not the there are errors to be shown.
		/// </summary>
		bool HasError { get; }

		/// <summary>
		/// Gets or sets an error message to be displayed.
		/// </summary>
		string PageError { get; set; }

		/// <summary>
		/// Gets the web control used to display any errors.
		/// </summary>
		WebControl ErrorWebControl { get; }

		/// <summary>
		/// Gets whether or not the there are results to be shown.
		/// </summary>
		bool HasResult { get; }

		/// <summary>
		/// Gets or sets a result message to be displayed.
		/// </summary>
		string PageResult { get; set; }

		/// <summary>
		/// Gets the web control used to display any results.
		/// </summary>
		WebControl ResultWebControl { get; }

		/// <summary>
		/// Discards all errors and results from the page.
		/// </summary>
		void ClearMessages();
	}
}
