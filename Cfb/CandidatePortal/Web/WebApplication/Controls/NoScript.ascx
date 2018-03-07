<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="NoScript.ascx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Controls.NoScript" %>
<noscript>
    You are seeing this message either because your browser is not current enough to
    support JavaScript, or you have JavaScript disabled.
    <%=_applicationName%>
    requires a modern browser with JavaScript enabled to be viewed properly. Please
    upgrade to a modern browser such as Google Chrome, Mozilla Firefox, or Microsoft
    Internet Explorer. Also please make sure that JavaScript is enabled for this site.
</noscript>
<script type="text/javascript">
//<![CDATA[
    $("noscript").remove();
//]]>
</script>
