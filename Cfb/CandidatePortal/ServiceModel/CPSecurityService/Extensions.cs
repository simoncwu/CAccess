using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Web.Security;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityService
{
    /// <summary>
    /// Defines supporting extension methods for the C-Access Security service.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Adds a new security user profile entity instance to the datastore.
        /// </summary>
        /// <param name="context">The datastore entity context to use.</param>
        /// <param name="username">The username for the new profile.</param>
        /// <param name="candidateID">The candidate ID for the new profile.</param>
        /// <param name="displayName">The display name for the profile.</param>
        /// <param name="type">The entity type of the CFB-registered contact used to create the profile.</param>
        /// <param name="committeeID">If not representing a candidate, the ID of the profile's committee.</param>
        /// <param name="electionCycle">If representing a treasurer, the election cycle for which the profile's committee is authorized.</param>
        /// <param name="liaisonID">If representing a liaison, the committee-relative liaison ID of the profile.</param>
        /// <param name="passwordExpired">Indicates whether or not the password on the newly created profile is to be expired.</param>
        /// <returns>A <see cref="SecurityUserProfile"/> instance representing the newly created profile that has been added to the datastore.</returns>
        public static SecurityUserProfile AddToSecurityUserProfiles(this CPSecurityEntities context, string username, string candidateID, string displayName = null, EntityType type = EntityType.Generic, char? committeeID = null, string electionCycle = null, byte? liaisonID = null, bool passwordExpired = true)
        {
            if (context == null || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(candidateID))
                return null;
            SecurityUserProfile profile = SecurityUserProfile.CreateSecurityUserProfile(username, candidateID, passwordExpired, (byte)type);
            profile.DisplayName = displayName;
            if (committeeID.HasValue)
                profile.CfisCommitteeID = committeeID.Value.ToString();
            profile.CfisCommitteeContactID = liaisonID.HasValue ? liaisonID.Value.ToString() : electionCycle;
            context.SecurityUserProfiles.AddObject(profile);
            context.SaveChanges();
            return profile;
        }

        /// <summary>
        /// Creates a new security user profile entity instance from a membership user and adds it to the datastore.
        /// </summary>
        /// <param name="context">The datastore entity context to use.</param>
        /// <param name="user">The membership user to convert and add.</param>
        /// <returns>A <see cref="SecurityUserProfile"/> instance representing the newly created profile that has been added to the datastore.</returns>
        public static SecurityUserProfile AddToSecurityUserProfiles(this CPSecurityEntities context, MembershipUser user)
        {
            if (user == null)
                return null;
            string username = user.UserName;
            string caid = UserManagement.GetCaid(username);
            SecurityUserProfile profile = context.AddToSecurityUserProfiles(username, UserManagement.GetCfisId(username),
                UserManagement.GetFullName(user),
                AccountAnalysis.ParseEntityType(caid),
                AccountAnalysis.ParseCommitteeID(caid),
                AccountAnalysis.ParseElectionCycle(caid),
                AccountAnalysis.ParseLiaisonID(caid),
                UserManagement.IsPasswordExpired(user));
            return profile;
        }

        /// <summary>
        /// Deletes a security user profile from the datastore.
        /// </summary>
        /// <param name="context">The datastore entity context to use.</param>
        /// <param name="username">The username of the profile to delete.</param>
        public static void DeleteSecurityUserProfile(this CPSecurityEntities context, string username)
        {
            var match = context.SecurityUserProfiles.FirstOrDefault(p => p.UserName == username);
            if (match != null)
            {
                try
                {
                    context.DeleteObject(match);
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {
                    context.Refresh(RefreshMode.ClientWins, match);
                    context.DeleteObject(match);
                }
            }
        }
    }
}
