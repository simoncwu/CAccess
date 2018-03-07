<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Submissions.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Submissions.Default" EnableViewState="false"
    CodeBehind="Default.aspx.cs" %>

<%@ MasterType VirtualPath="Submissions.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.submissions_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSubmissionLegend" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <div class="ui-tabs">
        <div class="ui-tabs-panel">
            Your campaign has not yet submitted any documents to the CFB. Document status information
            will appear on this page once your campaign files with the CFB.
        </div>
    </div>
</asp:Content>
