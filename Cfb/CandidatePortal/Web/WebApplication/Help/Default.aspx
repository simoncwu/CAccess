<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Help.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.Default"
    EnableViewState="false" CodeBehind="Default.aspx.cs" %>

<%@ MasterType VirtualPath="Help.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.help_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.help_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="helpPage columns">
        <div class="left column three-up">
            <h3 class="caption">
                C-Access Resources</h3>
            <div class="cp-boundingBox">
                <ul>
                    <li><a href="Purpose.aspx" title="<%=Resources.CPResources.help_about_title%>">
                        <%=Resources.CPResources.help_about_title%></a></li>
                    <li><a href="Breakdown.aspx" title="<%=Resources.CPResources.help_breakdown_title%>">
                        <%=Resources.CPResources.help_breakdown_title%></a></li>
                    <li><a href="Terms.aspx" title="<%=Resources.CPResources.termsdefs_title%>">
                        <%=Resources.CPResources.termsdefs_title%></a></li>
                    <li><a href="http://www.nyccfb.info/PDF/FilingDayGuide.pdf" title="<%=Resources.CPResources.generalDS_title%>">
                        <%=Resources.CPResources.generalDS_title%></a></li>
                </ul>
            </div>
            <h3 class="caption">
                Fine Print</h3>
            <div class="cp-boundingBox">
                <ul>
                    <li><a href="Disclaimer.aspx" title="<%=Resources.CPResources.disclaimer_title%>">
                        <%=Resources.CPResources.disclaimer_title%></a></li>
                    <li><a href="Privacy.aspx" target="_blank" title="<%=Resources.CPResources.privacy_title %>">
                        <%=Resources.CPResources.privacy_title %></a></li>
                </ul>
            </div>
        </div>
        <div class="left column three-up">
            <h3 class="caption">
                Online Support</h3>
            <div class="cp-boundingBox">
                <ul>
                    <li><a href="InaccurateData.aspx" title="<%=Resources.CPResources.help_baddata_title%>">
                        <%=Resources.CPResources.help_baddata_title%></a></li><%-- disabled until further notice
                    <li><a href="<%=PageUrlManager.ExtensionRequestPageUrl%>" title="<%=Resources.CPResources.extrequest_title%>">
                        <%=Resources.cfb.extrequest_title%></a></li>--%>
                    <li><a href="Contact.aspx" title="<%=Resources.CPResources.contact_title%>">
                        <%=Resources.CPResources.contact_title%></a></li>
                    <li><a href="ChangePassword.aspx" title="<%=Resources.CPResources.help_changepw_title%>">
                        <%=Resources.CPResources.help_changepw_title%></a></li><%-- disabled until CFIS integration is complete
                    <li><a href="/Messages/Settings.aspx" title="<%=Resources.CPResources.cmo_paperless_title%>">
                        <%=Resources.cfb.cmo_paperless_title%></a></li>--%>
                    <li><a href="AccessRequest.aspx" title="<%=Resources.CPResources.help_accountrequest_title%>">
                        <%=Resources.CPResources.help_accountrequest_title%></a></li>
                </ul>
            </div>
            <h3 class="caption">
                General Contact Information</h3>
            <div class="cp-boundingBox">
                <address>
                    <strong>
                        <%=CPProviders.SettingsProvider.AgencyName%></strong><br />
                    100 Church Street, 12th Floor<br />
                    New York, NY 10007<br />
                    Tel:
                    <%=CPProviders.SettingsProvider.ApplicationName%><br />
                    Fax: (212) 409-1705<br />
                    <a href="http://www.nyccfb.info" title="NYC Campaign Finance Board Web Site" target="_blank"
                        class="popup">www.nyccfb.info</a>
                </address>
            </div>
        </div>
        <div class="column three-up">
            <cfb:CfbResources runat="server"></cfb:CfbResources>
        </div>
    </div>
</asp:Content>
