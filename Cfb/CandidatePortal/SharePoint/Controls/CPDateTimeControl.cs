using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using System.Collections.Specialized;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// Represents a DateTime control.
	/// </summary>
	[DefaultProperty("Text")]
	[Designer(typeof(SPControlDesigner))]
	[ToolboxData("<{0}:CPDateTimeControl runat=server></{0}:CPDateTimeControl>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	public class CPDateTimeControl : SPCompositeControl, IValidator, IPostBackDataHandler, IDesignerEventAccessor
	{
		private bool autoPostBack;
		private GregorianCalendar defaultCalendar = new GregorianCalendar();
		private static readonly object EventDateChanged = new object();
		private bool m_bChangesByUserProccessed;
		private bool m_bDateValid = true;
		private bool m_bHasSelectedDate;
		private bool m_bShowWeekNumber;
		private bool m_bTimeSpanInit;
		private bool m_bUseTimeZoneAdjustment;
		private SPCalendarType m_calendar = SPCalendarType.Gregorian;
		private string m_calendargreyiconAltText = SPResource.GetString("CalendarGreyiconAltText", new object[0]);
		private string m_calendariconAltText = SPResource.GetString("CalendariconAltText", new object[0]);
		private string m_calendarImageUrl = "/_layouts/images/calendar.gif";
		private string m_cssClassDescription = "ms-formdescription";
		private string m_cssClassTextBox = "ms-input";
		private bool m_dateOnly;
		private DateOptions m_dateOptions;
		private string m_datepickerFrameID = "DatePickerFrame";
		private string m_datepickerImageID = "DatePickerImage";
		private TextBox m_dateTextBox;
		private string m_dateTextBoxLabel = SPResource.GetString("DateLabel", new object[0]);
		private bool m_enabled = true;
		private string m_errorMessage;
		private string m_errorMessage_validatorRange = SPResource.GetString("ErrorMessageValidatorRange", new object[0]);
		private string m_errorMessage_validatorRequired = SPResource.GetString("MissingRequiredField", new object[0]);
		private int m_firstDayOfWeek = -1;
		private short m_firstWeekOfYear;
		private bool m_fIsValid = true;
		private int m_hijriAdjustment;
		private DropDownList m_hoursDropDown;
		private string m_hoursDropDownLabel = SPResource.GetString("HoursLabel", new object[0]);
		private bool m_hoursMode24;
		private string m_labelText_format = SPResource.GetString("EnterDateFormat", new object[0]);
		private uint m_langId = ((uint)Thread.CurrentThread.CurrentUICulture.LCID);
		private int m_localeId = -1;
		private DateTime m_maxDate = DateTime.MaxValue;
		private string m_maxDateString;
		private int m_maxJDay;
		private DateTime m_minDate = DateTime.MinValue;
		private string m_minDateString;
		private int m_minJDay;
		private string[] m_minutes = new string[] { "00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55" };
		private DropDownList m_minutesDropDown;
		private string m_minutesDropDownLabel = SPResource.GetString("MinutesLabel", new object[0]);
		private string[] m_minutesExt = new string[] { "00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55", "" };
		private string m_PageDirection = SPResource.GetString("DirectionDirValue", new object[0]);
		private bool m_requiredField;
		private DateTime m_selectedDate = DateTime.MinValue;
		private short m_tabIndex = -1;
		private bool m_timeOnly;
		private TimeSpan m_timespan;
		private SPTimeZone m_timeZone;
		private int m_timeZoneID = -1;
		private string m_ToolTip;
		private string m_urlDatePickerFrame = "/_layouts/iframe.aspx";
		private string m_urlDatePickerJavaScript = "/_layouts/datepicker.js";
		/// <summary>
		/// This type or member supports  and is not intended to be used directly from your code.
		/// </summary>
		protected RequiredFieldValidator m_validatorRequired;
		private string m_workWeek = "0111110";
		private string onValueChangeClientScript;

		/// <summary>
		/// Occurs when the date selected in the control changes.
		/// </summary>
		public event EventHandler DateChanged
		{
			add { base.Events.AddHandler(EventDateChanged, value); }
			remove { base.Events.RemoveHandler(EventDateChanged, value); }
		}

		private void ChangesByUser(object sender, EventArgs e)
		{
			if (!this.m_bChangesByUserProccessed)
			{
				if (this.IsDateEmpty)
				{
					if (this.m_selectedDate == DateTime.MinValue)
					{
						this.m_bHasSelectedDate = false;
					}
					this.m_bChangesByUserProccessed = true;
					return;
				}
				this.EnsureDateOptions();
				DateTime dateTimeFromTextBox = this.GetDateTimeFromTextBox();
				if (dateTimeFromTextBox != DateTime.MaxValue)
				{
					dateTimeFromTextBox = this.GetSelectedDate(dateTimeFromTextBox);
					if (this.SelectedDate.Equals(dateTimeFromTextBox))
					{
						this.OnDateChanged(EventArgs.Empty);
					}
					this.SelectedDate = dateTimeFromTextBox;
					this.m_bDateValid = true;
				}
				else
				{
					this.InitErrorStrings();
					this.m_errorMessage = string.Format(CultureInfo.InvariantCulture, this.m_errorMessage_validatorRange, new object[] { this.m_minDateString, this.m_maxDateString });
					this.SelectedDate = DateTime.MaxValue;
					this.m_bDateValid = false;
				}
			}
			else
			{
				return;
			}
			this.m_bChangesByUserProccessed = true;
		}

		/// <summary>
		/// Clears the current selection in the control.
		/// </summary>
		public void ClearSelection()
		{
			this.EnsureChildControls();
			this.m_dateTextBox.Text = string.Empty;
			this.m_selectedDate = DateTime.MinValue;
			this.m_bHasSelectedDate = false;
		}

		/// <summary>
		/// Notifies the server control to create any child controls it contains in preparation for posting back or rendering.
		/// </summary>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.m_dateTextBox = new TextBox();
			this.m_dateTextBox.ID = this.ID + "Date";
			this.m_dateTextBox.MaxLength = 0x2d;
			this.m_dateTextBox.CssClass = this.CssClassTextBox;
			if (this.TabIndex > 0)
			{
				this.m_dateTextBox.TabIndex = this.TabIndex;
			}
			this.m_hoursDropDown = new DropDownList();
			this.m_hoursDropDown.ID = this.ID + "DateHours";
			if (this.TabIndex > 0)
			{
				this.m_hoursDropDown.TabIndex = this.TabIndex;
			}
			this.m_minutesDropDown = new DropDownList();
			this.m_minutesDropDown.ID = this.ID + "DateMinutes";
			if (this.TabIndex > 0)
			{
				this.m_minutesDropDown.TabIndex = this.TabIndex;
			}
			this.Controls.Add(this.m_dateTextBox);
			this.Controls.Add(this.m_hoursDropDown);
			this.Controls.Add(this.m_minutesDropDown);
			this.m_validatorRequired = new RequiredFieldValidator();
			this.m_validatorRequired.Display = ValidatorDisplay.Dynamic;
			this.m_validatorRequired.ErrorMessage = this.m_errorMessage_validatorRequired;
			this.m_validatorRequired.ControlToValidate = this.m_dateTextBox.ID;
			this.m_validatorRequired.Visible = this.IsRequiredField && !this.m_timeOnly;
			this.m_validatorRequired.EnableClientScript = this.IsRequiredField && !this.m_timeOnly;
			this.Controls.Add(this.m_validatorRequired);
			this.m_dateTextBox.TextChanged += new EventHandler(this.ChangesByUser);
			this.m_hoursDropDown.SelectedIndexChanged += new EventHandler(this.ChangesByUser);
			this.m_minutesDropDown.SelectedIndexChanged += new EventHandler(this.ChangesByUser);
		}

		private void DropTimeZone()
		{
			this.m_timeZone = null;
			this.m_bTimeSpanInit = false;
		}

		private void EnsureDateOptions()
		{
			if (this.m_dateOptions == null)
			{
				this.m_dateOptions = new DateOptions(this.LocaleId.ToString(CultureInfo.InvariantCulture), this.Calendar, this.WorkWeek, (this.FirstDayOfWeek >= 0) ? this.FirstDayOfWeek.ToString(CultureInfo.InvariantCulture) : null, this.HijriAdjustment.ToString(CultureInfo.InvariantCulture), (this.TimeZoneID >= 0) ? this.GetTimeSpanFromTimeZone().ToString() : null, null);
			}
		}

		/// <summary>
		/// Sets input focus to the control.
		/// </summary>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		public override void Focus()
		{
			this.EnsureChildControls();
			this.m_dateTextBox.Focus();
		}

		private DateTime GetDateTimeFromTextBox()
		{
			SimpleDate date;
			if (this.m_dateOptions.ParseShortDate(this.m_dateTextBox.Text, out date) && SPIntlCal.IsLocalDateValid(this.m_dateOptions.CalendarType, ref date, this.HijriAdjustment))
			{
				if (this.m_dateOptions.CalendarType != SPCalendarType.Gregorian)
				{
					int jDay = SPIntlCal.LocalToJulianDay(this.m_dateOptions.CalendarType, ref date, this.HijriAdjustment);
					SPIntlCal.JulianDayToLocal(SPCalendarType.Gregorian, jDay, ref date);
				}
				int hour = 0;
				int minute = 0;
				if (!this.DateOnly)
				{
					hour = this.m_hoursDropDown.SelectedIndex;
					minute = int.Parse(this.m_minutesDropDown.SelectedItem.Value, CultureInfo.InvariantCulture);
				}
				DateTime dt = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0, 0, this.defaultCalendar);
				if (this.ValidateDateTimeRange(dt))
				{
					return dt;
				}
			}
			return DateTime.MaxValue;
		}

		private DateTime GetLocalDate()
		{
			return this.m_bUseTimeZoneAdjustment ? this.GetTimeZone().UTCToLocalTime(this.SelectedDate) : this.SelectedDate;
		}

		private DateTime GetSelectedDate(DateTime someLocalDate)
		{
			return this.m_bUseTimeZoneAdjustment ? this.GetTimeZone().LocalTimeToUTC(someLocalDate) : someLocalDate;
		}

		internal DateTime GetSelectedTime()
		{
			this.EnsureChildControls();
			this.EnsureDateOptions();
			DateTime localDate = this.GetLocalDate();
			return new DateTime(localDate.Year, localDate.Month, localDate.Day, (this.m_hoursDropDown.SelectedIndex < 0) ? localDate.Hour : this.m_hoursDropDown.SelectedIndex, (this.m_minutesDropDown.SelectedIndex < 0) ? localDate.Minute : int.Parse(this.m_minutesDropDown.SelectedItem.Value, CultureInfo.InvariantCulture), 0, 0, this.defaultCalendar);
		}

		private TimeSpan GetTimeSpanFromTimeZone()
		{
			if (!this.m_bTimeSpanInit)
			{
				if (this.m_bUseTimeZoneAdjustment)
				{
					SPTimeZone timeZone = this.GetTimeZone();
					DateTime utcNow = DateTime.UtcNow;
					this.m_timespan = timeZone.UTCToLocalTime(utcNow).Subtract(utcNow);
					this.m_bTimeSpanInit = true;
				}
				else
				{
					this.m_timespan = new TimeSpan(0L);
					this.m_bTimeSpanInit = true;
				}
			}
			else
			{
				return this.m_timespan;
			}
			return this.m_timespan;
		}

		private SPTimeZone GetTimeZone()
		{
			if ((this.m_timeZone == null) && this.m_bUseTimeZoneAdjustment)
			{
				SPControl.GetContextWeb(HttpContext.Current);
				this.m_timeZone = SPContext.GetContext(this.Context).RegionalSettings.TimeZone;
				if (this.TimeZoneID != -1 && this.m_timeZone != null)
				{
					this.m_timeZone.ID = (ushort)this.TimeZoneID;
				}
			}
			return this.m_timeZone;
		}

		private string getUrlString(string strMember, string strFormat, string strData)
		{
			if ((strMember == null) && ((strFormat != null) && (strData != null)))
			{
				return string.Format(strFormat, strData);
			}
			if (strMember == null)
			{
				return string.Empty;
			}
			return strMember;
		}

		private void InitErrorStrings()
		{
			this.EnsureDateOptions();
			this.SetMinMaxDateTime();
			if (this.m_dateOptions.CalendarType == SPCalendarType.Gregorian)
			{
				this.m_minDateString = this.MinDate.ToString(this.m_dateOptions.ShortDatePattern, CultureInfo.InvariantCulture);
				this.m_maxDateString = this.MaxDate.ToString(this.m_dateOptions.ShortDatePattern, CultureInfo.InvariantCulture);
			}
			else
			{
				SimpleDate di = new SimpleDate(this.MinDate.Year, this.MinDate.Month, this.MinDate.Day);
				SimpleDate date2 = new SimpleDate(this.MaxDate.Year, this.MaxDate.Month, this.MaxDate.Day);
				try
				{
					int jDay = SPIntlCal.LocalToJulianDay(SPCalendarType.Gregorian, ref di);
					SPIntlCal.JulianDayToLocal(this.m_dateOptions.CalendarType, jDay, ref di, this.HijriAdjustment);
					jDay = SPIntlCal.LocalToJulianDay(SPCalendarType.Gregorian, ref date2);
					SPIntlCal.JulianDayToLocal(this.m_dateOptions.CalendarType, jDay, ref date2, this.HijriAdjustment);
				}
				catch (ArgumentException)
				{
				}
				this.m_minDateString = this.m_dateOptions.GetShortDateString(di);
				this.m_maxDateString = this.m_dateOptions.GetShortDateString(date2);
			}
		}

		/// <summary>
		/// Raises an event when the control designer loads.
		/// </summary>
		/// <param name="e">
		/// An <see cref="T:System.EventArgs"></see> object that contains the event data.
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		void IDesignerEventAccessor.OnDesignerLoad(EventArgs e)
		{
			this.OnLoad(e);
		}

		/// <summary>
		/// Raises an event after the control designer is loaded but prior to rendering.
		/// </summary>
		/// <param name="e">
		/// An <see cref="T:System.EventArgs"></see> object that contains the event data.
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		void IDesignerEventAccessor.OnDesignerPreRender(EventArgs e)
		{
			this.EnsureChildControls();
			this.OnPreRender(e);
		}

		/// <summary>
		/// Raises an event when the date displayed in the control has changed.
		/// </summary>
		/// <param name="e">
		/// An <see cref="T:System.EventArgs"></see> object that contains the event data.
		/// </param>
		protected virtual void OnDateChanged(EventArgs e)
		{
			EventHandler handler = (EventHandler)base.Events[EventDateChanged];
			if (handler != null)
			{
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises an event when the server control is initialized.
		/// </summary>
		/// <param name="e">
		/// An <see cref="T:System.EventArgs"></see> object that contains the event data. 
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.Validators.Add(this);
		}

		/// <summary>
		/// Raises an event after the control is loaded but before it renders.
		/// </summary>
		/// <param name="e">
		/// An <see cref="T:System.EventArgs"></see> object that contains the event data. 
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnPreRender(EventArgs e)
		{
			SimpleDate date2;
			this.EnsureChildControls();
			this.InitErrorStrings();
			this.m_validatorRequired.Visible = this.IsRequiredField && !this.m_timeOnly;
			this.m_validatorRequired.EnableClientScript = this.IsRequiredField && !this.m_timeOnly;
			this.Page.ClientScript.RegisterClientScriptBlock(typeof(DateTimeControl), "datepicker_js", "<script language=\"javascript\" src=\"" + this.m_urlDatePickerJavaScript + "\"></script>");
			this.m_validatorRequired.Enabled = this.m_enabled;
			this.m_dateTextBox.Enabled = this.m_enabled;
			if (this.m_dateTextBox.Enabled)
			{
				this.m_dateTextBox.ToolTip = this.m_ToolTip;
			}
			this.m_dateTextBox.Visible = !this.m_timeOnly;
			this.m_hoursDropDown.Enabled = this.m_enabled;
			this.m_minutesDropDown.Enabled = this.m_enabled;
			((IControlDesignerAccessor)this.m_hoursDropDown).GetDesignModeState()["EnableDesignTimeDataBinding"] = true;
			((IControlDesignerAccessor)this.m_minutesDropDown).GetDesignModeState()["EnableDesignTimeDataBinding"] = true;
			this.m_hoursDropDown.DataSource = this.m_dateOptions.GetHoursString(this.HoursMode24, false);
			this.m_hoursDropDown.DataBind();
			if (!(this.SelectedDate != DateTime.MaxValue))
			{
				goto Label_0318;
			}
			DateTime localDate = this.GetLocalDate();
			SimpleDate di = new SimpleDate(localDate.Year, localDate.Month, localDate.Day);
			if (this.m_dateOptions.CalendarType != SPCalendarType.Gregorian)
			{
				try
				{
					int jDay = SPIntlCal.LocalToJulianDay(SPCalendarType.Gregorian, ref di);
					SPIntlCal.JulianDayToLocal(this.m_dateOptions.CalendarType, jDay, ref di, this.HijriAdjustment);
				}
				catch (ArgumentException)
				{
					this.m_bHasSelectedDate = false;
				}
			}
			string str = string.Empty;
			try
			{
				if (this.m_bHasSelectedDate)
				{
					goto Label_020B;
				}
			Label_01F2:
				if (!this.m_timeOnly)
				{
					goto Label_021A;
				}
			Label_01FC:
				str = this.m_dateOptions.GetShortDateString(di);
				goto Label_021A;
			Label_020B:
				if (this.m_bDateValid)
				{
					goto Label_01FC;
				}
				goto Label_01F2;
			}
			catch
			{
			}
		Label_021A:
			this.m_dateTextBox.Text = str;
			if (!this.m_bHasSelectedDate)
			{
			}
			this.m_hoursDropDown.SelectedIndex = this.m_bDateValid ? localDate.Hour : 0;
			int index = 0;
			if (this.m_bHasSelectedDate && this.m_bDateValid)
			{
				if ((localDate.Minute % 5) > 0)
				{
					index = 12;
					this.m_minutesExt.SetValue((localDate.Minute < 10) ? ("0" + localDate.Minute.ToString(CultureInfo.InvariantCulture)) : localDate.Minute.ToString(CultureInfo.InvariantCulture), index);
					this.m_minutes = this.m_minutesExt;
				}
				else
				{
					index = localDate.Minute / 5;
				}
			}
			this.m_minutesDropDown.DataSource = this.m_minutes;
			this.m_minutesDropDown.DataBind();
			this.m_minutesDropDown.SelectedIndex = index;
		Label_0318:
			date2 = new SimpleDate(0x7d5, 10, 0x16);
			if (this.m_dateOptions.CalendarType != SPCalendarType.Gregorian)
			{
				try
				{
					int num3 = SPIntlCal.LocalToJulianDay(SPCalendarType.Gregorian, ref date2);
					SPIntlCal.JulianDayToLocal(this.m_dateOptions.CalendarType, num3, ref date2, this.HijriAdjustment);
				}
				catch (ArgumentException)
				{
				}
			}
			this.m_dateTextBox.AutoPostBack = this.autoPostBack;
			if (!string.IsNullOrEmpty(this.OnValueChangeClientScript))
			{
				this.m_dateTextBox.Attributes.Add("onchange", this.OnValueChangeClientScript);
				this.m_dateTextBox.Attributes.Add("onvaluesetfrompicker", this.OnValueChangeClientScript);
			}
			this.m_dateTextBox.Attributes.Add("AutoPostBack", this.autoPostBack ? "1" : "0");
			this.Page.ClientScript.RegisterClientScriptBlock(typeof(DateTimeControl), "_DATETIMECONTROLID_", "<script language=\"javascript\">var g_strDateTimeControlIDs = new Array();</script>");
			base.OnPreRender(e);
		}

		/// <summary>
		/// Sends the content of the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter"></see> object, which writes the content that is rendered on the client.
		/// </summary>
		/// <param name="output">
		/// The HtmlTextWriter object that receives the server control content. 
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void Render(HtmlTextWriter output)
		{
			if (base.DesignMode && !base.ChildControlsCreated)
			{
				this.OnPreRender(EventArgs.Empty);
			}
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureDateOptions();
			string scriptLiteralToEncode = null;
			if (this.Parent.Parent is DateTimeField)
			{
				scriptLiteralToEncode = "SP" + ((DateTimeField)this.Parent.Parent).Field.InternalName;
			}
			else
			{
				scriptLiteralToEncode = this.ID;
			}
			// TODO: convert
			output.WriteLine("<script language='javascript'>g_strDateTimeControlIDs[\"" + SPHttpUtility.EcmaScriptStringLiteralEncode(scriptLiteralToEncode) + "\"] = \"" + SPHttpUtility.EcmaScriptStringLiteralEncode(this.m_dateTextBox.ClientID) + "\";</script>");
			string format = "iframe id={0} src=\"/_layouts/images/blank.gif\" style=\"display:none;position:absolute; width:200px; z-index:101;\" title=\"{1}\" frameborder=\"0\"";
			string str3 = "a href=\"#\" onclick='clickDatePicker(\"{0}\", {1}, \"{2}\"); return false;' ";
			if (this.TabIndex > 0)
			{
				object obj2 = str3;
				str3 = string.Concat(new object[] { obj2, " tabindex=\"", this.TabIndex, "\"" });
			}
			string str4 = "img id={0} src=\"{1}\" border=\"0\" alt=\"{2}\"";
			string text = this.m_dateTextBox.Text;
			output.WriteLine("<table border=0 cellpadding=0 cellspacing=0><tr>");
			if (this.m_dateTextBox.Visible)
			{
				output.WriteLine("<td class=\"ms-dtinput\" >");
				output.WriteLine(string.Format(CultureInfo.InvariantCulture, "<label for=\"{0}\" style=\"display:none\">{1}</label>", new object[] { this.m_dateTextBox.ClientID, SPHttpUtility.HtmlEncode(this.m_ToolTip + " " + this.m_dateTextBoxLabel) }));
				this.m_dateTextBox.RenderControl(output);
				output.WriteLine("</td>");
			}
			if (!this.m_timeOnly)
			{
				output.WriteLine("<td class=\"ms-dtinput\" >");
				output.WriteFullBeginTag(string.Format(str3, this.m_dateTextBox.ClientID, this.BuildQueryString(), SPHttpUtility.EcmaScriptStringLiteralEncode(text)));
				output.WriteFullBeginTag(string.Format(str4, this.m_dateTextBox.ClientID + this.m_datepickerImageID, SPHttpUtility.HtmlUrlAttributeEncode(this.getUrlString(this.m_calendarImageUrl, null, null)), SPHttpUtility.HtmlEncode(this.m_calendariconAltText)));
				output.WriteEndTag("A");
				output.WriteFullBeginTag(string.Format(format, this.m_dateTextBox.ClientID + this.m_datepickerFrameID, SPHttpUtility.HtmlEncode(this.m_calendariconAltText)));
				output.WriteEndTag("iframe");
				output.WriteLine("</td>");
			}
			if (!this.DateOnly)
			{
				output.WriteLine("<td class=\"ms-dttimeinput\" nowrap>");
				output.WriteLine(string.Format(CultureInfo.InvariantCulture, "<label for=\"{0}\" style=\"display:none\">{1}</label>", new object[] { this.m_hoursDropDown.ClientID, SPHttpUtility.HtmlEncode(this.m_ToolTip + " " + this.m_hoursDropDownLabel) }));
				this.m_hoursDropDown.Attributes["dir"] = this.m_PageDirection;
				this.m_hoursDropDown.RenderControl(output);
				output.WriteLine("&nbsp;");
				output.WriteLine(string.Format(CultureInfo.InvariantCulture, "<label for=\"{0}\" style=\"display:none\">{1}</label>", new object[] { this.m_minutesDropDown.ClientID, SPHttpUtility.HtmlEncode(this.m_ToolTip + " " + this.m_minutesDropDownLabel) }));
				this.m_minutesDropDown.Attributes["dir"] = this.m_PageDirection;
				this.m_minutesDropDown.RenderControl(output);
				output.WriteLine("</td>");
			}
			output.WriteLine("</tr></table>");
			if (!this.IsValid)
			{
				output.Write("<span id=\"" + this.ID + "ErrorMessage\" class=\"ms-formvalidation\">");
				if (this.m_errorMessage == null)
				{
					output.Write(string.Format(CultureInfo.InvariantCulture, SPHttpUtility.HtmlEncode(this.m_errorMessage_validatorRange), new object[] { "<nobr>" + SPHttpUtility.HtmlEncode(this.m_minDateString) + "</nobr>", "<nobr>" + SPHttpUtility.HtmlEncode(this.m_maxDateString) + "</nobr>" }));
				}
				else
				{
					SPHttpUtility.HtmlEncode(this.ErrorMessage, output);
				}
				output.Write("</span><br/>");
			}
			if (this.IsRequiredField && !this.TimeOnly)
			{
				this.m_validatorRequired.RenderControl(output);
			}
		}

		private string BuildQueryString()
		{
			StringWriter output = new StringWriter(CultureInfo.InvariantCulture);
			output.Write("\"");
			output.Write(HttpUtility.JavaScriptStringEncode(this.m_urlDatePickerFrame));
			output.Write("?&cal=");
			output.Write((int)this.Calendar);
			output.Write("&lcid=");
			output.Write(this.LocaleId.ToString(CultureInfo.InvariantCulture));
			output.Write("&langid=");
			output.Write(Thread.CurrentThread.CurrentUICulture.LCID);
			if (this.UseTimeZoneAdjustment)
			{
				output.Write("&tz=");
				output.Write(this.GetTimeSpanFromTimeZone().ToString());
			}
			output.Write("&ww=");
			output.Write(this.WorkWeek);
			output.Write("&fdow=");
			output.Write((this.FirstDayOfWeek >= 0) ? this.FirstDayOfWeek : this.m_dateOptions.FirstDayOfWeek);
			output.Write("&fwoy=");
			output.Write((int)this.FirstWeekOfYear);
			output.Write("&hj=");
			output.Write(this.HijriAdjustment);
			output.Write("&swn=");
			output.Write(this.ShowWeekNumber);
			output.Write("&minjday=");
			output.Write(this.m_minJDay.ToString(CultureInfo.InvariantCulture));
			output.Write("&maxjday=");
			output.Write(this.m_maxJDay.ToString(CultureInfo.InvariantCulture));
			output.Write("&date=\"");
			return output.ToString();
		}

		private void SetMinMaxDateTime()
		{
			if (this.MinDate == DateTime.MinValue)
			{
				this.MinDate = new DateTime(0x76c, 1, 1);
			}
			if (this.MaxDate == DateTime.MaxValue)
			{
				switch (this.Calendar)
				{
					case SPCalendarType.Gregorian:
					case SPCalendarType.GregorianMEFrench:
					case SPCalendarType.GregorianArabic:
					case SPCalendarType.GregorianXLITEnglish:
					case SPCalendarType.GregorianXLITFrench:
						this.MaxDate = new DateTime(0x22c4, 12, 0x1f);
						return;

					case ((SPCalendarType)2):
					case (SPCalendarType.GregorianXLITFrench | SPCalendarType.Gregorian):
					case SPCalendarType.KoreaJapanLunar:
					case SPCalendarType.ChineseLunar:
						return;

					case SPCalendarType.Japan:
						this.MaxDate = new DateTime(0x827, 12, 0x1f);
						return;

					case SPCalendarType.Taiwan:
						this.MinDate = new DateTime(0x778, 1, 1);
						this.MaxDate = new DateTime(0xf52, 12, 0x1f);
						return;

					case SPCalendarType.Korea:
					case SPCalendarType.Thai:
						this.MaxDate = new DateTime(0xf9f, 12, 0x1f);
						return;

					case SPCalendarType.Hijri:
						this.MaxDate = new DateTime(0xf9f, 12, 20);
						return;

					case SPCalendarType.Hebrew:
						this.MaxDate = new DateTime(0x8bf, 9, 0x1d);
						return;

					case SPCalendarType.SakaEra:
						this.MaxDate = new DateTime(0xf9f, 12, 0x15);
						return;

					default:
						return;
				}
			}
		}

		private void SetSelectedDate()
		{
			if (!this.m_bChangesByUserProccessed)
			{
				if (this.IsDateEmpty)
				{
					if (this.m_selectedDate == DateTime.MinValue)
					{
						this.m_bHasSelectedDate = false;
					}
					return;
				}
				if ((this.Page != null) && !this.Page.IsPostBack)
				{
					return;
				}
			}
			else
			{
				return;
			}
			this.EnsureChildControls();
			this.EnsureDateOptions();
			DateTime dateTimeFromTextBox = this.GetDateTimeFromTextBox();
			if (dateTimeFromTextBox != DateTime.MaxValue)
			{
				this.SelectedDate = this.GetSelectedDate(dateTimeFromTextBox);
				this.m_bDateValid = true;
			}
			else
			{
				this.InitErrorStrings();
				this.m_errorMessage = string.Format(CultureInfo.InvariantCulture, this.m_errorMessage_validatorRange, new object[] { this.m_minDateString, this.m_maxDateString });
				this.SelectedDate = DateTime.MaxValue;
				this.m_bDateValid = false;
			}
			this.m_bChangesByUserProccessed = true;
		}

		/// <summary>
		/// Loads the posted content of the control, if it is different from the last posting.
		/// </summary>
		/// <returns>
		/// true if the posted content is different from the last posting; otherwise, false.
		/// </returns>
		/// <param name="postDataKey">
		/// The key identifier for the control, used to index the postCollection.
		/// </param>
		/// <param name="postCollection">
		/// A <see cref="T:"></see><see cref="System.Collections.Specialized.NameValueCollection"></see> object that contains value information indexed by control identifiers.
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return false;
		}

		/// <summary>
		/// Signals the server control to notify  that the state of the control has changed.
		/// </summary>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.OnDateChanged(EventArgs.Empty);
		}

		/// <summary>
		/// Validates the control data.
		/// </summary>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		public void Validate()
		{
			this.SetSelectedDate();
			this.IsValid = this.m_bDateValid;
		}

		private bool ValidateDateTimeRange(DateTime dt)
		{
			this.InitErrorStrings();
			if ((dt >= this.MinDate) && ((dt.Year < this.MaxDate.Year) || ((dt.Year == this.MaxDate.Year) && ((dt.Month < this.MaxDate.Month) || ((dt.Month == this.MaxDate.Month) && (dt.Day <= this.MaxDate.Day))))))
			{
				return true;
			}
			this.m_errorMessage = string.Format(CultureInfo.InvariantCulture, this.m_errorMessage_validatorRange, new object[] { this.m_minDateString, this.m_maxDateString });
			return false;
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether the control posts back to the server each time a user interacts with the control.
		/// </summary>
		/// <returns>
		/// true if the control posts back to the server each time a user interacts with the control; otherwise, false. The default is false. 
		/// </returns>
		public bool AutoPostBack
		{
			get
			{
				return this.autoPostBack;
			}
			set
			{
				this.autoPostBack = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the calendar type for the control.
		/// </summary>
		/// <returns>
		/// An <see cref="T:Microsoft.SharePoint.WebControls.SPCalendarType"></see> object that indicates the calendar type used by the control. 
		/// </returns>
		[Bindable(true), Category("Data"), DefaultValue("1")]
		public SPCalendarType Calendar
		{
			get
			{
				return this.m_calendar;
			}
			set
			{
				this.m_calendar = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the URL of the calendar image used by the control.
		/// </summary>
		/// <returns>
		/// A string that represents the URL of the calendar image. 
		/// </returns>
		[DefaultValue("calendar.gif"), Bindable(false), Category("Links")]
		public string CalendarImageUrl
		{
			get
			{
				return this.getUrlString(this.m_calendarImageUrl, null, null);
			}
			set
			{
				this.m_calendarImageUrl = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the cascading style sheet (CSS) class used for the control's description.
		/// </summary>
		/// <returns>
		/// A string that represents the name of the CSS class. 
		/// </returns>
		[Category("Appearance"), DefaultValue("ms-formdescription"), Bindable(false)]
		public string CssClassDescription
		{
			get
			{
				return this.m_cssClassDescription;
			}
			set
			{
				this.m_cssClassDescription = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the cascading style sheet (CSS) class used for the control's input text box.
		/// </summary>
		/// <returns>
		/// A string that represents the name of the CSS class.
		/// </returns>
		[Bindable(false), Category("Appearance"), DefaultValue("ms-formbody")]
		public string CssClassTextBox
		{
			get
			{
				return this.m_cssClassTextBox;
			}
			set
			{
				this.m_cssClassTextBox = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether or not the control displays only date values and not time values.
		/// </summary>
		/// <returns>
		/// true if the control displays only date values; otherwise, false. The default is false.
		/// </returns>
		[DefaultValue("false"), Bindable(true), Category("Data")]
		public bool DateOnly
		{
			get
			{
				return this.m_dateOnly;
			}
			set
			{
				if (this.TimeOnly)
				{
					while (value)
					{
						return;
					}
				}
				this.m_dateOnly = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the URL of the DatePicker frame .aspx file.
		/// </summary>
		/// <returns>
		/// A string that represents the URL. 
		/// </returns>
		[Category("Links"), Bindable(false), DefaultValue("iframe.aspx")]
		public string DatePickerFrameUrl
		{
			get
			{
				return this.m_urlDatePickerFrame;
			}
			set
			{
				this.m_urlDatePickerFrame = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the URL of the control's ECMAScript (JScript, JavaScript) file.
		/// </summary>
		/// <returns>
		/// A string that represents the URL. 
		/// </returns>
		[Bindable(false), Category("Links"), DefaultValue("DatePicker.js")]
		public string DatePickerJavaScriptUrl
		{
			get
			{
				return this.m_urlDatePickerJavaScript;
			}
			set
			{
				this.m_urlDatePickerJavaScript = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether or not the control is enabled.
		/// </summary>
		/// <returns>
		/// true if the control is enabled; otherwise, false. The default is true.
		/// </returns>
		[DefaultValue("true"), Bindable(true), Category("Data")]
		public bool Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the error message text generated when control validation fails.
		/// </summary>
		/// <returns>
		/// A string that represents the error message text. 
		/// </returns>
		public string ErrorMessage
		{
			[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
			get
			{
				return this.m_errorMessage;
			}
			[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
			set
			{
				this.m_errorMessage = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates the first day of the week displayed in the calendar.
		/// </summary>
		/// <returns>
		/// An integer in the range 0 to 6 that indicates the first day of the week displayed in the DatePicker; where 0 indicates Sunday, 1 indicates Monday, and so on. The default is 0 (Sunday). 
		/// </returns>
		[Category("Data"), Bindable(true), DefaultValue("-1")]
		public int FirstDayOfWeek
		{
			get
			{
				return this.m_firstDayOfWeek;
			}
			set
			{
				this.m_firstDayOfWeek = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates the first week of the year displayed in the calendar.
		/// </summary>
		/// <returns>
		/// A short that indicates the first week of the year displayed in the DatePicker; either 0 (the week in which January 1 occurs), 1 (the first week that has at least four days in the new year), or 2 (the first full week of the year). The default is 0 (the week in which January 1 occurs). 
		/// </returns>
		[Category("Data"), DefaultValue("0"), Bindable(true)]
		public short FirstWeekOfYear
		{
			get
			{
				return this.m_firstWeekOfYear;
			}
			set
			{
				this.m_firstWeekOfYear = value;
			}
		}

		internal bool HasSelectedDate
		{
			get
			{
				return this.m_bHasSelectedDate;
			}
		}

		/// <summary>
		/// Sets or retrieves the number of days to add or subtract from the calendar to accommodate the variances in the start and the end of Ramadan and to accommodate the date difference between countries/regions.
		/// </summary>
		/// <returns>
		/// An integer that indicates the number of days to add or subtract from the calendar. The default is 0. 
		/// </returns>
		[Category("Data"), DefaultValue("0"), Bindable(true)]
		public int HijriAdjustment
		{
			get
			{
				return this.m_hijriAdjustment;
			}
			set
			{
				this.m_hijriAdjustment = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether or not the time is displayed using a 24-hour clock.
		/// </summary>
		/// <returns>
		/// true if the time is displayed using a 24-hour clock; otherwise, false. The default is false.
		/// </returns>
		[DefaultValue("false"), Bindable(true), Category("Data")]
		public bool HoursMode24
		{
			get
			{
				return this.m_hoursMode24;
			}
			set
			{
				this.m_hoursMode24 = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether or not a date value is displayed in the control.
		/// </summary>
		/// <returns>
		/// true if a date value is displayed; otherwise, false. The default is true.
		/// </returns>
		[Bindable(true), Category("Data"), DefaultValue("true")]
		public bool IsDateEmpty
		{
			get
			{
				this.EnsureChildControls();
				if (this.m_dateTextBox != null)
				{
					while (this.m_dateTextBox.Text != null)
					{
						return (this.m_dateTextBox.Text.Length == 0);
					}
				}
				return true;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether or not the control's DateTime field is a required field.
		/// </summary>
		/// <returns>
		/// true if the field is a required field; otherwise, false. The default is false.
		/// </returns>
		[Bindable(true), Category("Data"), DefaultValue("false")]
		public bool IsRequiredField
		{
			get
			{
				return this.m_requiredField;
			}
			set
			{
				this.m_requiredField = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether or not the user-entered content in the control passes validation.
		/// </summary>
		/// <returns>
		/// true if the content passes validation; otherwise, false. The default is true.
		/// </returns>
		public bool IsValid
		{
			[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
			get
			{
				return this.m_fIsValid;
			}
			[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
			set
			{
				this.m_fIsValid = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the control's locale identifier.
		/// </summary>
		/// <returns>
		/// An integer that indicates the control's locale identifier. The default is 1033, which is the locale identifier for English. 
		/// </returns>
		[Category("Data"), Bindable(true)]
		public int LocaleId
		{
			get
			{
				if (this.m_localeId != -1)
				{
					return this.m_localeId;
				}
				using (DateTimeControl dtcontrol = new DateTimeControl())
				{
					return dtcontrol.LocaleId;
				}
			}
			set
			{
				this.m_localeId = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the maximum date and time that can be selected in the control.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.DateTime"></see> value that indicates the maximum date and time that can be selected. The default is 12/31/9999 11:59:59 PM. 
		/// </returns>
		[DefaultValue(""), Bindable(false), Category("Data")]
		public DateTime MaxDate
		{
			get
			{
				return this.m_maxDate;
			}
			set
			{
				this.m_maxDate = value;
				try
				{
					SimpleDate di = new SimpleDate(this.m_maxDate.Year, this.m_maxDate.Month, this.m_maxDate.Day);
					this.m_maxJDay = SPIntlCal.LocalToJulianDay(SPCalendarType.Gregorian, ref di);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		/// <summary>
		/// Sets or retrieves the minimum date and time that can be selected in the control.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.DateTime"></see> value that indicates the minimum date and time that can be selected. The default is 1/1/0001 12:00:00 AM. 
		/// </returns>
		[Category("Data"), Bindable(false), DefaultValue("")]
		public DateTime MinDate
		{
			get
			{
				return this.m_minDate;
			}
			set
			{
				this.m_minDate = value;
				try
				{
					SimpleDate di = new SimpleDate(this.m_minDate.Year, this.m_minDate.Month, this.m_minDate.Day);
					this.m_minJDay = SPIntlCal.LocalToJulianDay(SPCalendarType.Gregorian, ref di);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		/// <summary>
		/// Sets or retrieves the text of an OnValueChange script.
		/// </summary>
		/// <returns>
		/// A string that represents the script text. 
		/// </returns>
		public string OnValueChangeClientScript
		{
			get
			{
				return this.onValueChangeClientScript;
			}
			set
			{
				this.onValueChangeClientScript = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the currently selected date in the control.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.DateTime"></see> value that indicates the currently selected date. 
		/// </returns>
		[Category("Data"), DefaultValue(""), Bindable(false)]
		public DateTime SelectedDate
		{
			get
			{
				this.SetSelectedDate();
				if (this.m_bHasSelectedDate)
				{
					return this.m_selectedDate;
				}
				while (this.m_bUseTimeZoneAdjustment)
				{
					return DateTime.UtcNow;
				}
				return DateTime.Now;
			}
			set
			{
				this.m_bHasSelectedDate = true;
				this.m_selectedDate = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether to display the week number in the calendar.
		/// </summary>
		/// <returns>
		/// true if the displays the week number; otherwise, false. The default is false.
		/// </returns>
		[DefaultValue("true"), Bindable(true), Category("Data")]
		public bool ShowWeekNumber
		{
			get
			{
				return this.m_bShowWeekNumber;
			}
			set
			{
				this.m_bShowWeekNumber = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the tab index of the control.
		/// </summary>
		/// <returns>
		/// A short that indicates the tab index. The default is -1.
		/// </returns>
		[DefaultValue("-1"), Category("Data"), Bindable(true)]
		public short TabIndex
		{
			get
			{
				return this.m_tabIndex;
			}
			set
			{
				this.m_tabIndex = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether the control displays only time values and not date values.
		/// </summary>
		/// <returns>
		/// true if the control displays only time values; otherwise, false. The default is false.
		/// </returns>
		[Category("Data"), DefaultValue("false"), Bindable(true)]
		public bool TimeOnly
		{
			get
			{
				return this.m_timeOnly;
			}
			set
			{
				if (this.DateOnly)
				{
					while (value)
					{
						return;
					}
				}
				this.m_timeOnly = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the identifier for the time zone used to display time values.
		/// </summary>
		/// <returns>
		/// An integer that indicates the control's time zone identifier. The default is -1. 
		/// </returns>
		[Bindable(true), DefaultValue("-1"), Category("Data")]
		public int TimeZoneID
		{
			get
			{
				return this.m_timeZoneID;
			}
			set
			{
				this.DropTimeZone();
				this.m_timeZoneID = value;
			}
		}

		/// <summary>
		/// Sets or retrieves the control's tooltip text.
		/// </summary>
		/// <returns>
		/// A string that represents the tooltip text. 
		/// </returns>
		[DefaultValue(""), Bindable(true), Category("Data")]
		public string ToolTip
		{
			get
			{
				return this.m_ToolTip;
			}
			set
			{
				this.m_ToolTip = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a value that indicates whether the time values in the control are automatically adjusted for daylight savings time.
		/// </summary>
		/// <returns>
		/// true if time values are automatically adjusted for daylight savings time; otherwise, false. The default is false.
		/// </returns>
		[Category("Data"), Bindable(true), DefaultValue("false")]
		public bool UseTimeZoneAdjustment
		{
			get
			{
				return this.m_bUseTimeZoneAdjustment;
			}
			set
			{
				this.m_bUseTimeZoneAdjustment = value;
			}
		}

		/// <summary>
		/// Sets or retrieves a seven-character string that indicates the work days in a week.
		/// </summary>
		/// <returns>
		/// A seven-character string that indicates the work days of the week, where 1 is a work day and 0 is not a work day. The default is "0111110," which represents a Monday-Friday work week for a week whose first day is Sunday. 
		/// </returns>
		[Category("Data"), DefaultValue("0111110"), Bindable(true)]
		public string WorkWeek
		{
			get
			{
				return this.m_workWeek;
			}
			set
			{
				this.m_workWeek = value;
			}
		}
	}
}
