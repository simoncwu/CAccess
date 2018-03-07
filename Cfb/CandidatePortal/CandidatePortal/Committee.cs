using System;
using System.Runtime.Serialization;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Business object representation of a committee.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class Committee : Entity
    {
        /// <summary>
        /// The committee CFIS ID.
        /// </summary>
        [DataMember(Name = "ID")]
        private readonly char _id;

        /// <summary>
        /// Gets the committee CFIS ID.
        /// </summary>
        public char ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the committee's mailing address.
        /// </summary>
        [DataMember]
        public PostalAddress MailingAdress { get; set; }

        /// <summary>
        /// Gets or sets the date the committee authorized in filing with the board of elections.
        /// </summary>
        [DataMember]
        public DateTime? BoeDate { get; set; }

        /// <summary>
        /// Gets or sets the date the committee was terminated.
        /// </summary>
        [DataMember]
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// Gets or sets the committee's web site.
        /// </summary>
        [DataMember]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Creates a new committee with a CFIS ID.
        /// </summary>
        /// <param name="id">The committee's CFIS ID.</param>
        public Committee(char id)
        {
            _id = char.ToUpper(id);
        }
    }
}
