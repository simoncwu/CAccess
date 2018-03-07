using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// A custom web server control for displaying statement review history for a single candidate and election cycle.
	/// </summary>
	[ToolboxData("<{0}:StatementReviewHistory runat=server></{0}:StatementReviewHistory>")]
	public class StatementReviewHistory : AuditReviewHistory<StatementReview>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StatementReviewHistory"/> server control.
		/// </summary>
		public StatementReviewHistory()
		{
		}
	}
}
