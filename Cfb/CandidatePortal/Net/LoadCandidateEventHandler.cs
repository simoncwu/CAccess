using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.Net
{
    /// <summary>
    /// Represents the method that will handle requests for candidate information.
    /// </summary>
    /// <param name="candidateID">The ID of the candidate to retrieve.</param>
    /// <returns>A <see cref="Candidate"/> object representing the requested candidate.</returns>
    public delegate Candidate LoadCandidateEventHandler(string candidateID);
}
