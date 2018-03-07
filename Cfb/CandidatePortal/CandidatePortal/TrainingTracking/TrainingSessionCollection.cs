using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// A collection of all training sessions registered for by a single campaign's representatives.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class TrainingSessionCollection
	{
		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "Sessions")]
		private readonly Dictionary<int, TrainingSession> _sessions;

		/// <summary>
		/// 
		/// </summary>
		public Dictionary<int, TrainingSession> Sessions
		{
			get { return _sessions; }
		}

		/// <summary>
		/// Gets a collection of all training sessions of a specific training course type.
		/// </summary>
		/// <param name="type">The type of training sessions to retrieve.</param>
		/// <returns>A collection of all training sessions for the campaign of type <paramref name="type"/>.</returns>
		public List<TrainingSession> this[TrainingCourseType type]
		{
			get { return _sessions.Values.Where(s => s.CourseType == type).ToList(); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TrainingSessionCollection"/> collection.
		/// </summary>
		/// <param name="capacity">The initial number of training sessions that the collection can contain.</param>
		public TrainingSessionCollection(int capacity)
		{
			_sessions = new Dictionary<int, TrainingSession>(capacity);
		}
	}
}
