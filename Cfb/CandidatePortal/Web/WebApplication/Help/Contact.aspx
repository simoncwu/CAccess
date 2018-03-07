<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Help.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.Contact"
    CodeBehind="Contact.aspx.cs" %>

<%@ MasterType VirtualPath="Help.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.contact_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.contact_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:Panel ID="_contactForm" runat="server" CssClass="contactForm">
        <asp:ValidationSummary ID="_validationSummary" runat="server" CssClass="validators"
            DisplayMode="BulletList" ForeColor="" HeaderText="The following errors were found:"
            ShowMessageBox="true" ShowSummary="false" />
        <div>
            <p>
                Please select the topic that best describes your inquiry:
                <asp:DropDownList ID="Category" runat="server">
                    <asp:ListItem Value="">(please select a topic)</asp:ListItem>
                    <asp:ListItem Value="csu">Candidate Services</asp:ListItem>
                    <asp:ListItem Value="audit">Audit and Compliance</asp:ListItem>
                    <asp:ListItem Value="comments">General Comments</asp:ListItem>
                </asp:DropDownList>
            </p>
            <asp:RequiredFieldValidator ID="_categoryValidator" runat="server" ControlToValidate="Category"
                CssClass="validators" Display="Dynamic" Text="*" ErrorMessage="Please select a topic."
                InitialValue=""></asp:RequiredFieldValidator>
        </div>
        <div>
            <p>
                Please state the nature of your inquiry in as much detail as possible:
                <asp:RequiredFieldValidator ID="_messageValidator" runat="server" ControlToValidate="Message"
                    CssClass="validators" Display="Dynamic" Text="*" ErrorMessage="Please state the nature of your inquiry."
                    ForeColor=""></asp:RequiredFieldValidator></p>
            <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" />
        </div>
        <div class="buttons">
            <asp:Button runat="server" OnClick="OnSubmit" Text="Submit Inquiry" ToolTip="Submit Inquiry" />
        </div>
        <p>
            You may also contact us for immediate assistance via telephone at
            <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>, or write to us at the following
            mailing address:</p>
        <blockquote>
            <%=CPProviders.SettingsProvider.AgencyName%><br />
            100 Church Street, 12th Floor<br />
            New York, NY 10007
        </blockquote>
    </asp:Panel>
    <asp:Panel ID="_confirmation" runat="server" Visible="false">
        <p>
            The Campaign Finance Board has received your inquiry and will begin processing your
            request shortly. You may also contact us for immediate assistance via telephone
            at
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
