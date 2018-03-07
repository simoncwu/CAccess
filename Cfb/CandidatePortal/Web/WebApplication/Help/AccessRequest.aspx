<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="Help.master" CodeBehind="AccessRequest.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.AccessRequest" %>

<script runat="server">
    string _appName = CPProviders.SettingsProvider.ApplicationName;
</script>
<%@ MasterType VirtualPath="Help.master" %>
<%@ Import Namespace="Cfb.CandidatePortal.CampaignContacts" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="Server">
    <%=Resources.CPResources.help_accountrequest_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="Server">
    <%=Resources.CPResources.help_accountrequest_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="Server">
    <asp:Panel ID="_errorPanel" runat="server" Visible="false">
        <p class="error">
            An error occurred while processing your request. Please try again.</p>
    </asp:Panel>
    <div class="accessRequest columns">
        <p>
            You may request additional users for other contacts whom you wish to have access
            to your
            <%=_appName%>
            account. Please note that only requests for authorized campaign contacts appearing
            on your campaign&rsquo;s Filer Registration or Certification will be accepted and
            approved. If you wish to request an account for a contact not currently shown, please
            <a href="http://www.nyccfb.info/PDF/forms/<%=CPProfile.ElectionCycle%>/index.aspx?sm=candidates_"
                target="_blank" title="NYC Campaign Finance Board Candidate Forms" class="popup">
                amend</a> your Filer Registration or Certification as appropriate with the contact&rsquo;s
            information.</p>
        <div class="left two-up column cp-boundingBox">
            <div class="caption">
                Request Additional Account
            </div>
            <asp:UpdatePanel ID="_creationUpdatePanel" runat="server" ChildrenAsTriggers="false"
                UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="_contactSelection" runat="server">
                        <p>
                            Choose from the following list of eligible contacts:
                            <asp:DropDownList ID="_contactsList" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="_noContactsMessage" runat="server" CssClass="note" Visible="false">There are no more campaign contacts who are eligible for <%=CPProviders.SettingsProvider.ApplicationName%> accounts.</asp:Label>
                        </p>
                        <asp:Button ID="_getContact" runat="server" OnCommand="FetchContactCommand" Text="Begin >" />
                    </asp:Panel>
                    <asp:Panel ID="_contactDetails" runat="server" Visible="false">
                        <table class="ms-listviewtable">
                            <tr>
                                <th>
                                    First Name:
                                </th>
                                <td>
                                    <asp:Label ID="_firstName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Middle Initial:
                                </th>
                                <td>
                                    <asp:Label ID="_mi" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Last Name:
                                </th>
                                <td>
                                    <asp:Label ID="_lastName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    E-mail Address:
                                </th>
                                <td>
                                    <asp:Label ID="_email" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="buttons">
                            <asp:Button ID="_submitRequest" runat="server" Text="Request Access" OnCommand="RequestAccountCommand" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="_successPanel" runat="server" Visible="false">
                        <p>
                            You have successfully submitted an additional
                            <%=_appName%>
                            account request for the following contact:</p>
                        <table class="ms-listviewtable">
                            <tr>
                                <th>
                                    First Name:
                                </th>
                                <td>
                                    <asp:Label ID="_firstNameResult" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Middle Initial:
                                </th>
                                <td>
                                    <asp:Label ID="_miResult" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Last Name:
                                </th>
                                <td>
                                    <asp:Label ID="_lastNameResult" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    E-mail Address:
                                </th>
                                <td>
                                    <asp:Label ID="_emailResult" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <p>
                            Please allow up to 48 hours for your request to be processed by CFB staff. If approved,
                            the login credentials for this user will be sent to the e-mail address shown.</p>
                        <a href="AccessRequest.aspx" title="Request Another Account" class="button">Request
                            Another Account</a> <a href="/Help/" title="Return to <%=Resources.CPResources.help_title%>"
                                class="button">
                                <%=Resources.CPResources.help_title%></a>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="_submitRequest" EventName="Command" />
                    <asp:AsyncPostBackTrigger ControlID="_getContact" EventName="Command" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="two-up column">
            <p>
                The following contacts already have
                <%=_appName%>
                accounts:<br />
                <asp:BulletedList ID="_currentUsersList" runat="server">
                </asp:BulletedList>
                <asp:Label ID="_noUsersMessage" runat="server" CssClass="note" Visible="false">There are currently no campaign contacts with <%=CPProviders.SettingsProvider.ApplicationName%> accounts.</asp:Label>
            </p>
            <asp:Repeater ID="_ineligibleUsers" runat="server" Visible="false">
                <HeaderTemplate>
                    <p>
                        The following contacts are not eligible for
                        <%=_appName%>
                        accounts because of missing information:
                    </p>
                    <table class="ms-listviewtable">
                        <tr class="ms-viewheadertr">
                            <th class="sidemargin">
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Reason
                            </th>
                            <th class="sidemargin">
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="sidemargin">
                        </td>
                        <td>
                            <%#((AccountEligibilityStatus)Container.DataItem).Entity.Name%>
                        </td>
                        <td>
                            <%#((AccountEligibilityStatus)Container.DataItem).Status.GetDescription()%>
                        </td>
                        <td class="sidemargin">
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
