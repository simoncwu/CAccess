using System.ServiceModel;
using System.ServiceProcess;
using Cfb.CandidatePortal.ServiceModel.CPCmoService;

namespace Cfb.CandidatePortal.ServiceProcess.CPCmoServiceHost
{
	partial class CPCmoServiceHost : ServiceBase
	{
		private ServiceHost _serviceHost;

		public CPCmoServiceHost()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			if (_serviceHost != null)
				_serviceHost.Close();
			_serviceHost = new ServiceHost(typeof(CmoService));
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
				_serviceHost = new ServiceHost(typeof(CmoService));
			_serviceHost.Open();
		}

		protected override void OnPause()
		{
			if (_serviceHost == null)
				_serviceHost = new ServiceHost(typeof(CmoService));
			_serviceHost.Close();
		}
	}
}
