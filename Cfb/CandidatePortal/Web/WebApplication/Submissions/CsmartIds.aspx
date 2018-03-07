<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.CsmartIds" CodeBehind="CsmartIds.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.idsrequest_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionPageTitle" runat="server">
    <%=Resources.CPResources.idsrequest_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <p>
        Your campaign uses this form to request from the CFB a copy of the C-SMART software
        and/or request an IDS password for filing disclosure statements electronically.
        This form must be completed and returned to the CFB. Listed below is the history
        of C-SMART/IDS Requests, resubmissions of C-SMART/IDS Requests, and amendments to
        C-SMART/IDS Requests received by the CFB from your campaign. IDS/C-SMART Requests
        may have a status of Processing, Accepted, On-Hold, Rejected or No Processing Required.
        A &ldquo;Processing&rdquo; status does not indicate that a IDS/C-SMART Request has
        been &ldquo;Accepted&rdquo; by the CFB.</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <cfb:CsmartIdsHistoryPanel ID="CsmartIdsHistoryPanel" runat="server" TooltipCssClass="tooltip"
        RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
