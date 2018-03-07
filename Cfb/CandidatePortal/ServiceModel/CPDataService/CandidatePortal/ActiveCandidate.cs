using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.CampaignContacts;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.ActiveCandidateTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Returns profile information for a candidate who is active in an election cycle.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>The specified candidate's profile for the specified election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        ActiveCandidate GetActiveCandidate(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves basic profile information for all candidates active in a specific election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter by.</param>
        /// <returns>A collection of <see cref="ActiveCandidate"/> objects representing all candidates active in the <paramref name="electionCycle"/> election cycle, indexed by CFIS ID.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Dictionary<string, ActiveCandidate> GetActiveCandidates(string electionCycle);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Returns profile information for a candidate who is active in an election cycle.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>The specified candidate's profile for the specified election cycle.</returns>
        public ActiveCandidate GetActiveCandidate(string candidateID, string electionCycle)
        {
            using (ActiveCandidateTds ds = new ActiveCandidateTds())
            {
                using (ActiveCandidateTableAdapter ta = new ActiveCandidateTableAdapter())
                {
                    ta.FillByCandidate(ds.ActiveCandidate, candidateID, electionCycle);
                }
                foreach (ActiveCandidateTds.ActiveCandidateRow row in ds.ActiveCandidate.Rows)
                {
                    return Parse(row);
                }
                return null;
            }
        }

        /// <summary>
        /// Retrieves basic profile information for all candidates active in a specific election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter by.</param>
        /// <returns>A collection of <see cref="ActiveCandidate"/> objects representing all candidates active in the <paramref name="electionCycle"/> election cycle, indexed by CFIS ID.</returns>
        public Dictionary<string, ActiveCandidate> GetActiveCandidates(string electionCycle)
        {
            Dictionary<string, ActiveCandidate> candidates = new Dictionary<string, ActiveCandidate>();
            using (ActiveCandidateTds ds = new ActiveCandidateTds())
            {
                using (ActiveCandidateTableAdapter ta = new ActiveCandidateTableAdapter())
                {
                    ta.FillBy(ds.ActiveCandidate, electionCycle);
                }
                foreach (ActiveCandidateTds.ActiveCandidateRow row in ds.ActiveCandidate.Rows)
                {
                    ActiveCandidate c = Parse(row);
                    if (c != null)
                        candidates.Add(c.ID, c);
                }
            }
            return candidates;
        }

        /// <summary>
        /// Converts the <see cref="ActiveCandidateTds.ActiveCandidateRow"/> representation of an active candidate into its <see cref="ActiveCandidate"/> object equivalent.
        /// </summary>
        /// <param name="row">A <see cref="ActiveCandidateTds.ActiveCandidateRow"/> containing the active candidate to convert.</param>
        /// <returns>An <see cref="ActiveCandidate"/> equivalent to the active candidate represented by <paramref name="row"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="row"/> is a null reference.</exception>
        private static ActiveCandidate Parse(ActiveCandidateTds.ActiveCandidateRow row)
        {
            if (row == null)
                throw new ArgumentNullException("row", "Row must be valid and cannot be null.");
            char committeeID;
            return new ActiveCandidate(row.CandidateID.Trim(), row.ElectionCycle.Trim())
            {
                // basic information
                LastUpdated = row.LastUpdated,
                Honorific = CPConvert.ToHonorific(row.HonorificCode.Trim()),
                LastName = row.LastName.Trim(),
                FirstName = row.FirstName.Trim(),
                MiddleInitial = string.IsNullOrWhiteSpace(row.MiddleInitial) ? null : row.MiddleInitial.Trim().ToCharArray()[0] as char?,
                Address = new PostalAddress()
                {
                    StreetNumber = row.StreetNumber.Trim(),
                    StreetName = row.StreetName.Trim(),
                    Apartment = row.Apartment.Trim(),
                    City = row.City.Trim(),
                    State = row.State.Trim(),
                    Zip = new ZipCode(row.Zip.Trim())
                },
                DaytimePhone = new PhoneNumber(row.DaytimePhone.Trim())
                {
                    Extension = row.DaytimePhoneExt.Trim()
                },
                EveningPhone = new PhoneNumber(row.EveningPhone.Trim())
                {
                    Extension = row.EveningPhoneExt.Trim()
                },
                Fax = new PhoneNumber(row.Fax.Trim()),
                Email = row.Email.Trim(),

                // employer information
                Employer = new Entity()
                {
                    Type = EntityType.Employer,
                    LastName = row.EmployerName.Trim(),
                    Address = new PostalAddress()
                    {
                        StreetNumber = row.EmployerStreetNumber.Trim(),
                        StreetName = row.EmployerStreetName.Trim(),
                        City = row.EmployerCity.Trim(),
                        State = row.EmployerState.Trim(),
                        Zip = new ZipCode(row.EmployerZip.Trim())
                    },
                    DaytimePhone = new PhoneNumber(row.EmployerPhone.Trim())
                    {
                        Extension = row.EmployerPhoneExt.Trim()
                    },
                    Fax = new PhoneNumber(row.EmployerFax.Trim())
                },

                // activation information
                Office = new NycPublicOffice(CPConvert.ToNycPublicOfficeType(row.OfficeCode.Trim()))
                {
                    Borough = CPConvert.ToNycBorough(row.BoroughCode.Trim()),
                    District = string.IsNullOrEmpty(row.DistrictCode.Trim()) ? byte.MinValue : Convert.ToByte(row.DistrictCode.Trim())
                },
                CertificationDate = row.IsCertificationDateNull() ? null : row.CertificationDate as DateTime?,
                FilerRegistrationDate = row.IsFilerRegistrationDateNull() ? null : row.FilerRegistrationDate as DateTime?,
                TerminationDate = row.IsTerminationDateNull() ? null : row.TerminationDate as DateTime?,
                TerminationReason = CPConvert.ToTerminationReason(row.TerminationReasonCode),
                Classification = CPConvert.ToCfbClassification(row.ClassificationCode.Trim()),
                PoliticalParty = (row.IsPoliticalPartyNull()) ? string.Empty : row.PoliticalParty.Trim(),
                IsDirectDepositAuthorized = "Y".Equals(row.HasDirectDeposit, StringComparison.InvariantCultureIgnoreCase),
                IsRRDirectDepositAuthorized = "Y".Equals(row.HasRRDirectDeposit, StringComparison.InvariantCultureIgnoreCase),
                AuditorName = row.IsAuditorNameNull() ? null : row.AuditorName.Trim(),
                CsuLiaisonName = row.IsCsuLiaisonNameNull() ? null : row.CsuLiaisonName.Trim(),

                // principal committee info
                PrincipalCommittee = row.IsPrincipalCommitteeNull() ? null : row.PrincipalCommittee.Trim(),
                PrincipalCommitteeID = !row.IsPrincipalCommitteeIDNull() && char.TryParse(row.PrincipalCommitteeID.Trim(), out committeeID) ? committeeID as char? : null
            };
        }
    }
}
