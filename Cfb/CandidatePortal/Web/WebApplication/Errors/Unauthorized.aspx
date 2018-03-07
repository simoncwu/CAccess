<%@ Page Title="" Language="C#" MasterPageFile="~/CPMinimal.master" EnableViewState="false"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Errors.Unauthorized" CodeBehind="Unauthorized.aspx.cs" %>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="Server">
    Access Denied</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="Server">
    Access Denied</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="Server">
    <p>
        You are not authorized to access the requested application (<asp:Label ID="_appNameLabel"
            runat="server"></asp:Label>). If this is in error, please contact the Candidate
        Services Unit at
        <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>.</p>
    <p>
        You are currently signed in as: <span class="username">
            <%=User.Identity.Name %></span></p>
    <p>
        <a id="_signoutLink" runat="server" href="/Logout.aspx" title="Sign in as a different user">
            Sign in as a different user</a></p>
</asp:Content>
