namespace Cfb.Cfis.CampaignContacts
{
    /// <summary>
    /// Enumeration representing the different types of entities.
    /// </summary>
    public enum EntityType : byte
    {
        /// <summary>
        /// A generic entity.
        /// </summary>
        Generic,

        /// <summary>
        /// A candidate.
        /// </summary>
        Candidate,

        /// <summary>
        /// A campaign committee.
        /// </summary>
        Committee,

        /// <summary>
        /// A candidate or treasurer employer.
        /// </summary>
        Employer,

        /// <summary>
        /// A campaign treasurer.
        /// </summary>
        Treasurer,

        /// <summary>
        /// A campaign liaison.
        /// </summary>
        Liaison,

        /// <summary>
        /// A campaign consultant.
        /// </summary>
        Consultant
    }
}