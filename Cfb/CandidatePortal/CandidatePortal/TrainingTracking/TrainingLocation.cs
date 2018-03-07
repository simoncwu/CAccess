using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// Enumeration of the locations where training courses are conducted.
	/// </summary>
	public enum TrainingLocation : byte
	{
		/// <summary>
		/// Unknown location.
		/// </summary>
		Unknown,
		/// <summary>
		/// Conference room (7th floor).
		/// </summary>
		ConferenceRoom,
		/// <summary>
		/// Law library (7th floor).
		/// </summary>
		LawLibrary,
		/// <summary>
		/// Training room (19th floor).
		/// </summary>
		TrainingRoom
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
		/// Converts the value of the specified <see cref="TrainingLocation"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="location">A CFB training location.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="location"/>.</returns>
		public static string ToString(TrainingLocation location)
		{
			switch (location)
			{
				case TrainingLocation.ConferenceRoom:
					return "Conferemce Room (7FL)";
				case TrainingLocation.LawLibrary:
					return "Law Library (7FL)";
				case TrainingLocation.TrainingRoom:
					return "Training Room (19FL)";
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Parses and converts the specified code to its equivalent <see cref="TrainingLocation"/> representation.
		/// </summary>
		/// <param name="code">A CFB training location code.</param>
		/// <returns>The <see cref="TrainingLocation"/> equivalent of <paramref name="code"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
		public static TrainingLocation ToTrainingLocation(string code)
		{
			if (code == null) throw new ArgumentNullException("code");
			switch (code.ToUpper(CultureInfo.InvariantCulture))
			{
				case "CONF":
					return TrainingLocation.ConferenceRoom;
				case "LAWLIB":
					return TrainingLocation.LawLibrary;
				case "TRAIN":
					return TrainingLocation.TrainingRoom;
				default:
					return TrainingLocation.Unknown;
			}
		}
	}
}