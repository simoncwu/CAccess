<%@ Control Language="C#" EnableViewState="false" CodeBehind="PhotoGreeting.ascx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Controls.PhotoGreeting" %>
<div class="cp-PhotoGreeting">
    <asp:Panel ID="_candidatePhotoPanel" runat="server" CssClass="cp-CandidatePhoto">
        <asp:UpdatePanel runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Image ID="_photo" runat="server" ImageUrl="/Profile/CandidatePhoto.axd" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="_delete" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <div class="cp-UploadGreetingPanel">
        <asp:UpdatePanel ID="_updatePanel" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="_greetingPanel" runat="server" CssClass="cp-UserGreeting">
                    <h1>
                        Welcome,
                        <%=CPProfile.DisplayName%>
                    </h1>
                    <p>
                        You are currently viewing information for the <strong>
                            <%=CPProfile.ElectionCycle%>
                        </strong>election cycle.</p>
                    <asp:Button ID="_change" runat="server" Text="Change Photo" ToolTip="Change the photo on the home page."
                        OnClick="Change" CssClass="ChangePhotoButton" />
                </asp:Panel>
                <asp:Panel ID="_uploadPanel" runat="server" CssClass="cp-PhotoUpload">
                    <asp:Label ID="_uploadMessage" runat="server"></asp:Label>
                    <asp:FileUpload ID="_photoUpload" runat="server" ToolTip="Browse for a Photo" />
                    <asp:Button ID="_upload" runat="server" Text="Upload New Photo" OnClick="UploadPhoto"
                        ToolTip="Upload New Photo" CssClass="UploadButton" />
                    <asp:Button ID="_delete" runat="server" Text="Delete Current Photo" OnClick="DeletePhoto"
                        ToolTip="Delete Current Photo" CssClass="DeleteButton" />
                    <asp:Button ID="_cancel" runat="server" Text="Cancel" OnClick="Cancel" CssClass="CancelButton"
                        ToolTip="Cacnel" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="_change" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="_cancel" EventName="Click" />
                <asp:PostBackTrigger ControlID="_upload" />
                <asp:AsyncPostBackTrigger ControlID="_delete" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
