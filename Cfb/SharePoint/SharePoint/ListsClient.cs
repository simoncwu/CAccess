using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Xml;
using System.Xml.Linq;

namespace Cfb.SharePoint
{
	/// <summary>
	/// A client proxy for communicating with the SharePoint Lists Web Service, which provides methods for working with SharePoint lists and list data.
	/// </summary>
	public class ListsClient
	{
		/// <summary>
		/// XML name definitions
		/// </summary>
		private static readonly XName RowsetDataName = (XNamespace)"urn:schemas-microsoft-com:rowset" + "data";
		private static readonly XName RowsetRowName = (XNamespace)"#RowsetSchema" + "row";
		private static readonly XName AttachmentName = (XNamespace)"http://schemas.microsoft.com/sharepoint/soap/" + "Attachment";

		/// <summary>
		/// Absolute web path to SharePoint Lists Web Service.
		/// </summary>
		private const string ServicePath = "/_vti_bin/Lists.asmx";

		/// <summary>
		/// Returns information about items in the list based on the specified query.
		/// </summary>
		/// <param name="listName">A string that contains either the display name or the GUID for the list. It is recommended that you use the GUID, which must be surrounded by curly braces ({}). When querying the UserInfo table, the string contains "UserInfo".</param>
		/// <param name="query">A Query element containing the query that determines which records are returned and in what order, and that can be assigned to a <see cref="XmlNode"/> object.</param>
		/// <param name="viewFields">A ViewFields element that specifies which fields to return in the query and in what order, and that can be assigned to a <see cref="XmlNode"/> object.</param>
		/// <param name="queryOptions">An XML fragment that contains separate nodes for the various properties of the SPQuery object, and that can be assigned to a <see cref="XmlNode"/> object.</param>
		/// <param name="siteUrl">The base URL for the site hosting the SharePoint Lists Web Service (optional).</param>
		/// <returns>A collection of XML elements containing information about the list items requested.</returns>
		public static IEnumerable<XElement> GetListItems(string listName, string query, string viewFields, string queryOptions, string siteUrl = null)
		{
			using (ListsService.Lists proxy = new ListsService.Lists() { PreAuthenticate = true, UseDefaultCredentials = true })
			{
				if (!string.IsNullOrWhiteSpace(siteUrl))
					proxy.Url = siteUrl.TrimEnd('/') + ServicePath;
				// set up parameters
				XmlDocument xmlDoc = new XmlDocument();
				XmlNode xQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", string.Empty);
				xQuery.InnerXml = query;
				XmlNode xViewFields = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", string.Empty);
				xViewFields.InnerXml = viewFields;
				XmlNode xQueryOptions = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", string.Empty);
				xQueryOptions.InnerXml = queryOptions;
                XmlNode results;
                try
                {
                    results = proxy.GetListItems(listName, null, xQuery, xViewFields, null, xQueryOptions, null);
                }
                catch (WebException)
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml("<rs:data ItemCount=\"1\" xmlns:rs=\"urn:schemas-microsoft-com:rowset\"><z:row ows_Title=\"Cross-Election Announcement\" ows_Posted=\"2010-09-20 00:00:00\" ows__ModerationStatus=\"0\" ows_MetaInfo=\"4;#\" ows__Level=\"1\" ows_ID=\"4\" ows_owshiddenversion=\"2\" ows_UniqueId=\"4;#{A8B72A5A-6271-4625-92FD-32135FF4A76F}\" ows_FSObjType=\"4;#0\" ows_Created=\"2010-09-20 15:31:35\" ows_Category=\"None\" ows_FileRef=\"4;#Lists/Announcements/4_.000\" xmlns:z=\"#RowsetSchema\" /></rs:data>");
                    results = xmlDoc;
                }

				// retrieve and filter XML data
				XElement rsData = XElement.Parse(results.OuterXml).Descendants(RowsetDataName).FirstOrDefault();
				if (rsData != null)
				{
					XAttribute countAttribute = rsData.Attribute("ItemCount");
					return rsData.Descendants(RowsetRowName);
				}
				return new List<XElement>(0);
			}
		}

		/// <summary>
		/// Returns a collection of the URLs for attachments to the specified item.
		/// </summary>
		/// <param name="listName">A string that contains either the title or the GUID for the list.</param>
		/// <param name="listItemID">A string that contains the ID for the list item. This value does not correspond to the index of the item within the collection of list items.</param>
		/// <param name="siteUrl">The base URL for the site hosting the SharePoint Lists Web Service (optional).</param>
		/// <returns>A collection of URLs for the attachments.</returns>
		public static StringCollection GetListItemAttachmentUrls(string listName, string listItemID, string siteUrl = null)
		{
			using (ListsService.Lists proxy = new ListsService.Lists() { UseDefaultCredentials = true })
			{
				if (!string.IsNullOrWhiteSpace(siteUrl))
					proxy.Url = siteUrl.TrimEnd('/') + ServicePath;
				StringCollection urls = new StringCollection();
				foreach (var node in XElement.Parse(proxy.GetAttachmentCollection(listName, listItemID).OuterXml).Descendants(AttachmentName))
				{
					urls.Add(node.Value);
				}
				return urls;
			}
		}
	}
}