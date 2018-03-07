using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// A custom web server control for displaying Doing Business history for a single candidate.
	/// </summary>
	[ToolboxData("<{0}:DoingBusinessHistory runat=server></{0}:DoingBusinessHistory>")]
	public class DoingBusinessHistory : AuditReviewHistory<DoingBusinessReview>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DoingBusinessHistory"/> server control.
		/// </summary>
		public DoingBusinessHistory()
		{
		}
	}
}
