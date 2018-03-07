<%@ Page Language="C#" CodeBehind="Privacy.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Help.Privacy"
    MasterPageFile="~/CPMinimal.master" EnableViewState="false" %>

<script runat="server">
    string _appName = CPProviders.SettingsProvider.ApplicationName;
</script>
<%@ MasterType VirtualPath="~/CPMinimal.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.privacy_title%></asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
    <style type="text/css">
        .privacyLink
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.privacy_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <p>
        The privacy and confidentiality of a candidate&rsquo;s campaign is very important
        to the CFB. Our concern for your privacy extends to use of
        <%=_appName%>. That view underlies our policy, stated below:</p>
    <p>
        <%=_appName%>
        will only provide information specific to the campaign of that logged in user. Information
        specific to another candidate&rsquo;s campaign will not be provided. Only CFB staff
        and the campaign have access to that campaign&rsquo;s information.</p>
    <p>
        In addition:</p>
    <ul>
        <li>We do not give, sell, or transfer any personal information to third parties.</li>
        <li>We store a temporary &ldquo;cookie&rdquo; solely for tracking your logged-in status
            throughout the course of a single
            <%=_appName%>
            session. This cookie expires upon the end of your visit and does not monitor any
            browsing activity outside of
            <%=_appName%>. (A &ldquo;cookie&rdquo; is a file placed on your hard drive by a
            Web site that allows it to monitor your use of the site.)</li>
        <li>Information is collected for statistical purposes and we occasionally analyze user
            behavior to measure customer interest in the various areas of our site.</li>
    </ul>
    <p>
        If during your visit, you browse through the Web site, read documents, or download
        information, we will automatically gather and store certain information about your
        visit. We collect and store only the following information about your visit:</p>
    <ul>
        <li>The account used to access the web site;</li>
        <li>The Internet domain (such as &ldquo;nyccfb.info&rdquo; or &ldquo;school.edu&rdquo;,
            or &ldquo;business.com&rdquo;) and IP address (an IP address is a number that is
            automatically assigned to your computer whenever you are surfing the Web) from which
            you access our Web site;</li>
        <li>The time and date of your visit;</li>
        <li>The pages you view;</li>
        <li>Whether you successfully received the document you requested; and</li>
        <li>Any errors encountered while browsing the web site.</li>
    </ul>
    <p>
        We use this information to assist us in making our site more user-friendly&mdash;to
        learn about the number of visitors to our site, what they are looking for, and the
        types of technology they use.</p>
</asp:Content>
