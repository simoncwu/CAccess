<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="Threshold.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.Threshold" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.threshold_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="server">
    <%=Resources.CPResources.threshold_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <div class="submission details">
        <div class="submission description">
            <p>
                The threshold number and threshold dollar amount reflect valid matching claims at
                the time each report was prepared. All matching claims are subject to ongoing audit
                by the CFB. Each report does not constitute a guarantee that the committee will
                have met the threshold when the Board is prepared to disburse public funds, or a
                guarantee that the committee will receive public funds. Remember, meeting the threshold
                is only one of several eligibility requirements for receiving matching funds.</p>
            <p>
                If the candidate has not indicated on CFB forms which office s/he is seeking, the
                CFB cannot state the threshold requirements or determine how many residents can
                be counted for threshold purposes. This is indicated by &ldquo;(n/a)&rdquo; on this
                report. If the candidate wishes to declare an office sought, please contact the
                Candidate Services Unit.</p>
        </div>
        <aside class="submission legend cp-Threshold">
            <h3 class="caption">
                Explanation of Version</h3>
            <div id="SubmissionLegendAccordion" class="ui-accordion ui-widget ui-helper-reset"
                role="tablist">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top"
                    role="tab" aria-controls="ui-accordion-SubmissionLegendAccordion-panel-0" aria-selected="true">
                    <%=CPConvert.ToString(ThresholdRevisionType.Initial)%></h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active"
                    id="ui-accordion-SubmissionLegendAccordion-panel-0" aria-labelledby="ui-accordion-SubmissionLegendAccordion-header-0"
                    role="tabpanel" aria-expanded="true" aria-hidden="false">
                    Threshold count was generated based on an initial review of the candidates filing.</div>
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top"
                    role="tab" aria-controls="ui-accordion-SubmissionLegendAccordion-panel-1" aria-selected="true">
                    <%=CPConvert.ToString(ThresholdRevisionType.Revised)%></h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active"
                    id="ui-accordion-SubmissionLegendAccordion-panel-1" aria-labelledby="ui-accordion-SubmissionLegendAccordion-header-1"
                    role="tabpanel" aria-expanded="false" aria-hidden="false">
                    Threshold count was generated based on a review of the response and revisions provided
                    by a candidate to a previous filing.</div>
            </div>
        </aside>
    </div>
    <h3 class="cp-Threshold caption">
        Current Status</h3>
    <table class="cp-Threshold cp-ThresholdSummary ms-listviewtable detailsview">
        <tr>
            <th>
                Office Sought:
            </th>
            <td>
                <asp:Label ID="_officeSought" runat="server"></asp:Label>
            </td>
            <th>
                Through Statement:
            </th>
            <td>
                <asp:Label ID="_lastStatement" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Threshold Number:
            </th>
            <td>
                <asp:Label ID="_threshold" runat="server"></asp:Label>
            </td>
            <th>
                Threshold Dollar Amount:
            </th>
            <td>
                <asp:Label ID="_dollars" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Threshold Number Requirement:
            </th>
            <td>
                <asp:Label ID="_thresholdRequired" runat="server"></asp:Label>
            </td>
            <th>
                Threshold Dollar Amount Requirement:
            </th>
            <td>
                <asp:Label ID="_dollarsRequired" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <cfb:ThresholdHistoryControl ID="_history" runat="server" Title="Complete History" />
</asp:Content>
