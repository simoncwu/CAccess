<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/CandidatePortal.master"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Announcements.View" EnableViewState="false"
    CodeBehind="View.aspx.cs" %>

<%@ MasterType VirtualPath="~/CandidatePortal.master" %>
<%@ Assembly Name="Cfb.CandidatePortal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9320c5fb540d5df1" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.announcements_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.announcements_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <h2>
        <asp:Literal ID="_title" runat="server"></asp:Literal></h2>
    <div class="description">
        <asp:Literal ID="_description" runat="server" Mode="PassThrough" />
    </div>
    <asp:Panel ID="_attachments" runat="server" Visible="false">
        <h3>
            Attachments</h3>
        <asp:Panel ID="_attachmentList" runat="server">
        </asp:Panel>
    </asp:Panel>
</asp:Content>
