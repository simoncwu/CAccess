using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// Enumeration of CFB training trainer roles.
	/// </summary>
	public enum TrainerRole : byte
	{
		/// <summary>
		/// Unknown role.
		/// </summary>
		Unknown,
		/// <summary>
		/// Trainer's assistant.
		/// </summary>
		Assistant,
		/// <summary>
		/// Compliance training.
		/// </summary>
		Compliance,
		/// <summary>
		/// C-SMART application training.
		/// </summary>
		Csmart,
		/// <summary>
		/// Compliance and C-SMART application training.
		/// </summary>
		Combined
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
		/// Converts the value of the specified <see cref="TrainerRole"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="role">A CFB training trainer role.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="role"/>.</returns>
		public static string ToString(TrainerRole role)
		{
			switch (role)
			{
				case TrainerRole.Assistant:
					return "Trainer's Assistant";
				case TrainerRole.Combined:
					return "Compliance and C-SMART Application Training";
				case TrainerRole.Compliance:
					return "Compliance Training";
				case TrainerRole.Csmart:
					return "C-SMART Application Training";
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Parses and converts the specified code to its equivalent <see cref="TrainerRole"/> representation.
		/// </summary>
		/// <param name="code">A CFB training trainer role code.</param>
		/// <returns>The <see cref="TrainerRole"/> equivalent of <paramref name="code"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
		public static TrainerRole ToTrainerRole(string code)
		{
			if (code == null) throw new ArgumentNullException("code");
			switch (code.ToUpper(CultureInfo.InvariantCulture))
			{
				case "ASSIST":
					return TrainerRole.Assistant;
				case "COMCS":
					return TrainerRole.Combined;
				case "COMPLY":
					return TrainerRole.Compliance;
				case "CSMART":
					return TrainerRole.Csmart;
				default:
					return TrainerRole.Unknown;
			}
		}
	}
}