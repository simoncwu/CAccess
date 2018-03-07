<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Help.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.Terms"
    EnableViewState="false" CodeBehind="Terms.aspx.cs" %>

<%@ MasterType VirtualPath="Help.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.termsdefs_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.termsdefs_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="columns">
        <div class="left column two-up">
            <h3 class="terms">
                Document Statuses</h3>
            <dl title="Document Statuses">
                <dt>Processing</dt>
                <dd>
                    The document is in the possession of the CFB but has not been through the quality
                    control checks that would result in acceptance.</dd>
                <dt>On-Hold</dt>
                <dd>
                    The document is in possession of the CFB but is missing information needed to be
                    accepted.</dd>
                <dt>Accepted</dt>
                <dd>
                    The document has been reconciled and, if electronic, uploaded into the CFB&rsquo;s
                    systems.</dd>
                <dt>Rejected</dt>
                <dd>
                    The document has errors rendering it unacceptable and is returned to the committee.
                    The committee must correct all errors and resubmit.</dd>
                <dt>No Processing Required</dt>
                <dd>
                    The document is in the possession of the CFB but does not require any processing
                    by the CFB&rsquo;s systems.</dd>
            </dl>
        </div>
        <div class="column two-up">
            <h3 class="terms">
                Document Types</h3>
            <dl title="Document Types">
                <dt>Initial</dt>
                <dd>
                    An initial document is the first submission of a document type.</dd>
                <dt>Amendment</dt>
                <dd>
                    An amendment is a document updating or correcting information about the committee
                    or a submission in C-SMART adding or correcting information previously reported
                    in that particular disclosure statement.</dd>
                <dt>Resubmission</dt>
                <dd>
                    A resubmission is a disclosure statement sent to replace a previous filing.</dd>
                <dt>Internal Amendment</dt>
                <dd>
                    An internal amendment indicates when the CFB has updated or corrected committee
                    information, usually to clarify unclear information provided to the CFB.</dd>
                <dt>Documentation</dt>
                <dd>
                    Documentation consists of other disclosure information sent to the CFB in response
                    to an audit statement review.</dd>
            </dl>
        </div>
    </div>
</asp:Content>
