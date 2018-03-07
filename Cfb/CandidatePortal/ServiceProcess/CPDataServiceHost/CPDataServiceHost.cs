using System.ServiceModel;
using System.ServiceProcess;
using Cfb.CandidatePortal.ServiceModel.CPDataService;

namespace Cfb.CandidatePortal.ServiceProcess.CPDataServiceHost
{
	partial class CPDataServiceHost : ServiceBase
	{
		private ServiceHost _serviceHost;

		public CPDataServiceHost()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			if (_serviceHost != null)
				_serviceHost.Close();
			_serviceHost = new ServiceHost(typeof(DataService));
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
				_serviceHost = new ServiceHost(typeof(DataService));
			_serviceHost.Open();
		}

		protected override void OnPause()
		{
			if (_serviceHost == null)
				_serviceHost = new ServiceHost(typeof(DataService));
			_serviceHost.Close();
		}
	}
}
