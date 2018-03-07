<%@ Page Language="C#" MasterPageFile="~/CPMinimal.master" AutoEventWireup="false"
    CodeBehind="Offline.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Offline"
    EnableViewState="false" %>

<%@ MasterType VirtualPath="~/CPMinimal.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=_applicationName%>
    (Offline)
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
    <script type="text/javascript">
        //<![CDATA[
        setTimeout(function () { $("#refreshButton").click(); }, 60000);
        //]]>
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <div id="LoginContainer" class="offline">
        <img src="/images/caccess-logo-login.png" id="SiteLogo" width="162" height="42" alt="<%=_applicationName%> Logo" />
        <p class="error">
            <%=_applicationName%>
            is currently unavailable due to regularly scheduled maintenance. We apologize for
            any inconvenience this may cause. Please try again later. Thank you.
        </p>
        <asp:Button ID="refreshButton" runat="server" CssClass="formbutton" Text="Try Again"
            OnClick="refreshButton_Click" />
    </div>
</asp:Content>
