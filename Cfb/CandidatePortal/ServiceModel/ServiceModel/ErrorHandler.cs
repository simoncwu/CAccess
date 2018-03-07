using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Cfb.ExceptionHandling;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.ServiceModel
{
	/// <summary>
	/// Handler for catching fault messages resulting from errors and logging them.
	/// </summary>
	public class ErrorHandler : IErrorHandler, IServiceBehavior
	{
		#region IErrorHandler Members

		/// <summary>
		/// Enables error-related processing and returns a value that indicates whether subsequent <see cref="HandleError"/> implementations are called.
		/// </summary>
		/// <param name="error">The exception thrown during proessing.</param>
		/// <returns>true if subsequent <see cref="IErrorHandler"/> implementations must not be called; otherwise, false. The default is false.</returns>
		public bool HandleError(Exception error)
		{
			CfbExceptionPolicy.LogException(error);
			return true;
		}

		/// <summary>
		/// Enables the creation of a custom <see cref="FaultException{TDetail}"/> that is returned from an exception in the course of a service method.
		/// </summary>
		/// <param name="error">The <see cref="Exception"/> object thrown in the course of the service operation.</param>
		/// <param name="version">The SOAP version of the message.</param>
		/// <param name="fault">The <see cref="Message"/> object that is returned to the client, or service, in the duplex case.</param>
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			FaultException e;
			SqlException se = error as SqlException;
			if (se == null)
			{
				EntityException ee = error as EntityException;
				if (ee != null)
					se = ee.InnerException as SqlException;
			}
			if (se != null && se.IsOfflineException())
			{
				e = new FaultException<OfflineDataException>(new OfflineDataException(se), new FaultReason(se.Message));
			}
			else
			{
				FaultException<OfflineDataException> fe = error as FaultException<OfflineDataException>;
				if (fe != null)
				{
					e = fe;
				}
				else
				{
					e = new FaultException<Exception>(error, error.Message);
				}
			}
			fault = Message.CreateMessage(version, e.CreateMessageFault(), e.Action);
		}

		#endregion

		#region IServiceBehavior Members

		/// <summary>
		/// Provides the ability to pass custom data to binding elements to support the contract implementation.
		/// </summary>
		/// <param name="serviceDescription">The service description of the service.</param>
		/// <param name="serviceHostBase">The host of the service.</param>
		/// <param name="endpoints">The service endpoints.</param>
		/// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		/// <summary>
		/// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
		/// </summary>
		/// <param name="serviceDescription">The service description.</param>
		/// <param name="serviceHostBase">The host that is currently being built.</param>
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
			{
				chanDisp.ErrorHandlers.Add(this);
			}
		}

		/// <summary>
		/// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
		/// </summary>
		/// <param name="serviceDescription">The service description.</param>
		/// <param name="serviceHostBase">The service host that is currently being constructed.</param>
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		#endregion
	}
}
