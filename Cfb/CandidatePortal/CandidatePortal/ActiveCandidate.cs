using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Business object representation of an active candidate's profile.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class ActiveCandidate : Candidate
    {
        #region Active Candidate Properties

        /// <summary>
        /// The election cycle in which the candidate is active.
        /// </summary>
        [DataMember(Name = "ElectionCycle")]
        private readonly string _electionCycle;

        /// <summary>
        /// Gets the election cycle in which the candidate is active.
        /// </summary>
        public string ElectionCycle
        {
            get { return _electionCycle; }
        }

        /// <summary>
        /// Gets or sets the public office sought by the candidate.
        /// </summary>
        [DataMember]
        public NycPublicOffice Office { get; set; }

        /// <summary>
        /// Gets or sets the date the candidate certified, if applicable.
        /// </summary>
        [DataMember]
        public DateTime? CertificationDate { get; set; }

        /// <summary>
        /// Gets whether or not the candidate has certified with the CFB.
        /// </summary>
        public bool HasCertified
        {
            get { return this.CertificationDate.HasValue; }
        }

        /// <summary>
        /// Gets whether or not the candidate has registered with the CFB as a filer.
        /// </summary>
        public bool HasRegistered
        {
            get { return this.FilerRegistrationDate.HasValue; }
        }

        /// <summary>
        /// Gets or sets the date the candidate registered with the CFB via a Filer Registration Form, if applicable.
        /// </summary>
        [DataMember]
        public DateTime? FilerRegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the candidate's classification with respect to "the Program".
        /// </summary>
        [DataMember]
        public CfbClassification Classification { get; set; }

        /// <summary>
        /// Gets or sets the candidate's date of termination.
        /// </summary>
        [DataMember]
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// Gets whether or not the candidate has terminated.
        /// </summary>
        public bool IsTerminated
        {
            get { return this.TerminationDate.HasValue; }
        }

        /// <summary>
        /// Gets or sets the reason for the candidate's termination.
        /// </summary>
        [DataMember]
        public TerminationReason TerminationReason { get; set; }

        /// <summary>
        /// Gets or sets the candidate's political party affiliation.
        /// </summary>
        [DataMember]
        public string PoliticalParty { get; set; }

        /// <summary>
        /// Gets or sets whether or not the candidate is authorized for direct deposits.
        /// </summary>
        [DataMember]
        public bool IsDirectDepositAuthorized { get; set; }

        /// <summary>
        /// Gets or sets whether or not the candidate is authorized for run-off/re-run election direct deposits.
        /// </summary>
        [DataMember]
        public bool IsRRDirectDepositAuthorized { get; set; }

        /// <summary>
        /// Gets or sets the name of the auditor for the candidate.
        /// </summary>
        [DataMember]
        public string AuditorName { get; set; }

        /// <summary>
        /// Gets or sets the name of the CSU liaison for the candidate.
        /// </summary>
        [DataMember]
        public string CsuLiaisonName { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the name of the candidate's principal committee.
        /// </summary>
        [DataMember]
        public string PrincipalCommittee { get; set; }

        /// <summary>
        /// Gets or sets the CFIS ID of the candidate's principal committee.
        /// </summary>
        [DataMember]
        public char? PrincipalCommitteeID { get; set; }

        /// <summary>
        /// Creates a new instance of a candidate with a CFIS ID.
        /// </summary>
        /// <param name="candidateID">The candidate's CFIS ID.</param>
        /// <param name="electionCycle">The eleciton cycle in which the candidate is active.</param>
        public ActiveCandidate(string candidateID, string electionCycle)
            : base(candidateID)
        {
            _electionCycle = electionCycle;
        }

        /// <summary>
        /// Returns profile information for a candidate who is active in an election cycle.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>The specified candidate's profile for the specified election cycle.</returns>
        public static ActiveCandidate GetActiveCandidate(string candidateID, string electionCycle)
        {
            return CPProviders.DataProvider.GetActiveCandidate(candidateID, electionCycle);
        }

        /// <summary>
        /// Retrieves basic profile information for all candidates active in a specific election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter by.</param>
        /// <returns>A collection of <see cref="ActiveCandidate"/> objects representing all candidates active in the <paramref name="electionCycle"/> election cycle, indexed by CFIS ID.</returns>
        public static Dictionary<string, ActiveCandidate> GetActiveCandidates(string electionCycle)
        {
            return CPProviders.DataProvider.GetActiveCandidates(electionCycle);
        }
    }
}
