<%@ Control Language="C#"   %> <%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %> <%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register TagPrefix="SPMobile" Namespace="Microsoft.SharePoint.MobileControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDefaultSeparator">
	<Template>
		<mobile:Panel RunAt="Server" Alignment="Center" EnableViewState="False">
			<mobile:DeviceSpecific RunAt="Server">
				<Choice Filter="isMME">
					<ContentTemplate>
						<hr width="100%" size="1">
					</ContentTemplate>
				</Choice>
				<Choice Filter="isHTML32">
					<ContentTemplate>
						<hr width="100%" size="1">
					</ContentTemplate>
				</Choice>
				<Choice Filter="isCHTML10">
					<ContentTemplate>
						<hr width="100%" size="1">
					</ContentTemplate>
				</Choice>
				<Choice Filter="isXHTML-MP">
					<ContentTemplate>
						<hr width="100%" size="1" />
					</ContentTemplate>
				</Choice>
				<Choice>
					<ContentTemplate>
						<mobile:LiteralText RunAt="Server" Text="-----" BreakAfter="true" />
					</ContentTemplate>
				</Choice>
			</mobile:DeviceSpecific>
		</mobile:Panel>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListItemSeparator">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileDefaultSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileViewFieldSeparator">
	<Template>
		<mobile:Label RunAt="Server" Text="<%$Resources:wss, mobile_field_separator%>" BreakAfter="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileFormFieldSeparator">
	<Template>
		<mobile:Label RunAt="Server" Text="" BreakAfter="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileHomePageTitle">
	<Template>
		<SPMobile:SPMobileWebTitle RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_HomePage_Title">
	<Template>
		<SPMobile:SPMobileWeb RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDeletePageTitle">
	<Template>
		<SPMobile:SPMobileListTitle RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_DeletePage_Title">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListTitleControl RunAt="Server" BreakAfter="false" />
			<mobile:LiteralText                RunAt="Server" Text="<%$Resources:wss, mobile_listtitle_separator%>" BreakAfter="false" />
			<mobile:LiteralText                RunAt="Server" Text="<%$Resources:wss, mobile_deleteitem_text%>"     BreakAfter="false" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileViewTitle">
	<Template>
		<SPMobile:SPMobileListTitle RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_View_Title">
	<Template>
		<SPMobile:SPMobileListTitleControl RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileViewTitleWithFolder">
	<Template>
		<SPMobile:SPMobileListTitle RunAt="Server" WithFolder="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileFolder_Default_View_Title">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListTitleControl RunAt="Server" BreakAfter="false" />
			<mobile:LiteralText                RunAt="Server" Text="<%$Resources:wss, mobile_listtitle_separator%>" BreakAfter="false" />
			<SPMobile:SPMobileFolder           RunAt="Server" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileNewFormTitle">
	<Template>
		<SPMobile:SPMobileListTitle RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_NewForm_Title">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListTitleControl RunAt="Server" BreakAfter="false" />
			<mobile:LiteralText                RunAt="Server" Text="<%$Resources:wss, mobile_listtitle_separator%>" BreakAfter="false" />
			<mobile:Label                      RunAt="Server" Text="<%$Resources:wss, mobile_newitem_text%>"        BreakAfter="true" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileEditFormTitle">
	<Template>
		<SPMobile:SPMobileListTitle RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_EditForm_Title">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListTitleControl RunAt="Server" BreakAfter="false" />
			<mobile:LiteralText                RunAt="Server" Text="<%$Resources:wss, mobile_listtitle_separator%>" BreakAfter="false" />
			<SPMobile:SPMobileListItem         RunAt="Server" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDispFormTitle">
	<Template>
		<SPMobile:SPMobileListTitle RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_DispForm_Title">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListTitleControl RunAt="Server" BreakAfter="false" />
			<mobile:LiteralText                RunAt="Server" Text="<%$Resources:wss, mobile_listtitle_separator%>" BreakAfter="false" />
			<SPMobile:SPMobileListItem         RunAt="Server" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileHomePageContents">
	<Template>
		<SPMobile:SPMobileWebContents RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_HomePage_Contents">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="Mobile_STS_HomePage_Contents" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_STS_HomePage_Contents">
	<Template>
		<mobile:Label                  RunAt="Server" Text="<%$Resources:wss, mobile_listcategory_geneliclists_text%>" BreakAfter="true" />
		<SPMobile:SPMobileListIterator RunAt="Server">
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Announcements" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Categories" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Comments" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Contacts" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Events" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="IssueTracking" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Links" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="GanttTasks" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="GenericList" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Posts" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Tasks" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="Survey" />
		</SPMobile:SPMobileListIterator>
		<mobile:Label                  RunAt="Server" Text="<%$Resources:wss, mobile_listcategory_documents_text%>"    BreakAfter="true" />
		<SPMobile:SPMobileListIterator RunAt="Server">
			<SPMobile:SPMobileList RunAt="Server" TemplateType="DocumentLibrary" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="WebPageLibrary" />
			<SPMobile:SPMobileList RunAt="Server" TemplateType="XMLForm" />
		</SPMobile:SPMobileListIterator>
		<mobile:Label                  RunAt="Server" Text="<%$Resources:wss, mobile_listcategory_pictures_text%>"    BreakAfter="true" />
		<SPMobile:SPMobileListIterator RunAt="Server">
			<SPMobile:SPMobileList RunAt="Server" TemplateType="PictureLibrary" />
		</SPMobile:SPMobileListIterator>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_CENTRALADMIN_HomePage_Contents">
	<Template>
		<mobile:Label RunAt="Server" Text="<%$Resources:wss, mobile_sitetemplate_nosupport_text%>" BreakAfter="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_MPS_HomePage_Contents">
	<Template>
		<mobile:Label RunAt="Server" Text="<%$Resources:wss, mobile_sitetemplate_nosupport_text%>" BreakAfter="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<mobile:LiteralText                RunAt="Server" Text="<%$Resources:wss, mobile_list_bullet%>" BreakAfter="false" />
			<SPMobile:SPMobileListTitleControl RunAt="Server" Linkable="true" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDeletePageContents">
	<Template>
		<SPMobile:SPMobileListContents RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_DeletePage_Contents">
	<Template>
		<SPMobile:SPMobileDeleteMessageLabel RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileViewContents">
	<Template>
		<SPMobile:SPMobileListContents RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_View_Contents">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileViewPicker" />
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileDefaultSeparator" />
		</SPMobile:SPMobileControlContainer>
		<SPMobile:SPMobileListItemIterator RunAt="Server" ListItemSeparatorTemplateName="MobileListItemSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Events_View_Contents">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileViewPicker" />
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileDefaultSeparator" />
		</SPMobile:SPMobileControlContainer>
		<SPMobile:SPMobileEventsListItemIterator RunAt="Server" ListItemSeparatorTemplateName="MobileListItemSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileViewPicker">
	<Template>
		<SPMobile:SPMobileListViewIterator  RunAt="Server" ID="ListViewIteratorForMobile" Format="DropDown" BreakAfter="false" />
		<SPMobile:SPMobileRefreshNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_refresh_text%>" ControlForRefresh="ListViewIteratorForMobile" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileNewFormContents">
	<Template>
		<SPMobile:SPMobileListContents RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_NewForm_Contents">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="Mobile_Default_EditForm_Contents" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileEditFormContents">
	<Template>
		<SPMobile:SPMobileListContents RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_EditForm_Contents">
	<Template>
		<SPMobile:SPMobileListFieldIterator RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_DiscussionBoard_EditForm_Contents">
	<Template>
		<SPMobile:SPMobileDiscussionFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileFormFieldSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDispFormContents">
	<Template>
		<SPMobile:SPMobileListContents RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_DispForm_Contents">
	<Template>
		<SPMobile:SPMobileListFieldIterator RunAt="Server" />
		<SPMobile:SPMobileComponent         RunAt="Server" TemplateName="MobileFormFieldSeparator" />
		<SPMobile:SPMobileControlContainer  RunAt="Server">
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileCreatedModifiedInfo" />
			<mobile:Label               RunAt="Server" BreakAfter="true" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_WebPageLibrary_DispForm_Contents">
	<Template>
		<SPMobile:SPMobileWebPageLibraryItemFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileFormFieldSeparator" />
		<SPMobile:SPMobileComponent         RunAt="Server" TemplateName="MobileFormFieldSeparator" />
		<SPMobile:SPMobileControlContainer  RunAt="Server">
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileCreatedModifiedInfo" />
			<mobile:Label               RunAt="Server" BreakAfter="true" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_DiscussionBoard_DispForm_Contents">
	<Template>
		<SPMobile:SPMobileDiscussionFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileFormFieldSeparator" />
		<SPMobile:SPMobileComponent               RunAt="Server" TemplateName="MobileFormFieldSeparator" />
		<SPMobile:SPMobileControlContainer        RunAt="Server">
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileCreatedModifiedInfo" />
			<mobile:Label               RunAt="Server" BreakAfter="true" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileItemContents">
	<Template>
		<SPMobile:SPMobileItemFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileFormFieldSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileFolderContents">
	<Template>
		<SPMobile:SPMobileFolderFieldIterator RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDisplayItemContents">
	<Template>
		<SPMobile:SPMobileItemFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileFormFieldSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDisplayFolderContents">
	<Template>
		<SPMobile:SPMobileFolderFieldIterator RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCreatedModifiedInfo">
	<Template>
		<SPMobile:SPMobileFormattedString RunAt="Server" FormatText="<%$Resources:wss, form_createdby%>">
			<SPMobile:SPMobileListField    RunAt="Server" Fieldname="Created" BreakAfter="false" />
			<SPMobile:SPMobileListField    RunAt="Server" Fieldname="Author"  BreakAfter="false" />
			<SPMobile:SPMobileCreationType RunAt="Server" />
		</SPMobile:SPMobileFormattedString>
		<mobile:LiteralText               RunAt="Server" Text="" BreakAfter="true" />
		<SPMobile:SPMobileFormattedString RunAt="Server" FormatText="<%$Resources:wss, form_modifiedby%>">
			<SPMobile:SPMobileListField RunAt="Server" Fieldname="Modified" BreakAfter="false" />
			<SPMobile:SPMobileListField RunAt="Server" Fieldname="Editor"   BreakAfter="false" />
		</SPMobile:SPMobileFormattedString>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileHomePageNavigation">
	<Template>
		<SPMobile:SPMobileWebNavigation RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_HomePage_Navigation">
	<Template>
		<SPMobile:SPMobileHomePageNavigation   RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>" AccessKey="0" />
		<SPMobile:SPMobileLogoutPageNavigation RunAt="Server" Text="<%$Resources:wss, personalactions_logout%>"      AccessKey="*" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_BLOG_HomePage_Navigation">
	<Template>
		<SPMobile:SPMobilePostsHomePageNavigation RunAt="Server" Text="<%$Resources:wss, moblog_navigation_home_text%>" AccessKey="0" />
		<SPMobile:SPMobileLogoutPageNavigation    RunAt="Server" Text="<%$Resources:wss, personalactions_logout%>"      AccessKey="*" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDeleteNavigation">
	<Template>
		<SPMobile:SPMobileListNavigation RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_DeletePage_Navigation">
	<Template>
		<SPMobile:SPMobileFormDigest       RunAt="Server" />
		<SPMobile:SPMobileDeleteNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_delete_text%>" BreakAfter="false" />
		<SPMobile:SPMobileCancelNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" BreakAfter="true"  />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Comments_DeletePage_Navigation">
	<Template>
		<SPMobile:SPMobileFormDigest               RunAt="Server" />
		<SPMobile:SPMobileCommentsDeleteNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_delete_text%>" BreakAfter="false" />
		<SPMobile:SPMobileCommentsCancelNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" BreakAfter="true"  />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Posts_DeletePage_Navigation">
	<Template>
		<SPMobile:SPMobileFormDigest            RunAt="Server" />
		<SPMobile:SPMobilePostsDeleteNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_delete_text%>" BreakAfter="false" />
		<SPMobile:SPMobilePostsCancelNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileViewNavigation">
	<Template>
		<SPMobile:SPMobileListNavigation RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_View_Navigation">
	<Template>
		<SPMobile:SPMobileNewFormNavigation  RunAt="Server" Text="<%$Resources:wss, mobile_newitem_text%>"         AccessKey="7" />
		<SPMobile:SPMobileHomePageNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>" AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_DocumentLibrary_View_Navigation">
	<Template>
		<SPMobile:SPMobileHomePageNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>" AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Comments_View_Navigation">
	<Template>
		<SPMobile:SPMobileHomePageNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>" AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileViewNavigationWithFolder">
	<Template>
		<SPMobile:SPMobileListNavigation RunAt="Server" WithFolder="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileFolder_Default_View_Navigation">
	<Template>
		<SPMobile:SPMobileUpFolderNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_upfolder_text%>" />
		<SPMobile:SPMobileComponent          RunAt="Server" TemplateName="Mobile_Default_View_Navigation" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileFolder_DocumentLibrary_View_Navigation">
	<Template>
		<SPMobile:SPMobileUpFolderNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_upfolder_text%>" />
		<SPMobile:SPMobileComponent          RunAt="Server" TemplateName="Mobile_DocumentLibrary_View_Navigation" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileNewFormNavigation">
	<Template>
		<SPMobile:SPMobileListNavigation RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_NewForm_Navigation">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="Mobile_Default_EditForm_Navigation" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Events_NewForm_Navigation">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="Mobile_Events_EditForm_Navigation" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_DiscussionBoard_NewForm_Navigation">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="Mobile_DiscussionBoard_EditForm_Navigation" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileEditFormNavigation">
	<Template>
		<SPMobile:SPMobileListNavigation RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_EditForm_Navigation">
	<Template>
		<SPMobile:SPMobileFormDigest         RunAt="Server" />
		<SPMobile:SPMobileSaveNavigation     RunAt="Server" Text="<%$Resources:wss, mobile_button_save_text%>" BreakAfter="false" />
		<SPMobile:SPMobileCancelNavigation   RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" />
		<SPMobile:SPMobileListViewNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_backlist_text%>" AccessKey="#" />
		<SPMobile:SPMobileHomePageNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"     AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Events_EditForm_Navigation">
	<Template>
		<SPMobile:SPMobileFormDigest             RunAt="Server" />
		<SPMobile:SPMobileEventsSaveNavigation   RunAt="Server" Text="<%$Resources:wss, mobile_button_save_text%>" BreakAfter="false" />
		<SPMobile:SPMobileEventsCancelNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" />
		<SPMobile:SPMobileListViewNavigation     RunAt="Server" Text="<%$Resources:wss, mobile_navigation_backlist_text%>" AccessKey="#" />
		<SPMobile:SPMobileHomePageNavigation     RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"     AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_DiscussionBoard_EditForm_Navigation">
	<Template>
		<SPMobile:SPMobileFormDigest                    RunAt="Server" />
		<SPMobile:SPMobileDiscussionBoardSaveNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_save_text%>" BreakAfter="false" />
		<SPMobile:SPMobileCancelNavigation              RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" />
		<SPMobile:SPMobileListViewNavigation            RunAt="Server" Text="<%$Resources:wss, mobile_navigation_backlist_text%>" AccessKey="#" />
		<SPMobile:SPMobileHomePageNavigation            RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"     AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDispFormNavigation">
	<Template>
		<SPMobile:SPMobileListNavigation RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_DispForm_Navigation">
	<Template>
		<SPMobile:SPMobileEditFormNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_edit_text%>"     AccessKey="8" />
		<SPMobile:SPMobileListViewNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_backlist_text%>" AccessKey="#" />
		<SPMobile:SPMobileHomePageNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"     AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Events_DispForm_Navigation">
	<Template>
		<SPMobile:SPMobileEventsEditFormNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_edit_text%>"     AccessKey="8" />
		<SPMobile:SPMobileListViewNavigation       RunAt="Server" Text="<%$Resources:wss, mobile_navigation_backlist_text%>" AccessKey="#" />
		<SPMobile:SPMobileHomePageNavigation       RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"     AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_WebPageLibrary_DispForm_Navigation">
	<Template>
		<SPMobile:SPMobileListViewNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_backlist_text%>" AccessKey="#" />
		<SPMobile:SPMobileHomePageNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"     AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobilePaginateNavigation">
	<Template>
		<SPMobile:SPMobilePaginateNavigation
			RunAt="Server"
			FirstPageText="<%$Resources:wss, mobile_navigation_firstpage_text%>"
			LastPageText="<%$Resources:wss, mobile_navigation_lastpage_text%>"
			BreakAfter="true"
		/>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileHomePageRedirect">
	<Template>
		<SPMobile:SPMobileWebUrlRedirect RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_Default_HomePage_Redirect">
	<Template>
		<SPMobile:SPMobileUrlRedirection RunAt="Server" PageFilename="mbllists.aspx" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Mobile_BLOG_HomePage_Redirect">
	<Template>
		<SPMobile:SPMobileUrlRedirection RunAt="Server" PageFileName="bloghome.aspx" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListViewIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListView RunAt="Server" Linkable="true" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListItemIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListFieldIterator  RunAt="Server" />
			<SPMobile:SPMobileComponent          RunAt="Server" TemplateName="MobileViewFieldSeparator" />
			<SPMobile:SPMobileDispFormNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_display_text%>" BreakAfter="false" />
			<mobile:Label                        RunAt="Server" Text=""                                                   BreakAfter="true" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileEventsListItemIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileListItemIterator" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileFolderIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileFolderFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileViewFieldSeparator" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileItemIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileItemFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileViewFieldSeparator" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListFieldIterator">
	<Template>
		<SPMobile:SPMobileListFieldRendering RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListFieldViewRendering">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileListField" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListFieldFormRendering">
	<Template>
		<SPMobile:SPMobileCompositeField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCompositeField">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileFieldLabel RunAt="Server" />
			<SPMobile:SPMobileComponent  RunAt="Server" TemplateName="MobileListField" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileMultiValueCompositeField">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileFieldLabel RunAt="Server" />
			<mobile:Label                RunAt="Server" Text="" BreakAfter="true" />
			<SPMobile:SPMobileComponent  RunAt="Server" TemplateName="MobileListField" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileListField">
	<Template>
		<SPMobile:SPMobileListFieldSelector RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDefaultListField">
	<Template>
		<SPMobile:SPMobileListField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCustomListField_Contacts_Text_WorkPhone">
	<Template>
		<SPMobile:SPMobilePhoneCallSpecificField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCustomListField_Contacts_Text_HomePhone">
	<Template>
		<SPMobile:SPMobilePhoneCallSpecificField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCustomListField_Contacts_Text_CellPhone">
	<Template>
		<SPMobile:SPMobilePhoneCallSpecificField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCustomListField_Contacts_Text_Email">
	<Template>
		<SPMobile:SPMobileMailToSpecificField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCustomListField_PictureLibrary_Computed_ImageSize">
	<Template>
		<SPMobile:SPMobileImageSizeSpecificField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCustomListField_Posts_DateTime_PublishedDate">
	<Template>
		<SPMobile:SPMobileCurrentDateTimeSpecificField RunAt="Server" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileDefaultFieldLabel">
	<Template>
		<SPMobile:SPMobileFieldPropertyLabel RunAt="Server" PropertyName="Title" FormatString="<%$Resources:wss, mobile_fieldtitle_format%>" BreakAfter="false" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileRequiredFieldLabel">
	<Template>
		<mobile:LiteralText                  RunAt="Server" Text="<%$Resources:wss, mobile_requiredfield_symbol%>" BreakAfter="false" />
		<SPMobile:SPMobileFieldPropertyLabel RunAt="Server" PropertyName="Title" FormatString="<%$Resources:wss, mobile_fieldtitle_format%>" BreakAfter="false" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogCommentsViewTitle">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListTitleControl     RunAt="Server" BreakAfter="false" />
			<mobile:LiteralText                    RunAt="Server" Text="<%$Resources:wss, mobile_listtitle_separator%>" BreakAfter="false" />
			<SPMobile:SPMobileCommentsTitleControl RunAt="Server" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsViewTitle">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileWeb            RunAt="Server" BreakAfter="false" />
			<mobile:LiteralText              RunAt="Server" Text="<%$Resources:wss, mobile_listtitle_separator%>" BreakAfter="false" />
			<SPMobile:SPMobilePostsListTitle RunAt="Server" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Moblog_MyPosts_Title">
	<Template>
		<mobile:Label RunAt="Server" Text="<%$Resources:wss, moblog_navigation_myposts_text%>" BreakAfter="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Moblog_AllPosts_Title">
	<Template>
		<mobile:Label RunAt="Server" Text="<%$Resources:wss, moblog_navigation_allposts_text%>" BreakAfter="true" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogCommentsNewFormTitle">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MoblogCommentsViewTitle" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsNewFormTitle">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileNewFormTitle" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsDispFormTitle">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileDispFormTitle" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogCommentsViewContents">
	<Template>
		<SPMobile:SPMobileCommentsListItemIterator RunAt="Server" ListItemSeparatorTemplateName="MobileListItemSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsViewContents">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server" BreakAfter="false">
			<SPMobile:SPMobileCategoryPicker         RunAt="Server" ID="CategoryPickerForMoblog" BreakAfter="false" />
			<SPMobile:SPMobilePostsRefreshNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_refresh_text%>" controlforrefresh="CategoryPickerForMoblog" />
			<SPMobile:SPMobileComponent              RunAt="Server" TemplateName="MobileDefaultSeparator" />
		</SPMobile:SPMobileControlContainer>
		<SPMobile:SPMobilePostsListItemIterator RunAt="Server" ListItemSeparatorTemplateName="MobileListItemSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogCommentsNewFormContents">
	<Template>
		<SPMobile:SPMobileCommentsItemFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileFormFieldSeparator" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsNewFormContents">
	<Template>
		<SPMobile:SPMobileComponent RunAt="Server" TemplateName="MobileNewFormContents" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsDispFormContents">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server" BreakAfter="true">
			<SPMobile:SPMobileFormattedString RunAt="Server" FormatText="<%$Resources:wss, moblog_modifiedby%>">
				<SPMobile:SPMobileListField RunAt="Server" Fieldname="Modified" BreakAfter="false" />
				<SPMobile:SPMobileListField RunAt="Server" Fieldname="Editor"   BreakAfter="false" />
			</SPMobile:SPMobileFormattedString>
		</SPMobile:SPMobileControlContainer>
		<SPMobile:SPMobileListField        RunAt="Server" Fieldname="Attachments" BreakAfter="true" />
		<mobile:Label                      RunAt="Server" Text=" "                BreakAfter="true" />
		<SPMobile:SPMobileListField        RunAt="Server" Fieldname="Body"        BreakAfter="true" />
		<mobile:Label                      RunAt="Server" Text=" "                BreakAfter="true" />
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobilePostsItemFieldIterator RunAt="Server" FieldSeparatorTemplateName="MobileFormFieldSeparator" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogCommentsViewNavigation">
	<Template>
		<SPMobile:SPMobileCommentsNewFormNavigation RunAt="Server" Text="<%$Resources:wss, mobile_newcomment_text%>"          AccessKey="7" />
		<SPMobile:SPMobilePostsDispFormNavigation   RunAt="Server" Text="<%$Resources:wss, mobile_navigation_backpost_text%>" AccessKey="#" />
		<SPMobile:SPMobilePostsHomePageNavigation   RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"     AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsViewNavigation">
	<Template>
		<SPMobile:SPMobilePostsListNavigation     RunAt="Server" />
		<SPMobile:SPMobileHomePageNavigation      RunAt="Server" Text="<%$Resources:wss, mobile_navigation_allcontent_text%>" AccessKey="2" />
		<SPMobile:SPMobilePostsNewFormNavigation  RunAt="Server" Text="<%$Resources:wss, mobile_newpost_text%>"               AccessKey="7" />
		<SPMobile:SPMobilePostsListViewNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>"       AccessKey="0" />
		<SPMobile:SPMobileLogoutPageNavigation    RunAt="Server" Text="<%$Resources:wss, personalactions_logout%>"            AccessKey="*" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Moblog_MyPosts_Navigation">
	<Template>
		<SPMobile:SPMobilePostsListViewNavigation RunAt="Server" Text="<%$Resources:wss, moblog_navigation_myposts_text%>" FlipViewType="true" AccessKey="1" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="Moblog_AllPosts_Navigation">
	<Template>
		<SPMobile:SPMobilePostsListViewNavigation RunAt="Server" Text="<%$Resources:wss, moblog_navigation_allposts_text%>" FlipViewType="true" AccessKey="1" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogCommentsNewFormNavigation">
	<Template>
		<SPMobile:SPMobileFormDigest               RunAt="Server" />
		<SPMobile:SPMobileCommentsSaveNavigation   RunAt="Server" Text="<%$Resources:wss, mobile_button_submitcomment_text%>" BreakAfter="false" />
		<SPMobile:SPMobileCommentsCancelNavigation RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" />
		<SPMobile:SPMobilePostsHomePageNavigation  RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>" AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsNewFormNavigation">
	<Template>
		<SPMobile:SPMobileFormDigest              RunAt="Server" />
		<SPMobile:SPMobilePostsSaveNavigation     RunAt="Server" Text="<%$Resources:wss, mobile_button_saveasdraft_text%>" BreakAfter="false" />
		<SPMobile:SPMobilePublishNavigation       RunAt="Server" Text="<%$Resources:wss, mobile_button_publish_text%>"     BreakAfter="false" />
		<SPMobile:SPMobilePostsCancelNavigation   RunAt="Server" Text="<%$Resources:wss, mobile_button_cancel_text%>" />
		<SPMobile:SPMobilePostsListViewNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>" AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MoblogPostsDispFormNavigation">
	<Template>
		<SPMobile:SPMobileCommentsListViewNavigation RunAt="Server" Text="<%$Resources:wss, mobile_viewcomment_text%>"     AccessKey="1" />
		<SPMobile:SPMobileCommentsNewFormNavigation  RunAt="Server" Text="<%$Resources:wss, mobile_newcomment_text%>"      AccessKey="7" />
		<SPMobile:SPMobileDeletePageNavigation       RunAt="Server" Text="<%$Resources:wss, mobile_navigation_delete_text%>" />
		<SPMobile:SPMobilePostsHomePageNavigation    RunAt="Server" Text="<%$Resources:wss, mobile_navigation_home_text%>" AccessKey="0" />
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobileCommentsListItemIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server" BreakAfter="true">
			<SPMobile:SPMobileListField RunAt="Server" Fieldname="Title" Font-Bold="true" EllipsisEnabled="false" BreakAfter="true" />
		</SPMobile:SPMobileControlContainer>
		<SPMobile:SPMobileListField        RunAt="Server" Fieldname="Body" EllipsisEnabled="false" BreakAfter="true" />
		<mobile:Label                      RunAt="Server" Text=" " BreakAfter="true" />
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileFormattedString RunAt="Server" FormatText="<%$Resources:wss, moblog_modifiedby%>">
				<SPMobile:SPMobileListField RunAt="Server" Fieldname="Modified" EllipsisEnabled="false" BreakAfter="false" />
				<SPMobile:SPMobileListField RunAt="Server" Fieldname="Author"   EllipsisEnabled="false" BreakAfter="false" />
				<mobile:Label               RunAt="Server" Text=" " BreakAfter="false" />
			</SPMobile:SPMobileFormattedString>
			<mobile:Label                                  RunAt="Server" Text=" " BreakAfter="false" />
			<SPMobile:SPMobileCommentsDeletePageNavigation RunAt="Server" Text="<%$Resources:wss, mobile_navigation_delete_text%>" />
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate RunAt="Server" ID="MobilePostsListItemIterator">
	<Template>
		<SPMobile:SPMobileControlContainer RunAt="Server">
			<SPMobile:SPMobileListField               RunAt="Server" Fieldname="PublishedDate" EllipsisEnabled="false" BreakAfter="true" />
			<SPMobile:SPMobilePostsDispFormNavigation RunAt="Server" BreakAfter="false" />
			<SPMobile:SPMobileFormattedString         RunAt="Server" FormatText="<%$Resources:wss, moblog_num_comments%>">
				<SPMobile:SPMobileListField RunAt="Server" Fieldname="NumComments" EllipsisEnabled="false" BreakAfter="false" />
			</SPMobile:SPMobileFormattedString>
		</SPMobile:SPMobileControlContainer>
	</Template>
</SharePoint:RenderingTemplate>
