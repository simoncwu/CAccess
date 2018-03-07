<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Messages.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Messages.View"
    CodeBehind="View.aspx.cs" %>

<%@ MasterType VirtualPath="Messages.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="Server">
    <%=Resources.CPResources.cmo_message_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="Server">
    <%=Resources.CPResources.cmo_message_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="Server">
    <div class="CmoMessageView">
        <asp:Panel ID="CmoMessageDetails" runat="server" CssClass="CmoMessageDetails">
            <asp:UpdatePanel ID="MessageHeaderUpdatePanel" runat="server" ChildrenAsTriggers="false"
                UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="cmoToolbar ui-corner-tl ui-corner-tr">
                        <ul class="icons">
                            <li id="ReturnLink" runat="server" class="button ui-state-default"><span class="ui-icon ui-icon-arrowreturnthick-1-w">
                            </span></li>
                        </ul>
                        <asp:LinkButton ID="ArchiveButton" runat="server" OnCommand="DoMessageAction" CssClass="button" />
                        <asp:LinkButton ID="FlagButton" runat="server" OnCommand="DoMessageAction" CssClass="button" />
                        <asp:Panel ID="CmoMailboxIndexInfo" runat="server" CssClass="cmoMailboxIndexInfo">
                            <ul class="icons">
                                <li runat="server" id="PreviousLinkLI" class="ui-state-default button">
                                    <asp:HyperLink ID="PreviousLink" runat="server" CssClass="ui-icon ui-icon-arrowthick-1-w"
                                        ToolTip="Previous Message" /></li>
                            </ul>
                            <asp:Literal ID="MessageIndex" runat="server" />
                            <ul class="icons">
                                <li runat="server" id="NextLinkLI" class="ui-state-default button">
                                    <asp:HyperLink ID="NextLink" runat="server" CssClass="ui-icon ui-icon-arrowthick-1-e"
                                        ToolTip="Next Message" /></span></li>
                            </ul>
                        </asp:Panel>
                        <div class="clearer">
                        </div>
                    </div>
                    <div class="messageHeader">
                        <asp:Panel ID="MessageFollowUp" runat="server" CssClass="cmoMessageFollowUp ui-state-highlight ui-corner-all">
                            <span class="ui-icon ui-icon-flag"></span>Flagged for follow-up since
                            <asp:Label ID="MessageFollowUpDate" runat="server" />.
                        </asp:Panel>
                        <h2>
                            <asp:Literal ID="MessageTitle" runat="server" /></h2>
                        <asp:Table ID="MessageHeader" runat="server" CellPadding="0" CellSpacing="0">
                            <asp:TableRow>
                                <asp:TableHeaderCell>CFB Message ID:</asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:Literal ID="MessageId" runat="server" /></asp:TableCell>
                                <asp:TableHeaderCell CssClass="date">Received:</asp:TableHeaderCell>
                                <asp:TableCell CssClass="date">
                                    <asp:Literal ID="MessageReceived" runat="server" /></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell>Election Cycle:</asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:Literal ID="MessageEc" runat="server" /></asp:TableCell>
                                <asp:TableHeaderCell CssClass="date">Opened:</asp:TableHeaderCell>
                                <asp:TableCell CssClass="date">
                                    <asp:Literal ID="MessageOpened" runat="server" /></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="MessageHeaderArchivedRow" runat="server">
                                <asp:TableHeaderCell></asp:TableHeaderCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableHeaderCell CssClass="date">Archived:</asp:TableHeaderCell>
                                <asp:TableCell CssClass="date">
                                    <asp:Literal ID="MessageArchived" runat="server" /></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="FlagButton" EventName="Command" />
                    <asp:PostBackTrigger ControlID="ArchiveButton" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="description">
                <asp:Literal ID="MessageBody" runat="server" />
            </div>
            <asp:Panel ID="MessageAttachments" runat="server" CssClass="messageAttachments">
                <h3>
                    Attachments</h3>
                <asp:BulletedList ID="AttachmentsList" runat="server" DisplayMode="HyperLink">
                </asp:BulletedList>
                <p>
                    Please note that some message attachments are very large and may require a significant
                    amount of time to open. To guarantee a successful download, right-click the attachment
                    link and choose the &ldquo;Save As&rdquo; option.</p>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="CmoMessageNotFound" runat="server" Visible="false">
            <p>
                The message you have requested could not be found.</p>
        </asp:Panel>
    </div>
</asp:Content>
