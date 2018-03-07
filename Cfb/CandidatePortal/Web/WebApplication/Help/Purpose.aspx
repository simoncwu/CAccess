<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Help.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.Purpose"
    EnableViewState="False" CodeBehind="Purpose.aspx.cs" %>

<%@ MasterType VirtualPath="Help.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.help_about_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.help_about_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <p>
        <%=CPProviders.SettingsProvider.ApplicationName%>
        is an online resource for campaigns to streamline and manage their communications
        with the CFB.</p>
    <p>
        By providing a secure, internet-based gateway to the CFB,
        <%=CPProviders.SettingsProvider.ApplicationName%>
        will allow candidates and campaign staff to view and update their personalized CFB
        information 24 hours a day, seven days a week.</p>
    <p>
        Candidates can use
        <%=CPProviders.SettingsProvider.ApplicationName%>
        to receive information from the CFB in a paperless fashion. Campaigns will be able
        to check the status of the paperwork they&rsquo;ve submitted to the CFB as well
        as the status of their eligibility for public funds and post-election audits.</p>
    <p>
        <%=CPProviders.SettingsProvider.ApplicationName%>
        will be continuously updated with new functionality and information throughout the
        election cycle.</p>
</asp:Content>
