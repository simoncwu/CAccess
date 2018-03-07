<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="Default.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.Default" EnableViewState="false" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.audit_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    Your campaign currently does not have any audit review data on file with the CFB.
</asp:Content>
