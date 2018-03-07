<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Profile.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Profile.Committee"
    CodeBehind="Committee.aspx.cs" %>

<%@ MasterType VirtualPath="Profile.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.committee_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderProfilePageTitle">
    <%=Resources.CPResources.committee_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageContent">
    <p>
        Below is a summary of candidate information as submitted to the CFB by your campaign
        in a Filer Registration or Certification form. If any of this information has changed
        and is no longer current, you must update the appropriate <a href="http://www.nyccfb.info/PDF/forms/<%=CPProfile.ElectionCycle%>/index.aspx?sm=candidates_"
            target="_blank" title="NYC Campaign Finance Board Candidate Forms" class="popup">
            form</a>. In order to update the Filer Registration or Certification, you must
        submit an amended form in hard copy. Please contact your CSU liaison for further
        information.</p>
    <cfb:CommitteeProfile runat="server" />
</asp:Content>
