<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/CandidatePortal.master"
    CodeBehind="Default.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Calendar.Default"
    EnableEventValidation="false" %>

<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.calendar_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInBody">
    <%=Resources.CPResources.calendar_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
    <link href="/styles/calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $(".cal-datepicker input:text").datepicker({
                buttonText: "Select a date",
                onSelect: function () { $(".cal-datepicker input.cal-datenav").click(); }
            });
        });
    </script>
    <script type="text/javascript" src="/_layouts/1033/init.js"></script>
    <script type="text/javascript" src="/_layouts/1033/core.js"></script>
    <script type="text/javascript" src="/_layouts/datepicker.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <asp:WebPartManager runat="server" />
    <aside class="calendarSideBar">
        <cfb:AnnouncementsList ID="_announcementsList" runat="server" />
        <cfb:RemindersControl runat="server" View="Brief" />
        <div class="otherCalendars">
            <h3 class="caption">
                Other Calendars</h3>
            <div class="cp-boundingBox cp-table">
                <ul>
                    <li><a href="http://www.nyccfb.info/candidates/candidates/EC<%=CPProfile.ElectionCycle%>_Trainings.pdf"
                        target="_blank" title="Candidate Training Calendar" class="popup">CFB Training Calendar</a></li>
                    <li><a href="http://www.nyccfb.info/candidates/candidates/disclosure_deadlines/<%=CPProfile.ElectionCycle%>.htm"
                        target="_blank" title="Disclosure Statement Deadlines for the <%=CPProfile.ElectionCycle%> Election"
                        class="popup">EC<%=CPProfile.ElectionCycle%>
                        Filing Deadlines</a></li>
                    <li><a href="http://www.nyccfb.info/press/info/board_meetings.htm" target="_blank"
                        title="Board Meetings Schedule" class="popup">Board Meetings Schedule</a></li>
                    <li><a href="http://www.vote.nyc.ny.us/html/dates/dates.shtml" target="_blank" title="Board of Elections Calendar"
                        class="popup">Board of Elections Calendar</a></li>
                </ul>
            </div>
        </div>
    </aside>
    <article class="calendarGrid">
        <div class="ms-calheader">
            <input id="ExpandedWeeksId" name="ExpandedWeeks" type="hidden" value="" />
            <div class="ms-cal-navheader">
                <ul class="icons cal-monthnav">
                    <li class="ui-state-default ui-corner-all" title="Previous Month">
                        <asp:HyperLink ID="PreviousDateLink" runat="server"><span class="ui-icon ui-icon-circle-arrow-w"></span></asp:HyperLink></li>
                </ul>
                <asp:Label ID="HeaderDate" runat="server" CssClass="cal-monthheader"></asp:Label>
                <ul class="icons cal-monthnav">
                    <li class="ui-state-default ui-corner-all" title="Next Month">
                        <asp:HyperLink ID="NextDateLink" runat="server"><span class="ui-icon ui-icon-circle-arrow-e"></span></asp:HyperLink></li>
                </ul>
                <div class="cal-datepicker">
                    Go to a specific date:<asp:TextBox ID="NavDate" runat="server" ToolTip="Enter a target date"
                        MaxLength="10"></asp:TextBox>
                    <cfb:CPDateTimeControl ID="DateTimeControl" runat="server" DateOnly="true" ToolTip="Enter a target date."
                        Visible="false">
                    </cfb:CPDateTimeControl>
                    <asp:Button runat="server" OnClick="NavigateToDate" CssClass="cal-datenav" Text="Go" />
                </div>
            </div>
        </div>
        <div class="cp-CalendarView">
            <cfb:SPCalendarView ID="CalendarView" runat="server" />
        </div>
    </article>
</asp:Content>
