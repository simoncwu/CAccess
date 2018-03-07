using System;
using System.Collections.Generic;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Page request or querystring variable names for use in cross-page web part posting.
	/// </summary>
	internal struct PageVariables
	{
		/// <summary>
		/// Variable name for compliance deadline number.
		/// </summary>
		public const string ComplianceVisitNumber = "visit";

		/// <summary>
		/// Variable name for committee ID.
		/// </summary>
		public const string CommitteeID = "committee";

		/// <summary>
		/// Variable name for statement number.
		/// </summary>
		public const string StatementNumber = "stmt";

		/// <summary>
		/// Variable name for extension type.
		/// </summary>
		public const string ExtensionType = "type";

		/// <summary>
		/// Variable name for extension date.
		/// </summary>
		public const string ExtensionDate = "extDate";
	}
}
