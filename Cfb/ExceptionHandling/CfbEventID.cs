using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.ExceptionHandling
{
	/// <summary>
	/// Enumeration of known CFB event IDs.
	/// </summary>
	public enum CfbEventID : int
	{
		/// <summary>
		/// No event.
		/// </summary>
		None = 0,
		/// <summary>
		/// Informational event.
		/// </summary>
		Informational = 1,
		/// <summary>
		/// E-mail send failure event.
		/// </summary>
		EmailFailure = 1001
	}
}
