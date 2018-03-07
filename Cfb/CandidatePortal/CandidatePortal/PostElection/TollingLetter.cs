using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
    /// <summary>
    /// Definition of a tolling letter.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class TollingLetter
    {
        /// <summary>
        /// The tolling letter ID.
        /// </summary>
        [DataMember(Name = "ID")]
        private readonly byte _id;

        /// <summary>
        /// Gets the tolling letter ID.
        /// </summary>
        public byte ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the tolling source code.
        /// </summary>
        [DataMember]
        public TollingCode SourceCode { get; set; }

        /// <summary>
        /// Gets or sets the tolling event code.
        /// </summary>
        [DataMember]
        public TollingCode EventCode { get; set; }

        /// <summary>
        /// Gets or sets the tolling type code.
        /// </summary>
        [DataMember]
        public TollingCode TypeCode { get; set; }

        /// <summary>
        /// Gets or sets a user-friendly description of the tolling letter.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a user-friendly display name for the tolling letter.
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets whether or not the tolling letter is within the scope of an Initial Documentation Request.
        /// </summary>
        public bool HasIdrScope
        {
            get { return this.SourceCode != null && string.Equals(Properties.Settings.Default.IdrTollingSourceCode, this.SourceCode.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Gets whether or not the tolling letter is within the scope of IDR inadequate responses.
        /// </summary>
        public bool HasIdrInadequateScope
        {
            get { return this.SourceCode != null && string.Equals(Properties.Settings.Default.IdrInadequateResponseTollingSourceCode, this.SourceCode.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Gets whether or not the tolling letter is within the scope of a Draft Audit Report.
        /// </summary>
        public bool HasDarScope
        {
            get { return this.SourceCode != null && string.Equals(Properties.Settings.Default.DarTollingSourceCode, this.SourceCode.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Gets whether or not the tolling letter is within the scope of DAR inadequate responses.
        /// </summary>
        public bool HasDarInadequateScope
        {
            get { return this.SourceCode != null && string.Equals(Properties.Settings.Default.DarInadequateResponseTollingSourceCode, this.SourceCode.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Gets whether or not the tolling letter is within the scope of inadequate responses.
        /// </summary>
        public bool HasInadequateScope
        {
            get { return this.HasIdrInadequateScope || this.HasDarInadequateScope; }
        }

        /// <summary>
        /// Gets whether or not the tolling letter is within the scope of additional inadequate responses.
        /// </summary>
        public bool HasAdditionalInadequateScope
        {
            get { return this.EventCode != null && string.Equals(Properties.Settings.Default.AdditionalInadequateResponseTollingEventCode, this.EventCode.Code, StringComparison.InvariantCultureIgnoreCase); }
        }

        /// <summary>
        /// Gets whether or not the tolling letter is an extension.
        /// </summary>
        public bool IsExtension
        {
            get { return this.TypeCode != null && this.TypeCode.IsExtensionEvent; }
        }

        /// <summary>
        /// Gets whether or not the tolling letter is an inadequate response notice.
        /// </summary>
        public bool IsInadequate
        {
            get { return this.TypeCode != null && this.TypeCode.IsInadequateResponseType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TollingLetter"/> class.
        /// </summary>
        /// <param name="id">The tolling letter ID.</param>
        public TollingLetter(byte id)
        {
            _id = id;
        }

        /// <summary>
        /// Retrieves a tolling letter by tolling codes.
        /// </summary>
        /// <param name="sourceCode">The tolling source code to match.</param>
        /// <param name="eventCode">The tolling event code to match.</param>
        /// <param name="typeCode">The tolling type code to match.</param>
        /// <returns>The tolling letter designated by the specified tolling codes if one exists; otherwise, null.</returns>
        public static TollingLetter GetTollingLetter(string sourceCode, string eventCode, string typeCode)
        {
            return CPProviders.DataProvider.GetTollingLetter(sourceCode, eventCode, typeCode);
        }

        /// <summary>
        /// Retrieves a tolling letter by tolling letter ID.
        /// </summary>
        /// <param name="letterID">The ID of the tolling letter to retrieve.</param>
        /// <returns>The tolling letter designated by the specified tolling codes if found; otherwise, null.</returns>
        public static TollingLetter GetTollingLetter(int letterID)
        {
            return CPProviders.DataProvider.GetTollingLetter(letterID);
        }
    }
}
