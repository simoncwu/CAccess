using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.TrainingTracking
{
    /// <summary>
    /// The complete CFB training status for a single candidate/campaign.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class TrainingStatus
    {
        /// <summary>
        /// Whether or not C-SMART Web training is required for achieving compliance.
        /// </summary>
        [DataMember(Name = "CsmartWebRequired")]
        private readonly bool _webRequired;

        /// <summary>
        /// Gets whether or not C-SMART Web training is required for achieving compliance.
        /// </summary>
        public bool CsmartWebRequired
        {
            get { return _webRequired; }
        }

        /// <summary>
        /// The collection of trainees associated with the campaign, indexed by ID.
        /// </summary>
        [DataMember(Name = "Trainees")]
        private readonly Dictionary<short, Trainee> _trainees;

        /// <summary>
        /// Gets the collection of trainees associated with the campaign, indexed by ID.
        /// </summary>
        public Dictionary<short, Trainee> Trainees
        {
            get { return _trainees; }
        }

        /// <summary>
        /// The collection of training sessions registered for by the campaign.
        /// </summary>
        [DataMember(Name = "Sessions")]
        private readonly TrainingSessionCollection _sessions;

        /// <summary>
        /// Gets the collection of training sessions registered for by the campaign.
        /// </summary>
        public TrainingSessionCollection Sessions
        {
            get { return _sessions; }
        }

        /// <summary>
        /// The collection of training session registrations made by the campaign.
        /// </summary>
        [DataMember(Name = "Registrations")]
        private readonly TrainingRegistrationCollection _registrations;

        /// <summary>
        /// Gets the collection of training session registrations made by the campaign.
        /// </summary>
        public TrainingRegistrationCollection Registrations
        {
            get { return _registrations; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingStatus"/> class.
        /// </summary>
        /// <param name="traineeCount">The number of trainees associated with the campaign.</param>
        /// <param name="sessionCount">The number of sessions registered for by the campaign.</param>
        /// <param name="registrationCount">The number of session registrations made by the campaign.</param>
        /// <param name="isWebRequired">Whether or not C-SMART Web training is required to achieve compliance.</param>
        public TrainingStatus(int traineeCount, int sessionCount, int registrationCount, bool isWebRequired = true)
        {
            _trainees = new Dictionary<short, Trainee>(traineeCount);
            _sessions = new TrainingSessionCollection(sessionCount);
            _registrations = new TrainingRegistrationCollection();
            _webRequired = isWebRequired;
        }

        /// <summary>
        /// Retrieves the IDs of trainees who have met C-SMART training compliance requirements.
        /// </summary>
        /// <returns>A collection of IDs for trainees who have met training compliance requirements.</returns>
        public IEnumerable<short> GetCsmartComplianceAchievedTraineeIDs()
        {
            var superset = from r in _registrations.Registrations
                           where r.Completed
                           join s in
                               (from s in _sessions.Sessions.Values
                                where s.IsComplianceCourse
                                select s) on r.SessionID equals s.ID
                           join tid in
                               (from t in _trainees.Values
                                where t.ComplianceEligible
                                select t.ID) on r.TraineeID equals tid
                           select new { r.TraineeID, s.CourseType };
            // C-SMART course
            var csmart = from s in superset
                         where s.CourseType == (_webRequired ? TrainingCourseType.CsmartWeb : TrainingCourseType.Csmart)
                         select s.TraineeID;
            // Combination course
            return from s in superset
                   where s.CourseType == (_webRequired ? TrainingCourseType.CombinedWeb : TrainingCourseType.Combined)
                   || (csmart.Contains(s.TraineeID) && (s.CourseType == TrainingCourseType.Combined || s.CourseType == TrainingCourseType.Compliance))
                   || (!_webRequired && s.CourseType == TrainingCourseType.Supplemental)
                   select s.TraineeID;
        }

        /// <summary>
        /// Gets whether or not the campaign has met C-SMART training compliance requirements.
        /// </summary>
        public bool CsmartComplianceAchieved
        {
            get { return GetCsmartComplianceAchievedTraineeIDs().Any(); }
        }

        /// <summary>
        /// Determines whether or not a specific training session registration meets training compliance requirements.
        /// </summary>
        /// <param name="registration">The training session registration to analyze.</param>
        /// <returns>true if <paramref name="registration"/> meets training compliance requirements.</returns>
        public bool ComplianceAchievedBy(TrainingRegistration registration)
        {
            if (registration != null && registration.Completed)
            {
                TrainingSession session;
                Trainee trainee;
                if (_sessions.Sessions.TryGetValue(registration.SessionID, out session) && _trainees.TryGetValue(registration.TraineeID, out trainee))
                {
                    switch (session.CourseType)
                    {
                        case TrainingCourseType.Combined:
                        case TrainingCourseType.CombinedWeb:
                        case TrainingCourseType.Supplemental:
                            return trainee.ComplianceEligible;
                        case TrainingCourseType.Compliance:
                            return trainee.ComplianceEligible && GetCompletedRegistrationsBy(trainee, _webRequired ? TrainingCourseType.CsmartWeb : TrainingCourseType.Csmart).Any();
                        case TrainingCourseType.Csmart:
                        case TrainingCourseType.CsmartWeb:
                            return trainee.ComplianceEligible && GetCompletedRegistrationsBy(trainee, TrainingCourseType.Compliance).Any();
                        case TrainingCourseType.Audit:
                            return trainee.AuditComplianceEligible;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves a collection of registrations completed by a specific trainee for a specific type of training course.
        /// </summary>
        /// <param name="trainee">The trainee to search by.</param>
        /// <param name="type">The type of training course to search by.</param>
        /// <returns>A collection of registrations completed by the trainee specified and for the type of training course specified.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="trainee"/> is null.</exception>
        public IEnumerable<TrainingRegistration> GetCompletedRegistrationsBy(Trainee trainee, TrainingCourseType type)
        {
            if (trainee == null)
                throw new ArgumentNullException("trainee", "Trainee must not be null.");
            return from r in _registrations.Registrations
                   where r.Completed && r.TraineeID == trainee.ID
                   join s in
                       (from s in _sessions.Sessions.Values
                        where s.CourseType == type
                        select s) on r.SessionID equals s.ID
                   select r;
        }

        /// <summary>
        /// Retrieves the IDs of trainees who have met training compliance requirements.
        /// </summary>
        /// <returns>A collection of IDs for trainees who have met training compliance requirements.</returns>
        public IEnumerable<short> GetAuditComplianceAchievedTraineeIDs()
        {
            var matches =
                from r in _registrations.Registrations
                where r.Completed
                join sid in
                    (from s in _sessions.Sessions.Values
                     where s.IsAuditComplianceCourse
                     select s.ID)
                on r.SessionID equals sid
                join tid in
                    (from t in _trainees.Values
                     where t.AuditComplianceEligible
                     select t.ID)
                on r.TraineeID equals tid
                select r.TraineeID;
            return matches;
        }

        /// <summary>
        /// Gets whether or not the campaign has met audit compliance requirements.
        /// </summary>
        public bool AuditComplianceAchieved
        {
            get { return GetAuditComplianceAchievedTraineeIDs().Any(); }
        }

        /// <summary>
        /// Retrieves the current training tracking status for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose training tracking status is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The current training tracking status for the specified candidate and election cycle.</returns>
        public static TrainingStatus GetTrainingStatus(string candidateID, string electionCycle)
        {
            return CPProviders.DataProvider.GetTrainingStatus(candidateID, electionCycle);
        }
    }
}
