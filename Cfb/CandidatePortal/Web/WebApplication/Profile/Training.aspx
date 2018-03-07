<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Profile.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Profile.Training"
    CodeBehind="Training.aspx.cs" %>

<%@ MasterType VirtualPath="Profile.master" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInHead" runat="Server">
    <%=Resources.CPResources.training_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderProfilePageTitle" runat="Server">
    <%=Resources.CPResources.training_title%></asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageContent" runat="Server">
    <p>
        The New York City Campaign Finance Board offers Compliance and C-SMART trainings
        throughout each election cycle. Compliance trainings cover campaign finance laws
        and CFB rules. C-SMART trainings provide an overview of the CFB&rsquo;s web application
        that campaigns must use to manage and disclose financial activity. The candidate,
        treasurer, campaign manager, or an individual with significant managerial control
        over a campaign is required to attend both a Compliance and a C-SMART training.
        Failure to complete these trainings will result in a violation and potential penalties.</p>
    <p>
        Audit trainings are also available in the post-election period to assist campaigns
        with their Draft Audit Report (DAR) response. Attending the audit training will
        move up the deadline for issuance of the campaign&rsquo;s Final Audit Report by
        two months, possibly resulting in quicker completion of the audit process.</p>
    <p>
        Trainings are held in the afternoons and evenings at the CFB offices located at
        100 Church Street in lower Manhattan. Please contact the Candidate Services Unit
        at
        <%=CPProviders.SettingsProvider.AgencyPhoneNumber%>
        or <a href="mailto:CSUmail@nyccfb.info">CSUmail@nyccfb.info</a> to make a reservation.</p>
    <h3 class="subsection">
        Compliance and C-SMART Training
    </h3>
    <section class="complianceGroup">
        <details open="open">
            <summary>Fulfilled Training Requirement: <asp:Label ID="_complianceStatus" runat="server"
                Text="No"></asp:Label></summary>
            <div class="complianceGroupBody">
                <p id="_emptyComplianceMessage" runat="server">
                    At this time, no campaign staff have attended any Compliance or C-SMART trainings
                    for this election cycle. Please refer to the training calendar found in the Candidates
                    section of the <a href="http://www.nyccfb.info" class="popup">CFB website</a>.</p>
                <cfb:TrainingCourseHistoryControl ID="_combinedWebCourses" runat="server" CourseType="CombinedWeb"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" />
                <cfb:TrainingCourseHistoryControl ID="_combinedCourses" runat="server" CourseType="Combined"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" />
                <cfb:TrainingCourseHistoryControl ID="_supplementalCourses" runat="server" CourseType="Supplemental"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" />
                <cfb:TrainingCourseHistoryControl ID="_complianceCourses" runat="server" CourseType="Compliance"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" />
                <cfb:TrainingCourseHistoryControl ID="_csmartWebCourses" runat="server" CourseType="CsmartWeb"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" />
                <cfb:TrainingCourseHistoryControl ID="_csmartCourses" runat="server" CourseType="Csmart"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" />
                <cfb:TrainingCourseHistoryControl ID="_advancedCourses" runat="server" CourseType="AdvancedCsmart"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" />
            </div>
        </details>
    </section>
    <h3 class="subsection">
        Audit Training
    </h3>
    <section class="complianceGroup">
        <details open="open">
            <summary>Expedited Audit Achieved: <asp:Label ID="_auditComplianceStatus" runat="server"
                Text="No"></asp:Label></summary>
            <div class="complianceGroupBody">
                <p id="_emptyAuditMessage" runat="server">
                    At this time, no campaign staff have attended any Audit trainings for this election
                    cycle. Please refer to the training calendar found in the Candidates section of
                    the <a href="http://www.nyccfb.info" class="popup">CFB website</a>.</p>
                <cfb:TrainingCourseHistoryControl ID="_auditCourses" runat="server" CourseType="Audit"
                    YesImageUrl="/images/completed.png" NoImageUrl="/images/error.png" EmptyVisible="False" />
            </div>
        </details>
    </section>
    <p id="_footnote" runat="server">
        * Indicates Counts toward the campaign&rsquo;s training requirements.</p>
    <script type="text/javascript">
    //<![CDATA[
        $("details").accordion({ collapsible: true, heightStyle: 'content', header: 'summary', active: false, multiActive: true });
    //]]>
    </script>
</asp:Content>
