<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.StatementsOfNeed"
    CodeBehind="StatementsOfNeed.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.statementsofneed_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionPageTitle" runat="server">
    <%=Resources.CPResources.statementsofneed_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <p>
        Listed below is the history of Statements of Need received by the CFB from your
        campaign. Statements of Need may have a status of Processing, Accepted, On-Hold,
        Rejected or No Processing Required. A &ldquo;Processing&rdquo; status does not indicate
        that a Statement of Need has been &ldquo;Accepted&rdquo; by the CFB.</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <cfb:StatementOfNeedHistoryPanel ID="StatementOfNeedHistoryPanel" runat="server"
        TooltipCssClass="tooltip" RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
