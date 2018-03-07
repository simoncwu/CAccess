using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.CampaignContacts
{
    /// <summary>
    /// An <see cref="Entity"/>'s eligibility for a new C-Access account.
    /// </summary>
    public class AccountEligibilityStatus
    {
        /// <summary>
        /// Gets or sets an entity's account eligibility status.
        /// </summary>
        public AccountEligibility Status { get; set; }

        /// <summary>
        /// Gets or sets the entity whose eligibility is being evaluated.
        /// </summary>
        public Entity Entity { get; set; }

        /// <summary>
        /// Gets or sets the contact ID for the entity whose eligibility is being evaluated.
        /// </summary>
        public string ContactID { get; set; }

        /// <summary>
        /// Gets or sets a user that can be matched against the entity being evaluated.
        /// </summary>
        public CPUser MatchedUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountEligibilityStatus"/> class.
        /// </summary>
        /// <param name="entity">The entity attributed with status <paramref name="eligibility"/>.</param>
        /// <param name="eligibility">The eligiblity status of <paramref name="entity"/>.</param>
        /// <param name="user">The user that matches <paramref name="entity"/>.</param>
        /// <param name="contactID">The contact ID of <paramref name="entity"/>.</param>
        public AccountEligibilityStatus(Entity entity, AccountEligibility eligibility, CPUser user = null, string contactID = null)
        {
            this.Entity = entity;
            this.Status = eligibility;
            this.MatchedUser = user;
            this.ContactID = contactID;
        }
    }
}
