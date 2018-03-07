<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.DisclosureStatements"
    EnableEventValidation="false" CodeBehind="DisclosureStatements.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.disclosure_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderSubmissionPageTitle">
    <%=Resources.CPResources.disclosure_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageDescription">
    <p>
        Listed below is the history of all financial disclosure statements, resubmissions
        of financial disclosure statements, and amendments to financial disclosure statements
        received by the CFB from your campaign. Financial disclosure statements may have
        a status of Processing, Accepted, On-Hold, Rejected or No Processing Required. A
        &ldquo;Processing&rdquo; status does not indicate that a financial disclosure statement
        has been &ldquo;Accepted&rdquo; by the CFB. A financial disclosure statement received
        without required backup documentation will be considered &ldquo;On-Hold&rdquo; until
        that backup documentation is received.</p>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageContent">
    <cfb:DisclosureStatementHistoryPanel ID="DisclosureStatementHistoryPanel" runat="server"
        TooltipCssClass="tooltip" RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
