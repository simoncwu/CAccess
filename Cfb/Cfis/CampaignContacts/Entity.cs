using System.Runtime.Serialization;
using System.Text;

namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Business object representation of a campaign entity, which comprises any individual or organization reported to the CFB.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    [KnownType(typeof(Candidate))]
    [KnownType(typeof(Liaison))]
    public class Entity
    {
        /// <summary>
        /// Gets or sets the entity's honorific code.
        /// </summary>
        [DataMember]
        public Honorific Honorific { get; set; }

        /// <summary>
        /// Gets or sets the entity's last name.
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the entity's first name.
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the entity's middle initial, if available.
        /// </summary>
        [DataMember]
        public char? MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the entity's address.
        /// </summary>
        [DataMember]
        public PostalAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the entity's daytime phone number.
        /// </summary>
        [DataMember]
        public PhoneNumber DaytimePhone { get; set; }

        /// <summary>
        /// Gets or sets the entity's evening phone number.
        /// </summary>
        [DataMember]
        public PhoneNumber EveningPhone { get; set; }

        /// <summary>
        /// Gets or sets the entity's fax number.
        /// </summary>
        [DataMember]
        public PhoneNumber Fax { get; set; }

        /// <summary>
        /// Gets or sets the entity's employer.
        /// </summary>
        [DataMember]
        public Entity Employer { get; set; }

        /// <summary>
        /// Gets or sets the entity's e-mail address.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the entity's contact order priority.
        /// </summary>
        [DataMember]
        public ContactOrder ContactOrder { get; set; }

        /// <summary>
        /// Gets or sets the entity type.
        /// </summary>
        [DataMember]
        public EntityType Type { get; set; }

        /// <summary>
        /// Gets the entity's full name.
        /// </summary>
        public string Name
        {
            get { return GetFullName(false); }
        }

        /// <summary>
        /// Gets the entity's formal name (last name, first name format).
        /// </summary>
        public string FormalName
        {
            get { return GetFullName(true); }
        }

        /// <summary>
        /// Creates a new instance of an <see cref="Entity"/> object.
        /// </summary>
        public Entity()
        {
        }

        /// <summary>
        /// Gets the full name of the entity.
        /// </summary>
        /// <param name="formal">Whether or not to retrieve a formal name.</param>
        /// <returns>The full name of the entity.</returns>
        public string GetFullName(bool formal)
        {
            return Entity.ToFullName(this.FirstName, this.LastName, this.MiddleInitial, formal);
        }

        /// <summary>
        /// Converts a first and last name and middle initial to a full name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="middleInitial">The middle initial.</param>
        /// <param name="formal">Whether or not a formal name is desired.</param>
        /// <returns>The full name equivalent of the values supplied.</returns>
        public static string ToFullName(string firstName, string lastName, char? middleInitial, bool formal)
        {
            StringBuilder name = new StringBuilder();
            if (firstName != null)
                firstName = firstName.Trim();
            if (lastName != null)
                lastName = lastName.Trim();
            if (formal)
            {
                name.Append(lastName);
                if (!string.IsNullOrEmpty(firstName))
                    name.AppendFormat(", {0}", firstName);
                if (middleInitial.HasValue)
                    name.AppendFormat(" {0}.", middleInitial.Value);
            }
            else
            {
                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(firstName))
                    name.Append(firstName + " ");
                if (middleInitial.HasValue)
                    name.Append(middleInitial.Value + ". ");
                name.Append(lastName);
            }
            return name.ToString();
        }
    }
}
