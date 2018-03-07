<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.FilerRegistrations"
    CodeBehind="FilerRegistrations.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.fr_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionPageTitle" runat="server">
    <%=Resources.CPResources.fr_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <p>
        Listed below is the history of Filer Registrations, resubmissions of Filer Registrations,
        and amendments to Filer Registrations received by the CFB from your campaign. A
        Filer Registration may have a status of Processing, Accepted, On-Hold, Rejected
        or No Processing Required. A &ldquo;Processing&rdquo; status does not indicate that
        a Filer Registration has been &ldquo;Accepted&rdquo; by the CFB.</p>
    <p>
        Candidates should amend their Filer Registration when information reported on the
        form has changed.</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <cfb:FilerRegistrationHistoryPanel ID="FilerRegistrationHistoryPanel" runat="server"
        TooltipCssClass="tooltip" RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
