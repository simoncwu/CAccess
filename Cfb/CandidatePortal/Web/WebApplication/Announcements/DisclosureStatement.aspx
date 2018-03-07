<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/CandidatePortal.master"
    CodeBehind="DisclosureStatement.aspx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Announcements_DisclosureStatement"
    EnableViewState="false" %>

<%@ MasterType VirtualPath="~/CandidatePortal.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="server">
    <asp:Literal ID="_headTitle" runat="server" Text="<%$Resources:CPResources,generalDS_title%>"></asp:Literal></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInBody" runat="server">
    <asp:Literal ID="_titleText" runat="server" Text="<%$Resources:CPResources,generalDS_title%>"></asp:Literal></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <article class="description">
        <asp:Panel ID="_generalInfo" runat="server">
            <asp:Panel ID="_reminderHeader" runat="server" Visible="false">
                <p>
                    Disclosure statement
                    <%=Request.GetStatementNumber()%>
                    for the
                    <%=CPProfile.ElectionCycle%>
                    election cycle is due <strong>
                        <asp:Literal ID="_deadlineLong" runat="server"></asp:Literal></strong>. Your
                    disclosure statement <strong>must</strong> be submitted via the internet, or for
                    disk submissions, hand delivered to the <strong>CFB by 5 p.m.</strong>, or postmarked
                    by midnight of the due date. You can file electronically and/or mail in your disclosure
                    statement and backup documentation as early as
                    <asp:Literal ID="_earlyBirdDate" runat="server"></asp:Literal>.</p>
                <asp:Panel ID="_lastStatement" runat="server" Visible="false">
                    <p>
                        <strong>This is the last disclosure statement you will be filing with the CFB for the
                            <%=CPProfile.ElectionCycle%>
                            Election.</strong></p>
                </asp:Panel>
            </asp:Panel>
            <asp:Literal ID="_description" runat="server" Mode="PassThrough"></asp:Literal>
            <asp:Literal ID="_proceduresHeader" runat="server" Visible="false"><h3>Review of Filing Day Instructions</h3></asp:Literal>
            <p>
                If you have not submitted a Filer Registration (FR) please do so before the filing.
                <strong>You will not be able to file your disclosure statement without having submitted
                    the FR first!</strong> Also, if you have not yet completed an IDS password request
                form to enable electronic filing, we strongly suggest you do so. Filing electronically
                eliminates most causes of rejected disclosure statements, such as unsigned statements
                or a blank floppy disk or CD. Please visit our <a href="http://www.nyccfb.info/PDF/forms/<%=CPProfile.ElectionCycle%>/<%=CPProfile.ElectionCycle%>_C-SMART_Request_Form.pdf"
                    title="<%=CPProfile.ElectionCycle%> C-SMART Request Form" class="popup" target="_blank">
                    public web site</a> to download a C-SMART request form for obtaining an IDS
                password and return it to the CFB at least one day before you intend to file your
                disclosure statement. If you are filing by disk please make sure the disk contains
                the submission file created during the submission process, not a backup version
                of your database. A disclosure statement submitted by disk is <strong>not</strong>
                complete without coversheets and summary pages <strong>signed</strong> by either
                the candidate or the treasurer of the campaign (original signatures only).</p>
            <p>
                In order to file your disclosure statement with the New York State Board of Elections
                (NYSBOE) you will need to obtain a Filer ID number and PIN number. If you do not
                have your Filer ID and PIN please call the NYSBOE at 1-800-458-3453 and ask for
                the campaign finance department. Similar to the CFB they will be busy on filing
                day so make sure you call them as early as possible.</p>
            <p>
                If the campaign is claiming matching funds, they must provide backup documentation
                with the statement. Backup documentation consists of <strong>copies</strong> of:</p>
            <ul>
                <li>checks;</li>
                <li>money orders;</li>
                <li>contribution cards;</li>
                <li>credit card authorization cards and copies of all transaction reports and receipts
                    received from the credit card processor proving the real-time address verification;</li>
                <li>loan agreements between the campaign and the lendor as well as the front and back
                    of the cancelled check used to issue the loan; and</li>
                <li>cover letter disclosing the campaign&rsquo;s fundraising agents for the reporting
                    period.</li>
            </ul>
            <p class="notice">
                Note: If the campaign is accepting credit card contributions, it must have a unique
                merchant account and disclose the unique merchant account to the CFB <strong>before</strong>
                submitting the statement.</p>
            <p>
                The campaign has to reference all backup documentation with the statement number
                and C-SMART generated transaction ID number, and the backup documentation has to
                be ordered in ascending transaction ID order. Campaigns have had contributions invalidated
                and not matched due to improper backup documentation.</p>
            <p>
                <strong>Backup documentation is only submitted for the campaign&rsquo;s matchable contributions.</strong>
                Keep all other receipts and paper work organized for the post-election audit.</p>
            <p>
                If you have any questions, please feel free to contact your candidate liaison or
                the Candidate Services Unit via phone at
                <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>, or by using the online
                <%=CPProviders.SettingsProvider.ApplicationName%>
                <a href="/Help/Contact.aspx" title="<%=Resources.CPResources.contact_title%>">contact form</a>.</p>
        </asp:Panel>
        <asp:Literal ID="_override" runat="server" Mode="PassThrough" Visible="false"></asp:Literal>
    </article>
    <asp:Panel ID="_attachments" runat="server" Visible="false">
        <h3>
            Attachments</h3>
        <asp:Panel ID="_attachmentList" runat="server">
        </asp:Panel>
    </asp:Panel>
</asp:Content>
