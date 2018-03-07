using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// Represents the CalendarView control hosted inside a ListViewWebPart.
	/// </summary>
	[DefaultProperty("ViewType"),
	ToolboxData("<{0}:SPCalendarView runat=server></{0}:SPCalendarView>"),
	AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
	AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SPCalendarView : DataBoundControl, IDesignTimeHtmlProvider, INamingContainer
	{
		private SPCalendarContainer container;
		private bool m_bDownLevelRendering;
		private string m_chromeHeight;
		private Array m_ChromeTemplates;
		private string m_chromeWidth;
		private string m_displayFormUrl = string.Empty;
		private string m_editFormUrl = string.Empty;
		private string m_ListName;
		private string m_newFormUrl = string.Empty;
		private string m_SelectedDate;
		private SPRegionalSettings m_spRegionalSettings;
		//private string m_strCssFileName = "calendar.css";
		private string m_ViewGuid;
		private SPCalendarViewType m_ViewType;
		private string m_ViewTypeString = "month";

		/// <summary>
		/// 
		/// </summary>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.InitChromeTemplateArray();
			SPWeb contextWeb = null; // SPControl.GetContextWeb(this.Context);
			SPView view = null;
			if ((this.ListName != null) && (this.ViewGuid != null))
			{
				try
				{
					//view = contextWeb.Lists.ItemByInternalName(this.ListName).GetView(new Guid(this.ViewGuid));
				}
				catch
				{
				}
			}
			if (!this.m_bDownLevelRendering)
			{
				ITemplate templateByName = (ITemplate)this.m_ChromeTemplates.GetValue((int)this.m_ViewType);
				if (templateByName == null)
				{
					SPCalendarViewStyleCollection styles = new SPCalendarViewStyleCollection(contextWeb, view);
					SPCalendarViewStyle defaultViewStyle = null;
					try
					{
						defaultViewStyle = styles.CalendarViewStyleByType(this.m_ViewTypeString);
					}
					catch
					{
						defaultViewStyle = styles.DefaultViewStyle;
					}
					try
					{
						if ((defaultViewStyle != null) && (defaultViewStyle.Template != null))
						{
							templateByName = SPControlTemplateManager.GetTemplateByName(defaultViewStyle.Template);
						}
					}
					catch (ArgumentException)
					{
					}
				}
				if (templateByName == null)
				{
					this.SetDownLevelRenderingControl(contextWeb);
				}
				else
				{
					this.container = new SPCalendarContainer(contextWeb);
					this.SetSettigns(this.container, contextWeb);
					templateByName.InstantiateIn(this.container);
					this.Controls.Add(this.container);
					if (this.DataSource != null)
						container.DataSource = this.DataSource;
				}
			}
			else
			{
				this.SetDownLevelRenderingControl(contextWeb);
			}
		}

		private SPCalendarViewType GetCalendarViewType(string str)
		{
			string str2;
			if (!string.IsNullOrEmpty(str))
			{
				this.m_ViewTypeString = str;
				str2 = str;
				if (str2 == null)
				{
					goto Label_0054;
				}
				if (str2 == "month")
				{
					return SPCalendarViewType.Month;
				}
				if (str2 == "week")
				{
					return SPCalendarViewType.Week;
				}
			}
			else
			{
				return this.m_ViewType;
			}
			if (str2 == "day")
			{
				return SPCalendarViewType.Day;
			}
		Label_0054:
			return SPCalendarViewType.Custom;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		public string GetDesignTimeHtml()
		{
			HttpContext.Current.Items["CalendarViewPresentOnPage"] = 1;
			StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
			HtmlTextWriter output = new HtmlTextWriter(writer);
			SPWeb contextWeb = null; // SPControl.GetContextWeb(this.Context);
			if (((contextWeb != null) && (this.m_ListName != null)) && (this.m_ViewGuid != null))
			{
				//CssLink.RenderSingleCssLinkFromCssRef(output, contextWeb, this.m_strCssFileName, true);
				this.EnsureChildControls();
				this.RenderChildren(output);
			}
			else
			{
				//CssLink.RenderSingleCssLinkFromCssRef(output, null, this.m_strCssFileName, true);
				MonthlyCalendarView view = new MonthlyCalendarView();
				this.SetContextSettigns(view);
				view.RenderControl(output);
			}
			return writer.ToString();
		}

		private void InitChromeTemplateArray()
		{
			if (this.m_ChromeTemplates == null)
			{
				this.m_ChromeTemplates = Array.CreateInstance(typeof(ITemplate), Enum.GetValues(typeof(SPCalendarViewType)).Length);
			}
		}

		/// <param name="e">
		/// 
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnInit(EventArgs e)
		{
			this.EnableViewState = false;
			base.OnInit(e);
			//if (SPContext.Current != null)
			//{
			//    CssRegistration.Register(this.m_strCssFileName);
			//}
		}

		/// <param name="e">
		/// 
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnLoad(EventArgs e)
		{
			HttpContext.Current.Items["CalendarViewPresentOnPage"] = 1;
		}

		/// <param name="e">
		/// 
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnPreRender(EventArgs e)
		{
			this.EnsureChildControls();
			if (this.container != null)
			{
				this.container.ChromeWidth = this.m_chromeWidth;
				this.container.ChromeHeight = this.m_chromeHeight;
				try
				{
					this.container.DataBind();
				}
				catch (ArgumentNullException)
				{
				}
			}
			base.OnPreRender(e);
		}

		private void SetContextSettigns(SPCalendarBase view)
		{
			SPContext context = null;
			//try
			//{
			//    context = SPContext.GetContext(this.Context);
			//}
			//catch
			//{
			//}
			if ((context != null) || (this.m_spRegionalSettings != null))
			{
				if (this.m_spRegionalSettings == null)
				{
					this.m_spRegionalSettings = context.RegionalSettings;
				}
				view.LocaleId = (int)this.m_spRegionalSettings.LocaleId;
				view.Calendar = (SPCalendarType)this.m_spRegionalSettings.CalendarType;
				view.AlternateCalendar = (SPCalendarType)this.m_spRegionalSettings.AlternateCalendarType;
				view.Time24 = this.m_spRegionalSettings.Time24;
				view.HijriAdjustment = this.m_spRegionalSettings.AdjustHijriDays;
				view.FirstDayOfWeek = (int)this.m_spRegionalSettings.FirstDayOfWeek;
				//view.WorkWeek = SPUtility.BitsToString(this.m_spRegionalSettings.WorkDays, 7);
				view.WorkDayStartHour = this.m_spRegionalSettings.WorkDayStartHour;
				view.WorkDayEndHour = this.m_spRegionalSettings.WorkDayEndHour;
				view.TimeZoneSpan = SPUtility.GetTimeSpanFromTimeZone(this.m_spRegionalSettings.TimeZone);
			}
		}

		private void SetDownLevelRenderingControl(SPWeb web)
		{
			CPMonthlyCalendarView view = new CPMonthlyCalendarView
			{
				DownLevelRendering = true,
				DataSource = this.DataSource,
				SelectedDate = this.m_SelectedDate,
				ViewName = this.ViewGuid
			};
			this.SetContextSettigns(view);
			view.NewItemFormUrl = this.m_newFormUrl;
			view.EditItemFormUrl = this.m_editFormUrl;
			view.DisplayItemFormUrl = this.m_displayFormUrl;
			this.Controls.Add(view);
		}

		private void SetSettigns(SPCalendarContainer container, SPWeb web)
		{
			container.ListName = this.m_ListName;
			container.ViewName = this.m_ViewGuid;
			if ((this.SelectedDate == null) || (this.SelectedDate == string.Empty))
			{
				this.SelectedDate = SPUtility.GetSelectedDate(this.Context.Request, web);
			}
			container.SelectedDate = this.SelectedDate;
			container.ViewType = this.m_ViewTypeString;
			container.NewItemFormUrl = this.m_newFormUrl;
			container.EditItemFormUrl = this.m_editFormUrl;
			container.DisplayItemFormUrl = this.m_displayFormUrl;
			container.ChromeWidth = this.m_chromeWidth;
			container.ChromeHeight = this.m_chromeHeight;
		}

		/// <summary>
		/// 
		/// </summary>
		public string ChromeHeight
		{
			get { return this.m_chromeHeight; }
			set { this.m_chromeHeight = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ChromeWidth
		{
			get { return this.m_chromeWidth; }
			set { this.m_chromeWidth = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public override ControlCollection Controls
		{
			[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[TemplateContainer(typeof(SPCalendarContainer)), PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate CustomChromeTemplate
		{
			get
			{
				this.EnsureChildControls();
				return (ITemplate)this.m_ChromeTemplates.GetValue(3);
			}
			set
			{
				this.InitChromeTemplateArray();
				this.m_ChromeTemplates.SetValue(value, 3);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[TemplateContainer(typeof(SPCalendarContainer)), PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate DailyChromeTemplate
		{
			get
			{
				this.EnsureChildControls();
				return (ITemplate)this.m_ChromeTemplates.GetValue(2);
			}
			set
			{
				this.InitChromeTemplateArray();
				this.m_ChromeTemplates.SetValue(value, 2);
			}
		}

		/// <summary>
		/// Name for custom Display Form Name.
		/// </summary>
		[DefaultValue(""), Bindable(true), Category("Data"), Description("Name for custom Display Form Name.")]
		public string DisplayItemFormUrl
		{
			get
			{
				return this.m_displayFormUrl;
			}
			set
			{
				if (!SPUrlUtility.IsUrlRelative(value))
				{
					throw new ArgumentException(SPResource.GetString("UrlIsNotSafe", new object[0]));
				}
				this.m_displayFormUrl = value;
			}
		}

		/// <summary>
		/// Inicates that browser doesn't support DHML.
		/// </summary>
		[Bindable(true), Category("Behaivor"), DefaultValue(false), Description("Inicates that browser doesn't support DHML.")]
		public bool DownLevelRendering
		{
			get
			{
				return this.m_bDownLevelRendering;
			}
			set
			{
				this.m_bDownLevelRendering = value;
				base.ChildControlsCreated = false;
			}
		}

		/// <summary>
		/// Name for custom Edit Form Name.
		/// </summary>
		[DefaultValue(""), Bindable(true), Category("Data"), Description("Name for custom Edit Form Name.")]
		public string EditItemFormUrl
		{
			get
			{
				return this.m_editFormUrl;
			}
			set
			{
				if (!SPUrlUtility.IsUrlRelative(value))
				{
					throw new ArgumentException(SPResource.GetString("UrlIsNotSafe", new object[0]));
				}
				this.m_editFormUrl = value;
			}
		}

		/// <summary>
		/// List Name.
		/// </summary>
		[DefaultValue(""), Category("Data"), Description("List Name."), Bindable(true)]
		public string ListName
		{
			get
			{
				return this.m_ListName;
			}
			set
			{
				this.m_ListName = value;
				base.ChildControlsCreated = false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[TemplateContainer(typeof(SPCalendarContainer)), PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate MonthlyChromeTemplate
		{
			get
			{
				this.EnsureChildControls();
				return (ITemplate)this.m_ChromeTemplates.GetValue(0);
			}
			set
			{
				this.InitChromeTemplateArray();
				this.m_ChromeTemplates.SetValue(value, 0);
			}
		}

		/// <summary>
		/// Name for custom New Form Name.
		/// </summary>
		[Category("Data"), Bindable(true), Description("Name for custom New Form Name."), DefaultValue("")]
		public string NewItemFormUrl
		{
			get
			{
				return this.m_newFormUrl;
			}
			set
			{
				if (!SPUrlUtility.IsUrlRelative(value))
				{
					throw new ArgumentException(SPResource.GetString("UrlIsNotSafe", new object[0]));
				}
				this.m_newFormUrl = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public SPRegionalSettings RegionalSettings
		{
			get { return this.m_spRegionalSettings; }
			set { this.m_spRegionalSettings = value; }
		}

		/// <summary>
		/// DateTime as string for rendering point in Local Time, Langauge and Calendar.
		/// </summary>
		[Category("Data"), DefaultValue(""), Bindable(true), Description("DateTime as string for rendering point in Local Time, Langauge and Calendar. ")]
		public string SelectedDate
		{
			get { return this.m_SelectedDate; }
			set { this.m_SelectedDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[DefaultValue(""), Bindable(true), Description("View GUID as string."), Category("Data")]
		public string ViewGuid
		{
			get
			{
				return this.m_ViewGuid;
			}
			set
			{
				this.m_ViewGuid = value;
				base.ChildControlsCreated = false;
			}
		}

		/// <summary>
		/// View type of Calendar. Can be on of follow strings: "Month", "Week", "Day", "TimeLine", "Custom".
		/// </summary>
		[Description("View type of Calendar. Can be on of follow strings: 'Month', 'Week', 'Day', 'TimeLine', 'Custom'."), Category("Data"), Bindable(true), DefaultValue("Month")]
		public string ViewType
		{
			get
			{
				return this.m_ViewTypeString;
			}
			set
			{
				this.m_ViewType = this.GetCalendarViewType(value);
				base.ChildControlsCreated = false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(SPCalendarContainer))]
		public ITemplate WeeklyChromeTemplate
		{
			get
			{
				this.EnsureChildControls();
				return (ITemplate)this.m_ChromeTemplates.GetValue(1);
			}
			set
			{
				this.InitChromeTemplateArray();
				this.m_ChromeTemplates.SetValue(value, 1);
			}
		}
	}
}