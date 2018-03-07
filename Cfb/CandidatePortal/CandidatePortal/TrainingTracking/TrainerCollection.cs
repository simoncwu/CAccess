using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// A collection of trainers assigned to conduct a CFB training session.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class TrainerCollection
	{
		/// <summary>
		/// The collection of trainers grouped by trainer role.
		/// </summary>
		[DataMember(Name = "Trainers")]
		private Dictionary<TrainerRole, HashSet<CsuLiaison>> _trainers;

		/// <summary>
		/// Gets the collection of trainers grouped by trainer role.
		/// </summary>
		public Dictionary<TrainerRole, HashSet<CsuLiaison>> Trainers
		{
			get { return _trainers; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Trainers"/> collection.
		/// </summary>
		public TrainerCollection()
		{
			Array roles = Enum.GetValues(typeof(TrainerRole));
			_trainers = new Dictionary<TrainerRole, HashSet<CsuLiaison>>(roles.Length);
			foreach (TrainerRole role in roles)
			{
				_trainers.Add(role, new HashSet<CsuLiaison>());
			}
		}
	}
}
