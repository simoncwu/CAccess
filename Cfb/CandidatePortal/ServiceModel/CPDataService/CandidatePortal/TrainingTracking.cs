using System;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.TrainingTracking;
using Cfb.Cfis.CampaignContacts;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.TrainingTrackingTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves the current training tracking status for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose training tracking status is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The current training tracking status for the specified candidate and election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        TrainingStatus GetTrainingStatus(string candidateID, string electionCycle);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves the current training tracking status for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose training tracking status is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The current training tracking status for the specified candidate and election cycle.</returns>
        public TrainingStatus GetTrainingStatus(string candidateID, string electionCycle)
        {
            using (TrainingTrackingTds ds = new TrainingTrackingTds())
            {
                // fetch data
                using (TrainingTraineesTableAdapter ta = new TrainingTraineesTableAdapter())
                {
                    ta.Fill(ds.TrainingTrainees, candidateID, electionCycle);
                }
                using (TrainingSessionsTableAdapter ta = new TrainingSessionsTableAdapter())
                {
                    ta.Fill(ds.TrainingSessions, candidateID, electionCycle);
                }
                using (TrainingRegistrationsTableAdapter ta = new TrainingRegistrationsTableAdapter())
                {
                    ta.Fill(ds.TrainingRegistrations, candidateID, electionCycle);
                }
                var csu = GetCsuLiaisons(electionCycle);

                // create top-level object
                TrainingStatus status = new TrainingStatus(ds.TrainingTrainees.Count, ds.TrainingSessions.Count, ds.TrainingRegistrations.Count, "2012".CompareTo(electionCycle) <= 0);
                var trainees = status.Trainees;
                var sessions = status.Sessions.Sessions;
                var registrations = status.Registrations.Registrations;

                // create trainees
                foreach (TrainingTrackingTds.TrainingTraineesRow row in ds.TrainingTrainees)
                {
                    trainees[row.TraineeID] = new Trainee(row.TraineeID)
                    {
                        LastName = string.IsNullOrEmpty(row.LastName.Trim()) ? null : row.LastName.Trim(),
                        FirstName = string.IsNullOrEmpty(row.FirstName.Trim()) ? null : row.FirstName.Trim(),
                        MiddleInitial = string.IsNullOrWhiteSpace(row.MiddleInitial) ? null : row.MiddleInitial.Trim().ToCharArray()[0] as char?,
                        DaytimePhone = new PhoneNumber(row.Phone1.Trim()),
                        EveningPhone = new PhoneNumber(row.Phone2.Trim()),
                        CampaignRelationship = CPConvert.ToCampaignRelationship(row.RelationshipCode.Trim()),
                        LastUpdated = row.LastUpdated
                    };
                }

                // create sessions
                foreach (TrainingTrackingTds.TrainingSessionsRow row in ds.TrainingSessions)
                {
                    TrainingSession session = new TrainingSession(row.SessionID)
                    {
                        CourseType = CPConvert.ToTrainingCourseType(row.TypeCode.Trim()),
                        StartTime = row.StartTime,
                        EndTime = row.EndTime,
                        Location = CPConvert.ToTrainingLocation(row.LocationCode.Trim()),
                        Cancelled = row.Cancelled,
                        MaximumCapacity = row.MaxCapacity
                    };
                    sessions[session.ID] = session;
                    // add trainers
                    TrainerCollection trainers = session.Trainers;
                    CsuLiaison liaison;
                    string role;
                    if (row.TrainerID1 > 0 && !string.IsNullOrEmpty(role = row.TRoleCode1.Trim()) && csu.TryGetValue(row.TrainerID1, out liaison))
                    {
                        trainers.Trainers[CPConvert.ToTrainerRole(role)].Add(liaison);
                    }
                    if (row.TrainerID2 > 0 && !string.IsNullOrEmpty(role = row.TRoleCode2.Trim()) && csu.TryGetValue(row.TrainerID2, out liaison))
                    {
                        trainers.Trainers[CPConvert.ToTrainerRole(role)].Add(liaison);
                    }
                    if (row.TrainerID3 > 0 && !string.IsNullOrEmpty(role = row.TRoleCode3.Trim()) && csu.TryGetValue(row.TrainerID3, out liaison))
                    {
                        trainers.Trainers[CPConvert.ToTrainerRole(role)].Add(liaison);
                    }
                    if (row.TrainerID4 > 0 && !string.IsNullOrEmpty(role = row.TRoleCode4.Trim()) && csu.TryGetValue(row.TrainerID4, out liaison))
                    {
                        trainers.Trainers[CPConvert.ToTrainerRole(role)].Add(liaison);
                    }
                    if (row.TrainerID5 > 0 && !string.IsNullOrEmpty(role = row.TRoleCode5.Trim()) && csu.TryGetValue(row.TrainerID5, out liaison))
                    {
                        trainers.Trainers[CPConvert.ToTrainerRole(role)].Add(liaison);
                    }
                }

                // create registrations
                foreach (TrainingTrackingTds.TrainingRegistrationsRow row in ds.TrainingRegistrations)
                {
                    registrations.Add(new TrainingRegistration(row.TraineeID, row.SessionID)
                    {
                        RegisteringCandidateID = string.IsNullOrEmpty(row.RegisteringCandidateID.Trim()) ? null : row.RegisteringCandidateID.Trim(),
                        IsReservation = row.IsReservation,
                        ReservationDate = row.IsReservationDateNull() ? null : row.ReservationDate as DateTime?,
                        Attended = row.Attended,
                        Completed = row.Completed,
                        LastUpdated = row.LastUpdated
                    });
                }
                return status;
            }
        }
    }
}
