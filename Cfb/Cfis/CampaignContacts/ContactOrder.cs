namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Enumeration for the various orders of contact priority.
    /// </summary>
    public enum ContactOrder : byte
    {
        /// <summary>
        /// Unknown contact order.
        /// </summary>
        Unknown,
        /// <summary>
        /// First in line.
        /// </summary>
        First,
        /// <summary>
        /// Second in line.
        /// </summary>
        Second,
        /// <summary>
        /// Third in line.
        /// </summary>
        Third,
        /// <summary>
        /// Fourth in line.
        /// </summary>
        Fourth
    }
}
