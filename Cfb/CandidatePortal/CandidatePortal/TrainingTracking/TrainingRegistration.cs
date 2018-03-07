using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// A registration record by a trainee for a CFB training session.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class TrainingRegistration
	{
		/// <summary>
		/// The ID of the training session being registered.
		/// </summary>
		[DataMember(Name = "SessionID")]
		private readonly int _sessionID;

		/// <summary>
		/// Gets the ID of the training session being registered.
		/// </summary>
		public int SessionID
		{
			get { return _sessionID; }
		}

		/// <summary>
		/// The ID of the registering trainee.
		/// </summary>
		[DataMember(Name = "TraineeID")]
		private readonly short _traineeID;

		/// <summary>
		/// Gets the ID of the registering trainee.
		/// </summary>
		public short TraineeID
		{
			get { return _traineeID; }
		}

		/// <summary>
		/// Gets or sets the date and time the registration was last updated.
		/// </summary>
		[DataMember]
		public DateTime LastUpdated { get; set; }

		/// <summary>
		/// Gets or sets the ID of the candidate under which the registration was made.
		/// </summary>
		[DataMember]
		public string RegisteringCandidateID { get; set; }

		/// <summary>
		/// Gets or sets whether or not the registration is a reservation made in advance.
		/// </summary>
		[DataMember]
		public bool IsReservation { get; set; }

		/// <summary>
		/// Gets or sets the date the reservation was made, if applicable.
		/// </summary>
		[DataMember]
		public DateTime? ReservationDate { get; set; }

		/// <summary>
		/// Gets or sets whether or not the trainee attended the registered-for session.
		/// </summary>
		[DataMember]
		public bool Attended { get; set; }

		/// <summary>
		/// Gets or sets whether or not the trainee completed the registered-for session.
		/// </summary>
		[DataMember]
		public bool Completed { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TrainingRegistration"/> class.
		/// </summary>
		/// <param name="traineeID">The ID of the registering trainee.</param>
		/// <param name="sessionID">The ID of the training session being registered.</param>
		public TrainingRegistration(short traineeID, int sessionID)
		{
			_traineeID = traineeID;
			_sessionID = sessionID;
		}
	}
}
