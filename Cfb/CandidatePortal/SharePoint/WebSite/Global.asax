<%@ Assembly Name="Microsoft.SharePoint" %>
<%@ Application Language="C#" Inherits="Microsoft.SharePoint.ApplicationRuntime.SPHttpApplication" %>
<%@ Import Namespace="Cfb.CandidatePortal.Web" %>

<script RunAt="server">
	void Application_Start(object sender, EventArgs e)
	{
		// Code that runs on application startup
		CPApplication.Initialize(this.Application);
	}

	void Application_End(object sender, EventArgs e)
	{
		//  Code that runs on application shutdown
	}

	void Application_Error(object sender, EventArgs e)
	{
		// Code that runs when an unhandled error occurs

	}

	void Session_Start(object sender, EventArgs e)
	{
		// Code that runs when a new session is started

	}

	void Session_End(object sender, EventArgs e)
	{
		// Code that runs when a session ends. 
		// Note: The Session_End event is raised only when the sessionstate mode
		// is set to InProc in the Web.config file. If session mode is set to StateServer 
		// or SQLServer, the event is not raised.

	}
</script>

