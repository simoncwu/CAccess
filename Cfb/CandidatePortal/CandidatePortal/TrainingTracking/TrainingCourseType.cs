using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// Enumeration of the types of training courses offered by the Campaign Finance Board.
	/// </summary>
	public enum TrainingCourseType : byte
	{
		/// <summary>
		/// Unknown training.
		/// </summary>
		Unknown,
		/// <summary>
		/// Combined compliance and C-SMART training.
		/// </summary>
		Combined,
		/// <summary>
		/// Compliance training.
		/// </summary>
		Compliance,
		/// <summary>
		/// C-SMART training.
		/// </summary>
		Csmart,
		/// <summary>
		/// Supplemental training.
		/// </summary>
		Supplemental,
		/// <summary>
		/// Advanced C-SMART training.
		/// </summary>
		AdvancedCsmart,
		/// <summary>
		/// Audit (Draft Audit Report) training.
		/// </summary>
		Audit,
		/// <summary>
		/// C-SMART Web training.
		/// </summary>
		CsmartWeb,
		/// <summary>
		/// Combined compliance and C-SMART Web training.
		/// </summary>
		CombinedWeb
	}

	public static class TrainingCourseType_Extensions
	{
		/// <summary>
		/// Converts the value of the specified <see cref="TrainingCourseType"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="type">A CFB training course type.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="type"/>.</returns>
		public static string ToString(this TrainingCourseType type)
		{
			return CPConvert.ToString(type);
		}
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
		/// Converts the value of the specified <see cref="TrainingCourseType"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="type">A CFB training course type.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="type"/>.</returns>
		public static string ToString(TrainingCourseType type)
		{
			switch (type)
			{
				case TrainingCourseType.AdvancedCsmart:
					return "Advanced C-SMART Training";
				case TrainingCourseType.Combined:
					return "Combined Compliance and C-SMART Training";
				case TrainingCourseType.Compliance:
					return "Compliance Training Only";
				case TrainingCourseType.Csmart:
					return "C-SMART Training Only";
				case TrainingCourseType.Supplemental:
					return "Supplemental Training";
				case TrainingCourseType.Audit:
					return "Audit Training";
				case TrainingCourseType.CsmartWeb:
					return "C-SMART Web Training";
				case TrainingCourseType.CombinedWeb:
					return "Combined Compliance and C-SMART Web Training";
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Parses and converts the specified code to its equivalent <see cref="TrainingCourseType"/> representation.
		/// </summary>
		/// <param name="code">A CFB training course code.</param>
		/// <returns>The <see cref="TrainingCourseType"/> equivalent of <paramref name="code"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
		public static TrainingCourseType ToTrainingCourseType(string code)
		{
			if (code == null) throw new ArgumentNullException("code");
			switch (code.ToUpper(CultureInfo.InvariantCulture))
			{
				case "COMB":
					return TrainingCourseType.Combined;
				case "COMPL":
					return TrainingCourseType.Compliance;
				case "CSMAR":
					return TrainingCourseType.Csmart;
				case "SUPP":
					return TrainingCourseType.Supplemental;
				case "ADVCMT":
					return TrainingCourseType.AdvancedCsmart;
				case "AUDTRN":
					return TrainingCourseType.Audit;
				case "CSMARW":
					return TrainingCourseType.CsmartWeb;
				case "COMWEB":
					return TrainingCourseType.CombinedWeb;
				default:
					return TrainingCourseType.Unknown;
			}
		}
	}
}