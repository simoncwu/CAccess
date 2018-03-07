<%@ Control Language="C#" CodeBehind="CPCalendarView.ascx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Controls.CPCalendarView" %>
<div class="ms-calheader">
    <input id="ExpandedWeeksId" name="ExpandedWeeks" type="hidden" value="" />
    <div class="ms-cal-navheader">
        <asp:HyperLink ID="PreviousDateLink" runat="server" AccessKey="<%$Resources:wss,calendar_prev_AK%>"><img alt="Previous Month" src="/images/prevbuttonltr.gif" width="15" height="15" /></asp:HyperLink>
        <asp:Label ID="HeaderDate" runat="server" CssClass="CalendarGridHeader"></asp:Label>
        <asp:HyperLink ID="NextDateLink" runat="server" AccessKey="<%$Resources:wss,calendar_next_AK%>"><img alt="Next Month" src="/images/nextbuttonltr.gif" width="15" height="15" /></asp:HyperLink>
    </div>
    <div class="calendarDatePicker">
        <table class="calendarDatePicker">
            <tr>
                <td>
                    Go to a specific date:
                </td>
                <td class="DateTimeControl">
                    <cfb:CPDateTimeControl ID="DateTimeControl" runat="server" DateOnly="true" ToolTip="Enter a target date.">
                    </cfb:CPDateTimeControl>
                </td>
                <td class="GoToDateButton">
                    <asp:Button runat="server" OnClick="ChangeDate" Text="Go" />
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="cp-CalendarView">
    <cfb:SPCalendarView ID="CalendarView" runat="server" />
</div>
