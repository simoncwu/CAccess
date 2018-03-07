using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Enumeration for the different kinds of disclosure statement/backup documentation submission formats.
	/// </summary>
	[Flags]
	public enum SubmissionFormat : byte
	{
		/// <summary>
		/// No submission format.
		/// </summary>
		None = 0,
		/// <summary>
		/// Submission on paper.
		/// </summary>
		Paper = 1,
		/// <summary>
		/// Submission on floppy disk.
		/// </summary>
		FloppyDisk = 2,
		/// <summary>
		/// Submission on compact disc (CD).
		/// </summary>
		CD = 4,
		/// <summary>
		/// Submission from local C-SMART via Internet Delivery System (IDS).
		/// </summary>
		IDS = 8,
		/// <summary>
		/// Submission from C-SMART Web.
		/// </summary>
		CsmartWeb = 16,
		/// <summary>
		/// Submission as an electronic document.
		/// </summary>
		ElectronicDocument = 32
	}

	public static class SubmissionFormat_Extensions
	{
		/// <summary>
		/// Converts the value of the specified <see cref="SubmissionFormat"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="format">A submission format.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="format"/>.</returns>
		public static string ToString<T>(this SubmissionFormat format)
		{
			switch (format)
			{
				case SubmissionFormat.CD: return "CD";
				case SubmissionFormat.CsmartWeb: return "C-SMART Web";
				case SubmissionFormat.ElectronicDocument: return "Electronic Document";
				case SubmissionFormat.FloppyDisk: return "Floppy Disk";
				case SubmissionFormat.IDS: return "C-SMART IDS";
				case SubmissionFormat.Paper: return "Paper";
				default: return string.Empty;
			}
		}
	}
}
