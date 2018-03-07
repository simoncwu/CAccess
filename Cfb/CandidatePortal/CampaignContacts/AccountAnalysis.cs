using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.CampaignContacts
{
    /// <summary>
    /// An analysis of all current and potential C-Access user accounts for a campaign.
    /// </summary>
    public class AccountAnalysis
    {
        /// <summary>
        /// Prefix for liaison contact IDs.
        /// </summary>
        public const string LiaisonPrefix = "L-";

        /// <summary>
        /// Prefix for treasurer contact IDs.
        /// </summary>
        public const string TreasurerPrefix = "T-";

        /// <summary>
        /// Prefix for consultant contact IDs.
        /// </summary>
        public const string ConsultantPrefix = "C-";

        /// <summary>
        /// Prefix for candidate contact ID.
        /// </summary>
        public const string CandidatePrefix = "I-";

        /// <summary>
        /// Prefix for impersonation accounts.
        /// </summary>
        public const string ImpersonatorPrefix = "CFB-";

        /// <summary>
        /// The candidate whose campaign is being analyzed.
        /// </summary>
        private readonly Candidate _candidate;

        /// <summary>
        /// The elecion cycle context of the analysis.
        /// </summary>
        private string _electionCycle;

        /// <summary>
        /// The results of the analysis.
        /// </summary>
        private readonly AnalysisResults _results;

        /// <summary>
        /// A collection of existing users for the campaign.
        /// </summary>
        private IEnumerable<CPUser> _users;

        /// <summary>
        /// Gets the results of the analysis.
        /// </summary>
        public AnalysisResults Results
        {
            get { return _results; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountAnalysis"/> class.
        /// </summary>
        /// <param name="candidate">The candidate whose campaign is to be analyzed.</param>
        /// <param name="electionCycle">The election cycle context in which to perform the analysis.</param>
        /// <exception cref="ArgumentNullException"><paramref name="candidate"/> is null.</exception>
        private AccountAnalysis(Candidate candidate, string electionCycle = null)
        {
            if (candidate == null)
                throw new ArgumentNullException("candidate", "Candidate cannot be null.");
            _candidate = candidate;
            _electionCycle = electionCycle;
            _results = new AnalysisResults();
            _users = new CPUser[0];
        }

        /// <summary>
        /// Analyzes a campaign for all current and potential C-Access user accounts for a single election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle context in which to perform the analysis.</param>
        private void Analyze(string electionCycle)
        {
            _electionCycle = electionCycle;
            _users = CPSecurity.Provider.FindUsers(candidateID: _candidate.ID);
            // get candidate
            if (_candidate != null)
            {
                AnalyzeEntity(_candidate);
            }
            // get committee details
            var ac = CPProviders.DataProvider.GetAuthorizedCommittees(_candidate.ID, electionCycle);
            if (ac != null)
            {
                // get committee details
                foreach (AuthorizedCommittee c in ac.Committees.Values)
                {
                    // analyze treasurer
                    AnalyzeEntity(c.Treasurer, GetTreasurerID(c));
                    // analyze liaisons
                    foreach (var l in c.Liaisons.Values)
                    {
                        AnalyzeEntity(l, c);
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes a candidate to determine its eligibiilty for a new C-Access account.
        /// </summary>
        /// <param name="c">The <see cref="Candidate"/> to analyze.</param>
        private void AnalyzeEntity(Candidate c)
        {
            AnalyzeEntity(c, GetCandidateID(c));
        }

        /// <summary>
        /// Analyzes a liaison to determine its eligibility for a new C-Access account.
        /// </summary>
        /// <param name="l">The <see cref="Liaison"/> to analyze.</param>
        /// <param name="c">The <see cref="Committee"/> for which <paramref name="l"/> is a liaison.</param>
        private void AnalyzeEntity(Liaison l, Committee c)
        {
            AnalyzeEntity(l, GetLiaisonID(l, c));
        }

        /// <summary>
        /// Analyzes a contact to determine its eligibility for a new C-Access account.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to analyze.</param>
        /// <param name="id">The ID of the <paramref name="entity"/> to analyze.</param>
        private void AnalyzeEntity(Entity entity, string id)
        {
            if (entity == null)
                return;
            CPUser conflict;
            AccountEligibility eligibility = DetermineEligibility(entity, _candidate.ID, id, out conflict);
            switch (eligibility)
            {
                case AccountEligibility.Eligible:
                    _results.EligibleContacts.Add(id, entity);
                    break;
                case AccountEligibility.IneligibleNoEmail:
                case AccountEligibility.IneligibleNoFirstName:
                case AccountEligibility.IneligibleNoLastName:
                case AccountEligibility.IneligibleDuplicateEmail:
                    _results.IneligibleContacts.Add(new AccountEligibilityStatus(entity, eligibility, contactID: id));
                    break;
                case AccountEligibility.IneligibleExists:
                    _results.CurrentUsers.Add(conflict);
                    break;
                case AccountEligibility.IneligibleDuplicateAccount:
                    _results.OtherCampaignUsers.Add(new AccountEligibilityStatus(entity, eligibility, conflict, id));
                    break;
            }
        }

        /// <summary>
        /// Gets the ID for a committee treasurer.
        /// </summary>
        /// <param name="c">The <see cref="AuthorizedCommittee"/> affiliated with the treasurer.</param>
        /// <returns>An identifier for the treasurer of committee <paramref name="c"/>.</returns>
        private string GetTreasurerID(AuthorizedCommittee c)
        {
            return (c != null) ? TreasurerPrefix + c.ID + _electionCycle : string.Empty;
        }

        /// <summary>
        /// Gets the ID for a campaign liaison.
        /// </summary>
        /// <param name="l">The <see cref="Liaison"/> to examine.</param>
        /// <param name="c">The <see cref="Committee"/> for which <paramref name="l"/> is a liaison.</param>
        /// <returns>An identifier for liaison <paramref name="l"/>.</returns>
        private string GetLiaisonID(Liaison l, Committee c)
        {
            if (l == null)
                return string.Empty;
            else if (l.LiaisonType == LiaisonType.Consultant)
                return string.Format("{0}{1}{2}", ConsultantPrefix, c.ID, l.ID);
            else
                return string.Format("{0}{1}{2}", LiaisonPrefix, c.ID, l.ID);
        }

        /// <summary>
        /// Gets the ID for a candidate.
        /// </summary>
        /// <param name="c">The <see cref="Candidate"/> to examine.</param>
        /// <returns>An identifier for the candidate <paramref name="c"/>.</returns>
        private string GetCandidateID(Candidate c)
        {
            if (c == null)
                return string.Empty;
            else
                return CandidatePrefix + c.ID;
        }

        /// <summary>
        /// Parse a a contact identifier for the type of entity represented.
        /// </summary>
        /// <param name="contactID">The identifer to parse.</param>
        /// <returns>The <see cref="EntityType"/> represented by <paramref name="contactID"/>.</returns>
        public static EntityType ParseEntityType(string contactID)
        {
            if (string.IsNullOrEmpty(contactID))
                return EntityType.Generic;
            else if (contactID.StartsWith(TreasurerPrefix))
                return EntityType.Treasurer;
            else if (contactID.StartsWith(LiaisonPrefix))
                return EntityType.Liaison;
            else if (contactID.StartsWith(ConsultantPrefix))
                return EntityType.Consultant;
            else if (contactID.StartsWith(CandidatePrefix))
                return EntityType.Candidate;
            else
                return EntityType.Generic;
        }

        /// <summary>
        /// Parses a liaison or consultant identifier for its CFIS ID.
        /// </summary>
        /// <param name="contactID">The contact identifier to parse.</param>
        /// <returns>The CFIS ID of the liaison or consultant represented by <paramref name="contactID"/> if the parse succeeded; otherwise, null.</returns>
        public static byte? ParseLiaisonID(string contactID)
        {
            if (!string.IsNullOrEmpty(contactID))
            {
                byte result;
                int prefixLength;
                switch (ParseEntityType(contactID))
                {
                    case EntityType.Consultant:
                        prefixLength = ConsultantPrefix.Length + 1;
                        if (contactID.Length > prefixLength)
                            if (byte.TryParse(contactID.Substring(prefixLength), out result))
                                return result;
                        break;
                    case EntityType.Liaison:
                        prefixLength = LiaisonPrefix.Length + 1;
                        if (contactID.Length > prefixLength)
                            if (byte.TryParse(contactID.Substring(prefixLength), out result))
                                return result;
                        break;
                }
            }
            return null;
        }

        /// <summary>
        /// Parses a treasurer, liaison, or consultant identifier for the CFIS ID of the affiliated committee.
        /// </summary>
        /// <param name="contactID">The contact identifier to parse.</param>
        /// <param name="result">When this method returns, contains the CFIS ID of the committee affiliated with the treasurer, liaison, or consultant represented by <paramref name="contactID"/> if the parse succeeded, or an undefined value if the parse failed. The conversion fails if the <paramref name="contactID"/> does not contain a committee ID. This parameter is passed uninitialized.</param>
        /// <returns>The ID of the committee affiliated with the treasurer, liaison, or consultant if the parse succeeded; otherwise null.</returns>
        public static char? ParseCommitteeID(string contactID)
        {
            if (!string.IsNullOrEmpty(contactID) && contactID.Length > TreasurerPrefix.Length)
            {
                char result;
                switch (ParseEntityType(contactID))
                {
                    case EntityType.Treasurer:
                    case EntityType.Liaison:
                    case EntityType.Consultant:
                        if (char.TryParse(contactID.Substring(TreasurerPrefix.Length, 1), out result))
                            return result;
                        break;
                }
            }
            return null;
        }

        /// <summary>
        /// Parses a treasurer contact identifier for the election cycle in which the treasurer was involved with a campaign.
        /// </summary>
        /// <param name="treasurerID">The identifier of the treasurer to examine.</param>
        /// <returns>The election cycle in which associated with the treasurer if the parse succeeded; otherwise null.</returns>
        public static string ParseElectionCycle(string treasurerID)
        {
            if (!string.IsNullOrEmpty(treasurerID) && (ParseEntityType(treasurerID) == EntityType.Treasurer))
            {
                int prefixLength = TreasurerPrefix.Length + 1;
                if (treasurerID.Length > prefixLength)
                    return treasurerID.Substring(prefixLength);
            }
            return null;
        }

        /// <summary>
        /// Analyzes an <see cref="Entity"/> to see if it is eligible for C-Access account creation.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <param name="cid">The CFIS ID of the campaign of which entity is a member.</param>
        /// <param name="contactID">The C-Access ID of the entity.</param>
        /// <param name="user">For entities that are ineligible due to a conflict with a current C-Access user account, the conflicting user.</param>
        /// <returns>An <see cref="AccountEligibilityStatus"/> value representing the eligibility analysis results.</returns>
        private AccountEligibility DetermineEligibility(Entity entity, string cid, string contactID, out CPUser match)
        {
            match = null;
            if (entity == null)
                return AccountEligibility.IneligibleNull;
            if (string.IsNullOrEmpty(entity.FirstName))
                return AccountEligibility.IneligibleNoFirstName;
            else if (string.IsNullOrEmpty(entity.LastName))
                return AccountEligibility.IneligibleNoLastName;
            else if (string.IsNullOrEmpty(entity.Email))
                return AccountEligibility.IneligibleNoEmail;
            else
            {
                // look for matching user
                Func<CPUser, bool> caidMatches = (user) =>
                {
                    return string.Equals(contactID, user.GetCaid(), StringComparison.OrdinalIgnoreCase);
                };
                Func<CPUser, bool> firstMatches = (user) =>
                {
                    return user.DisplayName.StartsWith(entity.FirstName + " ");
                };
                Func<CPUser, bool> lastMatches = (user) =>
                {
                    return user.DisplayName.EndsWith(" " + entity.LastName);
                };
                match = _users.FirstOrDefault(u => string.Equals(u.Email, entity.Email, StringComparison.OrdinalIgnoreCase));
                if (match != null)
                {
                    bool caidMatch = caidMatches(match);
                    bool firstMatch = firstMatches(match);
                    bool lastMatch = lastMatches(match);
                    return caidMatch && (firstMatch || lastMatch) ? AccountEligibility.IneligibleExists :
                        !caidMatch && firstMatch && lastMatch ? AccountEligibility.IneligibleDuplicateAccount : AccountEligibility.IneligibleDuplicateEmail;
                }
                match = _users.FirstOrDefault(u => firstMatches(u) && lastMatches(u));
                return match != null ?
                    caidMatches(match) ? AccountEligibility.IneligibleExists : AccountEligibility.IneligibleDuplicateAccount :
                    AccountEligibility.Eligible;
            }
        }

        /// <summary>
        /// Analyzes a campaign for all current and potential C-Access user accounts for a single election cycle.
        /// </summary>
        /// <param name="candidate">The candidate whose campaign is to be analyzed.</param>
        /// <param name="electionCycle">The election cycle context in which to perform the analysis.</param>
        /// <param name="bypassAddress">The e-mail address to bypass when analyzing entities.</param>
        public static AnalysisResults Analyze(Candidate candidate, string electionCycle, string bypassAddress = null)
        {
            AccountAnalysis aa = new AccountAnalysis(candidate);
            aa.Analyze(electionCycle);
            if (bypassAddress != null)
                aa.Results.BypassEmail(bypassAddress);
            return aa.Results;
        }
    }
}
