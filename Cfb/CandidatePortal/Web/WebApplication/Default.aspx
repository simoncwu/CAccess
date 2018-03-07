<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/CandidatePortal.master"
    CodeBehind="Default.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Default" %>

<%@ MasterType VirtualPath="~/CandidatePortal.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    Home</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <asp:WebPartManager runat="server" />
    <div class="columns homepage">
        <div class="left column two-up">
            <cfb:PhotoGreeting runat="server" />
            <cfb:CandidateProfileSummary ID="_profileSummary" runat="server" />
        </div>
        <div class="column two-up">
            <cfb:AnnouncementsList ID="_announcementsList" runat="server" />
            <cfb:RemindersControl runat="server" View="Brief" />
            <cfb:CfbResources ID="CfbResources" runat="server" />
            <div class="pressBox">
                <%--<cfb:PressReleasesLink ID="_pressReleasesLink" runat="server" Text="View My Press Release References" />--%>
            </div>
        </div>
    </div>
</asp:Content>
