<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/CPMinimal.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.ResetPasswordPage" CodeBehind="ResetPassword.aspx.cs" %>

<%@ MasterType VirtualPath="~/CPMinimal.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.pwreset_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInBody">
    <%=Resources.CPResources.pwreset_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <asp:Panel ID="_resetPasswordPanel" runat="server" CssClass="cp-ResetPassword">
        <asp:Panel ID="_errorMessage" runat="server" Visible="false">
            <p class="error">
                <strong>The information was not recognized.</strong></p>
            <p>
                We were unable to find a match in our records for the information you provided.
                Please check your entries and try again.</p>
            <p>
                If you continue to have difficulty resetting your password, or require further assistance,
                please contact the Candidate Services Unit at
                <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>.</p>
        </asp:Panel>
        <asp:Panel ID="_deactivatedMessage" runat="server" Visible="false">
            <p class="error">
                The account requested has been deactivated and may no longer be used. Please contact
                our Candidate Services Unit at
                <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
                if you wish to use this account to enter
                <%=CPProviders.SettingsProvider.ApplicationName%>.</p>
        </asp:Panel>
        <asp:ValidationSummary ID="_validationSummary" runat="server" DisplayMode="BulletList"
            HeaderText="The following errors were found:" ShowSummary="false" ShowMessageBox="true"
            CssClass="validators" ForeColor="" />
        <p>
            If you have forgotten your username or password, please enter the following information
            and we will send you an e-mail containing your username and a new password:</p>
        <ul>
            <li>
                <p>
                    E-mail Address: This is the e-mail address you provided to us when you requested
                    your account.</p>
            </li>
            <li>
                <p>
                    Candidate ID: This is the 2 to 4 digit ID of the candidate that is used in C-SMART
                    and all communications with the Campaign Finance Board.</p>
            </li>
        </ul>
        <p class="note">
            Note: This process will reset your password, even if you only need your username.</p>
        <div>
            <label>
                <span class="label">E-mail Address:</span>
                <asp:TextBox ID="_email" runat="server" Columns="40"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_emailRequiredValidator" runat="server" ControlToValidate="_email"
                    CssClass="validators" Display="Dynamic" ForeColor="" SetFocusOnError="true" ErrorMessage="E-mail Address was not specified.">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="_emailFormatValidator" runat="server" ControlToValidate="_email"
                    CssClass="validators" Display="Dynamic" ErrorMessage="E-mail Address is not valid."
                    ForeColor="" SetFocusOnError="true" ValidationExpression="^[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,}">
                </asp:RegularExpressionValidator>
            </label>
        </div>
        <div>
            <label>
                <span class="label">Candidate ID:</span>
                <asp:TextBox ID="_cid" runat="server" Columns="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_cidValidator" runat="server" ControlToValidate="_cid"
                    CssClass="validators" Display="Dynamic" ErrorMessage="Candidate ID was not specified."
                    ForeColor="" SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="_cidFormatValidator" runat="server" ControlToValidate="_cid"
                    CssClass="validators" Display="Dynamic" ErrorMessage="Candidate ID is not valid."
                    ForeColor="" SetFocusOnError="true" ValidationExpression="\w\w+">
                </asp:RegularExpressionValidator>
            </label>
        </div>
        <div class="buttons">
            <asp:Button runat="server" OnClick="OnSubmit" Text="Reset Password" ToolTip="Reset Password" />
            <a href="<%=FormsAuthentication.LoginUrl%>" title="Return to Login Page" class="button">
                Return to Login Page</a>
        </div>
        <p>
            If you continue to encounter problems with your username or password, please contact
            the Candidate Services Unit at
            <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
            or <a href="mailto:csumail@nyccfb.info" title="Contact CSU">csumail@nyccfb.info</a>.</p>
    </asp:Panel>
    <asp:Panel ID="_successMessage" runat="server" Visible="false">
        <p>
            Your password has been successfully reset and sent via e-mail to the e-mail address
            on file for your account. Once logged in, you will be able to select a new password.</p>
        <a href="<%=FormsAuthentication.LoginUrl%>" title="Return to Login Page" class="button">
            Return to Login Page</a>
    </asp:Panel>
</asp:Content>
