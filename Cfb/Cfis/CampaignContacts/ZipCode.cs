using System.Runtime.Serialization;

namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Business object representation of an extended ZIP + 4 Zone Improvement Plan code.
    /// </summary>
    /// <remarks>
    /// A ZIP code is immutable, as changing it would represent a completely different ZIP code. To change ZIP codes, a new <see cref="ZipCode"/> has to be instantiated.
    /// </remarks>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class ZipCode
    {
        /// <summary>
        /// The basic first five digits of a ZIP+4 code.
        /// </summary>
        [DataMember(Name = "Zip5")]
        private readonly uint _zip5;

        /// <summary>
        /// Gets the basic first five digits of a ZIP+4 code.
        /// </summary>
        public string Zip5
        {
            get { return this.IsValid ? _zip5.ToString("D5") : string.Empty; }
        }

        /// <summary>
        /// Gets whether or not the ZIP code is valid.
        /// </summary>
        public bool IsValid
        {
            get { return _zip5 > 0; }
        }

        /// <summary>
        /// The last four add-on digits of a ZIP+4 code.
        /// </summary>
        [DataMember(Name = "Zip4")]
        private uint _zip4;

        /// <summary>
        /// Gets whether or not the zip code is a ZIP+4 code.
        /// </summary>
        public bool IsZip4
        {
            get { return _zip4 > 0; }
        }

        /// <summary>
        /// Gets the last four add-on digits of a ZIP+4 code.
        /// </summary>
        public string Zip4
        {
            get { return this.IsZip4 ? _zip4.ToString("D4") : string.Empty; }
        }

        /// <summary>
        /// Converts the <see cref="ZipCode"/> to its equivalent string representation.
        /// </summary>
        /// <returns>A formatted <see cref="String"/> representation of the ZIP code.</returns>
        public override string ToString()
        {
            return this.Zip5 + (this.IsZip4 ? "-" + this.Zip4 : string.Empty);
        }

        /// <summary>
        /// Creates a new <see cref="ZipCode"/> object representation of a ZIP code.
        /// </summary>
        /// <param name="zip">A <see cref="String"/> containing a 5-digit ZIP code or a 9-digit ZIP+4 code (numeric digits only).</param>
        public ZipCode(string zip)
        {
            _zip5 = 0;
            _zip4 = 0;
            if (string.IsNullOrEmpty(zip) || zip.Contains(" ")) return;
            if (zip.Length == 5)
                uint.TryParse(zip, out _zip5);
            else if (zip.Length > 5)
            {
                uint.TryParse(zip.Substring(0, 5), out _zip5);
                uint.TryParse(zip.Substring(5), out _zip4);
            }
        }

        /// <summary>
        /// Creates a new <see cref="ZipCode"/> object representation of a ZIP code. Unused.
        /// </summary>
        private ZipCode()
        {
        }
    }
}
