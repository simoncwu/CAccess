using System;
using System.Globalization;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Enumeration of the possible reasons for an audit suspension.
	/// </summary>
	public enum SuspensionReason : byte
	{
		/// <summary>
		/// Potential other criminal activity.
		/// </summary>
		Other,
		/// <summary>
		/// Potential breach of certification.
		/// </summary>
		Breach,
		/// <summary>
		/// Potential campaign related fraud.
		/// </summary>
		Fraud,
		/// <summary>
		/// Potential significant spending limit violation (General Election).
		/// </summary>
		GeneralSpending,
		/// <summary>
		/// Potential significant spending limit violation (Primary Election).
		/// </summary>
		PrimarySpending
	}
}

namespace Cfb.CandidatePortal
{
	partial class CPConvert
	{
		/// <summary>
		/// Converts the value of the specified <see cref="SuspensionReason"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="reason">A Post Election Audit suspension reason.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="reason"/>.</returns>
		public static string ToString(SuspensionReason reason)
		{
			switch (reason)
			{
				case SuspensionReason.Breach:
					return "Potential breach of certification";
				case SuspensionReason.Fraud:
					return "Potential campaign-related fraud";
				case SuspensionReason.GeneralSpending:
					return "Potential significant general election spending limit violation";
				case SuspensionReason.PrimarySpending:
					return "Potential significant primary election spending limit violation";
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Parses and converts the specified code to its equivalent <see cref="SuspensionReason"/> representation.
		/// </summary>
		/// <param name="code">A CFB training course code.</param>
		/// <returns>The <see cref="SuspensionReason"/> equivalent of <paramref name="code"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
		public static SuspensionReason ToSuspensionReason(string code)
		{
			if (code == null) throw new ArgumentNullException("code");
			switch (code.ToUpper(CultureInfo.InvariantCulture))
			{
				case "BREACH":
					return SuspensionReason.Breach;
				case "FRAUD":
					return SuspensionReason.Fraud;
				case "GENLMT":
					return SuspensionReason.GeneralSpending;
				case "PRILMT":
					return SuspensionReason.PrimarySpending;
				default:
					return SuspensionReason.Other;
			}
		}
	}
}