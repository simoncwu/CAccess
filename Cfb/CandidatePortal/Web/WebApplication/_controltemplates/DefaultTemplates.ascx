<%@ Control Language="C#" AutoEventWireup="false" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
	Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="SPHttpUtility" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
	Namespace="Microsoft.SharePoint.Utilities" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<SharePoint:RenderingTemplate ID="FieldLabelDefault" runat="server">
	<Template>
		<nobr><SharePoint:FieldProperty PropertyName="Title" runat="server"/></nobr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FieldLabelRequired" runat="server">
	<Template>
		<nobr><SharePoint:FieldProperty PropertyName="Title" runat="server"/><span class="ms-formvalidation"> *</span></nobr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FieldLabelForDisplay" runat="server">
	<Template>
		<a name="SPBookmark_<SharePoint:FieldProperty PropertyName='InternalName' runat='server'/>">
		</a>
		<SharePoint:FieldProperty PropertyName="Title" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CompositeField" runat="server">
	<Template>
		<td nowrap="nowrap" valign="top" width="190px" class="ms-formlabel">
			<h3 class="ms-standardheader">
				<SharePoint:FieldLabel runat="server" />
			</h3>
		</td>
		<td valign="top" class="ms-formbody" width="400px">
			<!-- FieldName="<SharePoint:FieldProperty PropertyName="Title" runat="server"/>"
			 FieldInternalName="<SharePoint:FieldProperty PropertyName="InternalName" runat="server"/>"
			 FieldType="SPField<SharePoint:FieldProperty PropertyName="Type" runat="server"/>"
		  -->
			<SharePoint:FormField runat="server" />
			<SharePoint:FieldDescription runat="server" />
			<SharePoint:AppendOnlyHistory runat="server" />
		</td>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DisplayCompositeField" runat="server">
	<Template>
		<td nowrap="true" valign="top" width="165px" class="ms-formlabel">
			<h3 class="ms-standardheader">
				<SharePoint:FieldLabel runat="server" />
			</h3>
		</td>
		<td valign="top" class="ms-formbody" width="450px" id="SPField<SharePoint:FieldProperty PropertyName='Type' runat='server'/>">
			<!-- FieldName="<SharePoint:FieldProperty PropertyName="Title" runat="server"/>"
			 FieldInternalName="<SharePoint:FieldProperty PropertyName="InternalName" runat="server"/>"
			 FieldType="SPField<SharePoint:FieldProperty PropertyName="Type" runat="server"/>"
		  -->
			<SharePoint:FormField runat="server" />
			<SharePoint:AppendOnlyHistory runat="server" />
		</td>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ListFieldIterator" runat="server">
	<Template>
		<tr>
			<SharePoint:CompositeField runat="server" />
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="TwoRowCompositeField" runat="server">
	<Template>
		<tr>
			<td valign="top" width="90%" class="ms-formlabel">
				<SharePoint:FieldLabel runat="server">
					<CustomTemplate>
						<SharePoint:FieldProperty PropertyName="Title" runat="server" />
					</CustomTemplate>
					<CustomAlternateTemplate>
						<SharePoint:FieldProperty PropertyName="Title" runat="server" />
						<span class="ms-formvalidation">*</span>
					</CustomAlternateTemplate>
				</SharePoint:FieldLabel>
			</td>
		</tr>
		<tr>
			<td valign="top" width="90%" class="ms-formbodysurvey">
				<!-- FieldName="<SharePoint:FieldProperty PropertyName="Title" runat="server"/>"
			 FieldInternalName="<SharePoint:FieldProperty PropertyName="InternalName" runat="server"/>"
			 FieldType="SPField<SharePoint:FieldProperty PropertyName="Type" runat="server"/>"
		  -->
				<SharePoint:FormField runat="server" />
				<SharePoint:FieldDescription runat="server" />
			</td>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="TwoRowFieldIterator" runat="server">
	<Template>
		<SharePoint:CompositeField TemplateName="TwoRowCompositeField" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="PropertyIterator" runat="server">
	<Template>
		<tr>
			<td nowrap="true" valign="top" class="ms-authoringcontrols">
				<SharePoint:FieldLabel runat="server" />
				&nbsp;
			</td>
			<td valign="top" class="ms-authoringcontrols">
				<SharePoint:FormField runat="server" />
				<SharePoint:FieldDescription runat="server" />
			</td>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="GenericForm" runat="server">
	<Template>
		<table class="ms-formtable" border="0" cellpadding="2">
			<SharePoint:ListFieldIterator runat="server" />
		</table>
		<SharePoint:RequiredFieldMessage runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ListForm" runat="server">
	<Template>
		<span id='part1'>
			<SharePoint:InformationBar runat="server" />
			<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&nbsp;"
				runat="server">
				<Template_RightButtons>
					<SharePoint:NextPageButton runat="server" />
					<SharePoint:SaveButton runat="server" />
					<SharePoint:GoBackButton runat="server" />
				</Template_RightButtons>
			</wssuc:ToolBar>
			<SharePoint:FormToolBar runat="server" />
			<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
				width="100%">
				<SharePoint:ChangeContentType runat="server" />
				<SharePoint:FolderFormFields runat="server" />
				<SharePoint:ListFieldIterator runat="server" />
				<SharePoint:ApprovalStatus runat="server" />
				<SharePoint:FormComponent TemplateName="AttachmentRows" runat="server" />
			</table>
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td class="ms-formline">
						<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
				<tr>
					<td width="100%">
						<SharePoint:ItemHiddenVersion runat="server" />
						<SharePoint:ParentInformationField runat="server" />
						<SharePoint:InitContentType runat="server" />
						<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
							runat="server">
							<Template_Buttons>
								<SharePoint:CreatedModifiedInfo runat="server" />
							</Template_Buttons>
							<Template_RightButtons>
								<SharePoint:SaveButton runat="server" />
								<SharePoint:GoBackButton runat="server" />
							</Template_RightButtons>
						</wssuc:ToolBar>
					</td>
				</tr>
			</table>
		</span>
		<SharePoint:AttachmentUpload runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BlogCommentsForm" runat="server">
	<Template>
		<table width="275" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td>
					<span id='part1'>
						<SharePoint:InformationBar runat="server" />
						<table class="ms-formtable" border="0" cellpadding="0" cellspacing="0" width="100%">
							<SharePoint:ChangeContentType runat="server" />
							<SharePoint:FolderFormFields runat="server" />
							<SharePoint:ListFieldIterator runat="server" ExcludeFields="PostTitle" />
							<SharePoint:ApprovalStatus runat="server" />
							<SharePoint:FormComponent TemplateName="AttachmentRows" runat="server" />
						</table>
						<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 10px">
							<tr>
								<td width="100%">
									<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
										runat="server">
										<Template_RightButtons>
											<SharePoint:SubmitCommentButton CssClass="ms-ButtonHeightWidth2" runat="server" Text="<%$Resources:wss,tb_submitcomment%>" />
										</Template_RightButtons>
									</wssuc:ToolBar>
									<SharePoint:InitContentType runat="server" />
									<SharePoint:ItemHiddenVersion runat="server" />
								</td>
							</tr>
						</table>
					</span>
			</tr>
			</td></table>
		<SharePoint:AttachmentUpload runat="server" />
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BlogForm" runat="server">
	<Template>
		<span id='part1'>
			<SharePoint:InformationBar runat="server" />
			<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&nbsp;"
				runat="server">
				<Template_RightButtons>
					<SharePoint:SaveAsDraftButton Text="<%$Resources:wss,tb_saveasdraft%>" runat="server" />
					<SharePoint:PublishButton Text="<%$Resources:wss,tb_publish%>" runat="server" />
					<SharePoint:GoBackButton runat="server" />
				</Template_RightButtons>
			</wssuc:ToolBar>
			<SharePoint:FormToolBar runat="server" />
			<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
				width="100%">
				<SharePoint:ChangeContentType runat="server" />
				<SharePoint:FolderFormFields runat="server" />
				<SharePoint:ListFieldIterator runat="server" />
				<SharePoint:ApprovalStatus runat="server" />
				<SharePoint:FormComponent TemplateName="AttachmentRows" runat="server" />
			</table>
			<table cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
				<tr>
					<td class="ms-formline">
						<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
				<tr>
					<td width="100%">
						<SharePoint:ItemHiddenVersion runat="server" />
						<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
							runat="server">
							<Template_Buttons>
								<SharePoint:InitContentType runat="server" />
								<SharePoint:CreatedModifiedInfo runat="server" />
							</Template_Buttons>
							<Template_RightButtons>
								<SharePoint:SaveAsDraftButton Text="<%$Resources:wss,tb_saveasdraft%>" runat="server" />
								<SharePoint:PublishButton Text="<%$Resources:wss,tb_publish%>" runat="server" />
								<SharePoint:GoBackButton runat="server" />
							</Template_RightButtons>
						</wssuc:ToolBar>
					</td>
				</tr>
			</table>
		</span>
		<SharePoint:AttachmentUpload runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="SurveyForm" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_RightButtons>
				<SharePoint:NextPageButton runat="server" />
				<SharePoint:SaveButton Text="<%$Resources:wss,tb_survey_save%>" AccessKey="<%$Resources:wss,tb_survey_save_AK%>"
					runat="server" />
				<SharePoint:MultiPageGoBackButton runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
		<SharePoint:FormToolBar runat="server" />
		<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
			width="100%">
			<SharePoint:SurveyFieldIterator runat="server" />
		</table>
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td class="ms-formline">
					<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
				</td>
			</tr>
		</table>
		<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
			<tr>
				<td width="100%">
					<SharePoint:ItemHiddenVersion runat="server" />
					<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
						runat="server">
						<Template_Buttons>
							<SharePoint:InitContentType runat="server" />
							<SharePoint:CreatedModifiedInfo runat="server" />
						</Template_Buttons>
						<Template_RightButtons>
							<SharePoint:NextPageButton runat="server" />
							<SharePoint:SaveButton Text="<%$Resources:wss,tb_survey_save%>" AccessKey="<%$Resources:wss,tb_survey_save_AK%>"
								runat="server" />
							<SharePoint:MultiPageGoBackButton runat="server" />
						</Template_RightButtons>
					</wssuc:ToolBar>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="AttendeesEditForm" runat="server">
	<Template>
		<span id='part1'>
			<SharePoint:InformationBar runat="server" />
			<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&nbsp;"
				runat="server">
				<Template_RightButtons>
					<SharePoint:NextPageButton runat="server" />
					<SharePoint:SaveButton runat="server" />
					<SharePoint:GoBackButton runat="server" />
				</Template_RightButtons>
			</wssuc:ToolBar>
			<SharePoint:FormToolBar runat="server" />
			<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
				width="100%">
				<SharePoint:ChangeContentType runat="server" />
				<SharePoint:FolderFormFields runat="server" />
				<SharePoint:ListFieldIterator runat="server" />
				<SharePoint:ApprovalStatus runat="server" />
				<SharePoint:FormComponent TemplateName="AttachmentRows" runat="server" />
			</table>
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td class="ms-formline">
						<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<SharePoint:AttendeeEmailResponse runat="server" />
			<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
				<tr>
					<td width="100%">
						<SharePoint:ItemHiddenVersion runat="server" />
						<SharePoint:ParentInformationField runat="server" />
						<SharePoint:InitContentType runat="server" />
						<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
							runat="server">
							<Template_Buttons>
								<SharePoint:CreatedModifiedInfo runat="server" />
							</Template_Buttons>
							<Template_RightButtons>
								<SharePoint:SaveButton runat="server" />
								<SharePoint:GoBackButton runat="server" />
							</Template_RightButtons>
						</wssuc:ToolBar>
					</td>
				</tr>
			</table>
		</span>
		<SharePoint:AttachmentUpload runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="AttendeeEmailResponse" runat="server">
	<Template>
		<table width="100%">
			<tr>
				<td class="ms-descriptiontext" colspan="2">
					<asp:CheckBox ID="cbSendEmail" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<table bgcolor="#D4D0C8" cellpadding="5" width="100%">
						<colgroup>
							<col width="1">
						</colgroup>
						<tr>
							<td nowrap class="ms-descriptiontext" align="right">
								<nobr>
							<asp:Label id="lblSubjectTitle" runat="server" />
						</nobr>
							</td>
							<td class="ms-descriptiontext">
								<asp:Label ID="lblSubject" runat="server" />
							</td>
						</tr>
						<tr>
							<td nowrap class="ms-descriptiontext" align="right" valign="top">
								<nobr>
							<asp:Label id="lblBodyTitle" runat="server" />
						</nobr>
							</td>
							<td class="ms-descriptiontext">
								<asp:Label ID="lblBody" runat="server" /><br>
								<asp:TextBox ID="txtEmailBody" TextMode="multiline" Title="<%$Resources:wss,mws_mtgAttendeeEmailBody%>"
									Wrap="true" Rows="8" class="ms-long" Style="width: 100%;" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<input type="hidden" id="MtgAttendeeEmailBodyPhrase" name="MtgAttendeeEmailBodyPhrase">
		<input type="hidden" id="MtgAttendeeEmailSubjectPhrase" name="MtgAttendeeEmailSubjectPhrase">
		<input type="hidden" id="OWS:Status:Dropdown" name="OWS:Status:Dropdown">
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="AttachmentRows" runat="server">
	<Template>
		<tr id="idAttachmentsRow">
			<td nowrap="true" valign="top" class="ms-formlabel" width="20%">
				<SharePoint:FieldLabel FieldName="Attachments" runat="server" />
			</td>
			<td valign="top" class="ms-formbody" width="80%">
				<SharePoint:AttachmentsField FieldName="Attachments" runat="server" />

				<script>
					var elm = document.getElementById("idAttachmentsTable");
					if (elm == null || elm.rows.length == 0)
						document.getElementById("idAttachmentsRow").style.display = 'none';
				</script>

			</td>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="AttachmentUpload" runat="server">
	<Template>
		<input type="hidden" name='attachmentsToBeRemovedFromServer'>
		<input type="hidden" name='RectGifUrl' value="/_layouts/images/rect.gif">
		<span id='partAttachment' style='display: none'>
			<table cellspacing="0" cellpadding="0" border="0" width="100%">
				<tbody>
					<tr>
						<td class="ms-descriptiontext" style="padding-bottom: 8px;" colspan="4" valign="top">
							<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_attachments_description%>"
								EncodeMethod='HtmlEncode' />
						</td>
					</tr>
					<tr>
						<td width="190px" class="ms-formlabel" valign="top" height="50px">
							<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_attachments_name%>"
								EncodeMethod='HtmlEncode' />
						</td>
						<td width="400px" class="ms-formbody" valign="bottom" height="15" id="attachmentsOnClient">
							<span dir="ltr">
								<input type="file" name="fileupload0" id="onetidIOFile" class="ms-fileinput" size="56"
									title="Name"></input>
							</span>
						</td>
					</tr>
					<tr>
						<td class="ms-formline" colspan="4" height="1">
							<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
						</td>
					</tr>
					<tr>
						<td colspan="4" height="10">
							<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
						</td>
					</tr>
					<tr>
						<td class="ms-attachUploadButtons" colspan="4">
							<input class="ms-ButtonHeightWidth" id="attachOKbutton" type="BUTTON" onclick='OkAttach()'
								value="OK">
							<span id="idSpace" class="ms-SpaceBetButtons"></span>
							<input t class="ms-ButtonHeightWidth" id="attachCancelButton" type="BUTTON" onclick='CancelAttach()'
								value="Cancel">
						</td>
					</tr>
					<tr>
						<td colspan="4" height="60">
							&nbsp;
						</td>
						<td>
							&nbsp;
						</td>
					</tr>
				</tbody>
			</table>

			<script>
				if (document.getElementById("onetidIOFile") != null)
					document.getElementById("onetidIOFile").title = "<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,form_attachments_name%>' EncodeMethod='EcmaScriptStringLiteralEncode'/>";
				if (document.getElementById("attachOKbutton") != null)
					document.getElementById("attachOKbutton").value = "<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,form_ok%>' EncodeMethod='EcmaScriptStringLiteralEncode'/>";
				if (document.getElementById("attachCancelButton") != null)
					document.getElementById("attachCancelButton").value = "<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,form_cancel%>' EncodeMethod='EcmaScriptStringLiteralEncode'/>";
			</script>

		</span>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="RequiredFieldMessage" runat="server">
	<Template>
		<span id="reqdFldTxt" style="white-space: nowrap; padding-right: 3px;" class="ms-descriptiontext">
			<asp:Literal runat="server" Text="<%$Resources:wss,form_required_field%>" /></span>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CreatedModifiedInfo" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0">
			<tr>
				<td nowrap class="ms-descriptiontext" id="onetidinfoblock1">
					<SharePoint:FormattedString FormatText="<%$Resources:wss,form_createdby%>" runat="server">
						<SharePoint:FormField ControlMode="Display" FieldName="Created" DisableInputFieldLabel="true"
							runat="server" />
						<SharePoint:FormField ControlMode="Display" FieldName="Author" DisableInputFieldLabel="true"
							runat="server" />
						<SharePoint:CreationType runat="server" />
					</SharePoint:FormattedString>
				</td>
			</tr>
			<tr>
				<td nowrap class="ms-descriptiontext" id="onetidinfoblock2">
					<SharePoint:FormattedString FormatText="<%$Resources:wss,form_modifiedby%>" runat="server">
						<SharePoint:FormField ControlMode="Display" FieldName="Modified" DisableInputFieldLabel="true"
							runat="server" />
						<SharePoint:FormField ControlMode="Display" FieldName="Editor" DisableInputFieldLabel="true"
							runat="server" />
					</SharePoint:FormattedString>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CreatedModifiedVersionInfo" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0">
			<tr>
				<td class="ms-descriptiontext" id="onetidinfoblockV">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_version%>"
						EncodeMethod='HtmlEncode' />
					<SharePoint:FormField ControlMode="Display" FieldName="_UIVersionString" runat="server" />
				</td>
			</tr>
			<tr>
				<td nowrap class="ms-descriptiontext" id="onetidinfoblock1">
					<SharePoint:FormattedString FormatText="<%$Resources:wss,form_createdby%>" runat="server">
						<SharePoint:FormField ControlMode="Display" FieldName="Created" runat="server" />
						<SharePoint:FormField ControlMode="Display" FieldName="Author" runat="server" />
						<SharePoint:CreationType runat="server" />
					</SharePoint:FormattedString>
				</td>
			</tr>
			<tr>
				<td nowrap class="ms-descriptiontext" id="onetidinfoblock2">
					<SharePoint:FormattedString FormatText="<%$Resources:wss,form_modifiedby%>" runat="server">
						<SharePoint:FormField ControlMode="Display" FieldName="Modified" runat="server" />
						<SharePoint:FormField ControlMode="Display" FieldName="Editor" runat="server" />
					</SharePoint:FormattedString>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CreatedVersionInfo" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td class="ms-descriptiontext" id="onetidinfoblockV">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_version%>"
						EncodeMethod='HtmlEncode' />
					<SharePoint:FormField ControlMode="Display" FieldName="_UIVersionString" runat="server" />
				</td>
			</tr>
			<tr>
				<td nowrap class="ms-descriptiontext" id="onetidinfoblock1">
					<SharePoint:FormattedString FormatText="<%$Resources:wss,form_createdby%>" runat="server">
						<SharePoint:FormField ControlMode="Display" FieldName="Modified" runat="server" />
						<SharePoint:FormField ControlMode="Display" FieldName="Editor" runat="server" />
						<SharePoint:CreationType runat="server" />
					</SharePoint:FormattedString>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DocumentTransformersInfo" runat="server">
	<Template>
		<table id="doctransforms" runat="server" border="0" cellpadding="2" cellspacing="0"
			width="100%" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="InformationBar" runat="server">
	<Template>
		<SharePoint:GenericInformationBar runat="server">
			<Template_Controls>
				<SharePoint:FileUploadedMessage runat="server" />
				<SharePoint:ApprovalMessage runat="server" />
				<SharePoint:EmailCalendarMessage runat="server" />
				<SharePoint:CopySourceInfo runat="server" />
				<SharePoint:AssignToEmailMessage runat="server" />
			</Template_Controls>
		</SharePoint:GenericInformationBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ApprovalStatus" runat="server">
	<Template>
		<tr>
			<td nowrap="true" valign="top" class="ms-formlabel">
				<SharePoint:FieldLabel FieldName="_ModerationStatus" runat="server" />
			</td>
			<td valign="top" class="ms-formbody">
				<SharePoint:FormField ControlMode="Display" FieldName="_ModerationStatus" runat="server" />
				<br>
				<SharePoint:FormField ControlMode="Display" FieldName="_ModerationComments" runat="server" />
			</td>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ChangeContentType" runat="server">
	<Template>
		<tr>
			<td nowrap="true" valign="top" class="ms-formlabel">
				<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_content_type%>"
					EncodeMethod='HtmlEncode' />
			</td>
			<td valign="top" class="ms-formbody">
				<asp:DropDownList ID="ContentTypeChoice" ToolTip="<%$Resources:wss,form_content_type%>"
					runat="server" />
				<br>
				<asp:Label ID="ContentTypeDescription" runat="server" />
			</td>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="InitContentType" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0">
			<tr>
				<td nowrap class="ms-descriptiontext">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_content_type%>"
						EncodeMethod='HtmlEncode' />: <asp:Label ID="InitContentType" runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ItemHiddenVersion" runat="server">
	<Template>
		<input id="owshiddenversion" type="HIDDEN" name="owshiddenversion" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WikiMiniConsole" runat="server">
	<Template>
		<div style="position: relative; top: 0; left: 0;">
			<div class="ms-wikieditouter">
				<table id="miniconsole" cellspacing="0" cellpadding="0" border="0" width="100%">
					<tr>
						<td>
							<table class="ms-wikieditsecond" cellspacing="0" cellpadding="0" border="0" width="100%">
								<tr>
									<td>
										<table class="ms-wikieditthird" cellspacing="0" cellpadding="0" border="0" width="100%">
											<tr>
												<td>
													<!-- this is for the orange cast inside the menu -->
													<table class="ms-wikieditorange" cellspacing="0" cellpadding="0" border="0" width="100%">
														<tr>
															<td class="ms-wikieditorangeinnera" style="width: 100%;">
																&nbsp;
															</td>
														</tr>
														<tr>
															<td>
																<wssuc:ToolBar CssClass="ms-wikitoolbar" runat="server">
																	<Template_Buttons>
																		<SharePoint:WikiEditItemButton Text="<%$Resources:wss,siteactions_wikieditpage%>"
																			runat="server" />
																		<SharePoint:WikiPageHistoryButton Text="<%$Resources:wss,siteactions_viewpagehistory%>"
																			ButtonID="WikiPageHistory" runat="server" />
																		<SharePoint:WikiIncomingLinksButton Text="<%$Resources:wss,siteactions_viewincominglinks%>"
																			ButtonID="WikiIncomingLinks" runat="server" />
																	</Template_Buttons>
																</wssuc:ToolBar>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BackLinksDisplay" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<SharePoint:BackLinksIterator ID="BackLinksIterator" runat="server" />
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BackLinksIterator" runat="server">
	<Template>
		<tr>
			<td style="padding-bottom: 5px;" class="ms-vb">
				<img src="/_layouts/images/square.gif" alt="">&nbsp;&nbsp;<asp:HyperLink ID="BackLink"
					runat="server" />
			</td>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="RecentChangesMenu" runat="server">
	<Template>
		<div class="ms-quicklaunchouter">
			<div class="ms-quickLaunch" style="width: 100%">
				<div class="ms-quicklaunchheader">
					<asp:HyperLink ID="RecentChangesHeaderLink" runat="server"><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recent_changes%>" EncodeMethod='HtmlEncode'/></asp:HyperLink>
				</div>
				<div>
					<table id="NavBarRecentChanges" class="ms-navSubMenu1" cellpadding="0" cellspacing="0"
						border="0">
						<tr>
							<td>
								<table border="0" cellpadding="0" cellspacing="0" class="ms-navSubMenu2">
									<SharePoint:RecentChangesIterator ID="RecentChangesIterator" runat="server" />
								</table>
							</td>
						</tr>
					</table>
				</div>
				<div>
					<table width="100%" cellpadding="0" cellspacing="0" border="0">
						<tr>
							<td class="ms-recentchanges">
								<img src="/_layouts/images/rect.gif" alt="">
							</td>
							<td width="100%" valign="top" style="padding: 4px">
								<SharePoint:SPLinkButton runat="server" CssClass="ms-addnew" ID="ViewAllPagesLink" />
							</td>
						</tr>
					</table>
				</div>
			</div>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="RecentChangesIterator" runat="server">
	<Template>
		<tr>
			<td>
				<table class="ms-navitem" cellpadding="0" cellspacing="0" border="0" width="100%">
					<tr>
						<td style="width: 100%;">
							<asp:HyperLink class="ms-navitem" ID="RecentChange" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DiffSelectorMenu" runat="server">
	<Template>
		<div class="ms-laction" style="width: 100%">
			<div class="ms-lactionheader">
				<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,versions%>" EncodeMethod='HtmlEncode' />
			</div>
			<div>
				<table id="DiffSelectorTable" class="ms-lactiontable" cellpadding="0" cellspacing="0"
					border="0">
					<SharePoint:DiffSelectorIterator ID="DiffSelectorIterator" runat="server" />
				</table>
			</div>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DiffSelectorIterator" runat="server">
	<Template>
		<asp:TableRow runat="server">
			<asp:TableCell ID="DiffVersionCell" runat="server">
				<table cellpadding="0" cellspacing="0">
					<tr>
						<td class="ms-lactionitem">
							<asp:HyperLink ID="DiffVersion" runat="server" />
						</td>
					</tr>
				</table>
			</asp:TableCell>
		</asp:TableRow>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="VersionDiffIterator" runat="server">
	<Template>
		<div class="ms-wikicontent">
			<div class="ms-wikifieldheader">
				<div>
					<asp:Label ID="VersionFieldName" runat="server" />
				</div>
			</div>
			<SharePoint:VersionDiff ID="VersionDiffControl" runat="server" />
			<p>
			</p>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DocumentLibraryFormCore" runat="server">
	<Template>
		<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" id="formTbl"
			cellspacing="0" width="100%">
			<SharePoint:ChangeContentType runat="server" />
			<SharePoint:DocumentLibraryFields runat="server" />
			<SharePoint:ApprovalStatus runat="server" />
		</table>
		<SharePoint:WebPartPageMaintenanceMessage runat="server" />
		<SharePoint:DocumentTransformersInfo runat="server" />
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td class="ms-formline">
					<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
				</td>
			</tr>
		</table>
		<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
			<tr>
				<td width="100%">
					<SharePoint:ItemHiddenVersion runat="server" />
					<SharePoint:InitContentType runat="server" />
					<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
						runat="server">
						<Template_Buttons>
							<SharePoint:CreatedModifiedInfo runat="server" />
						</Template_Buttons>
						<Template_RightButtons>
							<SharePoint:SaveButton runat="server" />
							<SharePoint:GoBackButton runat="server" />
						</Template_RightButtons>
					</wssuc:ToolBar>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DocumentLibraryForm" runat="server">
	<Template>
		<SharePoint:InformationBar runat="server" />
		<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_RightButtons>
				<SharePoint:SaveButton runat="server" />
				<SharePoint:GoBackButton runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
		<SharePoint:FormToolBar runat="server" />
		<SharePoint:FormComponent TemplateName="DocumentLibraryFormCore" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WebPartPageMaintenanceMessage" runat="server">
	<Template>
		<table width="100%">
			<tr>
				<td class="ms-formbody" valign="top">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,doclibtemplates_web_part_page_maintenance_message%>"
						EncodeMethod='HtmlEncode' /><br>
					<asp:HyperLink ID="WebPartMaintenancePageLink" NavigateUrl="" Text="<%$Resources:wss,doclibtemplates_web_part_page_maintenance_link_text%>"
						Target="_self" runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FileFormFields" runat="server">
	<Template>
		<SharePoint:ListFieldIterator runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FolderFormFields" runat="server">
	<Template>

		<script>			SetUploadPageTitle();</script>

		<tr>
			<SharePoint:CompositeField FieldName="FileLeafRef" runat="server" />
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="UploadFormToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DocLibEditFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:DeleteItemButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DocLibDisplayFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" runat="server" FocusOnToolbar="true">
			<Template_Buttons>
				<SharePoint:EnterFolderButton runat="server" />
				<SharePoint:EditItemButton runat="server" />
				<SharePoint:DeleteItemButton runat="server" />
				<SharePoint:ManagePermissionsButton runat="server" />
				<SharePoint:ManageCopiesButton runat="server" />
				<SharePoint:CheckInCheckOutButton runat="server" />
				<SharePoint:VersionHistoryButton runat="server" />
				<SharePoint:WorkflowsButton runat="server" />
				<SharePoint:AlertMeButton runat="server" />
				<SharePoint:ApprovalButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DocLibDisplayFormVersionToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar ID="toolBarTbl" runat="server" FocusOnToolbar="true">
			<Template_Buttons>
				<SharePoint:DeleteItemVersionButton runat="server" />
				<SharePoint:RestoreItemVersionButton runat="server" />
				<SharePoint:VersionHistoryButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="TemplateGalleryEditForm" runat="server">
	<Template>
		<SharePoint:FormToolBar runat="server">
			<template>
				<wssuc:ToolBar id="toolBarTbl" RightButtonSeparator="&nbsp;" runat="server">
					<Template_Buttons>
						<SharePoint:DeleteItemButton runat="server"/>
						<SharePoint:CheckInCheckOutButton runat="server"/>
						<SharePoint:VersionHistoryButton runat="server"/>
					</Template_Buttons>
				</wssuc:ToolBar>
			</template>
		</SharePoint:FormToolBar>
		<SharePoint:FormComponent TemplateName="DocumentLibraryFormCore" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WebPartGalleryEditForm" runat="server">
	<Template>
		<SharePoint:FormToolBar runat="server">
			<template>
				<wssuc:ToolBar id="toolBarTbl" RightButtonSeparator="&nbsp;" runat="server">
					<Template_Buttons>
						<SharePoint:DeleteItemButton runat="server"/>
						<SharePoint:ExportWebPartButton runat="server"/>
						<SharePoint:ViewWebPartXmlButton runat="server"/>
						<SharePoint:ManagePermissionsButton runat="server"/>
					</Template_Buttons>
				</wssuc:ToolBar>
			</template>
		</SharePoint:FormToolBar>
		<SharePoint:FormComponent TemplateName="DocumentLibraryFormCore" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WikiEditForm" runat="server">
	<Template>
		<SharePoint:InformationBar runat="server" />
		<SharePoint:FormToolBar runat="server">
			<template>
					<wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbltop"
							RightButtonSeparator="&nbsp;" runat="server">
						<Template_RightButtons>
							<SharePoint:SaveButton runat="server"/>
							<SharePoint:GoBackButton runat="server"/>
						</Template_RightButtons>
					</wssuc:ToolBar>
				</template>
		</SharePoint:FormToolBar>
		<SharePoint:FormToolBar runat="server" />
		<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" id="formTbl"
			cellspacing="0" width="100%">
			<tr>
				<td valign="top" class="ms-formbody" width="625px">
					<b>
						<SharePoint:FieldLabel FieldName="FileLeafRef" runat="server" />
					</b>&nbsp;
					<SharePoint:WikiFileField FieldName="FileLeafRef" AlternateTemplateName="FileFieldEditNoExtension"
						runat="server" />
				</td>
			</tr>
			<SharePoint:ListFieldIterator TemplateName="WideFieldListIterator" ExcludeFields="FileLeafRef"
				runat="server" />
		</table>
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td class="ms-formline">
					<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
				</td>
			</tr>
		</table>
		<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
			<tr>
				<td width="100%">
					<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
						runat="server">
						<Template_RightButtons>
							<SharePoint:SaveButton runat="server" />
							<SharePoint:GoBackButton runat="server" />
						</Template_RightButtons>
					</wssuc:ToolBar>
					<SharePoint:ItemHiddenVersion runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WideFieldListIterator" runat="server">
	<Template>
		<tr>
			<SharePoint:CompositeField runat="server">
				<template>
					<TD valign="top" class="ms-formbody" width="625px">
					<!-- FieldName="<SharePoint:FieldProperty PropertyName="Title" runat="server"/>"
						 FieldInternalName="<SharePoint:FieldProperty PropertyName="InternalName" runat="server"/>"
						 FieldType="SPField<SharePoint:FieldProperty PropertyName="Type" runat="server"/>"
					  -->
						<b><SharePoint:FieldLabel runat="server"/></b><br>
						<SharePoint:FormField runat="server"/>
						<SharePoint:FieldDescription runat="server"/>
					</TD>
				</template>
			</SharePoint:CompositeField>
		</tr>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="TextField" runat="server">
	<Template>
		<asp:TextBox ID="TextField" MaxLength="255" runat="server" /><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="NoteField" runat="server">
	<Template>
		<asp:TextBox ID="TextField" TextMode="MultiLine" runat="server" /><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="RichTextField" runat="server">
	<Template>
		<span dir="<%$Resources:wss,multipages_direction_dir_value%>" runat="server">
			<asp:TextBox ID="TextField" TextMode="MultiLine" runat="server" />
			<input id="TextField_spSave" type="HIDDEN" name="TextField_spSave" runat="server" />
		</span>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="NumberField" runat="server">
	<Template>
		<asp:TextBox ID="TextField" Size="11" runat="server" /><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="PercentageNumberField" runat="server">
	<Template>
		<asp:TextBox ID="TextField" Size="11" runat="server" />%<br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CurrencyField" runat="server">
	<Template>
		<asp:TextBox ID="TextField" Size="11" runat="server" /><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DateTimeField" runat="server">
	<Template>
		<SharePoint:DateTimeControl ID="DateTimeField" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BooleanField" runat="server">
	<Template>
		<asp:CheckBox ID="BooleanField" runat="server" /><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="UserField" runat="server">
	<Template>
		<input type="hidden" runat="server" id="HiddenUserFieldValue" />
		<SharePoint:PeopleEditor ID="UserField" runat="server" ValidatorEnabled="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="UrlField" runat="server">
	<Template>
		<span class="ms-formdescription">
			<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_type_web_address%>"
				EncodeMethod='HtmlEncode' />&nbsp;(<asp:HyperLink ID="UrlControlId" Target="_self"
					runat="server" />) &nbsp;<br>
		</span>
		<asp:TextBox ID="UrlFieldUrl" dir="ltr" MaxLength="255" runat="server" /><br>
		<span class="ms-formdescription">
			<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,form_type_description%>"
				EncodeMethod='HtmlEncode' />&nbsp;<br>
		</span>
		<asp:TextBox ID="UrlFieldDescription" MaxLength="255" runat="server" /><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FileFieldNew" runat="server">
	<Template>
		<input type="file" class="ms-fileinput" id="onetidIOFile" size="56" runat="Server"><br>
		<asp:RequiredFieldValidator ControlToValidate="onetidIOFile" Display="Dynamic" ErrorMessage="<%$Resources:wss,form_empty_value%>"
			runat="server" />
		</TD></TR>
		<tr id="diidIOUploadMultipleLink">
			<th class="ms-formlabel">
			</th>
			<td style="padding-left: 5px" nowrap>
				<a class="ms-toolbar" accesskey="U" href="javascript:MultipleUploadView()" target="_self">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,upload_document_upload_multiple%>"
						EncodeMethod='HtmlEncode' /></a>
			</td>
		</tr>
		<tr id="trUploadCtl">
			<td width="100%" colspan="3">
				<div id="dividMultipleView" style='display: none'>
					<table cellpadding="0" cellspacing="0" width="100%" height="100%" border="0">
						<tr>
							<td id="idUploadTD" name="idUploadTD" class="ms-uploadborder" width="100%" height="100%">

								<script>
									try {
										if (new ActiveXObject("STSUpld.UploadCtl"))
											document.write("<OBJECT id=idUploadCtl name=idUploadCtl CLASSID=CLSID:07B06095-5687-4d13-9E32-12B4259C9813 WIDTH='100%' HEIGHT='350px'></OBJECT>");
										else
											RemoveMultipleUploadItems();
									}
									catch (error) {
										RemoveMultipleUploadItems();
									}
								</script>

							</td>
							<input type="hidden" name="PostURL" value="<asp:Literal ID='PostURL' runat='server'/>" />
							<input type="hidden" name="Confirmation-URL" value="<asp:Literal ID='ConfirmationURL' runat='server'/>" />
							<input type="hidden" name="destination" value="<asp:Literal ID='destination' runat='server'/>" />
						</tr>
					</table>
				</div>
				<br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FileFieldEdit" runat="server">
	<Template>
		<span dir="ltr">
			<asp:TextBox ID="FileFieldEdit" runat="server" /><asp:Literal ID="FileExtension"
				runat="server" /></span><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FileFieldEditNoExtension" runat="server">
	<Template>
		<asp:TextBox ID="FileFieldEdit" runat="server" /><br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="AttachmentsField" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" id="idAttachmentsTable">
			<asp:Literal ID="AttachmentsList" runat="server" />
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CrossProjectLinkField" runat="server">
	<Template>
		<asp:Panel ID="CrossProjectLinkModifyPanel" runat="server">
			<asp:CheckBox ID="CrossProjectLinkField" Visible="false" runat="server" />
		</asp:Panel>
		<asp:HyperLink ID="ProjectLink" NavigateUrl="" Text="" Target="_self" Visible="false"
			runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="RecurrenceField" runat="server">
	<Template>
		<asp:Label ID="RecurrenceFieldDescription" runat="server" />
		<asp:CheckBox ID="RecurrenceField" AutoPostBack="true" Text="<%$Resources:wss,form_RecurrenceText%>"
			runat="server" />
		<SharePoint:RecurrenceDataControl ID="RecurrenceDataField" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="RecurrenceData" runat="server">
	<Template>
		<table>
			<tr>
				<td class="ms-formbody">
					<table class="ms-formrecurrence" cellspacing="0" cellpadding="0" border="0">
						<tbody>
							<tr>
								<td nowrap rowspan="5">
									<table cellspacing="1" cellpadding="0">
										<tbody>
											<tr>
												<td>
													<table>
														<tr>
															<td>
																<asp:RadioButton ID="recurrencePatternType2" GroupName="RecurrencePatternType" CssClass="ms-radiotext"
																	Text="<%$Resources:wss,recur_daily%>" Value="2" ToolTip="<%$Resources:wss,recur_radio_daily_TT%>"
																	onClick='RecurPatternType_ShowRecurType(this.id);' runat="server" />
															</td>
														</tr>
														<tr>
															<td>
																<asp:RadioButton ID="recurrencePatternType3" GroupName="RecurrencePatternType" CssClass="ms-radiotext"
																	Text="<%$Resources:wss,recur_weekly%>" Value="3" ToolTip="<%$Resources:wss,recur_radio_week_TT%>"
																	onClick='RecurPatternType_ShowRecurType(this.id);' runat="server" />
															</td>
														</tr>
														<tr>
															<td>
																<asp:RadioButton ID="recurrencePatternType4" GroupName="RecurrencePatternType" CssClass="ms-radiotext"
																	Text="<%$Resources:wss,recur_monthly%>" Value="4" ToolTip="<%$Resources:wss,recur_radio_month_TT%>"
																	onClick='RecurPatternType_ShowRecurType(this.id);' runat="server" />
															</td>
														</tr>
														<tr>
															<td>
																<asp:RadioButton ID="recurrencePatternType5" GroupName="RecurrencePatternType" onClick='RecurPatternType_ShowRecurType(this.id);'
																	CssClass="ms-radiotext" Text="<%$Resources:wss,recur_yearly%>" Value="5" ToolTip="<%$Resources:wss,recur_radio_year_TT%>"
																	runat="server" />
															</td>
														</tr>
														<tr>
															<td>
																<asp:RadioButton ID="recurrencePatternType6" GroupName="RecurrencePatternType" onClick='RecurPatternType_ShowRecurType(this.id);'
																	CssClass="ms-radiotext" Text="<%$Resources:wss,recur_custom%>" Value="6" ToolTip="<%$Resources:wss,recur_radio_custom_TT%>"
																	Visible="false" runat="server" />
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
								</td>
								<td nowrap rowspan="5">
									<img src="/_layouts/images/blank.gif" width="40" height="1" alt="">
								</td>
							</tr>
							<div id="idCustomDIV">
								<tr>
									<td valign="top" nowrap>
										<div id="recurPatternTextDiv" disabled>
											<nobr disabled><SPAN class=ms-formdescription> <SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recur_pattern%>" EncodeMethod='HtmlEncode'/></SPAN></nobr>
										</div>
									</td>
								</tr>
								<tr>
									<td valign="top" nowrap height="90" rowspan="4">
										<table class="ms-formrecurrence" cellspacing="0" cellpadding="0" border="0">
											<tbody>
												<tr>
													<td nowrap>
														<img src="/_layouts/images/blank.gif" width="12" height="1" alt="">
													</td>
												</tr>
												<tr>
													<td valign="top" nowrap>
														<div id="recurDailyDiv" style="display: none">
															<table class="ms-formrecurrence" cellspacing="1" cellpadding="0">
																<tbody>
																	<tr onkeypress='RecurType_SetRadioButton(this, "daily_FrequencyType0");' onmousedown='RecurType_SetRadioButton(this,"daily_FrequencyType0");'
																		onmousewheel='RecurType_SetRadioButton(this,"daily_FrequencyType0");' onclick='RecurType_SetRadioButton(this,"daily_FrequencyType0");'>
																		<td valign="top">
																			<asp:RadioButton ID="dailyRecurType0" GroupName="DailyRecurType" Checked="true" runat="server" />
																			<SharePoint:FormattedString ID="RecurDays" FormatText="<%$Resources:wss,RecurDays%>"
																				EncodeMethod="NoEncode" runat="server">
																				<asp:TextBox ID="daily_dayFrequency" CssClass="ms-input" MaxLength="255" ToolTip="<%$Resources:wss,recur_days_between_TT%>"
																					size="3" value="1" runat="server" />
																			</SharePoint:FormattedString>
																		</td>
																	</tr>
																	<tr onkeypress='RecurType_SetRadioButton(this, "daily_FrequencyType1");' onmousedown='RecurType_SetRadioButton(this,"daily_FrequencyType1");'
																		onmousewheel='RecurType_SetRadioButton(this,"daily_FrequencyType1");' onclick='RecurType_SetRadioButton(this,"daily_FrequencyType1");'>
																		<td valign="top">
																			<asp:RadioButton ID="dailyRecurType1" GroupName="DailyRecurType" runat="server" />
																			<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,recur_every_weekdays%>"
																				EncodeMethod='HtmlEncode' />
																		</td>
																	</tr>
																</tbody>
															</table>
														</div>
													</td>
												</tr>
												<tr>
													<td valign="top" nowrap>
														<div id="recurWeeklyDiv" style="display: none">
															<SharePoint:FormattedString ID="RecurWeek" FormatText="<%$Resources:wss,RecurWeek%>"
																EncodeMethod="NoEncode" runat="server">
																<asp:TextBox ID="weekly_weekFrequency" MaxLength="255" CssClass="ms-input" ToolTip="<%$Resources:wss,recur_weeks_between_TT%>"
																	size="3" value="1" runat="server" />
															</SharePoint:FormattedString>
															<div>
																<table class="ms-formrecurrence" cellspacing="1" cellpadding="0">
																	<tbody>
																		<tr>
																			<td>
																				<asp:CheckBoxList ID="weekly_multiDays" CssClass="ms-input" RepeatColumns="4" RepeatDirection="Horizontal"
																					runat="server">
																					<asp:ListItem Text="<%$Resources:wss,day_sun%>" ToolTip="<%$Resources:wss,recur_weeksdays_Su_TT%>"
																						Value="su" />
																					<asp:ListItem Text="<%$Resources:wss,day_mon%>" ToolTip="<%$Resources:wss,recur_weeksdays_Mo_TT%>"
																						Value="mo" />
																					<asp:ListItem Text="<%$Resources:wss,day_tue%>" ToolTip="<%$Resources:wss,recur_weeksdays_Tu_TT%>"
																						Value="tu" />
																					<asp:ListItem Text="<%$Resources:wss,day_wed%>" ToolTip="<%$Resources:wss,recur_weeksdays_We_TT%>"
																						Value="we" />
																					<asp:ListItem Text="<%$Resources:wss,day_thu%>" ToolTip="<%$Resources:wss,recur_weeksdays_Th_TT%>"
																						Value="th" />
																					<asp:ListItem Text="<%$Resources:wss,day_fri%>" ToolTip="<%$Resources:wss,recur_weeksdays_Fr_TT%>"
																						Value="fr" />
																					<asp:ListItem Text="<%$Resources:wss,day_sat%>" ToolTip="<%$Resources:wss,recur_weeksdays_Sa_TT%>"
																						Value="sa" />
																				</asp:CheckBoxList>
																			</td>
																		</tr>
																	</tbody>
																</table>
															</div>
														</div>
													</td>
												</tr>
												<tr>
													<td valign="top" nowrap>
														<div id="recurMonthlyDiv" style="display: none">
															<table class="ms-formrecurrence" cellspacing="1" cellpadding="0">
																<tbody>
																	<tr onkeypress='RecurType_SetRadioButton(this, "monthlyRecurType0");' onmousedown='RecurType_SetRadioButton(this,"monthlyRecurType0");'
																		onmousewheel='RecurType_SetRadioButton(this,"monthlyRecurType0");' onclick='RecurType_SetRadioButton(this,"monthlyRecurType0");'>
																		<td valign="top">
																			<asp:RadioButton ID="monthlyRecurType0" GroupName="MonthlyRecurType" Checked="true"
																				runat="server" />
																		</td>
																		<td class="ms-input" valign="baseline">
																			<nobr>
													<SharePoint:FormattedString id="RecurMonthDay" FormatText="<%$Resources:wss,RecurMonthDay%>" EncodeMethod="NoEncode" runat="server">
														 <asp:TextBox ID="monthly_day" MaxLength="255"
															 CssClass="ms-input"
															 ToolTip="<%$Resources:wss,recur_month_day_TT%>"
															 size=2 value=1 runat="server"/>
														 <asp:TextBox ID="monthly_monthFrequency" MaxLength="255"
															 CssClass="ms-input"
															 ToolTip="<%$Resources:wss,recur_month_monthFrequency_TT%>"
															 size=2 value=1 runat="server"/>
													</SharePoint:FormattedString >
													</nobr>
																		</td>
																	</tr>
																	<tr onkeypress='RecurType_SetRadioButton(this,"monthlyRecurType1");' onmousedown='RecurType_SetRadioButton(this,"monthlyRecurType1");'
																		onmousewheel='RecurType_SetRadioButton(this,"monthlyRecurType1");' onclick='RecurType_SetRadioButton(this,"monthlyRecurType1");'>
																		<td valign="top">
																			<asp:RadioButton ID="monthlyRecurType1" GroupName="MonthlyRecurType" runat="server" />
																		</td>
																		<td class="ms-input" valign="baseline">
																			<nobr>
												<SharePoint:FormattedString id="RecurMonthWeekDay" FormatText="<%$Resources:wss,RecurMonthWeekDay%>" EncodeMethod="NoEncode" runat="server">
												<SPAN class=ms-RadioText VALIGN="TOP" runat=server >
													 <asp:ListBox id="monthlyByDay_weekOfMonth"
														   Rows="1"
															ToolTip="<%$Resources:wss,recur_month_weekOfMonth_TT%>"
														   runat="server">
														 <asp:ListItem Text="<%$Resources:wss,week_first%>" Value="first" Selected=true />
														 <asp:ListItem Text="<%$Resources:wss,week_second%>" Value="second" />
														 <asp:ListItem Text="<%$Resources:wss,week_third%>" Value="third" />
														 <asp:ListItem Text="<%$Resources:wss,week_fourth%>" Value="fourth" />
														 <asp:ListItem Text="<%$Resources:wss,week_last%>" Value="last" />
													  </asp:ListBox>
												</SPAN>
												<SPAN class=ms-RadioText VALIGN="TOP" runat=server >
													 <asp:ListBox id="monthlyByDay_day"
														   Rows="1"
														   ToolTip="<%$Resources:wss,recur_month_dayofweek_TT%>"
														   runat="server">
														 <asp:ListItem Text="<%$Resources:wss,month_day%>" Value="day" />
														 <asp:ListItem Text="<%$Resources:wss,month_weekday%>" Value="weekday" />
														 <asp:ListItem Text="<%$Resources:wss,weekend_day%>" Value="weekend_day" />
														 <asp:ListItem Text="<%$Resources:wss,day_sun%>" Value="su" />
														 <asp:ListItem Text="<%$Resources:wss,day_mon%>" Value="mo" />
														 <asp:ListItem Text="<%$Resources:wss,day_tue%>" Value="tu" />
														 <asp:ListItem Text="<%$Resources:wss,day_wed%>" Value="we" />
														 <asp:ListItem Text="<%$Resources:wss,day_thu%>" Value="th" />
														 <asp:ListItem Text="<%$Resources:wss,day_fri%>" Value="fr" />
														 <asp:ListItem Text="<%$Resources:wss,day_sat%>" Value="sa" />
													  </asp:ListBox>
												  </SPAN>
												  <asp:TextBox ID="monthlyByDay_monthFrequency" MaxLength="255"
															 CssClass="ms-input"
															 ToolTip="<%$Resources:wss,recur_month_monthFrequency_TT%>"
															 size=2 value=1 runat="server"/>
												   </SharePoint:FormattedString >
												  </nobr>
																		</td>
																	</tr>
																</tbody>
															</table>
														</div>
													</td>
												</tr>
												<tr>
													<td valign="top" nowrap>
														<div id="recurYearlyDiv" style="display: none">
															<table class="ms-formrecurrence" cellspacing="1" cellpadding="0">
																<tbody>
																	<tr onkeypress='RecurType_SetRadioButton(this, "yearlyRecurType0");' onmousedown='RecurType_SetRadioButton(this,"yearlyRecurType0");'
																		onmousewheel='RecurType_SetRadioButton(this,"yearlyRecurType0");' onclick='RecurType_SetRadioButton(this,"yearlyRecurType0");'>
																		<td valign="top">
																			<asp:RadioButton ID="yearlyRecurType0" GroupName="YearlyRecurType" Checked="true"
																				runat="server" />
																		</td>
																		<td class="ms-input" valign="baseline">
																			<nobr>
												  <SharePoint:FormattedString id="RecurYearWeekDay" FormatText="<%$Resources:wss,RecurYearWeekDay%>" EncodeMethod="NoEncode" runat="server">
													   <asp:ListBox id="yearly_month"
															 Rows="1"
															 ToolTip="<%$Resources:wss,recur_month_TT%>"
															 runat="server">
														   <asp:ListItem Text="<%$Resources:wss,month_jan%>" Value="1" />
														   <asp:ListItem Text="<%$Resources:wss,month_feb%>" Value="2" />
														   <asp:ListItem Text="<%$Resources:wss,month_mar%>" Value="3" />
														   <asp:ListItem Text="<%$Resources:wss,month_apr%>" Value="4" />
														   <asp:ListItem Text="<%$Resources:wss,month_may%>" Value="5" />
														   <asp:ListItem Text="<%$Resources:wss,month_jun%>" Value="6" />
														   <asp:ListItem Text="<%$Resources:wss,month_jul%>" Value="7" />
														   <asp:ListItem Text="<%$Resources:wss,month_aug%>" Value="8" />
														   <asp:ListItem Text="<%$Resources:wss,month_sep%>" Value="9" />
														   <asp:ListItem Text="<%$Resources:wss,month_oct%>" Value="10" />
														   <asp:ListItem Text="<%$Resources:wss,month_nov%>" Value="11" />
														   <asp:ListItem Text="<%$Resources:wss,month_dec%>" Value="12" />
														</asp:ListBox>
													   <asp:TextBox ID="yearly_day" MaxLength="255"
														   CssClass="ms-input"
														   ToolTip="<%$Resources:wss,recur_year_day_TT%>"
														   size=2 value=1 runat="server"/>
												   </SharePoint:FormattedString >
												  </nobr>
																		</td>
																	</tr>
																	<tr onkeypress='RecurType_SetRadioButton(this,"yearlyRecurType1");' onmousedown='RecurType_SetRadioButton(this,"yearlyRecurType1");'
																		onmousewheel='RecurType_SetRadioButton(this,"yearlyRecurType1");' onclick='RecurType_SetRadioButton(this,"yearlyRecurType1");'>
																		<td valign="top">
																			<asp:RadioButton ID="yearlyRecurType1" GroupName="YearlyRecurType" runat="server" />
																		</td>
																		<td class="ms-input" valign="baseline">
																			<nobr>
												  <SharePoint:FormattedString id="RecurYearMonthDay" FormatText="<%$Resources:wss,RecurYearMonthDay%>" EncodeMethod="NoEncode" runat="server">
												 <SPAN class=ms-RadioText VALIGN="TOP" runat=server >
													<asp:ListBox id="yearlyByDay_weekOfMonth"
														 Rows="1"
														 ToolTip="<%$Resources:wss,recur_month_weekOfMonth_TT%>"
														 runat="server">
														 <asp:ListItem Text="<%$Resources:wss,week_first%>" Value="first" Selected=true />
														 <asp:ListItem Text="<%$Resources:wss,week_second%>" Value="second" />
														 <asp:ListItem Text="<%$Resources:wss,week_third%>" Value="third" />
														 <asp:ListItem Text="<%$Resources:wss,week_fourth%>" Value="fourth" />
														 <asp:ListItem Text="<%$Resources:wss,week_last%>" Value="last" />
													</asp:ListBox>
												 </SPAN>
												 <SPAN class=ms-RadioText VALIGN="TOP" runat=server >
													<asp:ListBox id="yearlyByDay_day"
														 Rows="1"
														 ToolTip="<%$Resources:wss,recur_month_dayofweek_TT%>"
														 runat="server">
														 <asp:ListItem Text="<%$Resources:wss,month_day%>" Value="day" />
														 <asp:ListItem Text="<%$Resources:wss,month_weekday%>" Value="weekday" />
														 <asp:ListItem Text="<%$Resources:wss,weekend_day%>" Value="weekend_day" />
														 <asp:ListItem Text="<%$Resources:wss,day_sun%>" Value="su" />
														 <asp:ListItem Text="<%$Resources:wss,day_mon%>" Value="mo" />
														 <asp:ListItem Text="<%$Resources:wss,day_tue%>" Value="tu" />
														 <asp:ListItem Text="<%$Resources:wss,day_wed%>" Value="we" />
														 <asp:ListItem Text="<%$Resources:wss,day_thu%>" Value="th" />
														 <asp:ListItem Text="<%$Resources:wss,day_fri%>" Value="fr" />
														 <asp:ListItem Text="<%$Resources:wss,day_sat%>" Value="sa" />
													</asp:ListBox>
												 </SPAN>
												 <SPAN class=ms-RadioText VALIGN="TOP" runat=server >
													<asp:ListBox id="yearlyByDay_month"
														 Rows="1"
														 ToolTip="<%$Resources:wss,recur_month_TT%>"
														 runat="server">
														   <asp:ListItem Text="<%$Resources:wss,month_jan%>" Value="1" />
														   <asp:ListItem Text="<%$Resources:wss,month_feb%>" Value="2" />
														   <asp:ListItem Text="<%$Resources:wss,month_mar%>" Value="3" />
														   <asp:ListItem Text="<%$Resources:wss,month_apr%>" Value="4" />
														   <asp:ListItem Text="<%$Resources:wss,month_may%>" Value="5" />
														   <asp:ListItem Text="<%$Resources:wss,month_jun%>" Value="6" />
														   <asp:ListItem Text="<%$Resources:wss,month_jul%>" Value="7" />
														   <asp:ListItem Text="<%$Resources:wss,month_aug%>" Value="8" />
														   <asp:ListItem Text="<%$Resources:wss,month_sep%>" Value="9" />
														   <asp:ListItem Text="<%$Resources:wss,month_oct%>" Value="10" />
														   <asp:ListItem Text="<%$Resources:wss,month_nov%>" Value="11" />
														   <asp:ListItem Text="<%$Resources:wss,month_dec%>" Value="12" />
													</asp:ListBox>
												 </SPAN>
												 </SharePoint:FormattedString >
												</nobr>
																		</td>
																	</tr>
																</tbody>
															</table>
														</div>
													</td>
												</tr>
											</tbody>
										</table>
									</td>
								</tr>
						</tbody>
					</table>
					<div id="recurCustomDiv">
						<table class="ms-formrecurrence" cellspacing="0" cellpadding="0" border="0">
							<tbody>
								<tr>
									<td nowrap>
										<img src="/_layouts/images/blank.gif" width="116" height="1" alt="">
									</td>
									<td valign="top" nowrap>
										<nobr><SPAN class=ms-formdescription><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recur_daterange%>" EncodeMethod='HtmlEncode'/></SPAN><BR><BR><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recur_daterange_startdate%>" EncodeMethod='HtmlEncode'/></nobr>
										<br>
										<nobr> <SharePoint:DateTimeControl ID="windowStart"
								  DateOnly=true
								  IsRequiredField=true
								  ToolTip="<%$Resources:wss,recur_windowStart_TT%>"
								  runat="server"/></nobr>
									</td>
									<td nowrap>
										<img src="/_layouts/images/blank.gif" width="10" height="1" alt="">
									</td>
									<td nowrap>
										<br>
										<br>
										<table class="ms-formrecurrence" cellspacing="1" cellpadding="0">
											<tbody>
												<tr onkeypress='RecurType_SetRadioButton(this,"endDateRangeType0");' onmousedown='RecurType_SetRadioButton(this,"endDateRangeType0");'
													onmousewheel='RecurType_SetRadioButton(this,"endDateRangeType0");' onclick='RecurType_SetRadioButton(this,"endDateRangeType0");'>
													<td valign="top">
														<asp:RadioButton ID="endDateRangeType0" GroupName="EndDateRangeType" ToolTip="<%$Resources:wss,recur_enddaterange_nodate_TT%>"
															runat="server" />
													</td>
													<td class="ms-input" valign="baseline">
														<nobr><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recur_daterange_noenddate%>" EncodeMethod='HtmlEncode'/></nobr>
													</td>
												</tr>
												<tr onkeypress='RecurType_SetRadioButton(this,"endDateRangeType1");' onmousedown='RecurType_SetRadioButton(this,"endDateRangeType1");'
													onmousewheel='RecurType_SetRadioButton(this,"endDateRangeType1");' onclick='RecurType_SetRadioButton(this,"this,endDateRangeType1");'>
													<td valign="top">
														<asp:RadioButton ID="endDateRangeType1" GroupName="EndDateRangeType" ToolTip="<%$Resources:wss,recur_enddaterange_dateafter_TT%>"
															runat="server" />
													</td>
													<td class="ms-input" valign="baseline">
														<nobr><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recur_daterange_endafter%>" EncodeMethod='HtmlEncode'/>
										   <asp:TextBox ID="repeatInstances"
											   MaxLength="255"
											   CssClass="ms-input"
											   ToolTip="<%$Resources:wss,recur_enddaterange_ntimes_TT%>"
											   size=4 value=10 runat="server"/><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recur_daterange_occurs%>" EncodeMethod='HtmlEncode'/>
								  </nobr>
													</td>
												</tr>
												<tr onkeypress='RecurType_SetRadioButton(this,"endDateRangeType2");' onmousedown='RecurType_SetRadioButton(this,"endDateRangeType2");'
													onmousewheel='RecurType_SetRadioButton(this,"endDateRangeType2");' onclick='RecurType_SetRadioButton(this,"endDateRangeType2");'>
													<td valign="top">
														<asp:RadioButton ID="endDateRangeType2" GroupName="EndDateRangeType" ToolTip="<%$Resources:wss,recur_enddaterange_dateby_TT%>"
															runat="server" />
													</td>
													<td class="ms-input" valign="baseline">
														<nobr><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,recur_daterange_endby%>" EncodeMethod='HtmlEncode'/>
									 <SharePoint:DateTimeControl ID="windowEnd"
												DateOnly=true
												ToolTip="<%$Resources:wss,recur_windowEnd_TT%>"
												runat="server"/>
								  </nobr>
													</td>
												</tr>
											</tbody>
										</table>
					</div>
				</td>
			</tr>
			</TBODY>
		</table>
		</DIV> </TD> </TR> </Table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="MultipleLookupField" runat="server">
	<Template>
		<SharePoint:GroupedItemPicker ID="MultiLookupPicker" runat="server" CandidateControlId="SelectCandidate"
			ResultControlId="SelectResult" AddButtonId="AddButton" RemoveButtonId="RemoveButton" />
		<table class="ms-long" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="ms-input">
					<SharePoint:SPHtmlSelect ID="SelectCandidate" Width="143" Height="125" runat="server"
						multiple="true" />
				</td>
				<td style="padding-left: 10px">
					<td align="center" valign="middle" class="ms-input">
						<button class="ms-buttonheightwidth" id="AddButton" runat="server">
							<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,multipages_gip_add%>"
								EncodeMethod='HtmlEncode' />
						</button>
						<br>
						<br>
						<button class="ms-buttonheightwidth" id="RemoveButton" runat="server">
							<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,multipages_gip_remove%>"
								EncodeMethod='HtmlEncode' />
						</button>
					</td>
					<td style="padding-left: 10px">
						<td class="ms-input">
							<SharePoint:SPHtmlSelect ID="SelectResult" Width="143" Height="125" runat="server"
								multiple="true" />
						</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="AllDayEventField" runat="server">
	<Template>
		<asp:Label ID="AllDayEventFieldDescription" runat="server" />
		<asp:CheckBox ID="AllDayEventField" AutoPostBack="true" Text="<%$Resources:wss,form_AlldayEventHelperText%>"
			runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="FormButton" runat="server">
	<Template>
		<wssuc:ToolBarButton ID="FormButton" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="NewFormToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:AttachmentButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BlogNewFormToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BlogEditFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:EditSeriesButton runat="server" />
				<SharePoint:DeleteItemButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="BlogCommentDisplayFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" runat="server" FocusOnToolbar="true">
			<Template_Buttons>
				<SharePoint:EditItemButton runat="server" />
				<SharePoint:EditSeriesButton runat="server" />
				<SharePoint:DeleteItemButton runat="server" />
				<SharePoint:ClaimReleaseTaskButton runat="server" />
				<SharePoint:ManagePermissionsButton runat="server" />
				<SharePoint:ManageCopiesButton runat="server" />
				<SharePoint:ApprovalButton runat="server" />
				<SharePoint:WorkflowsButton runat="server" />
				<SharePoint:AlertMeButton runat="server" />
				<SharePoint:VersionHistoryButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="EditFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:AttachmentButton runat="server" />
				<SharePoint:EditSeriesButton runat="server" />
				<SharePoint:DeleteItemButton runat="server" />
				<SharePoint:ClaimReleaseTaskButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WorkflowEditFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar ID="toolBarTbl" runat="server">
			<Template_Buttons>
				<SharePoint:ClaimReleaseTaskButton runat="server" />
				<SharePoint:DeleteItemButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DisplayFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" runat="server" FocusOnToolbar="true">
			<Template_Buttons>
				<SharePoint:EnterFolderButton runat="server" />
				<SharePoint:NewItemButton runat="server" />
				<SharePoint:EditItemButton runat="server" />
				<SharePoint:EditSeriesButton runat="server" />
				<SharePoint:DeleteItemButton runat="server" />
				<SharePoint:ClaimReleaseTaskButton runat="server" />
				<SharePoint:ManagePermissionsButton runat="server" />
				<SharePoint:ManageCopiesButton runat="server" />
				<SharePoint:ApprovalButton runat="server" />
				<SharePoint:WorkflowsButton runat="server" />
				<SharePoint:AlertMeButton runat="server" />
				<SharePoint:VersionHistoryButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DisplayFormVersionToolBar" runat="server">
	<Template>
		<wssuc:ToolBar ID="toolBarTbl" runat="server" FocusOnToolbar="true">
			<Template_Buttons>
				<SharePoint:DeleteItemVersionButton runat="server" />
				<SharePoint:RestoreItemVersionButton runat="server" />
				<SharePoint:VersionHistoryButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="UnlinkCopyButton" runat="server">
	<Template>
		<asp:LinkButton ID="diidIOUnlinkCopy" OnClientClick="return UnlinkCopyConfirmation();"
			CausesValidation="false" CommandName="UnlinkCopy" Text="<%$Resources:wss,tb_unlink%>"
			AccessKey="<%$Resources:wss,tb_unlink_AK%>" target="_self" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CopySourceInfo" runat="server">
	<Template>
		<SharePoint:CopySourceUrlInfo runat="server" />
		&nbsp;(&nbsp;<SharePoint:GoToCopySourceLink runat="server" />
		&nbsp;|<SharePoint:UnlinkCopyButton runat="server" />
		)

		<script>			var ItemIsCopy = true;</script>

	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="NextPageButton" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,multipages_direction_right_align_value%>' EncodeMethod='HtmlEncode'/>"
					width="100%" nowrap>
					<asp:Button UseSubmitBehavior="false" ID="diidIONextPage" CommandName="NextPage"
						Text="<%$Resources:wss,tb_nextpage%>" class="ms-ButtonHeightWidth" AccessKey="<%$Resources:wss,tb_nextpage_AK%>"
						target="_self" runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="SaveButton" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,multipages_direction_right_align_value%>' EncodeMethod='HtmlEncode'/>"
					width="100%" nowrap>
					<asp:Button UseSubmitBehavior="false" ID="diidIOSaveItem" CommandName="SaveItem"
						Text="<%$Resources:wss,tb_save%>" class="ms-ButtonHeightWidth" AccessKey="<%$Resources:wss,tb_save_AK%>"
						target="_self" runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="SaveAsDraftButton" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,multipages_direction_right_align_value%>' EncodeMethod='HtmlEncode'/>"
					width="100%" nowrap>
					<asp:Button UseSubmitBehavior="false" ID="diidIOSaveItem" CommandName="SaveItem"
						Text="<%$Resources:wss,tb_saveasdraft%>" class="ms-ButtonHeightWidth2" AccessKey="<%$Resources:wss,tb_saveasdraft_AK%>"
						target="_self" runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CancelCloseButton" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,multipages_direction_right_align_value%>' EncodeMethod='HtmlEncode'/>"
					width="100%" nowrap>
					<asp:Button UseSubmitBehavior="false" ID="diidIOGoBack" CommandName="CancelItem"
						class="ms-ButtonHeightWidth" AccessKey="<%$Resources:wss,tb_Cancel_AK%>" target="_self"
						runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:NewMenu AccessKey="<%$Resources:wss,tb_NewMenu_AK%>" runat="server" />
				<SharePoint:ActionsMenu AccessKey="<%$Resources:wss,tb_ActionsMenu_AK%>" runat="server" />
				<SharePoint:SettingsMenu AccessKey="<%$Resources:wss,tb_SettingsMenu_AK%>" runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="MWSViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:MWSNewMenu AccessKey="<%$Resources:wss,tb_NewMenu_AK%>" runat="server" />
				<SharePoint:MWSActionsMenu AccessKey="<%$Resources:wss,tb_ActionsMenu_AK%>" runat="server" />
				<SharePoint:MWSSettingsMenu AccessKey="<%$Resources:wss,tb_SettingsMenu_AK%>" runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:MWSListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="MWSAttendeeViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:MWSActionsMenu AccessKey="<%$Resources:wss,tb_ActionsMenu_AK%>" runat="server" />
				<SharePoint:MWSSettingsMenu AccessKey="<%$Resources:wss,tb_SettingsMenu_AK%>" runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DocumentLibraryViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:NewMenu AccessKey="<%$Resources:wss,tb_NewMenu_AK%>" runat="server" />
				<SharePoint:UploadMenu AccessKey="<%$Resources:wss,tb_UploadMenu_AK%>" runat="server" />
				<SharePoint:ActionsMenu AccessKey="<%$Resources:wss,tb_ActionsMenu_AK%>" runat="server" />
				<SharePoint:SettingsMenu AccessKey="<%$Resources:wss,tb_SettingsMenu_AK%>" runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WikiLibraryViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:NewMenu AccessKey="<%$Resources:wss,tb_NewMenu_AK%>" runat="server" />
				<SharePoint:ActionsMenu AccessKey="<%$Resources:wss,tb_ActionsMenu_AK%>" runat="server" />
				<SharePoint:SettingsMenu AccessKey="<%$Resources:wss,tb_SettingsMenu_AK%>" runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="PictureLibraryViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:NewMenu AccessKey="<%$Resources:wss,tb_NewMenu_AK%>" runat="server" />
				<SharePoint:UploadMenu AccessKey="<%$Resources:wss,tb_UploadMenu_AK%>" runat="server" />
				<SharePoint:ActionsMenu AccessKey="<%$Resources:wss,tb_ActionsMenu_AK%>" TemplateName="ToolbarActionsMenuForPictureLibrary"
					runat="server" />
				<SharePoint:SettingsMenu AccessKey="<%$Resources:wss,tb_SettingsMenu_AK%>" runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="WebPartGalleryViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:SPLinkButton ID="New0" Text="<%$Resources:wss,tb_new%>" ToolTip="<%$Resources:wss,tb_new%>"
					AccessKey="<%$Resources:wss,tb_new_ak%>" ImageUrl="/_layouts/images/newitem.gif"
					NavigateUrl="~site/_layouts/NewDwp.aspx" ShowImageAndText="true" HoverCellActiveCssClass="ms-menubuttonactivehover"
					HoverCellInActiveCssClass="ms-menubuttoninactivehover" PermissionsString="AddListItems"
					PermissionContext="CurrentList" runat="server" />
				<SharePoint:UploadMenu runat="server" />
				<SharePoint:GlobalGalleryActionsMenu runat="server" />
				<SharePoint:SettingsMenu runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="RelinkToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:RelinkButton ID="diidRepairItems" Text="<%$Resources:wss,tb_relink%>"
					AccessKey="<%$Resources:wss,tb_relinkAK%>" ImageUrl="/_layouts/images/relink.gif"
					HoverCellActiveCssClass="ms-menubuttonactivehover" HoverCellInActiveCssClass="ms-menubuttoninactivehover"
					runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="MergeToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:MergeButton ID="diidCombineItems" Text="<%$Resources:wss,tb_merge%>"
					AccessKey="<%$Resources:wss,tb_mergeAK%>" ImageUrl="/_layouts/images/merge.gif"
					HoverCellActiveCssClass="ms-menubuttonactivehover" HoverCellInActiveCssClass="ms-menubuttoninactivehover"
					runat="server" />
				<SharePoint:NewMenu AccessKey="<%$Resources:wss,tb_NewMenu_AK%>" runat="server" />
				<SharePoint:ActionsMenu AccessKey="<%$Resources:wss,tb_ActionsMenu_AK%>" runat="server" />
				<SharePoint:SettingsMenu runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="SiteListGalleryViewToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-menutoolbar" EnableViewState="false" ID="toolBarTbl"
			ButtonSeparator="<img src='/_layouts/images/blank.gif' alt=''>" RightButtonSeparator="&nbsp;&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:UploadMenu runat="server" />
				<SharePoint:GlobalGalleryActionsMenu runat="server" />
				<SharePoint:SettingsMenu runat="server" />
			</Template_Buttons>
			<Template_RightButtons>
				<SharePoint:PagingButton runat="server" />
				<SharePoint:ListViewSelector runat="server" />
			</Template_RightButtons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ToolbarUploadMenu" runat="server">
	<Template>
		<SharePoint:FeatureMenuTemplate runat="server" FeatureScope="Site" Location="Microsoft.SharePoint.StandardMenu"
			GroupId="UploadMenu" UseShortId="true">
			<SharePoint:MenuItemTemplate ID="Upload" Text="<%$Resources:wss,ToolBarMenuItemUpload%>"
				Description="<%$Resources:wss,ToolBarMenuItemUploadDescription%>" Sequence="100"
				ImageUrl="/_layouts/images/MenuUploadDocument.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="MultipleUpload" Text="<%$Resources:wss,ToolBarMenuItemMuliUpload%>"
				Description="<%$Resources:wss,ToolBarMenuItemMuliUploadDescription%>" Sequence="200"
				ImageUrl="/_layouts/images/MenuUploadMultiple.gif" HiddenScript="!browseris.ie55up"
				UseShortId="true" runat="server" />
		</SharePoint:FeatureMenuTemplate>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ToolbarActionsMenu" runat="server">
	<Template>
		<SharePoint:FeatureMenuTemplate runat="server" FeatureScope="Site" Location="Microsoft.SharePoint.StandardMenu"
			GroupId="ActionsMenu" UseShortId="true">
			<SharePoint:MenuItemTemplate ID="EditInGridButton" ImageUrl="/_layouts/images/menudatasheet.gif"
				PermissionsString="UseRemoteAPIs" MenuGroupId="200" Sequence="200" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="OpenInExplorer" Text="<%$Resources:wss,ToolBarMenuItemOpenInExplorer%>"
				Description="<%$Resources:wss,ToolBarMenuItemOpenInExplorerDescription%>" PermissionsString="UseClientIntegration"
				PermissionContext="CurrentList" MenuGroupId="200" Sequence="300" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="ChangeOrder" Text="<%$Resources:wss,ToolBarMenuItemChangeOrder%>"
				Description="<%$Resources:wss,ToolBarMenuItemChangeOrderDescription%>" MenuGroupId="200"
				Sequence="400" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="ShowInStdViewButton" Text="<%$Resources:wss,ToolBarMenuItemShowInStdView%>"
				Description="<%$Resources:wss,ToolBarMenuItemShowInStdViewDescription%>" MenuGroupId="300"
				Sequence="100" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="NewRowButton" Text="<%$Resources:wss,ToolBarMenuItemNewRow%>"
				Description="<%$Resources:wss,ToolBarMenuItemNewRowDescription%>" PermissionsString="AddListItems"
				PermissionContext="CurrentList" MenuGroupId="300" Sequence="200" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="TaskPaneButton" Text="<%$Resources:wss,ToolBarMenuItemTaskPane%>"
				Description="<%$Resources:wss,ToolBarMenuItemTaskPaneDescription%>" MenuGroupId="300"
				Sequence="300" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="TotalsButton" Text="<%$Resources:wss,ToolBarMenuItemTotals%>"
				Description="<%$Resources:wss,ToolBarMenuItemTotalsDescription%>" MenuGroupId="300"
				Sequence="400" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="RefreshDataButton" Text="<%$Resources:wss,ToolBarMenuItemRefreshData%>"
				Description="<%$Resources:wss,ToolBarMenuItemRefreshDataDescription%>" MenuGroupId="300"
				Sequence="500" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="OfflineButton" Description="<%$Resources:wss,ToolBarWorkOfflineOutlookDescription%>"
				PermissionsString="UseClientIntegration" PermissionContext="CurrentList" MenuGroupId="400"
				Sequence="100" ImageUrl="/_layouts/images/MenuPIM.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="ExportToSpreadsheet" Text="<%$Resources:wss,ToolBarMenuItemExportToSpreadsheet%>"
				Description="<%$Resources:wss,ToolBarMenuItemExportToSpreadsheetDescription%>"
				PermissionsString="UseClientIntegration" PermissionContext="CurrentList" MenuGroupId="400"
				Sequence="200" ImageUrl="/_layouts/images/MenuSpreadsheet.gif" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="ExportToDatabase" PermissionsString="UseClientIntegration"
				PermissionContext="CurrentList" MenuGroupId="400" Sequence="300" ImageUrl="/_layouts/images/MenuDatabase.gif"
				UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="DiagramButton" Description="<%$Resources:wss,ToolBarCreateDiagramDescription%>"
				PermissionsString="UseClientIntegration" PermissionContext="CurrentList" MenuGroupId="400"
				Sequence="500" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="ViewRSS" Text="<%$Resources:wss,ToolBarMenuItemViewRSSFeed%>"
				Description="<%$Resources:wss,ToolBarMenuItemViewRSSFeedDescription%>" MenuGroupId="500"
				Sequence="400" ImageUrl="/_layouts/images/MenuRSS.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="SubscribeButton" Text="<%$Resources:wss,ToolBarMenuItemAlertMe%>"
				Description="<%$Resources:wss,ToolBarMenuItemAlertMeDescription%>" PermissionsString="CreateAlerts"
				PermissionContext="CurrentList" PermissionMode="Any" MenuGroupId="500" Sequence="500"
				ImageUrl="/_layouts/images/MenuAlert.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="AddToMyLinksButton" Text="<%$Resources:wss,ToolBarMenuItemAddToMyLinks%>"
				MenuGroupId="500" Sequence="600" UseShortId="true" runat="server" />
		</SharePoint:FeatureMenuTemplate>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ToolbarActionsMenuForPictureLibrary" runat="server">
	<Template>
		<SharePoint:FeatureMenuTemplate runat="server" FeatureScope="Site" Location="Microsoft.SharePoint.StandardMenu"
			GroupId="ActionsMenu" UseShortId="true">
			<SharePoint:MenuItemTemplate ID="EditPictures" Text="<%$Resources:wss,ToolBarMenuItemEditPicutres%>"
				Description="<%$Resources:wss,ToolBarMenuItemEditPicutresDescription%>" MenuGroupId="100"
				Sequence="100" PermissionsString="EditListItems,UseClientIntegration" PermissionContext="CurrentList"
				ImageUrl="/_layouts/images/MenuEditPictures.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="DeletePictures" Text="<%$Resources:wss,ToolBarMenuItemDeletePicutres%>"
				Description="<%$Resources:wss,ToolBarMenuItemDeletePicutresDescription%>" PermissionsString="DeleteListItems"
				PermissionContext="CurrentList" MenuGroupId="100" Sequence="200" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="DownloadPictures" Text="<%$Resources:wss,ToolBarMenuItemDownloadPicutres%>"
				Description="<%$Resources:wss,ToolBarMenuItemDownloadPicutresDescription%>" PermissionsString="UseClientIntegration"
				PermissionContext="CurrentList" MenuGroupId="100" Sequence="300" ImageUrl="/_layouts/images/MenuDownload.gif"
				UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="SendPictures" Text="<%$Resources:wss,ToolBarMenuItemSendPicutres%>"
				Description="<%$Resources:wss,ToolBarMenuItemSendPicutresDescription%>" PermissionsString="UseClientIntegration"
				PermissionContext="CurrentList" MenuGroupId="100" Sequence="400" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="ViewSlideShow" Text="<%$Resources:wss,ToolBarMenuItemViewSlideShow%>"
				Description="<%$Resources:wss,ToolBarMenuItemViewSlideShowDescription%>" MenuGroupId="200"
				Sequence="100" ImageUrl="/_layouts/images/MenuSlideShow.gif" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="OpenInExplorer" Text="<%$Resources:wss,ToolBarMenuItemOpenInExplorer%>"
				Description="<%$Resources:wss,ToolBarMenuItemOpenInExplorerDescription%>" PermissionsString="UseClientIntegration"
				PermissionContext="CurrentList" MenuGroupId="200" Sequence="300" UseShortId="true"
				runat="server" />
			<SharePoint:MenuItemTemplate ID="OfflineButton" Description="<%$Resources:wss,ToolBarWorkOfflineOutlookDescription%>"
				PermissionsString="UseClientIntegration" PermissionContext="CurrentList" MenuGroupId="400"
				Sequence="100" ImageUrl="/_layouts/images/MenuPIM.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="ViewRSS" Text="<%$Resources:wss,ToolBarMenuItemViewRSSFeed%>"
				Description="<%$Resources:wss,ToolBarMenuItemViewRSSFeedDescription%>" MenuGroupId="500"
				Sequence="100" ImageUrl="/_layouts/images/MenuRSS.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="SubscribeButton" Text="<%$Resources:wss,ToolBarMenuItemAlertMe%>"
				Description="<%$Resources:wss,ToolBarMenuItemAlertMeDescription%>" PermissionsString="CreateAlerts"
				PermissionContext="CurrentList" PermissionMode="Any" MenuGroupId="500" Sequence="200"
				ImageUrl="/_layouts/images/MenuAlert.gif" UseShortId="true" runat="server" />
		</SharePoint:FeatureMenuTemplate>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ToolbarActionsMenuForSurvey" runat="server">
	<Template>
		<SharePoint:FeatureMenuTemplate runat="server" FeatureScope="Site" Location="Microsoft.SharePoint.StandardMenu"
			GroupId="ActionsMenuForSurvey" UseShortId="true">
			<SharePoint:MenuItemTemplate ID="ExportToSpreadsheet" Text="<%$Resources:wss,ToolBarMenuItemExportToSpreadsheet%>"
				Description="<%$Resources:wss,ToolBarMenuItemExportToSpreadsheetDescription%>"
				PermissionsString="UseClientIntegration" PermissionContext="CurrentList" MenuGroupId="800"
				Sequence="100" ImageUrl="/_layouts/images/MenuSpreadsheet.gif" UseShortId="true"
				runat="server" />
			<SharePoint:MenuSeparatorTemplate MenuGroupId="800" Sequence="300" runat="server" />
			<SharePoint:MenuItemTemplate ID="ViewRSS" Text="<%$Resources:wss,ToolBarMenuItemViewRSSFeed%>"
				Description="<%$Resources:wss,ToolBarMenuItemViewRSSFeedDescription%>" MenuGroupId="800"
				Sequence="400" ImageUrl="/_layouts/images/MenuRSS.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="SubscribeButton" Text="<%$Resources:wss,ToolBarMenuItemAlertMe%>"
				Description="<%$Resources:wss,ToolBarMenuItemAlertMeDescription%>" PermissionsString="CreateAlerts"
				PermissionContext="CurrentList" PermissionMode="Any" MenuGroupId="800" Sequence="500"
				ImageUrl="/_layouts/images/MenuAlert.gif" UseShortId="true" runat="server" />
		</SharePoint:FeatureMenuTemplate>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ToolbarSettingsMenu" runat="server">
	<Template>
		<SharePoint:FeatureMenuTemplate runat="server" FeatureScope="Site" Location="Microsoft.SharePoint.StandardMenu"
			GroupId="SettingsMenu" UseShortId="true">
			<SharePoint:MenuItemTemplate ID="AddColumn" Text="<%$Resources:wss,ToolBarMenuItemAddColumn%>"
				Description="<%$Resources:wss,ToolBarMenuItemAddColumnDescription%>" PermissionsString="ManageLists"
				PermissionContext="CurrentList" PermissionMode="Any" MenuGroupId="100" Sequence="100"
				ImageUrl="/_layouts/images/MenuAddColumn.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="AddView" Text="<%$Resources:wss,ToolBarMenuItemAddView%>"
				Description="<%$Resources:wss,ToolBarMenuItemAddViewDescription%>" PermissionsString="ManageLists"
				PermissionContext="CurrentList" PermissionMode="Any" MenuGroupId="100" Sequence="300"
				ImageUrl="/_layouts/images/MenuAddView.gif" UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="ListSettings" PermissionsString="ManageLists" PermissionContext="CurrentList"
				PermissionMode="Any" ImageUrl="/_layouts/images/MenuListSettings.gif" MenuGroupId="200"
				Sequence="400" UseShortId="true" runat="server" />
		</SharePoint:FeatureMenuTemplate>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ToolbarSettingsMenuForSurvey" runat="server">
	<Template>
		<SharePoint:FeatureMenuTemplate runat="server" FeatureScope="Site" Location="Microsoft.SharePoint.StandardMenu"
			GroupId="SettingsMenuForSurvey" UseShortId="true">
			<SharePoint:MenuItemTemplate ID="AddQuestions" Text="<%$Resources:wss,ToolBarMenuItemAddQuestions%>"
				Description="<%$Resources:wss,ToolBarMenuItemAddQuestionsDescription%>" PermissionsString="ManageLists"
				PermissionContext="CurrentList" PermissionMode="Any" MenuGroupId="100" Sequence="100"
				UseShortId="true" runat="server" />
			<SharePoint:MenuItemTemplate ID="ListSettings" Description="<%$Resources:wss,ToolBarMenuItemSurveySettingsDescription%>"
				PermissionsString="ManageLists" PermissionContext="CurrentList" PermissionMode="Any"
				MenuGroupId="200" Sequence="500" ImageUrl="/_layouts/images/MenuListSettings.gif"
				UseShortId="true" runat="server" />
		</SharePoint:FeatureMenuTemplate>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="PagingButton" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" style='margin-right: 4px'>
			<tr>
				<td nowrap class="ms-toolbar" id="topPagingCell<asp:Literal ID='WebPartQualifier' runat='server'/>">
					<td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="ViewSelector" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" style='margin-right: 4px'>
			<tr>
				<td nowrap class="ms-listheaderlabel">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,view_selector_view%>"
						EncodeMethod='HtmlEncode' />&nbsp;
				</td>
				<td nowrap class="ms-viewselector" id="onetViewSelector" onmouseover="this.className='ms-viewselectorhover'"
					onmouseout="this.className='ms-viewselector'" runat="server">
					<SharePoint:ViewSelectorMenu MenuAlignment="Right" AlignToParent="true" runat="server"
						ID="ViewSelectorMenu" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="AllContentViewSelector" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" style='margin-right: 4px'>
			<tr>
				<td nowrap class="ms-listheaderlabel">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,view_selector_view%>"
						EncodeMethod='HtmlEncode' />&nbsp;
				</td>
				<td nowrap class="ms-viewselector" id="onetViewSelector" onmouseover="this.className='ms-viewselectorhover'"
					onmouseout="this.className='ms-viewselector'" runat="server">
					<SharePoint:AllContentsViewSelectorMenu MenuAlignment="Right" AlignToParent="true"
						runat="server" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="MWSViewSelector" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" style='margin-right: 4px'>
			<tr>
				<td nowrap class="ms-listheaderlabel">
					<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,view_selector_view%>"
						EncodeMethod='HtmlEncode' />&nbsp;
				</td>
				<td nowrap class="ms-viewselector">
					<SharePoint:MWSViewSelectorMenu runat="server" ID="MWSViewSelectorMenu" />
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DistributionListsDisplayFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" runat="server" FocusOnToolbar="true">
			<Template_Buttons>
				<SharePoint:EditItemButton runat="server" />
				<SharePoint:DistributionListsApprovalButton runat="server" />
				<SharePoint:WorkflowsButton runat="server" />
				<SharePoint:AlertMeButton runat="server" />
				<SharePoint:VersionHistoryButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="DistributionListsEditFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:DistributionListsApprovalButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewmonthChrome" runat="server">
	<Template>
		<div id="MontlyViewDefault_CalendarView">
			<%--			<input type="hidden" id="ExpandedWeeksId" name="ExpandedWeeks" value="">
			<table border="0" width="100%" id="CalViewTable1" style="border-collapse: collapse"
				cellpadding="0">
				<tr>
					<td class="ms-calheader">
						<img src="/_layouts/images/blank.gif" width="742" height="1" alt="">
					</td>
				</tr>
				<tr>
					<td class="ms-calheader">
						<table border="0" width="100%" cellspacing="1" cellpadding="0" id="CalViewTable12"
							style="border-collapse: collapse">
							<tr>
								<td nowrap>
									<div class="ms-cal-navheader" nowrap>
										<a href="javascript:MoveToViewDate('<%# DataBinder.Eval(Container,"PreviousDate","") %>', null);"
											tabindex="1" style="visibility: <%# DataBinder.Eval(Container,"PreviousDateVisible","")%>"
											accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prev_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/prevbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prevmonth%>' EncodeMethod='HtmlEncode'/>"></a>
										<a href="javascript:MoveToViewDate('<%# DataBinder.Eval(Container,"NextDate","") %>', null);"
											tabindex="1" style="visibility: <%# DataBinder.Eval(Container,"NextDateVisible","")%>"
											accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_next_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/nextbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_nextmonth%>' EncodeMethod='HtmlEncode'/>"></a>
										&nbsp;<%# DataBinder.Eval(Container,"HeaderDate","") %>&nbsp;
									</div>
								</td>
								<td>
									&nbsp;
								</td>
								<td class="ms-cal-nav-buttons<%# DataBinder.Eval(Container,"Direction","")%>">
									<span id="ExpandAllId" dir="<%# DataBinder.Eval(Container,"Direction","")%>"><a href="javascript:"
										class="ms-cal-nav" onclick="javascript:GetMonthView('1111111');return false;"
										tabindex="1" accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_expandall_AK%>' EncodeMethod='HtmlEncode'/>"
										nowrap>
										<nobr><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,calendar_expandall%>" EncodeMethod='HtmlEncode'/></nobr>
									</a></span>&nbsp; <span id="CollapseAllId" dir="<%# DataBinder.Eval(Container,"Direction","")%>">
										<a href="javascript:" class="ms-cal-nav" onclick="javascript:GetMonthView('0000000');return false;"
											tabindex="1" accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_collapseall_AK%>' EncodeMethod='HtmlEncode'/>"
											nowrap>
											<nobr><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,calendar_collapseall%>" EncodeMethod='HtmlEncode'/></nobr>
										</a></span><span>&nbsp;|</span>
												<SharePoint:SPCalendarTabs runat="server" SelectedViewTab='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewType","")) %>'
										ListName='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ListName","")) %>'
										ViewGuid='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewName","")) %>'>
									</SharePoint:SPCalendarTabs>
											</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
			--%>
			<cfb:CPMonthlyCalendarView runat="server" SelectedDate='<%# DataBinder.Eval(Container,"SelectedDate","") %>'
				ExpandedWeeks='<%# SPHttpUtility.HtmlEncode( DataBinder.Eval(Container,"ExpandedWeeks","")) %>'
				ItemTemplateName="CalendarViewMonthItemTemplate" ItemAllDayTemplateName="CalendarViewMonthItemAllDayTemplate"
				ItemMultiDayTemplateName="CalendarViewMonthItemMultiDayTemplate" TabIndex="2">
			</cfb:CPMonthlyCalendarView>
			<%--					</td>
				</tr>
			</table>
			--%>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewweekChrome" runat="server">
	<Template>
		<div id="WeeklyViewDefault_CalendarView" style="display: block; overflow: auto; width: <%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ChromeWidth",""))%>;
			height: <%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ChromeHeight",""))%>;"
			dir="<%# DataBinder.Eval(Container,"Direction","")%>">
			<table border="0" width="100%" id="CalViewTable1" style="border-collapse: collapse"
				cellpadding="0">
				<tr>
					<td class="ms-calheader">
						<img src="/_layouts/images/blank.gif" width="742" height="1" alt="">
					</td>
				</tr>
				<tr>
					<td class="ms-calheader">
						<table border="0" width="100%" cellspacing="1" cellpadding="0" id="CalViewTable12"
							style="border-collapse: collapse">
							<tr>
								<td nowrap>
									<div class="ms-cal-navheader" nowrap>
										<a href="javascript:MoveToDate('<%# DataBinder.Eval(Container,"PreviousDate","") %>');"
											tabindex="1" style="visibility: <%# DataBinder.Eval(Container,"PreviousDateVisible","")%>"
											accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prev_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/prevbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prevweek%>' EncodeMethod='HtmlEncode'/>"></a>
										<a href="javascript:MoveToDate('<%# DataBinder.Eval(Container,"NextDate","") %>');"
											tabindex="1" style="visibility: <%# DataBinder.Eval(Container,"NextDateVisible","")%>"
											accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_next_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/nextbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_nextweek%>' EncodeMethod='HtmlEncode'/>"></a>
										&nbsp;<%# DataBinder.Eval(Container,"HeaderDate","") %>&nbsp;
									</div>
								</td>
								<td>
									&nbsp;
								</td>
								<td class="ms-cal-nav-buttons<%# DataBinder.Eval(Container,"Direction","")%>">
									<SharePoint:SPCalendarTabs runat="server" SelectedViewTab='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewType","")) %>'
										ListName='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ListName","")) %>'
										ViewGuid='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewName","")) %>'>
									</SharePoint:SPCalendarTabs>
								</>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<SharePoint:WeeklyCalendarView runat="server" SelectedDate='<%# DataBinder.Eval(Container,"SelectedDate","") %>'
							ItemTemplateName="CalendarViewWeekItemTemplate" ItemAllDayTemplateName="CalendarViewWeekItemAllDayTemplate"
							ItemMultiDayTemplateName="CalendarViewWeekItemMultiDayTemplate" TabIndex="2">
						</SharePoint:WeeklyCalendarView>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewdayChrome" runat="server">
	<Template>
		<div id="DailyViewDefault_CalendarView" style="display: block; overflow: auto; width: <%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ChromeWidth",""))%>;
			height: <%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ChromeHeight",""))%>;"
			dir="<%# DataBinder.Eval(Container,"Direction","")%>">
			<table border="0" width="100%" id="CalViewTable1" style="border-collapse: collapse"
				cellpadding="0">
				<tr>
					<td class="ms-calheader">
						<img src="/_layouts/images/blank.gif" width="742" height="1" alt="">
					</td>
				</tr>
				<tr>
					<td class="ms-calheader">
						<table border="0" width="100%" cellspacing="1" cellpadding="0" id="CalViewTable12"
							style="border-collapse: collapse">
							<tr>
								<td nowrap>
									<div class="ms-cal-navheader" nowrap>
										<a href="javascript:MoveToDate('<%# DataBinder.Eval(Container,"PreviousDate","") %>');"
											tabindex="1" style="visibility: <%# DataBinder.Eval(Container,"PreviousDateVisible","")%>"
											accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prev_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/prevbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prevday%>' EncodeMethod='HtmlEncode'/>"></a>
										<a href="javascript:MoveToDate('<%# DataBinder.Eval(Container,"NextDate","") %>');"
											tabindex="1" style="visibility: <%# DataBinder.Eval(Container,"NextDateVisible","")%>"
											accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_next_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/nextbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_nextday%>' EncodeMethod='HtmlEncode'/>"></a>
										&nbsp;<%# DataBinder.Eval(Container,"HeaderDate","") %>&nbsp;
									</div>
								</td>
								<td>
									&nbsp;
								</td>
								<td class="ms-cal-nav-buttons<%# DataBinder.Eval(Container,"Direction","")%>">
									<SharePoint:SPCalendarTabs runat="server" SelectedViewTab='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewType","")) %>'
										ListName='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ListName","")) %>'
										ViewGuid='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewName","")) %>'>
									</SharePoint:SPCalendarTabs>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<SharePoint:DailyCalendarView runat="server" SelectedDate='<%# DataBinder.Eval(Container,"SelectedDate","") %>'
							ItemTemplateName="CalendarViewDayItemTemplate" ItemAllDayTemplateName="CalendarViewDayItemAllDayTemplate"
							ItemMultiDayTemplateName="CalendarViewDayItemMultiDayTemplate">
						</SharePoint:DailyCalendarView>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewcustomChrome" runat="server">
	<Template>
		<input type="hidden" id="ExpandedWeeksId" name="ExpandedWeeks" value="">
		<div id="CustomViewDefault_CalendarView" style="display: block; overflow: auto; width: <%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ChromeWidth",""))%>;
			height: <%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ChromeHeight",""))%>;"
			dir="<%# DataBinder.Eval(Container,"Direction","")%>">
			<table border="0" width="100%" id="CalViewTable1" bgcolor="#D2D2DF" style="border-collapse: collapse"
				cellpadding="0">
				<tr>
					<td class="ms-calheader">
						<table border="0" width="100%" cellspacing="1" cellpadding="0" id="CalViewTable12"
							style="border-collapse: collapse">
							<tr>
								<td nowrap>
									<div class="ms-cal-navheader" nowrap>
										<a href="javascript:MoveToDate('<%# DataBinder.Eval(Container,"PreviousDate","") %>');"
											style="visibility: <%# DataBinder.Eval(Container,"PreviousDateVisible","")%>"
											accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prev_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/prevbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_prevmonth%>' EncodeMethod='HtmlEncode'/>"></a>
										<a href="javascript:MoveToDate('<%# DataBinder.Eval(Container,"NextDate","") %>');"
											style="visibility: <%# DataBinder.Eval(Container,"NextDateVisible","")%>" accesskey="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_next_AK%>' EncodeMethod='HtmlEncode'/>">
											<img border="0" src="/_layouts/images/nextbutton<%# DataBinder.Eval(Container,"Direction","")%>.gif"
												width="15" height="15" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_nextmonth%>' EncodeMethod='HtmlEncode'/>"></a>
										&nbsp;<%# DataBinder.Eval(Container,"HeaderDate","") %>&nbsp;
									</div>
								</td>
								<td>
									&nbsp;
								</td>
								<td class="ms-cal-nav-buttons<%# DataBinder.Eval(Container,"Direction","")%>">
									<span id="ExpandAllId"><a href="javascript:" class="ms-cal-more" onclick="javascript:GetMonthView('1111111');return false;">
										<nobr><img border=0 src="/_layouts/images/expandbttn.gif" width=9 height=9 tabindex=1 alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_expandall%>' EncodeMethod='HtmlEncode'/>" >&nbsp;<SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,calendar_expandall%>" EncodeMethod='HtmlEncode'/>&nbsp;</nobr>
									</a></span><span id="CollapseAllId"><a href="javascript:" class="ms-cal-more" onclick="javascript:GetMonthView('0000000');return false;">
										<nobr><img border=0 src="/_layouts/images/collapsebttn.gif" width=9 height=9 tabindex=1 alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,calendar_collapseall%>' EncodeMethod='HtmlEncode'/>" >&nbsp;<SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,calendar_collapseall%>" EncodeMethod='HtmlEncode'/>&nbsp;</nobr>
									</a></span>
								</td>
								<td>
									<td class="ms-cal-nav-buttons<%# DataBinder.Eval(Container,"Direction","")%>">
										<SharePoint:SPCalendarTabs runat="server" SelectedViewTab='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewType","") )%>'
											ListName='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ListName","")) %>'
											ViewGuid='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"ViewName","")) %>'>
										</SharePoint:SPCalendarTabs>
									</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="1" cellpadding="0" id="CalViewTable12" style="border-collapse: collapse">
							<tr height="100%">
								<td>
									&nbsp;
								</td>
								<td height="100%" width="50%">
									<SharePoint:MonthlyCalendarView runat="server" SelectedDate='<%# DataBinder.Eval(Container,"SelectedDate","") %>'
										ItemTemplateName="CalendarViewMonthItemTemplate" ItemAllDayTemplateName="CalendarViewMonthtemAllDayTemplate"
										ItemMultiDayTemplateName="CalendarViewMonthItemMultiDayTemplate">
									</SharePoint:MonthlyCalendarView>
								</td>
								<td height="100%" width="50%">
									<SharePoint:DailyCalendarView runat="server" SelectedDate='<%# DataBinder.Eval(Container,"SelectedDate","") %>'
										ItemTemplateName="CalendarViewDayItemTemplate" ItemAllDayTemplateName="CalendarViewDayItemAllDayTemplate"
										ItemMultiDayTemplateName="CalendarViewDayItemMultiDayTemplate">
									</SharePoint:DailyCalendarView>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewItemTemplate" runat="server">
	<Template>
		<div dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<a onfocus="OnLink(this)" align="center" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
				onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
				<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title",""))%>
			</a>
		</div>
		<br>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewMonthItemTemplate" runat="server">
	<Template>
		<table border="0" cellspacing="0" cellpadding="0" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<tr>
				<td class="ms-cal-monthitem">
					<a onfocus="OnLink(this)" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
						onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
						<nobr><b><%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DateTimeString","{0:G}"))%></b></nobr>
						<br>
						<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%>
					</a>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewMonthItemAllDayTemplate" runat="server">
	<Template>
		<div class="<%# DataBinder.Eval(Container,"DivClass","")%>" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
				<tr>
					<td class="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>"
						onmouseover="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>sel';"
						onmouseout="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>';"
						href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
						onclick="GoToLink(this);return false;" target="_self">
						<a onfocus="OnLink(this)" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
							onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
							<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%>
						</a>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewMonthItemMultiDayTemplate" runat="server">
	<Template>
		<div class="<%# DataBinder.Eval(Container,"DivClass","")%>" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
				<tr>
					<td class="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>"
						onmouseover="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>sel';"
						onmouseout="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>';"
						href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
						onclick="GoToLink(this);return false;" target="_self">
						<a onfocus="OnLink(this)" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
							onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
							<b>
								<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%></b>
						</a>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewDayItemTemplate" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0" border="0" class="ms-cal-tdayitem" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<tr>
				<td valign="top" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
					onclick="GoToLink(this);return false;" target="_self">
					<div>
						<img src="/_layouts/images/blank.gif" width="50" height="1" alt=""><br>
						<img src="/_layouts/images/recursml<%# DataBinder.Eval(Container,"ExceptionExtension","")%>.gif"
							class="<%# DataBinder.Eval(Container,"RecurrenceIconVisible")%>" alt="<%# DataBinder.Eval(Container,"ToolTip","")%>"
							align="absmiddle" />
						<a onfocus="OnLink(this)" class="ms-cal-dayitem" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
							onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
							<nobr><%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DateTimeString","{0:G}"))%></nobr>
							<%# DataBinder.Eval(Container,"MultiLineBreak","{0:G}")%>
							<b>
								<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%></b>
							<%# DataBinder.Eval(Container,"MultiLineBreak","{0:G}")%>
							<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Location","{0:G}"))%>
						</a>
					</div>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewDayItemAllDayTemplate" runat="server">
	<Template>
		<div class="<%# DataBinder.Eval(Container,"DivClass","")%>" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
				<tr>
					<td class="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>"
						onmouseover="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>sel';"
						onmouseout="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>';"
						href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
						onclick="GoToLink(this);return false;" target="_self">
						<img src="/_layouts/images/recursml<%# DataBinder.Eval(Container,"ExceptionExtension","")%>.gif"
							class="<%# DataBinder.Eval(Container,"RecurrenceIconVisible")%>" alt="<%# DataBinder.Eval(Container,"ToolTip","")%>"
							align="absmiddle" />
						<a onfocus="OnLink(this)" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
							onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
							<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%>
						</a>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewDayItemMultiDayTemplate" runat="server">
	<Template>
		<div class="<%# DataBinder.Eval(Container,"DivClass","")%>" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<table border="0" cellspacing="0" cellpadding="0" width="100%" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
				<tr>
					<td class="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>"
						onmouseover="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>sel';"
						onmouseout="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>';"
						href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
						onclick="GoToLink(this);return false;" target="_self">
						<img src="/_layouts/images/recursml<%# DataBinder.Eval(Container,"ExceptionExtension","")%>.gif"
							class="<%# DataBinder.Eval(Container,"RecurrenceIconVisible")%>" alt="<%# DataBinder.Eval(Container,"ToolTip","")%>"
							align="absmiddle" />
						<a onfocus="OnLink(this)" class="ms-cal-dayMultiDay" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
							onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
							<b>
								<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%></b>
						</a>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewWeekItemTemplate" runat="server">
	<Template>
		<table cellpadding="0" cellspacing="0" border="0" class="ms-cal-tweekitem" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<tr>
				<td valign="top" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
					onclick="GoToLink(this);return false;" target="_self">
					<div>
						<img src="/_layouts/images/blank.gif" width="50" height="1" alt=""><br>
						<img src="/_layouts/images/recursml<%# DataBinder.Eval(Container,"ExceptionExtension","")%>.gif"
							class="<%# DataBinder.Eval(Container,"RecurrenceIconVisible")%>" alt="<%# DataBinder.Eval(Container,"ToolTip","")%>"
							align="absmiddle" />
						<a onfocus="OnLink(this)" class="ms-cal-dayitem" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
							onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
							<nobr><%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DateTimeString","{0:G}"))%></nobr>
							<%# DataBinder.Eval(Container,"MultiLineBreak","{0:G}")%>
							<b>
								<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%></b>
							<%# DataBinder.Eval(Container,"MultiLineBreak","{0:G}")%>
							<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Location","{0:G}"))%>
						</a>
					</div>
				</td>
			</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewWeekItemAllDayTemplate" runat="server">
	<Template>
		<div class="<%# DataBinder.Eval(Container,"DivClass","")%>" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<table border="0" width="100%" cellspacing="0" cellpadding="0">
				<tr>
					<td class="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>"
						onmouseover="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>sel';"
						onmouseout="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>';"
						href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
						onclick="GoToLink(this);return false;" target="_self">
						<div dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
							<img src="/_layouts/images/recursml<%# DataBinder.Eval(Container,"ExceptionExtension","")%>.gif"
								class="<%# DataBinder.Eval(Container,"RecurrenceIconVisible")%>" alt="<%# DataBinder.Eval(Container,"ToolTip","")%>"
								align="absmiddle" />
							<a onfocus="OnLink(this)" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
								onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
								<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%>
							</a>
						</div>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewWeekItemMultiDayTemplate" runat="server">
	<Template>
		<div class="<%# DataBinder.Eval(Container,"DivClass","")%>" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
				<tr>
					<td class="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>"
						onmouseover="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>sel';"
						onmouseout="this.className='<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.BackgroundColorClassName",""))%>';"
						href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
						onclick="GoToLink(this);return false;" target="_self">
						<div dir="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Direction",""))%>">
							<img src="/_layouts/images/recursml<%# DataBinder.Eval(Container,"ExceptionExtension","")%>.gif"
								class="<%# DataBinder.Eval(Container,"RecurrenceIconVisible")%>" alt="<%# DataBinder.Eval(Container,"ToolTip","")%>"
								align="absmiddle" />
							<a onfocus="OnLink(this)" class="ms-cal-dayMultiDay" href="<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.DisplayFormUrl",""))%>?ID=<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ItemID",""))%>"
								onclick="GoToLink(this);return false;" target="_self" tabindex='<%# DataBinder.Eval(Container,"TabIndex")%>'>
								<b>
									<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title","{0:G}"))%></b>
							</a>
						</div>
					</td>
				</tr>
			</table>
		</div>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewTabTemplate" runat="server">
	<Template>
		&nbsp; <span class="ms-cal-nav" dir="<%# DataBinder.Eval(Container,"Direction","")%>">
			<a id="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Type",""))%>TabLinkId"
				href="javascript:MoveView('<%# SPHttpUtility.EcmaScriptStringLiteralEncode(SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.Type","")))%>');"
				accesskey="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.AccessKey",""))%>"
				tabindex="1">
				<img src="/_layouts/images/<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.ImageName",""))%>"
					border="0" width="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ImageWidth",""))%>"
					height="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ImageHeight",""))%>"
					alt=''>
				<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title",""))%>
			</a></span>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="CalendarViewTabSelectedTemplate" runat="server">
	<Template>
		&nbsp; <span class="ms-cal-navselected" dir="<%# DataBinder.Eval(Container,"Direction","")%>">
			<a id="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Type",""))%>TabLinkId"
				href="javascript:MoveView('<%# SPHttpUtility.EcmaScriptStringLiteralEncode(DataBinder.Eval(Container,"DataItem.Type",""))%>');"
				accesskey="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.AccessKey",""))%>"
				tabindex="1">
				<img src="/_layouts/images/<%# SPHttpUtility.HtmlUrlAttributeEncode(DataBinder.Eval(Container,"DataItem.ImageName",""))%>"
					border="0" width="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ImageWidth",""))%>"
					height="<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.ImageHeight",""))%>"
					alt=''>
				<%# SPHttpUtility.HtmlEncode(DataBinder.Eval(Container,"DataItem.Title",""))%>
			</a></span>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="UserListForm" runat="server">
	<Template>
		<span id='part1'>
			<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbltop" RightButtonSeparator="&nbsp;"
				runat="server">
				<Template_RightButtons>
					<SharePoint:SaveButton runat="server" />
					<SharePoint:GoBackButton runat="server" />
				</Template_RightButtons>
			</wssuc:ToolBar>
			<SharePoint:UserInfoListFormToolBar runat="server" />
			<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
				width="100%">
				<tr>
					<SharePoint:CompositeField FieldName="Name" ControlMode="Display" runat="server" />
				</tr>
				<SharePoint:ListFieldIterator runat="server" />
				<SharePoint:FormComponent TemplateName="AttachmentRows" runat="server" />
			</table>
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td class="ms-formline">
						<img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
				<tr>
					<td width="100%">
						<SharePoint:ItemHiddenVersion runat="server" />
						<wssuc:ToolBar CssClass="ms-formtoolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
							runat="server">
							<Template_Buttons>
								<SharePoint:CreatedModifiedInfo runat="server" />
							</Template_Buttons>
							<Template_RightButtons>
								<SharePoint:SaveButton runat="server" />
								<SharePoint:GoBackButton runat="server" />
							</Template_RightButtons>
						</wssuc:ToolBar>
					</td>
				</tr>
			</table>
		</span>
		<SharePoint:AttachmentUpload runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="UserInfoListEditFormToolBar" runat="server">
	<Template>
		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" RightButtonSeparator="&nbsp;"
			runat="server">
			<Template_Buttons>
				<SharePoint:AttachmentButton runat="server" />
				<SharePoint:UserInfoListDeleteItemButton runat="server" />
				<SharePoint:ChangePasswordButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="UserInfoListDisplayFormToolBar" runat="server">
	<Template>

		<script>
			recycleBinEnabled = <SharePoint:ProjectProperty Property="RecycleBinEnabled" runat="server"/>;
		</script>

		<wssuc:ToolBar CssClass="ms-toolbar" ID="toolBarTbl" runat="server" FocusOnToolbar="true">
			<Template_Buttons>
				<SharePoint:UserInfoListEditItemButton runat="server" />
				<SharePoint:UserInfoListDeleteItemButton runat="server" />
				<SharePoint:ChangePasswordButton runat="server" />
				<SharePoint:MyRegionalSettingsButton runat="server" />
				<SharePoint:MyAlertsButton runat="server" />
			</Template_Buttons>
		</wssuc:ToolBar>
	</Template>
</SharePoint:RenderingTemplate>
