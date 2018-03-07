using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
	/// <summary>
	/// Represents the method that will handle <see cref="AccountManagementControl"/> events.
	/// </summary>
	/// <param name="e">The source of the event.</param>
	/// <param name="sender">A <see cref="AccountManagementEventArgs"/> that contains the event data.</param>
	public delegate void AccountManagementEventHandler(object sender, AccountManagementEventArgs e);
}
