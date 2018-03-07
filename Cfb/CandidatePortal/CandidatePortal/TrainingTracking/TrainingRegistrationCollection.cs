using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// A set of training session registrations that supports filtering by either session or trainee ID.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class TrainingRegistrationCollection
	{
		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "Registrations")]
		private readonly HashSet<TrainingRegistration> _registrations;

		/// <summary>
		/// 
		/// </summary>
		public HashSet<TrainingRegistration> Registrations
		{
			get { return _registrations; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TrainingRegistrationCollection"/> class.
		/// </summary>
		public TrainingRegistrationCollection()
		{
			_registrations = new HashSet<TrainingRegistration>();
		}

		/// <summary>
		/// Retrieves a collection of all registrations made by a specific trainee.
		/// </summary>
		/// <param name="traineeID">The ID of the trainee.</param>
		/// <returns>A collection of all registrations made by the trainee with ID <paramref name="traineeID"/>.</returns>
		public List<TrainingRegistration> GetRegistrationsByTrainee(int traineeID)
		{
			return _registrations.Where(r => r.TraineeID == traineeID).ToList();
		}

		/// <summary>
		/// Retrieves a collection of all registrations made for a specific training session.
		/// </summary>
		/// <param name="sessionID">The ID of the session.</param>
		/// <returns>A collection of all registrations made for the training session with ID <paramref name="sessionID"/>.</returns>
		public List<TrainingRegistration> GetRegistrationsBySession(int sessionID)
		{
			return _registrations.Where(r => r.SessionID == sessionID).ToList();
		}
	}
}
