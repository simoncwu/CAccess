<%@ Control Language="C#" Inherits="Microsoft.SharePoint.WebControls.ToolBarButton,Microsoft.SharePoint,Version=12.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c"
	AutoEventWireup="false" CompilationMode="Always" %>
<%@ Import Namespace="Microsoft.SharePoint.Utilities" %>
<table cellpadding="1" cellspacing="0" border="0" <%= ToggleOn ? "class='ms-toolbar-togglebutton-on'" : ""%>>
	<tr>
		<asp:PlaceHolder ID="PlaceHolderImage" runat="server">
			<td class="ms-toolbar" nowrap="nowrap" style="padding: <%= SPHttpUtility.HtmlEncode(Padding) %>">
				<a id="LinkImage" runat="server" class="ms-toolbar">
					<asp:Image ID="ImageOfButton" runat="server" border="0" Width="16" Height="16" GenerateEmptyAlternateText="true" />
				</a>
			</td>
		</asp:PlaceHolder>
		<td class="ms-toolbar" nowrap="nowrap" style="padding: <%= SPHttpUtility.HtmlEncode(Padding) %>">
			<asp:PlaceHolder ID="PlaceHolderControl" runat="server"><a id="LinkText" runat="server"
				class="ms-toolbar" /></asp:PlaceHolder>
			<a id="<%= SPHttpUtility.NoEncode(this.ClientID) %>" href="<%= NavigateUrl.StartsWith("javascript:") ? SPHttpUtility.NoEncode(NavigateUrl) : SPHttpUtility.HtmlUrlAttributeEncode(NavigateUrl) %>"
				style="visibility: hidden;"></a>
		</td>
	</tr>
</table>
