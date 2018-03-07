using System.ComponentModel;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Enumeration for the different ways payments can be made.
    /// </summary>
    public enum PaymentMethod : byte
    {
        /// <summary>
        /// Paper check.
        /// </summary>
        Check,
        /// <summary>
        /// Electronic Funds Transfer.
        /// </summary>
        [Description("EFT")]
        Eft
    }
}
