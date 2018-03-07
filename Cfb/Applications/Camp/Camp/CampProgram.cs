using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.CPSettings;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.ServiceModel.CPSecurityClient;

namespace Cfb.Camp
{
	static class CampProgram
	{
		/// <summary>
		/// The main top-level CAMP form.
		/// </summary>
		private static CampForm _camp;

		/// <summary>
		/// Gets the main top-level CAMP form.
		/// </summary>
		public static CampForm CampForm
		{
			get { return _camp; }
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += new ThreadExceptionEventHandler(CampExceptionHandler.Application_ThreadException);
			CPDataProvider provider = new CPDataProvider();
			CPProviders.DataProvider = provider;
			CmoProviders.DataProvider = provider;
			GC.KeepAlive(provider);
			GC.KeepAlive(CPProviders.SettingsProvider = new CPSettingsProvider());
			GC.KeepAlive(CPSecurity.Provider = new SecurityProvider());
			SettingsManager.Initialize();
			_camp = new CampForm();
			Application.Run(_camp);
		}
	}
}
