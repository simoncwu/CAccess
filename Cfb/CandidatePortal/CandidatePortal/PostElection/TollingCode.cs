using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
    /// <summary>
    /// A code representing a tolling source, event, or type.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class TollingCode
    {
        /// <summary>
        /// The tolling code type.
        /// </summary>
        [DataMember(Name = "Type")]
        private readonly TollingCodeType _type;

        /// <summary>
        /// Gets the tolling code type.
        /// </summary>
        public TollingCodeType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// The tolling code ID.
        /// </summary>
        [DataMember(Name = "ID")]
        private readonly byte _id;

        /// <summary>
        /// Gets the tolling code ID.
        /// </summary>
        public byte ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the tolling code.
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the user-friendly description of the tolling code.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets whether or not the tolling code represents an extension event.
        /// </summary>
        public bool IsExtensionEvent
        {
            get { return _type == TollingCodeType.EventCode && this.Code != null && Properties.Settings.Default.ExtensionTollingEventCodes.Contains(this.Code.ToUpper()); }
        }

        /// <summary>
        /// Gets whether or not the tolling code represents an extension type.
        /// </summary>
        public bool IsExtensionType
        {
            get { return _type == TollingCodeType.TypeCode && string.Equals(Properties.Settings.Default.ExtensionTollingTypeCode, this.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Gets whether or not the tolling code represents a late response.
        /// </summary>
        public bool IsLateResponseType
        {
            get { return string.Equals(Properties.Settings.Default.LateResponseTollingTypeCode, this.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Gets whether or not the tolling code represents an inadequate response.
        /// </summary>
        public bool IsInadequateResponseType
        {
            get { return string.Equals(Properties.Settings.Default.InadequateResponseTollingTypeCode, this.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TollingCode"/> class with a specific tolling code type and ID.
        /// </summary>
        /// <param name="type">The tolling code type.</param>
        /// <param name="id">The tolling code ID.</param>
        public TollingCode(TollingCodeType type, byte id)
        {
            _type = type;
            _id = id;
        }
    }
}
