using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Business object representation of an authorized committee.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class AuthorizedCommittee : Committee
    {
        /// <summary>
        /// Gets or sets whether or not the committee is active in the current election cycle.
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets whether or not the committee is a principal committee.
        /// </summary>
        [DataMember]
        public bool IsPrincipal { get; set; }

        /// <summary>
        /// Gets or sets the date the the certification was notarized.
        /// </summary>
        [DataMember]
        public DateTime? NotarizationDate { get; set; }

        /// <summary>
        /// Gets or sets the committee's treasurer.
        /// </summary>
        [DataMember]
        public Entity Treasurer { get; set; }

        /// <summary>
        /// Gets or sets the date of the most recent election in which the candidate ran for office.
        /// </summary>
        [DataMember]
        public DateTime? LastElectionDate { get; set; }

        /// <summary>
        /// Gets or sets the most recent political office sought by the candidate.
        /// </summary>
        [DataMember]
        public string LastElectionOffice { get; set; }

        /// <summary>
        /// Gets or sets the district of the most recent political office sought by the candidate, if applicable.
        /// </summary>
        [DataMember]
        public string LastElectionDistrict { get; set; }

        /// <summary>
        /// Gets or sets the party of the last primary election entered by the candidate.
        /// </summary>
        [DataMember]
        public string LastPrimaryParty { get; set; }

        /// <summary>
        /// Gets or sets the committee's campaign liaisons.
        /// </summary>
        [DataMember]
        public Dictionary<byte, Liaison> Liaisons { get; set; }

        /// <summary>
        /// Gets or sets the committee's bank accounts.
        /// </summary>
        [DataMember]
        public Dictionary<byte, BankAccount> BankAccounts { get; set; }

        /// <summary>
        /// Gets or sets the date when the candidate's profile was last updated in CFIS.
        /// </summary>
        [DataMember]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Creates a new authorized committee with a CFIS ID.
        /// </summary>
        /// <param name="id">The committee's CFIS ID.</param>
        internal AuthorizedCommittee(char id)
            : base(id)
        {
        }

        /// <summary>
        /// Retrieves all authorized committees for a candidate in a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose authorized committees are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all authorized committees on record for the specified candidate and election cycle.</returns>
        public static AuthorizedCommittees GetAuthorizedCommittees(string candidateID, string electionCycle)
        {
            return CPProviders.DataProvider.GetAuthorizedCommittees(candidateID, electionCycle);
        }

        /// <summary>
        /// Retrieves the CFIS ID of a candidate's primary committee for a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate desired.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The CFIS ID of the candidate's primary committee if found, else null.</returns>
        public static char? GetPrimaryCommitteeID(string candidateID, string electionCycle)
        {
            return CPProviders.DataProvider.GetPrimaryCommitteeID(candidateID, electionCycle);
        }
    }
}
