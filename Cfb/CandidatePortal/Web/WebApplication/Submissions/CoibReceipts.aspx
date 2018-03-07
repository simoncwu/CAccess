<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.CoibReceipts" CodeBehind="CoibReceipts.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.coibreceipt_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionPageTitle" runat="server">
    <%=Resources.CPResources.coibreceipt_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <p>
        To be eligible to receive public funds, you must provide the CFB with an original
        raised seal receipt from the Conflicts of Interest Board (&ldquo;COIB&rdquo;) evidencing
        submission of your personal financial disclosure statement. Listed below is the
        history of COIB Receipts, resubmissions of COIB Receipts, and amendments to COIB
        Receipts received by the CFB from your campaign. COIB Receipts may have a status
        of Processing, Accepted, On-Hold, Rejected or No Processing Required. A &ldquo;Processing&rdquo;
        status does not indicate that a COIB Receipt has been &ldquo;Accepted&rdquo; by
        the CFB. Your campaign can submit either an original <strong>or</strong> a copy
        of your COIB Receipt.</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <cfb:CoibReceiptHistoryPanel ID="CoibReceiptHistoryPanel" runat="server" TooltipCssClass="tooltip"
        RegularTooltipCssClass="submission-tooltip-reg" AmendmentTooltipCssClass="submission-tooltip-amend"
        ResubmissionTooltipCssClass="submission-tooltip-resub" IAmendmentTooltipCssClass="submission-tooltip-iamend"
        DocumentationTooltipCssClass="submission-tooltip-doconly" />
</asp:Content>
