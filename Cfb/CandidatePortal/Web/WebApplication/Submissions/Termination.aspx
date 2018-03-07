<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.Termination" CodeBehind="Termination.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.termination_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionPageTitle" runat="server">
    <%=Resources.CPResources.termination_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <p>
        A campaign submits a Candidacy Termination if it has ceased all campaigning, and
        wishes to terminate a candidacy. By filing this form, your campaign will be relieved
        of its obligation to file disclosure statements with the CFB, even if you continue
        to be on the ballot for the election. Your campaign remains subject to all other
        requirements, however, including recordkeeping, responding to CFB requests, an audit
        of your campaign finances, and paying penalties for violations of CFB requirements.</p>
    <p>
        The CFB on its own may determine that a candidacy is terminated if you did not file
        ballot petitions, or you filed ballot petitions but were disqualified (and a court
        has not reversed the disqualification).</p>
    <p>
        Listed below is the history of all Candidacy Terminations, resubmissions of Candidacy
        Terminations, and amendments to Candidacy Terminations received by the CFB from
        your campaign. A &ldquo;Processing&rdquo; status does not indicate that your Candidacy
        Termination Form has been &ldquo;Accepted&rdquo; by the CFB.</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <cfb:TerminationHistoryPanel ID="TerminationHistoryPanel" runat="server" TooltipCssClass="tooltip"
        RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
