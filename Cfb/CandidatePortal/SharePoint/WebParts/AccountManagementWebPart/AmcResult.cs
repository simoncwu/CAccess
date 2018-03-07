using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
	/// <summary>
	/// Enumeration representing various result codes returnable from an <see cref="AccountManagementControl"/> to an <see cref="AccountManagementControl"/>.
	/// </summary>
	internal enum AmcResult : byte
	{
		/// <summary>
		/// General success.
		/// </summary>
		Success,
		/// <summary>
		/// General failure.
		/// </summary>
		Failure,
		GoToMain,
		GoToCreate
	}
}
