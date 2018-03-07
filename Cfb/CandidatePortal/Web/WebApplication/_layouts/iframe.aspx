<%@ Page Language="C#" Inherits="Microsoft.SharePoint.ApplicationPages.DatePickerFrame" %>

<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
	Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<% SPSite spServer = SPControl.GetContextSite(Context); SPWeb spWeb = SPControl.GetContextWeb(Context); %>
<html dir="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,multipages_direction_dir_value%>' EncodeMethod='HtmlEncode'/>"
	xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta content="Microsoft SharePoint" name="GENERATOR" />
	<sharepoint:csslink runat="server"></sharepoint:csslink>

	<script language="javascript" src="/_layouts/DatePicker.js" type="text/javascript"></script>

	<title>Date Picker</title>
</head>
<body onkeydown="OnKeyDown(this);" onload="PositionFrame('DatePickerDiv');" style="margin: 0;">
	<sharepoint:spdatepickercontrol id="DatePickerWebCustomControl" runat="server">
		  </sharepoint:spdatepickercontrol>
</body>
</html>
