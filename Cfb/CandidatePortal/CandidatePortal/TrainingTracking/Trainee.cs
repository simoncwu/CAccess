using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.TrainingTracking
{
    /// <summary>
    /// A CFB training trainee.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class Trainee : Entity
    {
        /// <summary>
        /// The ID of the trainee.
        /// </summary>
        [DataMember(Name = "ID")]
        private readonly short _id;

        /// <summary>
        /// Gets the ID of the trainee.
        /// </summary>
        public short ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the date the trainee was last updated.
        /// </summary>
        [DataMember]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the relationship the trainee has with the campaign.
        /// </summary>
        [DataMember]
        public CampaignRelationship CampaignRelationship { get; set; }

        /// <summary>
        /// Gets whether or not this trainee is eligible for satisfying audit compliance.
        /// </summary>
        public bool AuditComplianceEligible
        {
            get
            {
                switch (this.CampaignRelationship)
                {
                    case CampaignRelationship.Candidate:
                    case CampaignRelationship.Treasurer:
                    case CampaignRelationship.Manager:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Gets whether or not this trainee is eligible for satisfying compliance.
        /// </summary>
        public bool ComplianceEligible
        {
            get
            {
                switch (this.CampaignRelationship)
                {
                    case CampaignRelationship.Candidate:
                    case CampaignRelationship.Treasurer:
                    case CampaignRelationship.Manager:
                    case CampaignRelationship.Authority:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trainee"/> class.
        /// </summary>
        /// <param name="id">The trainee ID.</param>
        public Trainee(short id)
        {
            _id = id;
        }
    }
}
