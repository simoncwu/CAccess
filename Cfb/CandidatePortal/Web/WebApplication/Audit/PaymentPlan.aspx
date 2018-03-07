<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Audit.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Audit.PaymentPlan"
    CodeBehind="PaymentPlan.aspx.cs" EnableViewState="false" %>

<%@ MasterType VirtualPath="Audit.master" %>
<%--<%@ Register TagPrefix="WpNs0" Namespace="Cfb.CandidatePortal.SharePoint.WebParts"
    Assembly="Cfb.CandidatePortal.SharePoint.WebParts.PaymentPlanWebPart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9320c5fb540d5df1" %>--%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <%=Resources.CPResources.penalty_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAuditPageTitle" runat="server">
    <%=Resources.CPResources.penalty_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="server">
    <asp:WebPartManager runat="server" />
    <p>
        Your campaign may have come to a settlement with the CFB after the election for
        repaying public funds or penalties. This page provides a summary of the payment
        plan and your payment schedule.</p>
    <asp:WebPartZone ID="PaymentPlanTabbedPanelZone" runat="server" FrameType="TitleBarOnly"
        Padding="0" CssClass="WebPartZone">
        <ZoneTemplate>
<%--            <WpNs0:PaymentPlanWebPart runat="server" ID="PaymentPlanWebPart1" AllowHide="False"
                Description="A web part for displaying the details of a candidate's payment plan."
                ExportMode="All" Title="Payment Plan Web Part" ChromeType="None" AllowMinimize="False"
                EnableViewState="False" AllowClose="False" __markuptype="vsattributemarkup" __webpartid="{18854df5-d994-4455-89c1-2b699c8f6ff2}"
                webpart="true" partorder="1">
            </WpNs0:PaymentPlanWebPart>
--%>        </ZoneTemplate>
    </asp:WebPartZone>
</asp:Content>
