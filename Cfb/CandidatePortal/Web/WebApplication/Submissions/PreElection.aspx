<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.PreElectionPage"
    CodeBehind="PreElection.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.pedisclosure_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionPageTitle" runat="server">
    <%=Resources.CPResources.pedisclosure_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <p>
        Listed below is the history of all Daily Pre-Election Disclosure Statements, resubmissions
        of Daily Pre-Election Disclosure Statements, and amendments to Daily Pre-Election
        Disclosure Statements received by the CFB from your campaign. Daily Pre-Election
        Disclosure Statements may have a status of Processing, Accepted, On-Hold, Rejected
        or No Processing Required. A &ldquo;Processing&rdquo; status does not indicate that
        a Daily Pre-Election Disclosure Statement has been &ldquo;Accepted&rdquo; by the
        CFB.</p>
    <p>
        Candidates should amend their Daily Pre-Election Disclosure Statements when information
        reported on this has changed.</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <cfb:PreElectionHistoryPanel ID="PreElectionHistoryPanel" runat="server" TooltipCssClass="tooltip"
        RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
