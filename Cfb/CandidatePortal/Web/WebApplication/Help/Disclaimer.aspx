<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Help.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.Disclaimer"
    EnableViewState="false" CodeBehind="Disclaimer.aspx.cs" %>

<%@ MasterType VirtualPath="Help.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.disclaimer_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.disclaimer_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <p>
        The
        <%=CPProviders.SettingsProvider.ApplicationName%>
        web site includes information about financial disclosure statements, other forms
        submitted to the Campaign Finance Board (CFB) and, other campaign related information
        and campaign compliance information entered by CFB staff. All financial disclosure
        data are as reported to the CFB by campaigns in their financial disclosure statements.
        <%=CPProviders.SettingsProvider.ApplicationName%>
        data is subject to change as a result of ongoing audits, additional amendments to
        filings received from candidates, and on-going CFB actions. The data was last updated
        as indicated by the &ldquo;<%=CPProviders.SettingsProvider.ApplicationName%>
        last updated&rdquo; date. Please contact the CFB if you have any questions regarding
        this data.</p>
    <cfb:WebTransferDisclaimer runat="server" />
</asp:Content>
