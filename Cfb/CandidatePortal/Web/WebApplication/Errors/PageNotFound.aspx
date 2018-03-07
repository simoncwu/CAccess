<%@ Page Language="C#" CodeBehind="PageNotFound.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Errors.PageNotFound"
    MasterPageFile="~/CPMinimal.master" EnableViewState="false" %>

<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.error404_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInBody">
    <%=Resources.CPResources.error404_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <p>
        The page you are looking for might have been removed, had its name changed, or is
        temporarily unavailable.</p>
    <p>
        Please try the following:</p>
    <ul>
        <li>Make sure that the Web site address displayed in the address bar of your browser
            is spelled and formatted correctly.</li>
        <li>Click the Back button to try another link.</li>
        <li>Go directly to the <a href="<%=CPApplication.SiteUrl%>" title="<%=CPProviders.SettingsProvider.ApplicationName%>">
            <%=CPProviders.SettingsProvider.ApplicationName%></a> home page and follow the links until you find
            what you are looking for.</li>
    </ul>
    <p>
        If you reached this page by clicking a link within the
        <%=CPProviders.SettingsProvider.ApplicationName%>
        web site, please report the broken link via e-mail at <a href="mailto:ca&#99;c%65s&#37;73&#64;n&#37;79cc&#102;b&#46;%69nfo">
            &#99;&#97;cce&#115;s&#64;&#110;y&#99;cf&#98;&#46;inf&#111;</a>.</p>
</asp:Content>
