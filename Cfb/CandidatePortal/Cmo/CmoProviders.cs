using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// Provides access to C-Access Messages Online providers.
	/// </summary>
	public static class CmoProviders
	{
		/// <summary>
		/// Gets or sets the CMO data provider.
		/// </summary>
		public static ICmoDataProvider DataProvider { get; set; }

		/// <summary>
		/// Initializes a static <see cref="CmoProviders"/> object.
		/// </summary>
		static CmoProviders() { }
	}
}
