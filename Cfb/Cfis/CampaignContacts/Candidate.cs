using System;
using System.Runtime.Serialization;

namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Business object representation of a candidate's profile.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class Candidate : Entity
    {
        /// <summary>
        /// The candidate's CFIS ID.
        /// </summary>
        [DataMember(Name = "ID")]
        protected readonly string _ID;

        /// <summary>
        /// Gets the candidate's CFIS ID.
        /// </summary>
        public string ID
        {
            get { return _ID; }
        }

        /// <summary>
        /// Gets or sets the date when the candidate's profile was last updated in CFIS.
        /// </summary>
        [DataMember]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Creates a new instance of a candidate with a CFIS ID.
        /// </summary>
        /// <param name="candidateID">The candidate's CFIS ID.</param>
        public Candidate(string candidateID)
        {
            _ID = candidateID;
        }
    }
}
