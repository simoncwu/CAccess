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
	/// Represents the calendar container.
	/// </summary>
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true),
	AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
	SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true),
	AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SPCalendarContainer : RenderingTemplateContainer, ISPCalendarSettings
	{
		private DateOptions m_AlternateDateOptions;
		private string m_chromeHeight;
		private string m_chromeWidth;
		private DateOptions m_DateOptions;
		private string m_ListName;
		private object m_Obj;
		private string m_SelectedDate;
		private string m_strDisplayItemFormUrl;
		private string m_strEditItemFormUrl;
		private string m_strNewItemFormUrl;
		private string m_ViewGuid;
		private string m_ViewTypeString;
		private SPWeb m_Web;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Microsoft.SharePoint.WebControls.SPCalendarContainer"></see> class.
		/// </summary>
		/// <param name="obj">
		/// 
		/// </param>
		public SPCalendarContainer(object obj)
		{
			if (obj is SPWeb)
			{
				this.m_Web = (SPWeb)obj;
			}
			this.m_Obj = obj;
		}

		private DateOptions getAlternateDateOptions()
		{
			if (this.m_AlternateDateOptions == null)
			{
				this.m_AlternateDateOptions = new DateOptions(this.LocaleId.ToString(CultureInfo.InvariantCulture), this.AlternateCalendarType, this.WorkWeek, this.FirstDayOfWeek.ToString(CultureInfo.InvariantCulture), this.HijriAdjustment.ToString(CultureInfo.InvariantCulture), this.TimeZoneSpan.ToString(), this.SelectedDate);
			}
			return this.m_AlternateDateOptions;
		}

		private DateOptions getDateOptions()
		{
			if (this.m_DateOptions == null)
			{
				this.m_DateOptions = new DateOptions(this.LocaleId.ToString(), this.CalendarType, this.WorkWeek, this.FirstDayOfWeek.ToString(), this.HijriAdjustment.ToString(), this.TimeZoneSpan.ToString(), this.SelectedDate);
			}
			return this.m_DateOptions;
		}

		private string getNavigationDate(bool bNext)
		{
			// TODO: reverse engineer
			return string.Empty;
		}

		/// <param name="e">
		/// 
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		/// <summary>
		/// 
		/// </summary>
		public SPCalendarType AlternateCalendarType
		{
			get { return SPCalendarType.None; }
		}

		/// <summary>
		/// 
		/// </summary>
		public SPCalendarType CalendarType
		{
			get { return SPCalendarType.None; }
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
		public new object DataItem
		{
			[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
			get { return this.m_Obj; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Direction
		{
			get { return SPResource.GetString("DirectionDirValue", new object[0]); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string DisplayItemFormUrl
		{
			get { return this.m_strDisplayItemFormUrl; }
			set { this.m_strDisplayItemFormUrl = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string EditItemFormUrl
		{
			get { return this.m_strEditItemFormUrl; }
			set { this.m_strEditItemFormUrl = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ExpandedWeeks
		{
			get { return (string)this.Context.Items["ExpandedWeeks"]; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int FirstDayOfWeek
		{
			get { return 0; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string HeaderDate
		{
			get
			{
				string str;
				string str2;
				DateOptions options = this.getDateOptions();
				DateOptions options2 = this.getAlternateDateOptions();
				bool flag = this.AlternateCalendarType != SPCalendarType.None;
				bool flag2 = false; // options.isEastAsiaCalendar();
				bool flag3 = flag && false; // options2.isEastAsiaCalendar();
				SimpleDate selectedDate = options.SelectedDate;
				if (string.IsNullOrEmpty(this.Direction))
				{
					str = "<span dir=\"rtl\">";
					str2 = "</span>";
				}
				else
				{
					str = "";
					str2 = "";
				}
				if ((this.AlternateCalendarType != SPCalendarType.None) && (this.AlternateCalendarType != this.CalendarType))
				{
					try
					{
						int jDay = SPIntlCal.LocalToJulianDay(this.m_DateOptions.CalendarType, ref selectedDate, this.m_DateOptions.HijriAdjustment);
						SPIntlCal.JulianDayToLocal(this.AlternateCalendarType, jDay, ref selectedDate, this.m_DateOptions.HijriAdjustment, jDay);
					}
					catch (ArgumentException)
					{
						flag = false;
					}
				}
				switch (this.ViewType)
				{
					case "day":
						return SPHttpUtility.NoEncode(options.GetDowLongDateString(options.SelectedDate) + (flag ? (" " + str + "(" + options2.GetDowLongDateString(selectedDate) + ")" + str2) : ""));

					case "week":
						{
							string str3;
							string str4;
							SimpleDate di = options.SelectedDate;
							SimpleDate date3 = options.SelectedDate;
							int num2 = SPIntlCal.LocalToJulianDay(this.m_DateOptions.CalendarType, ref di, this.m_DateOptions.HijriAdjustment);
							int num3 = (num2 + 1) % 7;
							int firstDayOfWeek = this.m_DateOptions.FirstDayOfWeek;
							int num5 = 0;
							int num6 = num2;
							if (num3 != firstDayOfWeek)
							{
								num5 = ((num3 - firstDayOfWeek) + 7) % 7;
								num6 -= num5;
							}
							try
							{
								SPIntlCal.JulianDayToLocal(this.m_DateOptions.CalendarType, num6, ref di, this.m_DateOptions.HijriAdjustment);
							}
							catch
							{
							}
							int num7 = (num6 + 7) - 1;
							SimpleDate date4 = options.SelectedDate;
							SimpleDate date5 = options.SelectedDate;
							try
							{
								SPIntlCal.JulianDayToLocal(this.m_DateOptions.CalendarType, num7, ref date4, this.m_DateOptions.HijriAdjustment);
							}
							catch
							{
							}
							if (flag)
							{
								try
								{
									SPIntlCal.JulianDayToLocal(this.AlternateCalendarType, num6, ref date3, this.m_DateOptions.HijriAdjustment);
									SPIntlCal.JulianDayToLocal(this.AlternateCalendarType, num7, ref date5, this.m_DateOptions.HijriAdjustment);
								}
								catch (ArgumentException)
								{
									flag = false;
								}
							}
							bool flag4 = di.Year != date4.Year;
							if (flag2)
							{
								str3 = flag4 ? "{2} {0} - {3} {1}" : "{3} {0} - {1}";
							}
							else
							{
								str3 = flag4 ? "{0} {2} - {1} {3}" : "{0} - {1} {3}";
							}
							if (flag3)
							{
								str4 = flag4 ? "{2} {0} - {3} {1}" : "{3} {0} - {1}";
							}
							else
							{
								str4 = flag4 ? "{0} {2} - {1} {3}" : "{0} - {1} {3}";
							}
							return SPHttpUtility.NoEncode(string.Format(str3, new object[] { options.GetMonthDayDateString(di), options.GetMonthDayDateString(date4), options.GetYearString(di), options.GetYearString(date4) }) + (flag ? (" " + str + "(" + string.Format(str4, new object[] { options2.GetMonthDayDateString(date3), options2.GetMonthDayDateString(date5), options2.GetYearString(date3), options2.GetYearString(date5) }) + ")" + str2) : ""));
						}
				}
				return SPHttpUtility.NoEncode(options.GetMonthYearString(options.SelectedDate) + (flag ? (" " + str + "(" + options2.GetMonthYearString(selectedDate) + ")" + str) : ""));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int HijriAdjustment
		{
			get { return 0; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ListName
		{
			get { return this.m_ListName; }
			set { this.m_ListName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int LocaleId
		{
			get { return 0x409; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string NewItemFormUrl
		{
			get { return this.m_strNewItemFormUrl; }
			set { this.m_strNewItemFormUrl = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string NextDate
		{
			get { return SPHttpUtility.EcmaScriptStringLiteralEncode(this.getNavigationDate(string.IsNullOrEmpty(this.Direction))); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string NextDateVisible
		{
			get { return string.IsNullOrEmpty(this.NextDate) ? "hidden" : "visible"; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string PreviousDate
		{
			get { return SPHttpUtility.EcmaScriptStringLiteralEncode(this.getNavigationDate(!string.IsNullOrEmpty(this.Direction))); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string PreviousDateVisible
		{
			get { return string.IsNullOrEmpty(this.PreviousDate) ? "hidden" : "visible"; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string SelectedDate
		{
			get { return this.m_SelectedDate; }
			set { this.m_SelectedDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Time24
		{
			get { return false; }
		}

		/// <summary>
		/// 
		/// </summary>
		public TimeSpan TimeZoneSpan
		{
			get { return new TimeSpan(); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			get { return this.Context.Request.Url.ToString(); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ViewName
		{
			get { return this.m_ViewGuid; }
			set { this.m_ViewGuid = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ViewType
		{
			get { return this.m_ViewTypeString; }
			set { this.m_ViewTypeString = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public short WorkDayEndHour
		{
			get { return (short)0x11; }
		}

		/// <summary>
		/// 
		/// </summary>
		public short WorkDayStartHour
		{
			get { return (short)8; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string WorkWeek
		{
			get { return "0111110"; }
		}

		/// <summary>
		/// Gets or sets the object from which the data-bound control retrieves its list of items.
		/// </summary>
		public object DataSource
		{
			get
			{
				CPMonthlyCalendarView view = FindCalendarView();
				return view != null ? view.DataSource : null;
			}
			set
			{
				CPMonthlyCalendarView view = FindCalendarView();
				if (view != null)
					view.DataSource = value;
			}
		}

		/// <summary>
		/// Searches the child controls for a calendar view control.
		/// </summary>
		/// <returns>The first calendar view child control found if one exists; otherwise, null.</returns>
		private CPMonthlyCalendarView FindCalendarView()
		{
			foreach (Control c in this.Controls)
			{
				if (c is CPMonthlyCalendarView)
					return c as CPMonthlyCalendarView;
			}
			return null;
		}
	}
}