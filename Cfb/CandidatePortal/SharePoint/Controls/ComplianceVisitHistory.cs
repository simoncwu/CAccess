using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// A custom web server control for displaying compliance visit history for a single candidate and election cycle.
	/// </summary>
	[ToolboxData("<{0}:ComplianceVisitHistory runat=server></{0}:ComplianceVisitHistory>")]
	public class ComplianceVisitHistory : AuditReviewHistory<ComplianceVisit>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ComplianceVisitHistory"/> server control.
		/// </summary>
		public ComplianceVisitHistory()
		{
		}
	}
}
