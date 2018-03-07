using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.SharePoint.Properties;
using Cfb.SharePoint;

namespace Cfb.CandidatePortal.SharePoint
{
	/// <summary>
	/// Factory for retrieving 
	/// </summary>
	public static class AnnouncementsClient
	{
		/// <summary>
		/// Regular expression to match comma/semi-colon separated string of election cycles.
		/// </summary>
		private const string ElectionCycleRegExFilter = @"(\w*[;,]\s*)*?{0}([;,]\s*(\w*[;,]\s*)*\w*)?";

		/// <summary>
		/// A ViewFields element that specifies which fields to return when querying an announcements list and in what order.
		/// </summary>
		private const string ListViewFields = @"<FieldRef Name=""Title"" /><FieldRef Name=""Posted"" /><FieldRef Name=""ElectionCycles"" />";

		/// <summary>
		/// A ViewFields element that specifies which fields to return when querying a specific announcement and in what order.
		/// </summary>
		private const string ItemViewFields = @"<FieldRef Name=""Title"" /><FieldRef Name=""Posted"" /><FieldRef Name=""ElectionCycles"" /><FieldRef Name=""Body"" /><FieldRef Name=""FilingDateOverride"" />";

		/// <summary>
		/// An XML fragment that contains separate nodes for the various properties of the announcements query.
		/// </summary>
		private const string QueryOptions = @"<ViewAttributes Scope=""Recursive"" />";

		/// <summary>
		/// A collection of characters to be used as election cycle delimiters.
		/// </summary>
		private static readonly char[] _electionCycleDelimiters = { ';', ',', ' ' };

		/// <summary>
		/// Retrieves a collection of unexpired announcements filtered by election cycle.
		/// </summary>
		/// <param name="electionCycle">The election cycle to filter results by, or an empty string to retrieve announcements across all elections.</param>
		/// <returns>A collection of unexpired announcements filtered by the election cycle specified.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="electionCycle"/> is null.</exception>
		public static IEnumerable<Announcement> GetAnnouncements(string electionCycle)
		{
			if (electionCycle == null)
				throw new ArgumentNullException("electionCycle", "Target election cycle must not be null.");
			Settings settings = Properties.Settings.Default;
			var xmlData = ListsClient.GetListItems(settings.AnnouncementsListName, string.Format(settings.AnnouncementListQueryFilter, electionCycle), ListViewFields, QueryOptions);
			return ParseAnnouncements(electionCycle, xmlData);
		}

		/// <summary>
		/// Retrieves an announcement by unique ID.
		/// </summary>
		/// <param name="id">The ID of the announcement to retrieve.</param>
		/// <returns>The announcement matching the ID specified if found; otherwise, null.</returns>
		public static Announcement GetAnnouncement(string id)
		{
			Settings settings = Properties.Settings.Default;
			foreach (var announcement in ParseAnnouncements(string.Empty, ListsClient.GetListItems(settings.AnnouncementsListName, string.Format(settings.AnnouncementItemQueryFilter, id), ItemViewFields, QueryOptions)))
			{
				return announcement;
			}
			return null;
		}

		/// <summary>
		/// Retrieves a filing date announcement by statement number and election cycle.
		/// </summary>
		/// <param name="statementNumber">The filing statement number to search for.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The announcement matching the filing statement number and election cycle specified if found; otherwise, null.</returns>
		public static Announcement GetFilingDateAnnouncement(byte statementNumber, string electionCycle)
		{
			Settings settings = Properties.Settings.Default;
			foreach (var announcement in ParseAnnouncements(electionCycle, ListsClient.GetListItems(settings.AnnouncementsListName, string.Format(settings.FilingDateAnnouncementListQueryFilter, statementNumber, electionCycle), ItemViewFields, QueryOptions)))
			{
				return announcement;
			}
			return null;
		}

		/// <summary>
		/// Parses and converts XML result set data from SharePoint web services to a collection of <see cref="Announcement"/> objects.
		/// </summary>
		/// <param name="electionCycle">The election cycle to filter data by, or an empty string to convert announcements across all elections.</param>
		/// <param name="rsData">The XML result set data to convert.</param>
		/// <returns>A collection of <see cref="Announcement"/> objects representing the data contained in <paramref name="rsData"/>.</returns>
		private static IEnumerable<Announcement> ParseAnnouncements(string electionCycle, IEnumerable<XElement> rsData)
		{
			List<Announcement> announcements = new List<Announcement>(rsData.Count());
			Regex re = new Regex(string.Format(ElectionCycleRegExFilter, electionCycle));
			Settings settings = Properties.Settings.Default;
			foreach (var node in rsData)
			{
				string electionValue = SafelyGetAttributeValue(node, settings.AnnouncementElectionCyclesAttributeName) ?? string.Empty;
				// skip if announcement does not target the specified election cycle
				if (!string.IsNullOrWhiteSpace(electionCycle) && !string.IsNullOrWhiteSpace(electionValue) && !re.IsMatch(electionValue))
					continue;
				DateTime dt;
				int id;
				if (int.TryParse(node.Attribute(settings.ListItemIDAttributeName).Value, out id))
				{
					Announcement ann = new Announcement(id)
					{
						Title = SafelyGetAttributeValue(node, settings.AnnouncementTitleAttributeName),
						Body = SafelyGetAttributeValue(node, settings.AnnouncementBodyAttributeName),
						Posted = DateTime.TryParse(SafelyGetAttributeValue(node, settings.AnnouncementPostedAttributeName), out dt) ? dt : (DateTime?)null,
						Approved = SafelyGetAttributeValue(node, settings.ListItemModerationAttributeName) == "0",
						OverridesFilingInfoTemplate = SafelyGetAttributeValue(node, settings.AnnouncementFilingOverrideAttributeName) == "1"
					};
					foreach (var ec in electionValue.Split(_electionCycleDelimiters, StringSplitOptions.RemoveEmptyEntries))
					{
						ann.ElectionCycles.Add(ec);
					}
					announcements.Add(ann);
				}
			}
			return announcements;
		}

		/// <summary>
		/// Safely returns the value of the <see cref="XAttribute"/> with the specified <see cref="XName"/> for the specified <see cref="XElement"/>.
		/// </summary>
		/// <param name="element">The <see cref="XElement"/> to analyze.</param>
		/// <param name="name">The <see cref="XName"/> of the <see cref="XAttribute"/> to retrieve.</param>
		/// <returns>The value of the attribute for the element specified if found; otherwise, null.</returns>
		private static string SafelyGetAttributeValue(XElement element, XName name)
		{
			if (element != null && name != null)
			{
				XAttribute attr = element.Attribute(name);
				if (attr != null)
					return attr.Value;
			}
			return null;
		}
	}
}
