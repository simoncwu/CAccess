using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;

namespace Cfb.CandidatePortal.ServiceProcess.CPReminderDistributionService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new ReminderDistributionService() 
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
