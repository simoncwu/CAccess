using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// A single session of a CFB training course.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class TrainingSession
	{
		/// <summary>
		/// The ID of the training session.
		/// </summary>
		[DataMember(Name = "ID")]
		private readonly int _id;

		/// <summary>
		/// Gets the ID of the training session.
		/// </summary>
		public int ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets or sets the course type of the training session.
		/// </summary>
		[DataMember]
		public TrainingCourseType CourseType { get; set; }

		/// <summary>
		/// Gets or sets the starting time of the training session.
		/// </summary>
		[DataMember]
		public DateTime StartTime { get; set; }

		/// <summary>
		/// Gets or sets the ending time of the training session.
		/// </summary>
		[DataMember]
		public DateTime EndTime { get; set; }

		/// <summary>
		/// Gets the date of the training session.
		/// </summary>
		public DateTime Date
		{
			get { return this.StartTime.Date; }
		}

		/// <summary>
		/// Gets or sets the location of the training session.
		/// </summary>
		[DataMember]
		public TrainingLocation Location { get; set; }

		/// <summary>
		/// Gets or sets whether or not the session is cancelled.
		/// </summary>
		[DataMember]
		public bool Cancelled { get; set; }

		/// <summary>
		/// Gets or sets the maximum trainee capacity for the training session.
		/// </summary>
		[DataMember]
		public short MaximumCapacity { get; set; }

		/// <summary>
		/// The collectino of trainers assigned to conduct the training session.
		/// </summary>
		[DataMember(Name = "Trainers")]
		private readonly TrainerCollection _trainers;

		/// <summary>
		/// Gets the collection of trainers assigned to conduct the training session.
		/// </summary>
		public TrainerCollection Trainers
		{
			get { return _trainers; }
		}

		/// <summary>
		/// Gets whether or not this session is for a training course that counts towards audit compliance.
		/// </summary>
		public bool IsAuditComplianceCourse
		{
			get { return this.CourseType == TrainingCourseType.Audit; }
		}

		/// <summary>
		/// Gets whether or not this session is for a training course that counts towards compliance.
		/// </summary>
		public bool IsComplianceCourse
		{
			get
			{
				switch (this.CourseType)
				{
					case TrainingCourseType.Combined:
					case TrainingCourseType.Supplemental:
					case TrainingCourseType.Compliance:
					case TrainingCourseType.Csmart:
					case TrainingCourseType.CsmartWeb:
					case TrainingCourseType.CombinedWeb:
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of a <see cref="TrainingSession"/>.
		/// </summary>
		/// <param name="id">The ID of the training session.</param>
		public TrainingSession(int id)
		{
			_id = id;
			_trainers = new TrainerCollection();
		}
	}
}
