using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace Cfb.CandidatePortal.ServiceProcess.CPReminderDistributionService
{
	/// <summary>
	/// Service installer for C-Access reminder distribution.
	/// </summary>
	[RunInstaller(true)]
	public partial class ProjectInstaller : Installer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
		/// </summary>
		public ProjectInstaller()
		{
			InitializeComponent();
		}
	}
}
