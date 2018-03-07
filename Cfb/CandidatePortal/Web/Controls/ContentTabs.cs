using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Web;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// Control for displaying content in a tabbed panel.
    /// </summary>
    public abstract class ContentTabs : Panel
    {
        private const string TabScriptTemplate = "<script type='text/javascript'>$('#{0}').tabs({1});$('#{0} .ui-tabs-active a').attr('href','{2}');$('#{0} .ui-tabs-anchor').off('click');</script>";

        /// <summary>
        /// The list of tabs.
        /// </summary>
        private ContentTabList _tabList;

        /// <summary>
        /// The index of the currently active tab.
        /// </summary>
        private int _activeTabIndex;

        /// <summary>
        /// The currently active tab.
        /// </summary>
        private ListItem _activeTab;

        /// <summary>
        /// Gets or sets the ID to use for the active tab.
        /// </summary>
        public string ActiveTabID { get; set; }

        /// <summary>
        /// Gets the navigation target of the default tab.
        /// </summary>
        public string DefaultTabTarget
        {
            get
            {
                this.EnsureChildControls();
                return _tabList != null && _tabList.Items.Count > 0 ? _tabList.Items[0].Value : null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTabs"/> class.
        /// </summary>
        protected ContentTabs()
            : base()
        {
            this.ActiveTabID = this.ClientID + "_SelectedTab";
            _activeTabIndex = -1;
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // construct tabs
            _tabList = new ContentTabList() { DisplayMode = BulletedListDisplayMode.HyperLink };
            NameValueCollection tabs = GetAvailableTabs();
            foreach (string name in tabs.AllKeys)
            {
                this.AddTab(name, tabs[name]);
            }

            // generate temporary Panel to determine automatically generated client ID
            Panel tempPanel = new Panel()
            {
                ID = this.ActiveTabID
            };
            this.Controls.Add(tempPanel);

            // configure active tab item
            string activeTabValue = null;
            if (_activeTab != null)
            {
                activeTabValue = _activeTab.Value;
                _activeTab.Value = "#" + tempPanel.ClientID;
            }
            this.Controls.Remove(tempPanel);

            // register client script
            ClientScriptManager cs = this.Page.ClientScript;
            Type scriptType = this.GetType();
            string scriptName = scriptType.FullName;
            if (!cs.IsClientScriptBlockRegistered(scriptType, scriptName))
            {
                cs.RegisterStartupScript(scriptType, scriptName, string.Format(TabScriptTemplate, this.ClientID, _activeTabIndex < 0 ? null : string.Format("{{active:'{0}'}}", _activeTabIndex), activeTabValue));
            }
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer. Overridden method to generate tab list and tab panel markup around panel contents.
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            _tabList.RenderControl(writer);
            if (_activeTab != null)
                writer.AddAttribute(HtmlTextWriterAttribute.Id, _activeTab.Value.Substring(1));
            if (_tabList.Items.Count == 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ui-tabs-panel ui-widget-content ui-cornerbottom");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.RenderContents(writer);
            writer.RenderEndTag();
        }

        /// <summary>
        /// Adds a tabbed panel tab.
        /// </summary>
        /// <param name="name">The name to display on the tab.</param>
        /// <param name="target">The target page for the tab.</param>
        private void AddTab(string name, string target)
        {
            ListItem item = new ListItem(name, target);
            if (this.CurrentTabMatches(target))
            {
                _activeTab = item;
                _activeTabIndex = _tabList.Items.Count;
            }
            _tabList.Items.Add(item);
        }

        /// <summary>
        /// Determines whether or not the currently viewed tab matches a specific tab target.
        /// </summary>
        /// <param name="target">The target tab to compare against.</param>
        protected virtual bool CurrentTabMatches(string target)
        {
            return Page != null && Page.Request.Path.Equals(target, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Retrieves information regarding the tabs that are available for the current candidate context.
        /// </summary>
        /// <returns>A <see cref="NameValueCollection"/> containing mappings of tab names to destination page URLs for all tabs to be rendered.</returns>
        public abstract NameValueCollection GetAvailableTabs();

        private class ContentTabList : System.Web.UI.WebControls.BulletedList
        {
            protected override void RenderBulletText(ListItem item, int index, HtmlTextWriter writer)
            {
                switch (this.DisplayMode)
                {
                    case BulletedListDisplayMode.HyperLink:
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, item.Value);
                        if (!string.IsNullOrEmpty(this.Target))
                            writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Target);
                        break;
                    default:
                        base.RenderBulletText(item, index, writer);
                        break;
                }
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                HttpUtility.HtmlEncode(item.Text, writer);
                writer.RenderEndTag();
            }
        }
    }
}