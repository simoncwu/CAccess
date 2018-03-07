<%@ Page Language="C#" CodeBehind="ServerError.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Errors.ServerError"
    MasterPageFile="~/CPMinimal.master" EnableViewState="false" AutoEventWireup="True" %>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.error_title%>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <%=Resources.CPResources.error_title%>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <p>
        A server error has occurred while attempting to process your previous request, and our technicians have been notified accordingly. We apologize for any inconvenience this may cause. If you require immediate assistance, please contact the Candidate Services Unit via telephone at
        <asp:Literal ID="_agencyPhoneNumber" runat="server"></asp:Literal>.
    </p>
</asp:Content>
