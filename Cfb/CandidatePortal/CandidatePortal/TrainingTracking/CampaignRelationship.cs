using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.TrainingTracking
{
	/// <summary>
	/// Enumeration of the kinds of relationships a CFB training trainee can have with a campaign.
	/// </summary>
	public enum CampaignRelationship : byte
	{
		/// <summary>
		/// Unknown relationship.
		/// </summary>
		Unknown,
		/// <summary>
		/// A candidate.
		/// </summary>
		Candidate,
		/// <summary>
		/// A treasurer.
		/// </summary>
		Treasurer,
		/// <summary>
		/// A campaign manager.
		/// </summary>
		Manager,
		/// <summary>
		/// A campaign authority.
		/// </summary>
		Authority,
		/// <summary>
		/// An assistant treasurer.
		/// </summary>
		AsisstantTreasurer,
		/// <summary>
		/// A campaign coordinator.
		/// </summary>
		Coordinator,
		/// <summary>
		/// A campaign consultant.
		/// </summary>
		Consultant,
		/// <summary>
		/// A campaign liaison.
		/// </summary>
		Liaison,
		/// <summary>
		/// A campaign volunteer.
		/// </summary>
		Volunteer,
		/// <summary>
		/// A campaign worker.
		/// </summary>
		Worker,
		/// <summary>
		/// A trainee who is no longer associated with the campaign.
		/// </summary>
		FormerStaff
	}
}

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Utility class for converting and parsing enumerations.
	/// </summary>
	public partial class CPConvert
	{
		/// <summary>
		/// Converts the value of the specified <see cref="CampaignRelationship"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="relationship">A campaign-trainee relationship type.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="relationship"/>.</returns>
		public static string ToString(CampaignRelationship relationship)
		{
			switch (relationship)
			{
				case CampaignRelationship.AsisstantTreasurer:
					return "Assistant Treasurer";
				case CampaignRelationship.Manager:
					return "Campaign Manager";
				case CampaignRelationship.Coordinator:
					return "Campaign Coordinator";
				case CampaignRelationship.Authority:
					return "Campaign Authority";
				case CampaignRelationship.FormerStaff:
					return "No Longer Associated with Campaign";
				default:
					return relationship.ToString();
			}
		}

		/// <summary>
		/// Parses and converts the specified code to its equivalent <see cref="CampaignRelationship"/> representation.
		/// </summary>
		/// <param name="code">A campaign-trainee relationship code.</param>
		/// <returns>The <see cref="CampaignRelationship"/> equivalent of <paramref name="code"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
		public static CampaignRelationship ToCampaignRelationship(string code)
		{
			if (code == null) throw new ArgumentNullException("code");
			switch (code.ToUpper(CultureInfo.InvariantCulture))
			{
				case "VOLUN":
					return CampaignRelationship.Volunteer;
				case "CMAN":
					return CampaignRelationship.Manager;
				case "CAND":
					return CampaignRelationship.Candidate;
				case "CONSUL":
					return CampaignRelationship.Consultant;
				case "WORK":
					return CampaignRelationship.Worker;
				case "TREAS":
					return CampaignRelationship.Treasurer;
				case "ASTTRE":
					return CampaignRelationship.AsisstantTreasurer;
				case "CAUTH":
					return CampaignRelationship.Authority;
				case "LIAISO":
					return CampaignRelationship.Liaison;
				case "CAMCOO":
					return CampaignRelationship.Coordinator;
				case "REMOVE":
					return CampaignRelationship.FormerStaff;
				case "UNK":
				default:
					return CampaignRelationship.Unknown;
			}
		}
	}
}