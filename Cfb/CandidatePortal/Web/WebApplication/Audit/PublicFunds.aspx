<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" CodeBehind="PublicFunds.aspx.cs"
    Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.PublicFunds" %>

<%@ MasterType VirtualPath="Audit.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="Server">
    <%=Resources.CPResources.publicFunds_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="Server">
    <%=Resources.CPResources.publicFunds_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="Server">
    <p>
        Below is a detailed listing of public fund payments disbursed to your campaign during
        this election. In addition to showing the amount and date of the payment, the data
        below also indicate for which period of the election the payment was disbursed (Primary,
        General, Runoff/Rerun) as well as the method by which it was disbursed&mdash;electronically
        (EFT) or Check.
    </p>
    <cfb:PublicFundsPaymentHistoryGrid ID="_paymentGrid" runat="server" />
</asp:Content>
