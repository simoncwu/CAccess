using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Provides access to C-Access providers.
	/// </summary>
	public class CPProviders
	{
		/// <summary>
		/// Gets or sets the data provider.
		/// </summary>
		public static IDataProvider DataProvider { get; set; }

		/// <summary>
		/// Gets or sets the global settings provider.
		/// </summary>
		public static ISettingsProvider SettingsProvider { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		private CPProviders() { }
	}
}
