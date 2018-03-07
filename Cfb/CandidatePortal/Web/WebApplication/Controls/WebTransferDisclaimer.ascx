<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebTransferDisclaimer.ascx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Controls.WebTransferDisclaimer" %>
<script runat="server">
    string _appName = CPProviders.SettingsProvider.ApplicationName;
</script>
<p>
    Please be aware that
    <%=_appName%>
    is updated at least once every business day. However, it may take 24 hours for Information
    submitted to the CFB to be posted on
    <%=_appName%>. Therefore, if you do not see the information you are expecting currently
    posted on
    <%=_appName%>, you may want to wait 24 hours before contacting the CFB.</p>
