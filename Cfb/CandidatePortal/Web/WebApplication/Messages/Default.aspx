<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Messages.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Messages.Default"
    CodeBehind="Default.aspx.cs" %>

<%@ MasterType VirtualPath="Messages.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="Server">
    <%=Resources.CPResources.cmo_inbox_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="Server">
    Message Center
    <%--<span class="cmoPaperSettingsLink"><a
        href="Settings.aspx" title="<%=Resources.CPResources.cmo_paperless_title%>">Change your
        paper mailings settings</a></span>--%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="Server">
    <div class="cmoToolbar ui-corner-tl ui-corner-tr">
        <asp:LinkButton ID="ArchiveButton" runat="server" OnCommand="MultipleMessageAction"
            CssClass="button" />
        <asp:LinkButton ID="UnarchiveButton" runat="server" OnCommand="MultipleMessageAction"
            CssClass="button" />
        <asp:LinkButton ID="FlagButton" runat="server" OnCommand="MultipleMessageAction"
            CssClass="button" />
        <asp:LinkButton ID="ClearFlagButton" runat="server" OnCommand="MultipleMessageAction"
            CssClass="button" />
    </div>
    <cfb:CmoMailboxMessageList ID="CmoMailboxMessageList" runat="server" />
</asp:Content>
