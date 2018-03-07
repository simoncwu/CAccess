<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="StatementReviews.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.StatementReviews" EnableViewState="false" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.sr_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="server">
    <%=Resources.CPResources.sr_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <p>
        CFB staff will review each financial disclosure statement submitted by your campaign.
        Candidates seeking public matching funds will also be reviewed for invalid matching
        claims, meeting threshold, backup requirements and to determine eligibility for
        public fund payment.</p>
    <cfb:StatementReviewHistory ID="_history" runat="server" />
</asp:Content>
