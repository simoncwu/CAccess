using System.ServiceModel;
using System.ServiceProcess;
using Cfb.CandidatePortal.ServiceModel.CPSecurityService;

namespace Cfb.CandidatePortal.ServiceProcess.CPSecurityServiceHost
{
	partial class SecurityServiceHost : ServiceBase
	{
		private ServiceHost _serviceHost;

		public SecurityServiceHost()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			if (_serviceHost != null)
				_serviceHost.Close();
			_serviceHost = new ServiceHost(typeof(SecurityService));
			_serviceHost.Open();
		}

		protected override void OnStop()
		{
			if (_serviceHost != null)
			{
				_serviceHost.Close();
				_serviceHost = null;
			}
		}

		protected override void OnContinue()
		{
			if (_serviceHost == null)
				_serviceHost = new ServiceHost(typeof(SecurityService));
			_serviceHost.Open();
		}

		protected override void OnPause()
		{
			if (_serviceHost == null)
				_serviceHost = new ServiceHost(typeof(SecurityService));
			_serviceHost.Close();
		}
	}
}
