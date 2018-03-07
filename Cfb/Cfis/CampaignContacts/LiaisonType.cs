using System;
using System.Globalization;

namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Enumeration for the different campaign liaison types.
    /// </summary>
    public enum LiaisonType : byte
    {
        /// <summary>
        /// A campaign liaison.
        /// </summary>
        Liaison,
        /// <summary>
        /// A campaign manager.
        /// </summary>
        CampaignManager,
        /// <summary>
        /// A campaign consultant.
        /// </summary>
        Consultant,
        /// <summary>
        /// A Transition and Inauguration Entity (TIE) liaison.
        /// </summary>
        TIE
    }
}
