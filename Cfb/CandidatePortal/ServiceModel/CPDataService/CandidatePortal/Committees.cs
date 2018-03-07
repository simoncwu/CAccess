using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.CampaignContacts;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.AuthorizedCommitteeTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves all authorized committees for a candidate in a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose authorized committees are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all authorized committees on record for the specified candidate and election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        AuthorizedCommittees GetAuthorizedCommittees(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves the CFIS ID of a candidate's primary committee for a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate desired.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The CFIS ID of the candidate's primary committee if found, else null.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        char? GetPrimaryCommitteeID(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves committees for a candidate.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate desired.</param>
        /// <param name="committeeID">The ID of a committee to search for.</param>
        /// <returns>A collection of all committees on record for the specified candidate with the specified committee ID (if provided).</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        List<Committee> GetCommittees(string candidateID, char? committeeID = null);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves all authorized committees for a candidate in a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose authorized committees are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all authorized committees on record for the specified candidate and election cycle.</returns>
        public AuthorizedCommittees GetAuthorizedCommittees(string candidateID, string electionCycle)
        {
            using (AuthorizedCommitteeTds ds = new AuthorizedCommitteeTds())
            {
                using (AuthorizedCommitteesTableAdapter ta = new AuthorizedCommitteesTableAdapter())
                {
                    ta.Fill(ds.AuthorizedCommittees, candidateID, electionCycle);
                }
                AuthorizedCommittees c = new AuthorizedCommittees(ds.AuthorizedCommittees.Count);
                foreach (AuthorizedCommitteeTds.AuthorizedCommitteesRow row in ds.AuthorizedCommittees.Rows)
                {
                    if (string.IsNullOrWhiteSpace(row.CommitteeID))
                        continue;

                    // basic committee info
                    AuthorizedCommittee ac = new AuthorizedCommittee(row.CommitteeID.ToCharArray()[0])
                    {
                        // authorized committee info
                        NotarizationDate = row.IsSwornDateNull() ? null : row.SwornDate as DateTime?,
                        IsActive = "Y".Equals(row.IsActive.Trim(), StringComparison.CurrentCultureIgnoreCase),
                        IsPrincipal = "Y".Equals(row.IsPrincipal.Trim(), StringComparison.CurrentCultureIgnoreCase),
                        ContactOrder = CPConvert.ToContactOrder(row.TreasurerContactOrder),
                        LastUpdated = row.LastUpdated,

                        // treasurer info
                        Treasurer = new Entity()
                        {
                            Type = EntityType.Treasurer,
                            Honorific = CPConvert.ToHonorific(row.TreasurerHonorificCode.Trim()),
                            LastName = row.TreasurerLastName.Trim(),
                            FirstName = row.TreasurerFirstName.Trim(),
                            MiddleInitial = string.IsNullOrWhiteSpace(row.TreasurerMI) ? null : row.TreasurerMI.Trim().ToCharArray()[0] as char?,
                            Address = new PostalAddress()
                            {
                                StreetNumber = row.TreasurerStreetNumber.Trim(),
                                StreetName = row.TreasurerStreetName.Trim(),
                                Apartment = row.TreasurerApartment.Trim(),
                                City = row.TreasurerCity.Trim(),
                                State = row.TreasurerState.Trim(),
                                Zip = new ZipCode(row.TreasurerZip.Trim())
                            },
                            DaytimePhone = new PhoneNumber(row.TreasurerDaytimePhone.Trim())
                            {
                                Extension = row.TreasurerDaytimePhoneExt.Trim()
                            },
                            EveningPhone = new PhoneNumber(row.TreasurerEveningPhone.Trim())
                            {
                                Extension = row.TreasurerEveningPhoneExt.Trim()
                            },
                            Fax = new PhoneNumber(row.TreasurerFax.Trim()),
                            Email = row.TreasurerEmail.Trim(),
                            ContactOrder = CPConvert.ToContactOrder(row.TreasurerContactOrder),
                            // treasurer employer
                            Employer = new Entity()
                            {
                                Type = EntityType.Employer,
                                LastName = row.TreasurerEmployerName.Trim(),
                                Address = new PostalAddress()
                                {
                                    StreetNumber = row.TreasurerEmployerStreetNumber.Trim(),
                                    StreetName = row.TreasurerEmployerStreetName.Trim(),
                                    City = row.TreasurerEmployerCity.Trim(),
                                    State = row.TreasurerEmployerState.Trim(),
                                    Zip = new ZipCode(row.TreasurerEmployerZip.Trim())
                                },
                                DaytimePhone = new PhoneNumber(row.TreasurerEmployerPhone.Trim())
                                {
                                    Extension = row.TreasurerEmployerPhoneExt.Trim()
                                },
                                Fax = new PhoneNumber(row.TreasurerEmployerFax.Trim()),
                            }
                        },

                        // last election info
                        LastElectionDate = row.IsLastElectionDateNull() ? null : row.LastElectionDate as DateTime?,
                        LastElectionOffice = row.LastElectionOffice.Trim(),
                        LastElectionDistrict = row.LastElectionDistrict.Trim(),
                        LastPrimaryParty = row.IsLastPrimaryPartyNull() ? null : row.LastPrimaryParty.Trim()
                    }.LoadCommitteeData(row);

                    // liaisons
                    ac.Liaisons = this.GetLiaisons(candidateID, ac.ID);

                    // bank accounts
                    ac.BankAccounts = this.GetBankAccounts(candidateID, electionCycle, ac.ID);

                    c.Committees.Add(ac.ID, ac);
                }
                return c;
            }
        }

        /// <summary>
        /// Retrieves the CFIS ID of a candidate's primary committee for a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate desired.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The CFIS ID of the candidate's primary committee if found, else null.</returns>
        public char? GetPrimaryCommitteeID(string candidateID, string electionCycle)
        {
            using (AuthorizedCommitteeTds ds = new AuthorizedCommitteeTds())
            {
                using (AuthorizedCommitteesTableAdapter ta = new AuthorizedCommitteesTableAdapter())
                {
                    ta.Fill(ds.AuthorizedCommittees, candidateID, electionCycle);
                }
                foreach (AuthorizedCommitteeTds.AuthorizedCommitteesRow row in ds.AuthorizedCommittees.Rows)
                {
                    if ("Y".Equals(row.IsPrincipal.Trim()))
                    {
                        string id = row.CommitteeID.Trim();
                        if (!string.IsNullOrEmpty(id))
                            return id.ToCharArray()[0];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves committees for a candidate.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate desired.</param>
        /// <param name="committeeID">The ID of a committee to search for.</param>
        /// <returns>A collection of all committees on record for the specified candidate with the specified committee ID (if provided).</returns>
        public List<Committee> GetCommittees(string candidateID, char? committeeID = null)
        {
            using (AuthorizedCommitteeTds ds = new AuthorizedCommitteeTds())
            {
                using (AuthorizedCommitteesTableAdapter ta = new AuthorizedCommitteesTableAdapter())
                {
                    ta.FillCommitteeBy(ds.AuthorizedCommittees, candidateID, committeeID.HasValue ? committeeID.Value.ToString() : null);
                }
                List<Committee> comms = new List<Committee>(ds.AuthorizedCommittees.Count);
                foreach (AuthorizedCommitteeTds.AuthorizedCommitteesRow row in ds.AuthorizedCommittees.Rows)
                {
                    if (string.IsNullOrWhiteSpace(row.CommitteeID))
                        continue;
                    comms.Add(new Committee(row.CommitteeID.ToCharArray()[0]).LoadCommitteeData(row));
                }
                return comms;
            }
        }
    }

    /// <summary>
    /// Extensions for committee data operations.
    /// </summary>
    internal static class Committees_Extensions
    {
        /// <summary>
        /// Loads a committee with data from a <see cref="AuthorizedCommitteeTds.AuthorizedCommitteesRow"/> row.
        /// </summary>
        /// <param name="c">The committee to load data into.</param>
        /// <param name="data">The data to load.</param>
        internal static T LoadCommitteeData<T>(this T c, AuthorizedCommitteeTds.AuthorizedCommitteesRow data) where T : Committee
        {
            if (c == null || data == null)
                return null;
            c.LastName = data.Name.Trim();

            // address
            c.Address = new PostalAddress()
            {
                StreetNumber = data.StreetNumber.Trim(),
                StreetName = data.StreetName.Trim(),
                Apartment = data.Apartment.Trim(),
                City = data.City.Trim(),
                State = data.State.Trim(),
                Zip = new ZipCode(data.Zip.Trim())
            };
            c.DaytimePhone = new PhoneNumber(data.DaytimePhone.Trim())
            {
                Extension = data.DaytimePhoneExt.Trim()
            };
            c.EveningPhone = new PhoneNumber(data.EveningPhone.Trim())
            {
                Extension = data.EveningPhoneExt.Trim()
            };
            c.Fax = new PhoneNumber(data.Fax.Trim());
            c.BoeDate = data.IsBoeDateNull() ? null : data.BoeDate as DateTime?;
            c.TerminationDate = data.IsTerminationDateNull() ? null : data.TerminationDate as DateTime?;
            c.WebsiteUrl = data.WebsiteUrl.Trim();

            // mailing address
            c.MailingAdress = new PostalAddress()
            {
                AddressLine1 = data.MailingAddressLine1.Trim(),
                StreetNumber = data.MailingStreetNumber.Trim(),
                StreetName = data.MailingStreetName.Trim(),
                Apartment = data.MailingApartment.Trim(),
                City = data.MailingCity.Trim(),
                State = data.MailingState.Trim(),
                Zip = new ZipCode(data.MailingZip.Trim())
            };
            c.Email = data.Email.Trim();
            return c;
        }
    }
}
