using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
	/// <summary>
	/// An editor part for selecting the list to be displayed in an <see cref="AnnouncementsWebPart"/>.
	/// </summary>
	public class AnnouncementsEditorPart : EditorPart
	{
		/// <summary>
		/// A drop down list control for displaying this web's lists.
		/// </summary>
		private DropDownList _listList;

		/// <summary>
		/// A drop down list control for displaying a selection of categories.
		/// </summary>
		private DropDownList _categoryList;

		#region EditorPart Member Methods

		/// <summary>
		/// Saves the values in an <see cref="EditorPart"/> control to the corresponding properties in the associated <see cref="WebPart"/> control.
		/// </summary>
		/// <returns></returns>
		public override bool ApplyChanges()
		{
			AnnouncementsWebPart wp = this.WebPartToEdit as AnnouncementsWebPart;
			if (wp == null)
				return false;
			wp.Category = _categoryList.SelectedValue;
			return true;
		}

		/// <summary>
		/// Retrieves the property values from a <see cref="WebPart"/> control for its associated <see cref="EditorPart"/> control.
		/// </summary>
		public override void SyncChanges()
		{
			this.EnsureChildControls();
			AnnouncementsWebPart wp = this.WebPartToEdit as AnnouncementsWebPart;
			ListItem item = _categoryList.Items.FindByValue(wp.Category);
			if (item != null)
				item.Selected = true;
			else
				_categoryList.ClearSelection();
		}

		/// <summary>
		/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			// set up lists UI control
			Panel settings = new Panel();
			this.Controls.Add(settings);
			Panel p = new Panel();
			settings.Controls.Add(p);
			p.CssClass = "UserSectionHead";
			Label l = new Label();
			p.Controls.Add(l);
			l.Text = "Announcements List";
			_listList = new DropDownList();
			l.AssociatedControlID = _listList.ID;
			p = new Panel();
			settings.Controls.Add(p);
			p.CssClass = "UserSectionBody";
			p.Controls.Add(_listList);
			p = new Panel();
			settings.Controls.Add(p);
			p.CssClass = "UserDottedLine";
			p.Style.Add(HtmlTextWriterStyle.Width, "100%");

			// set up list of lists
			SPWeb web = SPContext.Current.Web;
			web.Lists.ListsForCurrentUser = true;
			SPListCollection lists = web.GetListsOfType(SPBaseType.GenericList);
			_listList.Items.Add(new ListItem("(Select a list)", string.Empty));
			foreach (SPList list in lists) {
				_listList.Items.Add(new ListItem(list.Title, list.ID.ToString()));
			}

			// set up categories UI control
			settings = new Panel();
			this.Controls.Add(settings);
			p = new Panel();
			settings.Controls.Add(p);
			p.CssClass = "UserSectionHead";
			l = new Label();
			p.Controls.Add(l);
			l.Text = "Category Filter";
			_categoryList = new DropDownList();
			l.AssociatedControlID = _categoryList.ID;
			p = new Panel();
			settings.Controls.Add(p);
			p.CssClass = "UserSectionBody";
			p.Controls.Add(_categoryList);
			p = new Panel();
			settings.Controls.Add(p);
			p.CssClass = "UserDottedLine";
			p.Style.Add(HtmlTextWriterStyle.Width, "100%");
			
			// set up list of categories
			_categoryList.Items.Add(new ListItem("Currently Posted Items", string.Empty));
			_categoryList.Items.Add(new ListItem("Board Meetings", "Board Meetings"));
			_categoryList.Items.Add(new ListItem("Pre-Election Disclosure", "Pre-Election Disclosure"));
			_categoryList.Items.Add(new ListItem("Voter Guide", "Voter Guide"));
		}

		#endregion
	}
}
