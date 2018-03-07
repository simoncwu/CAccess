using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using System.Text;
using Cfb.Cfis.CampaignContacts;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.CandidateTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves basic profile information for a candidate.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <returns>The requested candidate's profile information if found, otherwise null.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Candidate GetCandidate(string candidateID);

        /// <summary>
        /// Retrieves basic profile information for all known candidates.
        /// </summary>
        /// <returns>A collection of <see cref="Candidate"/> objects representing all known candidates indexed by CFIS ID.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Dictionary<string, Candidate> GetCandidates();

        /// <summary>
        /// Retrieves the full name of a candidate by CFIS ID.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate to find.</param>
        /// <param name="formal">Whether or not to retrieve a formal name.</param>
        /// <returns>The full name of the specified candidate.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        string GetCandidateName(string candidateID, bool formal);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves basic profile information for a candidate.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <returns>The requested candidate's profile information if found, otherwise null.</returns>
        public Candidate GetCandidate(string candidateID)
        {
            using (CandidateTds ds = new CandidateTds())
            {
                using (CandidateTableAdapter ta = new CandidateTableAdapter())
                {
                    ta.FillBy(ds.Candidate, candidateID);
                }
                foreach (CandidateTds.CandidateRow row in ds.Candidate.Rows)
                {
                    return Parse(row);
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves basic profile information for all known candidates.
        /// </summary>
        /// <returns>A collection of <see cref="Candidate"/> objects representing all known candidates indexed by CFIS ID.</returns>
        public Dictionary<string, Candidate> GetCandidates()
        {
            Dictionary<string, Candidate> candidates = new Dictionary<string, Candidate>();
            using (CandidateTds ds = new CandidateTds())
            {
                using (CandidateTableAdapter ta = new CandidateTableAdapter())
                {
                    ta.Fill(ds.Candidate);
                }
                foreach (CandidateTds.CandidateRow row in ds.Candidate.Rows)
                {
                    Candidate c = Parse(row);
                    if (c != null)
                        candidates.Add(c.ID, c);
                }
            }
            return candidates;
        }

        /// <summary>
        /// Converts the <see cref="CandidateTds.CandidateRow"/> representation of a candidate into its <see cref="Candidate"/> object equivalent.
        /// </summary>
        /// <param name="row">A <see cref="CandidateTds.CandidateRow"/> containing the candidate to convert.</param>
        /// <returns>A <see cref="Candidate"/> equivalent to the candidate represented by <paramref name="row"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="row"/> is a null reference.</exception>
        private Candidate Parse(CandidateTds.CandidateRow row)
        {
            if (row == null)
                throw new ArgumentNullException("row", "Row must be valid and cannot be null.");
            return new Candidate(row.CandidateID.Trim())
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
                }
            };
        }

        /// <summary>
        /// Retrieves the full name of a candidate by CFIS ID.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate to find.</param>
        /// <param name="formal">Whether or not to retrieve a formal name.</param>
        /// <returns>The full name of the specified candidate.</returns>
        public string GetCandidateName(string candidateID, bool formal)
        {
            if (string.IsNullOrEmpty(candidateID))
                return string.Empty;
            using (CandidateTds ds = new CandidateTds())
            {
                using (CandidateNameTableAdapter ta = new CandidateNameTableAdapter())
                {
                    ta.Fill(ds.CandidateName, candidateID);
                }
                foreach (CandidateTds.CandidateNameRow row in ds.CandidateName)
                {
                    return Candidate.ToFullName(row.FirstName, row.LastName, string.IsNullOrWhiteSpace(row.MiddleInitial) ? null : row.MiddleInitial.Trim().ToCharArray()[0] as char?, formal);
                }
                return null;
            }
        }
    }
}
