using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

using Cfb.CandidatePortal.Web;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
	/// <summary>
	/// A web part for displaying a concise list of currently posted, unexpired list.
	/// </summary>
	public class AnnouncementsWebPart : WebPart
	{
		/// <summary>
		/// The URL of the form for displaying full announcement details.
		/// </summary>
		const string _DisplayFormUrl = "/Announcements/View.aspx";

		/// <summary>
		/// Regular expression to match comma/semi-colon separated string of election cycles.
		/// </summary>
		const string _ElectionCyclesExpressionFormat = @"(\w*[;,]\s*)*?{0}([;,]\s*(\w*[;,]\s*)*\w*)?";

		/// <summary>
		/// CAML filter query that returns only current, unexpired items.
		/// </summary>
		const string _CurrentUnexpiredFilter = @"
<OrderBy>
	<FieldRef Name=""Posted"" Ascending=""TRUE"" />
</OrderBy>
<Where>
	<And>
		<And>
			<Or>
				<IsNull>
					<FieldRef Name=""Expires"" />
				</IsNull>
				<Geq>
					<FieldRef Name=""Expires"" />
					<Value Type=""DateTime"">
						<Today />
					</Value>
				</Geq>
			</Or>
			<Leq>
				<FieldRef Name=""Posted"" />
				<Value Type=""DateTime"">
					<Today />
				</Value>
			</Leq>
		</And>
		<Or>
			<IsNull>
				<FieldRef Name=""ElectionCycles"" />
			</IsNull>
			<Contains>
				<FieldRef Name=""ElectionCycles"" />
				<Value Type=""Text"">{0}</Value>
			</Contains>
		</Or>
	</And>
</Where>";

		/// <summary>
		/// CAML filter query that returns only unexpired items of a specific category.
		/// </summary>
		const string _UnexpiredCategoryFilterFormat = @"
<OrderBy>
	<FieldRef Name=""Posted"" Ascending=""TRUE"" />
</OrderBy>
<Where>
	<And>
		<And>
			<Or>
				<IsNull>
					<FieldRef Name=""Expires"" />
				</IsNull>
				<Geq>
					<FieldRef Name=""Expires"" />
					<Value Type=""DateTime"">
						<Today />
					</Value>
				</Geq>
			</Or>
			<Eq>
				<FieldRef Name=""Category"" />
				<Value Type=""Text"">{0}</Value>
			</Eq>
		</And>
		<Or>
			<IsNull>
				<FieldRef Name=""ElectionCycles"" />
			</IsNull>
			<Contains>
				<FieldRef Name=""ElectionCycles"" />
				<Value Type=""Text"">{1}</Value>
			</Contains>
		</Or>
	</And>
</Where>";

		/// <summary>
		/// Number of days past posted date until an announcement is no longer "new".
		/// </summary>
		const double NewItemCutoffDays = 7;

		/// <summary>
		/// Optional category filter.
		/// </summary>
		string _category;

		/// <summary>
		/// Gets or sets an optional category filter.
		/// </summary>
		/// <remarks>
		/// Setting an optional category filter will show all category items instead of current items only.
		/// </remarks>
		[Personalizable(PersonalizationScope.Shared)]
		[WebBrowsable(false)]
		[WebDisplayName("Category Filter")]
		[WebDescription("Show items of a specific type")]
		public string Category
		{
			get { return _category; }
			set
			{
				_category = value;
				_queryCaml = string.IsNullOrEmpty(_category) ? string.Format(_CurrentUnexpiredFilter, CPProfile.ElectionCycle) : string.Format(_UnexpiredCategoryFilterFormat, value, CPProfile.ElectionCycle);
			}
		}

		/// <summary>
		/// A top-level panel container for controls to be rendered.
		/// </summary>
		Panel _panel;

		/// <summary>
		/// The query CAML string to be used for retrieving announcements.
		/// </summary>
		string _queryCaml;

		/// <summary>
		/// Initializes the web part.
		/// </summary>
		public AnnouncementsWebPart()
		{
			this.ExportMode = WebPartExportMode.All;
			_queryCaml = _CurrentUnexpiredFilter;
		}

		#region WebPart Member Methods

		/// <summary>
		/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			_panel = new Panel();
			_panel.CssClass = "cp-AnnouncementsWebPart";
			SPWeb web = SPContext.Current.Web;
			web.Lists.ListsForCurrentUser = true;
			// set up table header
			Literal header = new Literal();
			header.Text = string.Format("<h3 class=\"caption\"><img src=\"/images/captions/announcements.gif\" width=\"118\" height=\"10\" alt=\"{0}\" class=\"caption\" /></h3><div class=\"cp-boundingBox cp-table cp-Announcements\">", Properties.Resources.Caption);
			_panel.Controls.Add(header);
			try
			{
				SPList list = web.Lists["Announcements"];
				// filter for posted, unexpired items
				SPQuery query = new SPQuery();
				query.ViewFields = @"<FieldRef Name=""Title"" /><FieldRef Name=""Posted"" /><FieldRef Name=""ElectionCycles"" />";
				query.ViewAttributes = @"Scope=""Recursive""";
				query.Query = _queryCaml;
				SPListItemCollection items = list.GetItems(query);
				bool hasItems = false;
				if (items.Count > 0)
				{
					// see if any announcements apply to current election cycle
					Regex re = new Regex(string.Format(_ElectionCyclesExpressionFormat, CPProfile.ElectionCycle));
					string listRootUrl = list.RootFolder.ServerRelativeUrl;
					foreach (SPListItem item in items)
					{
						// only process approved items
						if (item.ModerationInformation.Status != SPModerationStatusType.Approved)
							continue;
						string ec = item["ElectionCycles"] as string;
						if (string.IsNullOrEmpty(ec) || re.IsMatch(ec))
						{
							// applies to current election cycle, so display it
							Panel p = new Panel();
							// show indicator if new
							if (string.IsNullOrEmpty(_category) && (DateTime.Today.AddDays(-NewItemCutoffDays).CompareTo((DateTime)item["Posted"]) < 0))
							{
								Image newStatusImage = new Image();
								newStatusImage.ImageUrl = "/images/new.gif";
								newStatusImage.AlternateText = "New";
								newStatusImage.CssClass = "newAlert";
								p.Controls.Add(newStatusImage);
							}
							HyperLink link = new HyperLink();
							link.ToolTip = link.Text = item["Title"] as string;
							link.NavigateUrl = string.Format("{0}?ID={1}&RootFolder={2}", _DisplayFormUrl, item.ID, HttpContext.Current.Server.UrlEncode(listRootUrl));
							p.Controls.Add(link);
							_panel.Controls.Add(p);
							if (!hasItems) hasItems = true;
						}
					}
				}
				if (!hasItems)
				{
					Literal emptyText = new Literal();
					emptyText.Text = "There are currently no announcements available.";
					_panel.Controls.Clear();
					_panel.Controls.Add(header);
					_panel.Controls.Add(emptyText);
				}
			}
			catch (SPException)
			{
				Literal emptyText = new Literal();
				emptyText.Text = "Error occurred retrieving announcement data.";
				_panel.Controls.Add(emptyText);
			}
			// set up table footer
			Literal footer = new Literal();
			footer.Text = "</div>";
			_panel.Controls.Add(footer);
			this.Controls.Add(_panel);
		}

		/// <summary>
		/// Returns a collection of custom <see cref="EditorPart"/> controls that can be used to edit a <see cref="WebPart"/> control when it is in edit mode.
		/// </summary>
		/// <returns>An <see cref="EditorPartCollection"/> that contains custom <see cref="EditorPart"/> controls associated with a <see cref="WebPart"/> control.</returns>
		public override EditorPartCollection CreateEditorParts()
		{
			List<EditorPart> editorParts = new List<EditorPart>();
			AnnouncementsEditorPart editor = new AnnouncementsEditorPart();
			editor.ID = this.ID + "_editor";
			editor.Title = "Announcement List Configuration";
			editorParts.Add(editor);
			EditorPartCollection epc = new EditorPartCollection(editorParts);
			return epc;
		}

		#endregion
	}
}
