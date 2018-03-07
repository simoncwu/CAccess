<%@ Control Language="C#" CodeBehind="CandidateProfile.ascx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Controls.CandidateProfile" %>
<asp:Label ID="_errorMessage" runat="server" Visible="false"></asp:Label>
<asp:Panel ID="_details" runat="server" CssClass="cp-CandidateInfo">
    <section class="candidateInfo">
        <div class="fieldset">
            <cfb:FormField ID="_salutation" runat="server" CssClass="salutation" LabelText="Salutation" />
            <cfb:FormField ID="_lastName" runat="server" CssClass="lastName" LabelText="Last Name" />
            <cfb:FormField ID="_firstName" runat="server" CssClass="firstName" LabelText="First Name" />
            <cfb:FormField ID="_mi" runat="server" CssClass="middleInitial" LabelText="M.I." />
            <cfb:FormField ID="_ID" runat="server" CssClass="cp-candidateID" LabelText="CFB ID" />
        </div>
        <div class="address">
            <div class="fieldset">
                <cfb:FormField ID="_streetNumber" runat="server" CssClass="streetNum" LabelText="Building #" />
                <cfb:FormField ID="_streetName" runat="server" CssClass="streetName" LabelText="Street" />
                <cfb:FormField ID="_apartment" runat="server" CssClass="apt" LabelText="Apt #" />
            </div>
            <div class="fieldset">
                <cfb:FormField ID="_city" runat="server" CssClass="city" LabelText="City" />
                <cfb:FormField ID="_state" runat="server" CssClass="state" LabelText="State" />
                <cfb:FormField ID="_zip" runat="server" CssClass="zip" LabelText="ZIP" />
            </div>
        </div>
        <div class="phoneNumbers fieldset">
            <cfb:FormField ID="_daytimePhone" runat="server" CssClass="phone" LabelText="Daytime Telephone" />
            <cfb:FormField ID="_eveningPhone" runat="server" CssClass="phone" LabelText="Evening Telephone" />
            <cfb:FormField ID="_fax" runat="server" CssClass="fax" LabelText="Fax" />
            <cfb:FormField ID="_email" runat="server" CssClass="email" LabelText="E-mail Address" />
        </div>
    </section>
    <section class="employerInfo">
        <details open="open">
            <summary>Employer Information</summary>
            <div class="fieldset">
                <cfb:FormField ID="_empName" runat="server" CssClass="entityName" LabelText="Employer Name" />
            </div>
            <div class="address">
                <div class="fieldset">
                    <cfb:FormField ID="_empStreetNumber" runat="server" CssClass="streetNum" LabelText="Building #" />
                    <cfb:FormField ID="_empStreetName" runat="server" CssClass="streetName" LabelText="Street" />
                </div>
                <div class="fieldset">
                    <cfb:FormField ID="_empCity" runat="server" CssClass="city" LabelText="City" />
                    <cfb:FormField ID="_empState" runat="server" CssClass="state" LabelText="State" />
                    <cfb:FormField ID="_empZip" runat="server" CssClass="zip" LabelText="ZIP" />
                </div>
            </div>
            <div class="phoneNumbers fieldset">
                <cfb:FormField ID="_empPhone" runat="server" CssClass="phone" LabelText="Telephone" />
                <cfb:FormField ID="_empFax" runat="server" CssClass="fax" LabelText="Fax" />
            </div>
        </details>
    </section>
    <section class="activationInfo">
        <details open="open">
            <summary>Additional Information</summary>
            <div class="fieldset">
                <cfb:FormField ID="_classification" runat="server" CssClass="candClass" LabelText="CFB Classification" />
                <cfb:FormField ID="_office" runat="server" CssClass="office" LabelText="Office Sought" />
                <cfb:FormField ID="_boroDistrict" runat="server" CssClass="district" />
                <cfb:FormField ID="_party" runat="server" CssClass="polparty" LabelText="Political Party Registration" />
                <% 
                    if (!_isTIE)
                    {  %>
            </div>
            <div class="fieldset">
                <%
                    } %>
                <cfb:FormField ID="_frDate" runat="server" CssClass="date" LabelText="Filer Registration Date" />
                <cfb:FormField ID="_certDate" runat="server" CssClass="date" LabelText="Certification Date" />
                <cfb:FormField ID="_terminationDate" runat="server" CssClass="terminated date" LabelText="Termination Date"
                    Visible="false" />
                <cfb:FormField ID="_ddAuth" runat="server" CssClass="isDDAuth" LabelText="Direct Deposit Public Funds?" />
                <cfb:FormField ID="_rrddAuth" runat="server" CssClass="isRRDDAuth" LabelText="Runoff/Rerun Direct Deposit?"
                    Visible="false" />
            </div>
        </details>
    </section>
    <asp:Label ID="_lastUpdated" runat="server" CssClass="lastUpdated"></asp:Label>
</asp:Panel>
