using System;
using System.Globalization;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Enumeration of the reasons for the end of a tolling event.
	/// </summary>
	public enum TollingEndReason
	{
		/// <summary>
		/// Unknown reason.
		/// </summary>
		Unknown,
		/// <summary>
		/// The event was cancelled due to a disposition error.
		/// </summary>
		Error,
		/// <summary>
		/// The Draft Audit Report was issued.
		/// </summary>
		DraftAuditReport,
		/// <summary>
		/// The Final Audit Report was issued.
		/// </summary>
		FinalAuditReport,
		/// <summary>
		/// A Notice of Alleged Violation letter was sent.
		/// </summary>
		AllegedViolationNotice,
		/// <summary>
		/// The response due date passed without receipt of the response.
		/// </summary>
		NoResponse,
		/// <summary>
		/// An extension was granted.
		/// </summary>
		Extension,
		/// <summary>
		/// A response was received.
		/// </summary>
		ResponseReceived,
		/// <summary>
		/// A mutual agreement to an extension expired.
		/// </summary>
		AgreementExpired
	}
}

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Utility class for converting and parsing enumerations.
	/// </summary>
	public partial class CPConvert
	{
		/// <summary>
		/// Converts the value of the specified <see cref="TollingEndReason"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="reason">A tolling event end reason.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="reason"/>.</returns>
		public static string ToString(TollingEndReason reason)
		{
			switch (reason)
			{
				case TollingEndReason.AllegedViolationNotice:
					return "Notice of Alleged Violation";
				case TollingEndReason.Error:
					return "Disposition Error";
				case TollingEndReason.DraftAuditReport:
					return "Draft Audit Report Issued";
				case TollingEndReason.Extension:
					return "Extension Granted";
				case TollingEndReason.FinalAuditReport:
					return "Final Audit Report Issued";
				case TollingEndReason.NoResponse:
					return "No Response Received";
				case TollingEndReason.ResponseReceived:
					return "Response Received";
				case TollingEndReason.AgreementExpired:
					return "Agreement Expired";
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Parses and converts the specified code to its equivalent <see cref="TollingEndReason"/> representation.
		/// </summary>
		/// <param name="code">A tolling event end reason code.</param>
		/// <returns>The <see cref="TollingEndReason"/> equivalent of <paramref name="code"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
		public static TollingEndReason ToTollingEndReason(string code)
		{
			if (code == null) throw new ArgumentNullException("code");
			switch (code.ToUpper(CultureInfo.InvariantCulture))
			{
				case "CANCEL":
					return TollingEndReason.Error;
				case "DARSEN":
					return TollingEndReason.DraftAuditReport;
				case "DUEDT":
					return TollingEndReason.NoResponse;
				case "EXTGRT":
					return TollingEndReason.Extension;
				case "FARSEN":
					return TollingEndReason.FinalAuditReport;
				case "NAVLET":
					return TollingEndReason.AllegedViolationNotice;
				case "REPLY":
					return TollingEndReason.ResponseReceived;
				case "EXPIRE":
					return TollingEndReason.AgreementExpired;
				default:
					return TollingEndReason.Unknown;
			}
		}
	}
}