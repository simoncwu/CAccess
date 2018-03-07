<%@ Page Title="Sign In : C-SMART" Language="C#" AutoEventWireup="false" MasterPageFile="Login.master"
    CodeBehind="Csmart.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Login.Csmart" %>

<%@ MasterType VirtualPath="Login.master" %>
<script runat="server">

</script>
<asp:Content ContentPlaceHolderID="SiteLogoContent" runat="server">
    <img src="/images/sso/csmart-logo.png" id="SiteLogo" width="279" height="66" alt="C-SMART" /></asp:Content>
<asp:Content ContentPlaceHolderID="AppInfoContent" runat="server">
    <p>
        C-SMART (Candidate Software for Managing and Reporting Transactions) is the CFB&rsquo;s
        proprietary software used by registered candidates to record financial transactions,
        file disclosure statements, and maintain contributors&rsquo; information.</p>
    <p>
        C-SMART generates disclosure statements for both the CFB and New York State Board
        of Elections.</p>
    <p>
        For more information, contact your Candidate Services Liaison at
        <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
        or <a href="mailto:csumail@nyccfb.info" title="Contact CSU via E-mail" class="popup">
            CSUmail@nyccfb.info</a>.</p>
</asp:Content>
