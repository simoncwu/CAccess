<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="ExtensionRequest.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.ExtensionRequest" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="Server">
    <%=Resources.CPResources.extrequest_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="Server">
    <%=Resources.CPResources.extrequest_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="Server">
    <div id="ExtensionRequestForm">
        <asp:UpdatePanel ID="_requestFormUpdatePanel" runat="server" ChildrenAsTriggers="false"
            RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                    <asp:Literal ID="_introText" runat="server">You can use this page to request extensions for any of your outstanding audit response deadlines. All fields are required for submission.</asp:Literal><asp:Literal
                        ID="_confirmText" runat="server" Visible="false">Please confirm and verify the following extension request details before submitting:</asp:Literal></p>
                <asp:ValidationSummary ID="_validationSummary" runat="server" />
                <asp:Table runat="server" BorderWidth="0" CellPadding="0" CellSpacing="0">
                    <asp:TableRow runat="server">
                        <asp:TableHeaderCell runat="server">Election Cycle:</asp:TableHeaderCell>
                        <asp:TableCell ID="_electionCycle" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableHeaderCell runat="server">Response Type:</asp:TableHeaderCell>
                        <asp:TableCell runat="server">
                            <asp:DropDownList ID="_types" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedTypeChanged">
                            </asp:DropDownList>
                            <asp:Literal ID="_typesValue" runat="server" Visible="false"></asp:Literal>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="_numberRow" runat="server" Visible="false">
                        <asp:TableHeaderCell ID="_numberLabelText" runat="server"></asp:TableHeaderCell>
                        <asp:TableCell runat="server">
                            <asp:DropDownList ID="_numberDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedReviewNumberChanged">
                            </asp:DropDownList>
                            <asp:Literal ID="_numberValue" runat="server" Visible="false"></asp:Literal>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="_dueDateRow" runat="server" Visible="false">
                        <asp:TableHeaderCell runat="server">Requested Due Date:</asp:TableHeaderCell>
                        <asp:TableCell runat="server">
                            <asp:DropDownList ID="_dueDateDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedDateChanged">
                            </asp:DropDownList>
                            <asp:Literal ID="_dueDateValue" runat="server" Visible="false"></asp:Literal>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <p>
                    <asp:Literal ID="_reasonIntroText" runat="server">Please state your reason for requesting an extension in as much relevant detail
                    as possible. Omission of any important details will delay your request and may even
                    cause it to be denied.</asp:Literal><asp:Literal ID="_reasonLabelText" runat="server"
                        Visible="false">Reason for extension request request:</asp:Literal>
                    <asp:RequiredFieldValidator ID="_reasonValidator" runat="server" ControlToValidate="_reason"
                        CssClass="validators" Display="Dynamic" Text="*" ErrorMessage="A reason for requesting an extension is required."
                        InitialValue="" Enabled="false"></asp:RequiredFieldValidator>
                </p>
                <div>
                    <asp:TextBox ID="_reason" runat="server" TextMode="MultiLine" /><asp:Literal ID="_reasonValue"
                        runat="server" Visible="false"></asp:Literal>
                </div>
                <div class="buttons">
                    <asp:Button ID="_hiddenSubmit" runat="server" CssClass="noDisplay" Text="Continue &amp; Verify"
                        OnCommand="OnCommand" Enabled="false" />
                    <asp:LinkButton ID="_modify" runat="server" CssClass="button" Text="Modify" ToolTip="Modify Extension Request"
                        OnCommand="OnCommand" Visible="false" />
                    <asp:LinkButton ID="_submit" runat="server" CssClass="button" Text="Continue &amp; Verify"
                        ToolTip="Continue and Verify Extension Request" OnCommand="OnCommand" Enabled="false" />
                    <asp:LinkButton ID="_cancel" runat="server" CssClass="button" Text="Cancel" ToolTip="Cancel Extension Request"
                        OnCommand="OnCommand" CausesValidation="false" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="_types" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="_numberDropDown" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="_hiddenSubmit" EventName="Command" />
                <asp:AsyncPostBackTrigger ControlID="_modify" EventName="Command" />
                <asp:AsyncPostBackTrigger ControlID="_submit" EventName="Command" />
                <asp:PostBackTrigger ControlID="_cancel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
