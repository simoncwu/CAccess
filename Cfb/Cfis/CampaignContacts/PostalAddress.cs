using System;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Business object representation of a United States postal address.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class PostalAddress
    {
        /// <summary>
        /// Gets or sets the first line of the address (company name, c/o, etc.).
        /// </summary>
        [DataMember]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the street address number.
        /// </summary>
        [DataMember]
        public string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the street address name.
        /// </summary>
        [DataMember]
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the apartment portion of the address.
        /// </summary>
        [DataMember]
        public string Apartment { get; set; }

        /// <summary>
        /// Gets or sets the city portion of the address.
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state portion of the address.
        /// </summary>
        [DataMember]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the ZIP code portion of the address.
        /// </summary>
        [DataMember]
        public ZipCode Zip { get; set; }

        /// <summary>
        /// Gets whether or not the postal address is valid.
        /// </summary>
        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(this.State) || !string.IsNullOrEmpty(this.StreetName); }
        }

        /// <summary>
        /// Converts the <see cref="PostalAddress"/> to its equivalent string representation.
        /// </summary>
        /// <returns>A formatted <see cref="String"/> representation of the postal address.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.AddressLine1))
            {
                sb.Append(this.AddressLine1);
                sb.Append(Environment.NewLine);
            }
            string street = string.Format("{0} {1}", this.StreetNumber, this.StreetName).Trim();
            bool hasLine2 = false;
            if (!string.IsNullOrEmpty(street))
            {
                sb.Append(street);
                hasLine2 = true;
            }
            if (!string.IsNullOrEmpty(this.Apartment))
            {
                sb.Append("Apt. ");
                sb.Append(this.Apartment);
                hasLine2 = true;
            }
            if (hasLine2)
                sb.Append(Environment.NewLine);
            sb.Append(string.Format("{0}, {1} {2}", this.City, this.State, (this.Zip == null) ? string.Empty : this.Zip.ToString()));
            return sb.ToString();
        }
    }
}
