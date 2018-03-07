<%@ Control Language="C#" CodeBehind="CommitteeProfile.ascx.cs" Inherits="Cfb.CandidatePortal.Web.WebApplication.Controls.CommitteeProfile" %>
<div class="cp-CommitteeProfile cp-data">
    <asp:Label ID="_errorMessage" runat="server" Visible="false"></asp:Label>
    <asp:Panel ID="_committeePanel" runat="server" CssClass="cp-CommitteeInfo">
        <div class="cp-SelectedCommittee ui-front fieldset">
            <label class="entityName field">
                <span class="fieldLabel">Committee Name</span><asp:DropDownList ID="_committeeSelection"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="_committeeSelection_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="_singleCommittee" runat="server" CssClass="fieldData" Visible="false"></asp:Label>
            </label>
        </div>
        <asp:Panel ID="_committeeDetails" runat="server">
            <section class="committeeInfo">
                <div class="address">
                    <div class="fieldset">
                        <cfb:FormField ID="_StreetNumber" runat="server" CssClass="streetNum" LabelText="Building #" />
                        <cfb:FormField ID="_StreetName" runat="server" CssClass="streetName" LabelText="Street" />
                        <cfb:FormField ID="_Apartment" runat="server" CssClass="apt" LabelText="Apt #" />
                    </div>
                    <div class="fieldset">
                        <cfb:FormField ID="_City" runat="server" CssClass="city" LabelText="City" />
                        <cfb:FormField ID="_State" runat="server" CssClass="state" LabelText="State" />
                        <cfb:FormField ID="_Zip" runat="server" CssClass="zip" LabelText="ZIP" />
                    </div>
                </div>
                <div class="fieldset">
                    <cfb:FormField ID="_email" runat="server" CssClass="email" LabelText="Email Address" />
                    <cfb:FormField ID="_webUrl" runat="server" CssClass="url" LabelText="Website Address(es)" />
                    <cfb:FormField ID="_principal" runat="server" CssClass="isPrincipal" LabelText="Principal?" />
                    <cfb:FormField ID="_active" runat="server" CssClass="isActive" LabelText="Active?" />
                </div>
                <div class="fieldset">
                    <cfb:FormField ID="_daytimePhone" runat="server" CssClass="phone" LabelText="Daytime Telephone" />
                    <cfb:FormField ID="_eveningPhone" runat="server" CssClass="phone" LabelText="Evening Telephone" />
                    <cfb:FormField ID="_fax" runat="server" CssClass="fax" LabelText="Fax" />
                    <cfb:FormField ID="_boeDate" runat="server" CssClass="boe date" LabelText="B.O.E. Auth Date" />
                    <cfb:FormField ID="_terminationDate" runat="server" CssClass="termination date" LabelText="Termination Date"
                        Visible="false" />
                </div>
            </section>
            <section class="CommitteeMailingAddress">
                <details class="address" open="open">
                    <summary>Mailing Address (if different)</summary>
                    <div class="fieldset">
                        <cfb:FormField ID="_mailingLine1" runat="server" CssClass="streetAddress" LabelText="Address Line 1" />
                    </div>
                    <div class="fieldset">
                        <cfb:FormField ID="_mailingStreetNumber" runat="server" CssClass="streetNum" LabelText="Building #" />
                        <cfb:FormField ID="_mailingStreetName" runat="server" CssClass="streetName" LabelText="Street" />
                        <cfb:FormField ID="_mailingApartment" runat="server" CssClass="apt" LabelText="Apt. #" />
                    </div>
                    <div class="fieldset">
                        <cfb:FormField ID="_mailingCity" runat="server" CssClass="city" LabelText="City" />
                        <cfb:FormField ID="_mailingState" runat="server" CssClass="state" LabelText="State" />
                        <cfb:FormField ID="_mailingZip" runat="server" CssClass="zip" LabelText="ZIP" />
                    </div>
                </details>
            </section>
            <section class="treasurerInfo">
                <details open="open">
                    <summary>Treasurer Information</summary>
                    <div class="fieldset">
                        <cfb:FormField ID="_salutation" runat="server" CssClass="salutation" LabelText="Salutation" />
                        <cfb:FormField ID="_lastName" runat="server" CssClass="lastName" LabelText="Last Name" />
                        <cfb:FormField ID="_firstName" runat="server" CssClass="firstName" LabelText="First Name" />
                        <cfb:FormField ID="_mi" runat="server" CssClass="middleInitial" LabelText="M.I." />
                        <cfb:FormField ID="_contactOrder" runat="server" CssClass="contactOrder" LabelText="Contact Order" />
                    </div>
                    <div class="address">
                        <div class="fieldset">
                            <cfb:FormField ID="_treasStreetNumber" runat="server" CssClass="streetNum" LabelText="Building #" />
                            <cfb:FormField ID="_treasStreetName" runat="server" CssClass="streetName" LabelText="Street" />
                            <cfb:FormField ID="_treasApartment" runat="server" CssClass="apt" LabelText="Apt. #" />
                        </div>
                        <div class="fieldset">
                            <cfb:FormField ID="_treasCity" runat="server" CssClass="city" LabelText="City" />
                            <cfb:FormField ID="_treasState" runat="server" CssClass="state" LabelText="State" />
                            <cfb:FormField ID="_treasZip" runat="server" CssClass="zip" LabelText="ZIP" />
                        </div>
                    </div>
                    <div class="fieldset">
                        <cfb:FormField ID="_treasDaytimePhone" runat="server" CssClass="phone" LabelText="Daytime Telephone" />
                        <cfb:FormField ID="_treasEveningPhone" runat="server" CssClass="phone" LabelText="Evening Telephone" />
                        <cfb:FormField ID="_treasFax" runat="server" CssClass="fax" LabelText="Fax" />
                        <cfb:FormField ID="_treasEmail" runat="server" CssClass="email" LabelText="E-mail Address" />
                    </div>
                    <details open="open">
                        <summary>Treasurer Employer</summary>
                        <div class="employerInfo">
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
                            <div class="fieldset">
                                <cfb:FormField ID="_empPhone" runat="server" CssClass="phone" LabelText="Telephone" />
                                <cfb:FormField ID="_empFax" runat="server" CssClass="fax" LabelText="Fax" />
                            </div>
                        </div>
                    </details>
                </details>
            </section>
            <section class="previousElection">
                <details open="open">
                    <summary>Previous Elections</summary>
                    <div class="fieldset">
                        <cfb:FormField ID="_lastDate" runat="server" CssClass="date" LabelText="Date of Election" />
                        <cfb:FormField ID="_lastOffice" runat="server" CssClass="office" LabelText="Position Sought" />
                        <cfb:FormField ID="_lastDistrict" runat="server" CssClass="district" LabelText="District" />
                        <cfb:FormField ID="_lastParty" runat="server" CssClass="polparty" LabelText="Party Primary Entered" />
                    </div>
                </details>
            </section>
            <section>
                <details open="open">
                    <summary>Other Campaign Contacts</summary>
                    <div class="cp-SelectionBar">
                        <label class="ui-front">
                            <asp:DropDownList ID="_liaisonList" runat="server" AutoPostBack="True" CssClass="AjaxAutoPostBackControl"
                                OnSelectedIndexChanged="ShowLiaison">
                                <asp:ListItem Text="(select a campaign contact)" Value="0">
                                </asp:ListItem>
                            </asp:DropDownList>
                            <span>(* Denotes a Consultant)</span>
                        </label>
                    </div>
                    <asp:UpdatePanel ID="_liaisonUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="_liaisonError" runat="server" Visible="False" />
                            <asp:Panel ID="_liaisonDetails" runat="server" Visible="False" CssClass="contacts">
                                <div class="fieldset">
                                    <cfb:FormField ID="_liaisonSalutation" runat="server" CssClass="salutation" LabelText="Salutation" />
                                    <cfb:FormField ID="_liaisonLastName" runat="server" CssClass="lastName" LabelText="Last Name" />
                                    <cfb:FormField ID="_liaisonFirstName" runat="server" CssClass="firstName" LabelText="First Name" />
                                    <cfb:FormField ID="_liaisonMiddleInitial" runat="server" CssClass="middleInitial"
                                        LabelText="M.I." />
                                    <cfb:FormField ID="_liaisonOrder" runat="server" CssClass="contactOrder" LabelText="Contact Order" />
                                </div>
                                <div class="address">
                                    <div class="fieldset">
                                        <cfb:FormField ID="_liaisonStreetNumber" runat="server" CssClass="streetNum" LabelText="Building #" />
                                        <cfb:FormField ID="_liaisonStreetName" runat="server" CssClass="streetName" LabelText="Street" />
                                        <cfb:FormField ID="_liaisonApartment" runat="server" CssClass="apt" LabelText="Apt. #" />
                                    </div>
                                    <div class="fieldset">
                                        <cfb:FormField ID="_liaisonCity" runat="server" CssClass="city" LabelText="City" />
                                        <cfb:FormField ID="_liaisonState" runat="server" CssClass="state" LabelText="State" />
                                        <cfb:FormField ID="_liaisonZip" runat="server" CssClass="zip" LabelText="ZIP" />
                                    </div>
                                </div>
                                <div class="fieldset">
                                    <cfb:FormField ID="_liaisonDaytimePhone" runat="server" CssClass="phone" LabelText="Daytime Telephone" />
                                    <cfb:FormField ID="_liaisonEveningPhone" runat="server" CssClass="phone" LabelText="Evening Telephone" />
                                    <cfb:FormField ID="_liaisonFax" runat="server" CssClass="fax" LabelText="Fax" />
                                    <cfb:FormField ID="_liaisonEmail" runat="server" CssClass="email" LabelText="E-mail" />
                                </div>
                                <div class="fieldset">
                                    <cfb:FormField ID="_liaisonType" runat="server" CssClass="liaisonType" LabelText="Contact Type" />
                                    <cfb:FormField ID="_liaisonManagerial" runat="server" CssClass="managerial" LabelText="Mngrl Control?" />
                                    <cfb:FormField ID="_liaisonVG" runat="server" CssClass="vgLiaison" LabelText="VG Liaison?" />
                                    <cfb:FormField ID="_entityName" runat="server" CssClass="consultant entityName" LabelText="Consultant Entity Name"
                                        Visible="false" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="_liaisonList" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </details>
            </section>
            <section>
                <details open="open">
                    <summary>Bank Accounts</summary>
                    <div class="cp-SelectionBar">
                        <label class="ui-front">
                            <asp:DropDownList ID="_bankAccountsList" runat="server" AutoPostBack="True" CssClass="AjaxAutoPostBackControl"
                                OnSelectedIndexChanged="ShowBankAccount">
                                <asp:ListItem Text="(select a bank account)" Value="0">
                                </asp:ListItem>
                            </asp:DropDownList>
                        </label>
                    </div>
                    <asp:UpdatePanel ID="_bankAccountsUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="_bankError" runat="server" Visible="False" />
                            <asp:Panel ID="_bankAccountDetails" runat="server" Visible="False" CssClass="bankAccounts">
                                <div class="address fieldset">
                                    <cfb:FormField ID="_bankName" runat="server" CssClass="entityName" LabelText="Bank Name" />
                                    <cfb:FormField ID="_bankCity" runat="server" CssClass="city" LabelText="City" />
                                    <cfb:FormField ID="_bankState" runat="server" CssClass="state" LabelText="State" />
                                    <cfb:FormField ID="_bankZip" runat="server" CssClass="zip" LabelText="ZIP" />
                                </div>
                                <div class="fieldset">
                                    <cfb:FormField ID="_accountNumber" runat="server" CssClass="accountNumber" LabelText="Account Number" />
                                    <cfb:FormField ID="_accountName" runat="server" CssClass="accountName" LabelText="Account Name" />
                                </div>
                                <div class="fieldset">
                                    <cfb:FormField ID="_accountType" runat="server" CssClass="accountTypePurpose" LabelText="Type of Account" />
                                    <cfb:FormField ID="_accountPurpose" runat="server" CssClass="accountTypePurpose"
                                        LabelText="Purpose of Account" />
                                </div>
                                <div class="fieldset">
                                    <cfb:FormField ID="_dateOpened" runat="server" CssClass="date" LabelText="Date Opened" />
                                    <cfb:FormField ID="_dateClosed" runat="server" CssClass="date" LabelText="Date Closed (If Any)" />
                                    <cfb:FormField ID="_balance" runat="server" CssClass="balance" LabelText="Balance" />
                                    <cfb:FormField ID="_balanceDate" runat="server" CssClass="date" LabelText="As Of" />
                                    <cfb:FormField ID="_directDeposit" runat="server" CssClass="isDDAuth" LabelText="Direct Deposit Public Funds?" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="_bankAccountsList" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </details>
            </section>
        </asp:Panel>
    </asp:Panel>
</div>
