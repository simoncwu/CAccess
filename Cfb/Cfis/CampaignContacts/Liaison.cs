using System.Runtime.Serialization;

namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Business object representation of a campaign liaison or consultant.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class Liaison : Entity
    {
        /// <summary>
        /// The liaison identifier.
        /// </summary>
        [DataMember(Name = "ID")]
        private byte _id;

        /// <summary>
        /// Gets the liaison identifier.
        /// </summary>
        public byte ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the liaison type.
        /// </summary>
        [DataMember]
        public LiaisonType LiaisonType { get; set; }

        /// <summary>
        /// The consultant entity name (if applicable).
        /// </summary>
        [DataMember(Name = "EntityName")]
        private string _entityName;

        /// <summary>
        /// Gets or sets the consultant entity name (if applicable).
        /// </summary>
        public string EntityName
        {
            get { return this.LiaisonType == LiaisonType.Consultant ? _entityName : null; }
            set { _entityName = this.LiaisonType == LiaisonType.Consultant ? value : null; }
        }

        /// <summary>
        /// Gets or sets whether or not the liaison has managerial control in the campaign.
        /// </summary>
        [DataMember]
        public bool HasManagerialControl { get; set; }

        /// <summary>
        /// Gets or sets whether or not the liaison has rights to the campaign's Voter Guide profile.
        /// </summary>
        [DataMember]
        public bool IsVGLiaison { get; set; }

        /// <summary>
        /// Creates a new instance of a <see cref="Liaison"/> object.
        /// </summary>
        /// <param name="id">A liaison identifier.</param>
        public Liaison(byte id)
        {
            _id = id;
        }
    }
}
