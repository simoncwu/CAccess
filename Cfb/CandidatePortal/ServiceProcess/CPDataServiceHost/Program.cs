﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Cfb.CandidatePortal.ServiceProcess.CPDataServiceHost
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread()]
		static void Main()
		{
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new CPDataServiceHost() 
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
