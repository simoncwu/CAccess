<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Help.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.ChangePassword"
    CodeBehind="ChangePassword.aspx.cs" %>

<%@ MasterType VirtualPath="Help.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.help_changepw_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInBody">
    <%=Resources.CPResources.help_changepw_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <div class="cp-boundingBox notice">
        <h2>
            Security Reminder</h2>
        <p>
            Always keep your username and password credentials in a safe and secure place, as
            forgotten passwords cannot be retrieved, and will have to be reset instead. Please
            also note that these credentials are not to be disclosed to any parties.</p>
    </div>
    <asp:Panel ID="_successPanel" runat="server" Visible="false">
        <p>
            Your password was changed successfully.</p>
        <asp:HyperLink ID="ReturnToHomeLink" runat="server" CssClass="button" NavigateUrl="~/Default.aspx"
            Text="Return to Home Page" ToolTip="Return to Home Page"></asp:HyperLink>
    </asp:Panel>
    <asp:Panel ID="_changePasswordPanel" runat="server" CssClass="cp-ChangePassword">
        <asp:Panel ID="_forceChangeMessage" runat="server" Visible="false">
            <p>
                You must specify a new password before using the
                <%=CPProviders.SettingsProvider.ApplicationName%>
                web site.</p>
        </asp:Panel>
        <p>
            Your password must be at least 8 characters long, is case-sensitive, and may include
            any combination of letters, numbers, and special characters. You may not use your
            username as your password.</p>
        <asp:ValidationSummary ID="_validationSummary" runat="server" CssClass="validators"
            DisplayMode="BulletList" ForeColor="" HeaderText="The following errors were found:"
            ShowMessageBox="false" ShowSummary="true" EnableClientScript="false" />
        <asp:Panel ID="_wrongPasswordMessage" runat="server" CssClass="validators" Visible="false">
            <p>
                The current password provided was incorrect. Please re-enter and try again.</p>
        </asp:Panel>
        <asp:Panel ID="_usernameAsPasswordMessage" runat="server" CssClass="validators" Visible="false">
            <p>
                You may not use your username as your password.</p>
        </asp:Panel>
        <div>
            <label>
                <asp:Label ID="_currentLabel" runat="server" CssClass="label">Current Password:</asp:Label>
                <asp:TextBox ID="_currentPassword" runat="server" AutoCompleteType="None" CausesValidation="true"
                    TextMode="Password">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="_currentRequiredValidator" runat="server" ControlToValidate="_currentPassword"
                    CssClass="validators" Display="Dynamic" ErrorMessage="Current Password is required."
                    ForeColor="" SetFocusOnError="true" Text="*" EnableClientScript="false"></asp:RequiredFieldValidator>
            </label>
        </div>
        <div>
            <label>
                <span class="label">Enter New Password:</span>
                <asp:TextBox ID="_newPassword" runat="server" AutoCompleteType="None" CausesValidation="true"
                    TextMode="Password">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="_newRequiredValidator" runat="server" ControlToValidate="_newPassword"
                    CssClass="validators" Display="Dynamic" ErrorMessage="New Password is required."
                    ForeColor="" SetFocusOnError="true" Text="*" EnableClientScript="false"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="_minNewLengthValidator" runat="server" ControlToValidate="_newPassword"
                    CssClass="validators" Display="Dynamic" ErrorMessage="The minimum acceptable length for New Password is 8 characters."
                    ForeColor="" SetFocusOnError="true" ValidationExpression=".{8,}" Text="*" EnableClientScript="false"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="_newPassword" Display="Dynamic"
                    SetFocusOnError="true" ErrorMessage="New Password cannot begin or end with a space."
                    ForeColor="" CssClass="validators" EnableClientScript="false" ValidationExpression="^\S+.*\S+$"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="_confirmCompareValidator" runat="server" ControlToCompare="_confirmPassword"
                    ControlToValidate="_newPassword" CssClass="validators" Display="Dynamic" ErrorMessage=""
                    ForeColor="" SetFocusOnError="true" Text="*" EnableClientScript="false"></asp:CompareValidator>
            </label>
        </div>
        <div>
            <label>
                <span class="label">Confirm New Password:</span>
                <asp:TextBox ID="_confirmPassword" runat="server" AutoCompleteType="None" CausesValidation="true"
                    TextMode="Password">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="_confirmRequiredValidator" runat="server" ControlToValidate="_confirmPassword"
                    CssClass="validators" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                    ForeColor="" SetFocusOnError="true" Text="*" EnableClientScript="false">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="_minConfirmLengthValidator" runat="server" ControlToValidate="_confirmPassword"
                    CssClass="validators" Display="Dynamic" ErrorMessage="The minimum acceptable length for Confirm New Password is 8 characters."
                    ForeColor="" SetFocusOnError="true" ValidationExpression=".{8,}" Text="*" EnableClientScript="false">
                </asp:RegularExpressionValidator>
                <asp:CompareValidator ID="_newCompareValidator" runat="server" ControlToCompare="_newPassword"
                    ControlToValidate="_confirmPassword" CssClass="validators" Display="Dynamic"
                    ErrorMessage="New passwords do not match." ForeColor="" SetFocusOnError="true"
                    Text="*" EnableClientScript="false">
                </asp:CompareValidator>
            </label>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CommandName="Submit" OnCommand="SubmitChange" Text="Change Password"
                ToolTip="Change Password" />
            <a id="_cancelButton" runat="server" href="/Help/Default.aspx" class="button" title="Cancel Change">
                Cancel</a> <a id="_logoutButton" runat="server" href="/Logout.aspx" class="button"
                    title="Sign Out" visible="false">Sign Out</a>
        </div>
        <p>
            Having difficulty changing your password? Please contact the Candidate Services
            Unit at
            <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
            for assistance.</p>
    </asp:Panel>
</asp:Content>
