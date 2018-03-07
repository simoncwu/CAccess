<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="DoingBusiness.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.DoingBusinessPage" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.db_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="server">
    <%=Resources.CPResources.db_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <p>
        CFB staff will review each financial disclosure statement submitted by your campaign
        for contributions that are subject to the <a href="http://www.nyccfb.info/candidates/candidates/doing_biz.htm?sm=candidates_51"
            class="popup" target="_blank" title="Doing Business Overview">doing business regulations</a>.</p>
    <cfb:DoingBusinessHistory ID="_history" runat="server" />
</asp:Content>
