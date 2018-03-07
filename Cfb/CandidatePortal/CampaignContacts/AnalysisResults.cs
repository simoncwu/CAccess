using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.CampaignContacts
{
    /// <summary>
    /// Represents the results of a analysis for current and potential campaign users.
    /// </summary>
    public class AnalysisResults
    {
        /// <summary>
        /// A collection of campaign contacts eligible for new C-Access accounts indexed by an identifer unique for the campaign.
        /// </summary>
        private readonly Dictionary<string, Entity> _eligibleContacts;

        /// <summary>
        /// Gets a collection of campaign contacts eligible for new C-Access accounts indexed by an identifer unique for the campaign.
        /// </summary>
        public Dictionary<string, Entity> EligibleContacts
        {
            get { return _eligibleContacts; }
        }

        /// <summary>
        /// A collection of current accounts for a campaign.
        /// </summary>
        private readonly List<CPUser> _currentUsers;

        /// <summary>
        /// Gets a collection of current accounts for a campaign.
        /// </summary>
        public List<CPUser> CurrentUsers
        {
            get { return _currentUsers; }
        }

        /// <summary>
        /// A collection of accounts for a different campaign for the candidate.
        /// </summary>
        private readonly List<AccountEligibilityStatus> _otherCampaignUsers;

        /// <summary>
        /// Gets a collection of accounts for a different campaign for the candidate.
        /// </summary>
        public List<AccountEligibilityStatus> OtherCampaignUsers
        {
            get { return _otherCampaignUsers; }
        }

        /// <summary>
        /// A collection of campaign contacts whom are ineligible for C-Access accounts indexed and sorted by eligibility status.
        /// </summary>
        readonly List<AccountEligibilityStatus> _ineligibleContacts;

        /// <summary>
        /// Gets a collection of campaign contacts whom are ineligible for C-Access accounts indexed and sorted by eligibility status.
        /// </summary>
        public List<AccountEligibilityStatus> IneligibleContacts
        {
            get { return _ineligibleContacts; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AnalysisResults"/> class.
        /// </summary>
        internal AnalysisResults()
        {
            _eligibleContacts = new Dictionary<string, Entity>();
            _currentUsers = new List<CPUser>();
            _ineligibleContacts = new List<AccountEligibilityStatus>();
            _otherCampaignUsers = new List<AccountEligibilityStatus>();
        }

        /// <summary>
        /// Removes an e-mail address from all analysis results.
        /// </summary>
        /// <param name="address">The e-mail address to remove.</param>
        public void BypassEmail(string address)
        {
            if (!string.IsNullOrWhiteSpace(address))
            {
                var key = _eligibleContacts.Where(c => string.Equals(c.Value.Email, address, StringComparison.InvariantCultureIgnoreCase)).Select(u => u.Key).FirstOrDefault();
                if (key != null)
                    _eligibleContacts.Remove(key);
                var user = _currentUsers.Where(u => string.Equals(u.Email, address, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (user != null)
                    _currentUsers.Remove(user);
                var ineligible = _ineligibleContacts.FirstOrDefault(c => string.Equals(c.Entity.Email, address, StringComparison.InvariantCultureIgnoreCase));
                if (ineligible != null)
                    _ineligibleContacts.Remove(ineligible);
            }
        }
    }
}