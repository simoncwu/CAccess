<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.Certifications"
    CodeBehind="Certifications.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.certification_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionPageTitle" runat="server">
    <%=Resources.CPResources.certification_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <p>
        Listed below is the history of Certifications, resubmissions of Certifications,
        and amendments to your Certifications received by the CFB from your campaign. The
        Certification officially makes the campaign a participant in the Campaign Finance
        Program. Certifications may have a status of Processing, Accepted, On-Hold, Rejected
        or No Processing Required. A &ldquo;Processing&rdquo; status does not indicate that
        a Certification has been &ldquo;Accepted&rdquo; by the CFB.</p>
    <p>
        Candidates should amend their Certifications when information reported on the form
        has changed.</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <cfb:CertificationHistoryPanel ID="CertificationHistoryPanel" runat="server" TooltipCssClass="tooltip"
        RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
