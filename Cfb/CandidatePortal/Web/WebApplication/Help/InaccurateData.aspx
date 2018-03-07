<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Help.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.InaccurateData"
    CodeBehind="InaccurateData.aspx.cs" %>

<%@ MasterType VirtualPath="Help.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.help_baddata_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.help_baddata_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:Panel ID="_contactForm" runat="server" CssClass="contactForm">
        <asp:ValidationSummary ID="_validationSummary" runat="server" CssClass="validators"
            DisplayMode="SingleParagraph" ForeColor="" ShowMessageBox="false" ShowSummary="true" />
        <p>
            If you believe any data posted on
            <%=CPProviders.SettingsProvider.ApplicationName%>
            is inaccurate or incomplete, please contact the Candidate Services Unit at
            <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>.</p>
        <div class="cp-boundingBox notice">
            <h2>
                Data Accuracy Disclaimer</h2>
            <cfb:WebTransferDisclaimer runat="server" />
        </div>
        <p>
            Please describe the nature of the data inaccuracy in as much detail as possible.
            <asp:RequiredFieldValidator ID="_messageValidator" runat="server" ControlToValidate="Message"
                CssClass="validators" Display="Dynamic" Text="*" ErrorMessage="Please state the nature of the data inaccuracy."
                ForeColor=""></asp:RequiredFieldValidator></p>
        <asp:TextBox ID="Message" runat="server" TextMode="MultiLine"></asp:TextBox>
        <div class="buttons">
            <asp:Button runat="server" CssClass="button" Text="Submit Inquiry" ToolTip="Submit Inquiry"
                OnClick="OnSubmit" />
        </div>
        <p>
            You may also contact the Candidate Services Unit for immediate assistance via telephone
            at
            <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>, or write to us at the following
            mailing address:</p>
        <blockquote>
            <address>
                <strong>NYC Campaign Finance Board</strong><br />
                100 Church Street, 12th Floor<br />
                New York, NY 10007</address>
        </blockquote>
    </asp:Panel>
    <asp:Panel ID="_confirmation" runat="server" Visible="false">
        <p>
            The Campaign Finance Board has received your message and will begin processing it
            shortly. You may also contact the Candidate Services Unit for immediate assistance
            via telephone at
            <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>.</p>
        <h3>
            Notification Disclaimer</h3>
        <p>
            Communications made through this electronic mail system shall in no way be deemed
            to constitute:</p>
        <ol>
            <li>compliance by any participating candidate with statement filings, or any other Program
                requirement; or</li>
            <li>a communication that is sufficient for purposes of responding to a notice or inquiry
                from the Board, or for providing information to the Board; or</li>
            <li>legal notice to the New York City Campaign Finance Board, or any of its officers,
                employees, agents, or representatives.</li>
        </ol>
        <div class="buttons">
            <a href="/Default.aspx" class="button" title="Return to Home Page">
                <%=CPProviders.SettingsProvider.ApplicationName%>
                Home</a>
        </div>
    </asp:Panel>
</asp:Content>
