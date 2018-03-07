<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="PostElection.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.PostElection" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.postelection_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="server">
    <%=Resources.CPResources.postelection_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <p>
        The Draft Audit Report (DAR) is the result of CFB&rsquo;s preliminary review of
        your campaign&rsquo;s reporting and recordkeeping to assure it complies with the
        Campaign Finance Act and Board Rules. It is posted to your C-Access account and
        mailed to both the candidate&rsquo;s and treasurer&rsquo;s home addresses. Once
        your campaign responds, the CFB will review your submissions again.</p>
    <p>
        The CFB must complete your Final Audit Report (FAR) or issue a notice regarding
        alleged violation and/or a potential repayment of public funds to complete the post-election
        audit process within 14&ndash;18 months (depending on the office sought) from your
        last disclosure statement. This deadline will be extended (tolled), and the new
        deadline posted below, by the number of days of:</p>
    <ul>
        <li>any extensions requested by your campaign,</li>
        <li>any extensions requested by the CFB that you approve,</li>
        <li>responses submitted late by your campaign, and</li>
        <li>responses submitted by your campaign and deemed inadequate.</li>
    </ul>
    <p>
        In addition, attending audit trainings will move up the deadline for issuance of
        the campaign&rsquo;s Final Audit Report by two months.</p>
    <p>
        If the audit raises serious issues of non-compliance, the deadline may be suspended
        while the issues are investigated.</p>
    <p>
        If you have any questions, contact the Audit Unit at
        <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
        or via the <a href="/Help/Contact.aspx" title="<%=Resources.CPResources.contact_title %>">
            <%=Resources.CPResources.contact_title%></a> page.</p>
    <section class="cp-PostElection">
        <cfb:PostElectionAuditNavigation ID="_postElectionAuditNavigation" runat="server"
            IdrOnImageUrl="~/images/nav/pe_idr_on.png" IdrOffImageUrl="~/images/nav/pe_idr_off.png"
            DarOnImageUrl="~/images/nav/pe_dar_on.png" DarOffImageUrl="~/images/nav/pe_dar_off.png"
            DarDisabledImageUrl="~/images/nav/pe_dar_disabled.png" FarOnImageUrl="~/images/nav/pe_far_on.png"
            FarOffImageUrl="~/images/nav/pe_far_off.png" FarDisabledImageUrl="~/images/nav/pe_far_disabled.png"
            Title="Post Election Audit Progress" />
        <section class="cp-PostElectionSummary">
            <h3 class="caption">
                Campaign Response Deadlines
                <img src="/images/tooltip.png" width="16" height="16" alt="" class="tooltip" title="Displays the CFB&rsquo;s most recent notice addressed to your campaign, the date your response is due, and date the response was received." />
            </h3>
            <div class="columns ms-listviewtable detailsview cp-PostElectionAuditReportDates">
                <cfb:PostElectionAuditSummary ID="_reportSummary" runat="server" CssClass="column" />
                <cfb:PostElectionAuditSummary ID="_inadequateSummary" runat="server" CssClass="column two-up"
                    Visible="false" />
            </div>
            <div class="currentStatus">
                Current Status:
                <asp:Label ID="_currentStatus" runat="server">Please contact the CFB</asp:Label>
            </div>
        </section>
        <section class="cp-PostElectionIssueDates">
            <h3 class="caption">
                CFB Issuance Deadlines
                <img src="/images/tooltip.png" width="16" height="16" alt="" class="tooltip" title="Displays the CFB&rsquo;s deadlines for completing your campaign&rsquo;s Draft and Final Audit Reports." />
            </h3>
            <div class="cp-boundingBox">
                <table border="0" class="ms-listviewtable">
                    <tr class="ms-viewheadertr caption">
                        <th colspan="2">
                            Draft Audit Report
                        </th>
                    </tr>
                    <tr>
                        <td>
                            Original Deadline
                        </td>
                        <td>
                            <asp:Label ID="_darOriginalIssueDateLabel" runat="server" Text="(n/a)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            DAR Tolling Days Incurred
                        </td>
                        <td>
                            <asp:Label ID="_darTollingDaysLabel" runat="server" Text="(n/a)"></asp:Label>
                        </td>
                    </tr>
                    <tr class="tollingDeadline">
                        <td>
                            DAR Target Deadline
                        </td>
                        <td>
                            <asp:Label ID="_darCurrentIssueDateLabel" runat="server" Text="(n/a)"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table border="0" class="ms-listviewtable">
                    <tr class="ms-viewheadertr caption">
                        <th colspan="2">
                            End of Audit
                            <img src="/images/tooltip_clear.png" width="16" height="16" alt="" class="tooltip"
                                title="The audit process ends when the CFB issues your Final Audit Report or notifies your campaign of potential penalties and/or public funds repayment obligations." />
                        </th>
                    </tr>
                    <tr>
                        <td>
                            Original Deadline
                        </td>
                        <td>
                            <asp:Label ID="_farOriginalIssueDateLabel" runat="server" Text="(n/a)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            FAR Tolling Days Incurred
                        </td>
                        <td>
                            <asp:Label ID="_farTollingDaysLabel" runat="server" Text="(n/a)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total Tolling Days
                        </td>
                        <td>
                            <asp:Label ID="_totalTollingDaysLabel" runat="server" Text="(n/a)"></asp:Label>
                        </td>
                    </tr>
                    <tr class="tollingDeadline">
                        <td>
                            FAR Target Deadline<span id="TrainingComplianceReference" runat="server">*</span>
                        </td>
                        <td>
                            <asp:Label ID="_farCurrentIssueDateLabel" runat="server" Text="(n/a)"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="TrainingCompliance" runat="server" class="ms-listviewtable">
                    * Audit expedited
                </div>
            </div>
        </section>
        <cfb:TollingEventsList ID="_tollingEventsList" runat="server" OnUpdateDataSources="_tollingEventsList_UpdateDataSources" />
    </section>
</asp:Content>
