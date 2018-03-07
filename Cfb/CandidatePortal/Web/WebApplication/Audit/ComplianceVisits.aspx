<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="ComplianceVisits.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.ComplianceVisits" EnableViewState="false" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.cv_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="server">
    <%=Resources.CPResources.cv_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <p>
        The purpose of a compliance visit is to determine if your campaign is in compliance
        with the Board&rsquo;s rules concerning record keeping and contribution and expenditure
        limit requirements. Your campaign must provide the Board with a written response
        to the findings discovered during a compliance visit. Compliance visits are preliminary
        reviews and are in no way a final determination concerning any aspect of the Campaign&rsquo;s
        compliance or the acceptability of any specific document. Your campaign will only
        receive a compliance visit letter following the compliance visit if CFB staff have
        found issues that must be addressed.</p>
    <cfb:ComplianceVisitHistory ID="_history" runat="server" />
</asp:Content>
