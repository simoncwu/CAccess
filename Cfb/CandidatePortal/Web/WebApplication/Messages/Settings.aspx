<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/CandidatePortal.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Messages.Settings" CodeBehind="Settings.aspx.cs" %>

<script runat="server">
    string _appName = CPProviders.SettingsProvider.ApplicationName;
    string _agencyName = CPProviders.SettingsProvider.AgencyName;
</script>
<%@ MasterType VirtualPath="~/CandidatePortal.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
    <script type="text/javascript">
        //<![CDATA[
        function confirmResume() {
            return confirm("You are choosing to receive paper mailings for notices, letters, reports and other communications from the <%=_agencyName%> via the U.S. Postal Service.\n\nIf this is correct, click OK.") ? true : false;
        }
        //]]>";
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.cmo_paperless_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderCmoCounter">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInBody">
    <%=Resources.CPResources.cmo_paperless_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <asp:Panel ID="_changePanel" runat="server">
        <asp:Panel ID="_stopPaperPanel" runat="server">
            <p>
                <%=_appName%>
                <%=Resources.CPResources.cmo_title%>
                is the fastest, easiest and most reliable way for candidates, treasurers and other
                campaign representatives to receive communications electronically from the
                <%=_agencyName%>. With
                <%=Resources.CPResources.cmo_title%>:</p>
            <ul>
                <li>Your messages are available to you 24/7 from any computer with an internet connection.</li>
                <li>You can access your messages from the
                    <%=_agencyName%>
                    immediately upon posting without waiting for U.S. Postal Service delivery.</li>
                <li>Your messages are secure. Only individuals authorized to view your
                    <%=_appName%>
                    account will be able to see messages posted to your campaign.</li>
                <li>Statement Reviews, Draft Audit Reports, and various other messages will be available
                    in PDF format (requires free and easy-to-use Adobe Acrobat Reader, or equivalent).</li>
            </ul>
            <h2>
                Paper Mailings</h2>
            <p>
                With
                <%=Resources.CPResources.cmo_title%>, your campaign can choose to stop receiving
                documents from the
                <%=_agencyName%>
                that were traditionally sent as hard paper copies via U.S. Postal Service. By going
                green, your campaign will help save trees and energy resources. Less paper will
                also mean less office clutter and more space.</p>
            <p>
                <sup class="footnote">*</sup>The
                <%=_agencyName%>
                will continue to mail hard copies of certain documents via the U.S. Postal Service
                regardless of your paper mailings preference. These include the Notice of Alleged
                Violation, Draft Audit Report, Final Audit Report, Repayment Letter and Final Board
                Determination.</p>
            <asp:Panel ID="_forceSelectionPanel" runat="server" Visible="false">
                <p>
                    <strong>You must make a selection before using the
                        <%=_appName%>
                        web site. Remember, you can change your settings at any point.</strong></p>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="_confirmStopPaperanel" runat="server" Visible="false">
            <h2>
                Stop Paper Mailings Authorization</h2>
            <p>
                To stop receiving paper mailings from the
                <%=_agencyName%>, you must be able to view and save or print PDF files. This requires
                Adobe Acrobat Reader (or equivalent) to be installed on your computer.</p>
            <p>
                To confirm that your computer meets these requirements and complete your request,
                check both boxes below and click &ldquo;Stop Paper&rdquo;.</p>
            <div class="notice cp-boundingBox">
                <asp:ValidationSummary ID="_validationSummary" runat="server" CssClass="validators"
                    ForeColor="" ShowMessageBox="false" ShowSummary="true" DisplayMode="SingleParagraph"
                    HeaderText="To stop receiving paper mailings, you must select and agree to both items listed." />
                <p>
                    <label for="<%=_confirmPDF.ClientID%>">
                        <asp:CheckBox ID="_confirmPDF" runat="server" />
                        I confirm that my computer is capable of viewing, printing, and saving PDF files
                        via Adobe Acrobat Reader (or equivalent).<cfb:RequiredCheckBoxValidator runat="server"
                            ControlToValidate="_confirmPDF" Display="Dynamic" ForeColor="" CssClass="validators"></cfb:RequiredCheckBoxValidator></label></p>
                <p>
                    <label for="<%=_confirmPaperless.ClientID%>">
                        <asp:CheckBox ID="_confirmPaperless" runat="server" />
                        On behalf of the committee, candidate and treasurer, I authorize the
                        <%=_agencyName%>
                        to stop sending paper mailings of letters, reports and other communications to my
                        campaign. I understand that items posted to
                        <%=_appName%>
                        <%=Resources.CPResources.cmo_title%>
                        are considered delivered to the candidate, treasurer and committee when posted.
                        I also understand that these items might not be delivered by any other means.<cfb:RequiredCheckBoxValidator
                            runat="server" ControlToValidate="_confirmPaperless" Display="Dynamic" ForeColor=""
                            CssClass="validators"></cfb:RequiredCheckBoxValidator></label></p>
            </div>
            <h2>
                Obtaining a Paper Hard Copy</h2>
            <p>
                If at any point you would like to receive a hard paper copy of any particular message,
                you can do so by contacting the Candidate Services Unit at
                <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
                or via the
                <%=_appName%>
                contact form.</p>
            <h2>
                Resuming Paper Mailings</h2>
            <p>
                To resume receiving paper mailings from the
                <%=_agencyName%>
                at any point, you can return here and update your settings.</p>
        </asp:Panel>
        <asp:Panel ID="_resumePaperPanel" runat="server" Visible="false">
            <h2>
                Resume Paper Mailings</h2>
            <p>
                Your campaign does not currently receive paper mailings from the
                <%=_agencyName%>. Do you wish to resume receiving paper mailings?</p>
            <p>
                Resuming paper mailings will result in all future notices, letters, reports and
                other communications from the
                <%=_agencyName%>
                being mailed as hard paper copies via the U.S. Postal Service. It will not affect
                <%=Resources.CPResources.cmo_title%>, so you will continue to receive messages electronically
                via
                <%=_appName%>
                in addition to paper copies.</p>
            <p>
                Do you wish to receive paper mailings?</p>
        </asp:Panel>
        <div class="buttons">
            <asp:LinkButton ID="_stopButton" runat="server" CssClass="button" OnCommand="SubmitChange"
                Text="Stop Paper Mailings" Visible="false" />
            <asp:LinkButton ID="_resumeButton" runat="server" CssClass="button" OnCommand="SubmitChange"
                OnClientClick="return confirmResume()" Text="Resume Paper Mailings" Visible="false" />
        </div>
    </asp:Panel>
    <asp:Panel ID="_paperStoppedPanel" runat="server" Visible="false">
        <h2>
            You have stopped paper mailings for your campaign.</h2>
        <p>
            Thank you for choosing to stop receiving paper mailings from the
            <%=_agencyName%>. Please allow several days for your request to take effect.</p>
        <p>
            Please also note that the
            <%=_agencyName%>
            will always mail certain documents via the U.S. Postal Service. These include the
            Notice of Alleged Violation, Final Audit Report, Repayment Letter and Final Board
            Determination. Even with
            <%=Resources.CPResources.cmo_title%>, you will still receive these documents in
            hard copy via postal mail.</p>
        <p>
            If at any point you would like to receive a hard paper copy of any particular message,
            you can do so by contacting the Candidate Services Unit at
            <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
            or via the
            <%=_appName%>
            contact form.</p>
    </asp:Panel>
    <asp:Panel ID="_deferPanel" runat="server" Visible="false">
        <h2>
            Continue Paper Mailings</h2>
        <p>
            You have chosen not to stop paper mailings from the
            <%=_agencyName%>
            at this time. You will continue to receive paper mailings from the
            <%=_agencyName%>
            via U.S. Postal Service as usual.</p>
        <p>
            If you decide at any point to stop receiving paper mailings from the
            <%=_agencyName%>, you can do so by revisiting this page from the Help section.</p>
    </asp:Panel>
    <asp:Panel ID="_paperResumedPanel" runat="server" Visible="false">
        <h2>
            Resume Paper Mailings&mdash;Confirmation</h2>
        <p>
            You have resumed paper mailings to your campaign from the
            <%=_agencyName%>. You will also continue to receive messages from the
            <%=_agencyName%>
            via
            <%=_appName%>
            in addition to paper mailings.</p>
        <p>
            If you decide at any point to stop receiving paper mailings from the
            <%=_agencyName%>, you can do so by revisiting this page from the Help section.</p>
    </asp:Panel>
    <asp:HyperLink ID="_returnLink" runat="server" CssClass="button" NavigateUrl="~/Default.aspx"
        Text="Return to Home Page" ToolTip="Return to Home Page" Visible="false">
    </asp:HyperLink>
</asp:Content>
