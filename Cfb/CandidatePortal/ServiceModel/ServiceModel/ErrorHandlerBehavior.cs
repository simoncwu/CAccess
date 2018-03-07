using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace Cfb.CandidatePortal.ServiceModel
{
	/// <summary>
	/// Provides injection of WCF service model error handling behavior through an application configuration file.
	/// </summary>
	public class ErrorHandlerBehavior : BehaviorExtensionElement
	{
		/// <summary>
		/// Creates a behavior extension based on the current configuration settings.
		/// </summary>
		/// <returns>The behavior extension.</returns>
		protected override object CreateBehavior()
		{
			return new ErrorHandler();
		}

		/// <summary>
		/// Gets the type of the behavior.
		/// </summary>
		public override Type BehaviorType
		{
			get { return typeof(ErrorHandler); }
		}
	}
}
