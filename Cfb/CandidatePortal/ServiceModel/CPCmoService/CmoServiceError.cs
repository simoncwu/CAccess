using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.ServiceModel.CPCmoService
{
	/// <summary>
	/// Enumeration of error codes for the <see cref="CmoService"/> WCF service.
	/// </summary>
	public enum CmoServiceError : int
	{
		/// <summary>
		/// No failure.
		/// </summary>
		None = 0,
		/// <summary>
		/// General failure.
		/// </summary>
		MessageGeneralFailure = -2147483648,
		/// <summary>
		/// Failure while adding a new message.
		/// </summary>
		MessageAddFailure = -2147483647,
		/// <summary>
		/// Failure while saving an attachment.
		/// </summary>
		AttachmentFailure = -2147483646,
		/// <summary>
		/// Failure while posting the message.
		/// </summary>
		MessagePostFailure = -2147483645,
		/// <summary>
		/// Failure while setting the payment run.
		/// </summary>
		PaymentRunFailure = -2147483644,
		/// <summary>
		/// Failure while setting tolling properties.
		/// </summary>
		TollingFailure = -2147483643,
		/// <summary>
		/// Failure due to unsupported tolling letter codes.
		/// </summary>
		UnsupportedTollingLetter = -2147483642,
		/// <summary>
		/// Failure while setting post election audit request type properties.
		/// </summary>
		PostElectionRequestTypeFailure = -2147483641,
		/// <summary>
		/// Failure while setting the statement number.
		/// </summary>
		StatementNumberFailure = -2147483640
	}
}
