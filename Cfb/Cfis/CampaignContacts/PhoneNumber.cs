using System;
using System.Runtime.Serialization;

namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Business object representation of a phone number.
    /// </summary>
    /// <remarks>
    /// A phone number is immutable, as changing any digit would represent a completely different number. To change a phone number, a new <see cref="PhoneNumber"/> has to be instantiated. The only exception is the phone number extension, which can be changed at any time.
    /// </remarks>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class PhoneNumber
    {
        /// <summary>
        /// The three-digit Numbering Plan Area code of a phone number.
        /// </summary>
        [DataMember(Name = "AreaCode")]
        private ushort _areaCode;

        /// <summary>
        /// Gets the three-digit Numbering Plan Area code of a phone number.
        /// </summary>
        public string AreaCode
        {
            get { return this.HasValidAreaCode ? _areaCode.ToString() : string.Empty; }
        }

        /// <summary>
        /// Gets whether or not the phone number has a valid Numbering Plan Area code.
        /// </summary>
        private bool HasValidAreaCode
        {
            get { return _areaCode > 200; }
        }

        /// <summary>
        /// The Exchange code, or first three digits, of a phone number.
        /// </summary>
        [DataMember(Name = "ExchangeCode")]
        private ushort _exchangeCode;

        /// <summary>
        /// Gets whether or not the phone number has a valid Exchange code.
        /// </summary>
        private bool HasValidExchangeCode
        {
            get { return _exchangeCode > 200; }
        }

        /// <summary>
        /// The Station code, or last four digits, of a phone number.
        /// </summary>
        [DataMember(Name = "StationCode")]
        private ushort _stationCode;

        /// <summary>
        /// Gets the string-formatted seven-digit numeric portion of a phone number.
        /// </summary>
        public string Number
        {
            get { return this.HasValidExchangeCode ? _exchangeCode.ToString() + _stationCode.ToString("D4") : string.Empty; }
        }

        /// <summary>
        /// Gets or sets the phone number extension.
        /// </summary>
        [DataMember]
        public string Extension { get; set; }

        /// <summary>
        /// Converts the <see cref="PhoneNumber"/> to its equivalent string representation.
        /// </summary>
        /// <returns>A formatted <see cref="String"/> representation of the phone number.</returns>
        public override string ToString()
        {
            if (!this.IsValid)
                return string.Empty;
            string area = this.AreaCode;
            string number = this.Number;
            string ext = this.Extension;
            ext = !string.IsNullOrEmpty(ext) ? string.Format(" x{0}", ext) : string.Empty;
            return string.Format("({0}) {1}-{2}{3}", _areaCode.ToString("D3"), _exchangeCode.ToString("D3"), _stationCode.ToString("D4"), ext);
        }

        /// <summary>
        /// Gets whether or not this phone number is valid.
        /// </summary>
        public bool IsValid
        {
            get { return (_areaCode + _exchangeCode + _stationCode) > 0; }
        }

        /// <summary>
        /// Creates a new <see cref="PhoneNumber"/> object representation of a phone number.
        /// </summary>
        /// <param name="number">A <see cref="String"/> containing a ten-digit phone number.</param>
        public PhoneNumber(string number)
        {
            _areaCode = 0;
            _exchangeCode = 0;
            _stationCode = 0;
            if (!string.IsNullOrEmpty(number) && !number.Contains(" "))
            {
                if (number.Length == 10)
                {
                    ushort.TryParse(number.Substring(0, 3), out _areaCode);
                    number = number.Substring(3);
                }
                if (number.Length == 7)
                {
                    ushort.TryParse(number.Substring(0, 3), out _exchangeCode);
                    ushort.TryParse(number.Substring(3), out _stationCode);
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="PhoneNumber"/> object representation of a phone number. Unused.
        /// </summary>
        private PhoneNumber()
        {
        }
    }
}
