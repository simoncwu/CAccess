<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="Help.master" CodeBehind="Breakdown.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.Breakdown" EnableViewState="false" %>

<script runat="server">
    string _appName = CPProviders.SettingsProvider.ApplicationName;
</script>
<%@ MasterType VirtualPath="Help.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.help_breakdown_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.help_breakdown_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <h3>
        Homepage</h3>
    <p>
        The
        <%=_appName%>
        Homepage includes a listing of upcoming events specific to your campaign, general
        announcements to all campaigns, links to important campaign resources on the CFB
        web site, a confirmation of basic information about your campaign, and the ability
        to upload a candidate or campaign photo.</p>
    <h3>
        Calendar</h3>
    <p>
        The
        <%=_appName%>
        Calendar includes links to other important campaign related calendars, upcoming
        events, announcements, as well as the ability to flip through months and years on
        a calendar and then select a particular month for monthly details.</p>
    <h3>
        Document Submission Status</h3>
    <p>
        Your campaign will be submitting different types of documents to the CFB throughout
        the course of an election. The first time your campaign submits a specific type
        of document, a new tab will appear in this section providing status information
        for that document. Document submissions may include: Filer Registrations, Certifications,
        IDS/C-SMART Requests, Financial Disclosure Statements, COIB Receipts, Small Campaign
        Disclosure, Special Pre Election Disclosure, Statements of Need, and Candidacy Terminations.</p>
    <h3>
        Audit Reviews</h3>
    <p>
        The
        <%=_appName%>
        Audit Reviews section includes status information of your campaign regarding CFB
        Statement Reviews, Compliance Visits, Threshold Status, Public Fund payment amounts
        and date of Public Funds payments, including the period of the election (Primary,
        General, Runoff/Rerun) that the Public Fund payment was disbursed.</p>
    <h3>
        Campaign Profile</h3>
    <p>
        The
        <%=_appName%>
        Campaign Profile section includes information on the candidate and committee that
        was submitted to the CFB on your campaign&rsquo;s Filer Registration or Certification.
        The Campaign Profile also contains a Financial Profile section which provides a
        snapshot of your campaign&rsquo;s financial information. Data on the Financial Profile
        was submitted to the CFB on the candidate&rsquo;s financial disclosure statements.</p>
    <h3>
        <%=Resources.CPResources.cmo_title%></h3>
    <p>
        <%=_appName%>
        <%=Resources.CPResources.cmo_title%>
        provides candidates, treasurers and other campaign representatives with a reliable
        way to receive communications electronically from the
        <%=Resources.CPResources.agency_name%>. These communications, can include Statement
        Reviews, Draft Audit Reports, and various other messages available in PDF format
        (requires free and easy-to-use Adobe Acrobat Reader, or equivalent).</p>
    <h3>
        Help</h3>
    <p>
        The
        <%=_appName%>
        Help section includes helpful information for using
        <%=_appName%>. This information includes a purpose statement, descriptions of possible
        content found in each section, CFB contact information, and information on how to
        change your
        <%=_appName%>
        password.</p>
</asp:Content>
