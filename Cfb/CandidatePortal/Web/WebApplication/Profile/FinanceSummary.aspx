<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="Profile.master" Inherits="Cfb.CandidatePortal.Web.WebApplication.Profile.FinanceSummary"
    EnableViewState="false" CodeBehind="FinanceSummary.aspx.cs" %>

<%@ MasterType VirtualPath="Profile.master" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInHead">
    <%=Resources.CPResources.cfs_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderProfilePageTitle">
    <%=Resources.CPResources.cfs_title%></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageContent">
    <p>
        Below is a summary of all financial information submitted to the CFB by your campaign
        on its financial disclosure statements. All data are as reported to the CFB by campaigns
        in their financial disclosure statements. The data are subject to change as a result
        of ongoing audits and additional amendments to filings received from candidates.
        To view the campaign finance summaries of all candidates for the
        <%=CPProfile.ElectionCycle%>
        election cycle, please visit the <a href="http://www.nyccfb.info/VSApps/WebForm_Finance_Summary.aspx?as_election_cycle=<%=CPProfile.ElectionCycle%>&sm=press_"
            target="_blank" class="popup">CFB public web site</a>.
    </p>
    <div class="cp-FinanceSummaryPanel">
        <div class="cp-boundingBox">
            <table class="cp-FinanceSummaryHeader ms-listviewtable">
                <colgroup>
                    <col class="FinanceSummaryCandidate" />
                    <col class="FinanceSummaryOfficeSought" />
                    <col class="FinanceSummaryClassification" runat="server" id="FSClassificationCol" />
                    <col class="FinanceSummaryLastStatement" />
                </colgroup>
                <tr>
                    <th>
                        <span>Candidate</span>
                    </th>
                    <th>
                        <span>Office Sought</span>
                    </th>
                    <th runat="server" id="FSClassificationHeader">
                        <span>Classification</span>
                    </th>
                    <th>
                        <span>Last Statement Filed</span>
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="CandidateName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="OfficeLabel" runat="server"></asp:Label>
                    </td>
                    <td runat="server" id="FSClassificationCell">
                        <asp:Label ID="ClassificationLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LastStatementLabel" runat="server" Text="Has not submitted"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="columns ms-listviewtable detailsview">
            <div class="left column two-up">
                <table>
                    <caption>
                        Receipts</caption>
                    <tr>
                        <th>
                            Net Contributions
                        </th>
                        <td>
                            <asp:Label ID="NetContributionsLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Miscellaneous Receipts
                        </th>
                        <td>
                            <asp:Label ID="MiscellaneousReceiptsLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Loans Received
                        </th>
                        <td class="cp-FinanceSummary">
                            <asp:Label ID="LoansReceivedLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <%
                        if (!this._isTIE)
                        { %>
                    <tr>
                        <th>
                            Public Funds Received
                        </th>
                        <td>
                            <asp:Label ID="PublicFundsReceivedLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <%
                        } %>
                </table>
            </div>
            <div class="column two-up">
                <table>
                    <caption>
                        Disbursements</caption>
                    <tr>
                        <th>
                            Net Expenditures
                        </th>
                        <td>
                            <asp:Label ID="NetExpendituresLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Loans Paid
                        </th>
                        <td>
                            <asp:Label ID="LoansPaidLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Outstanding Bills
                        </th>
                        <td class="cp-FinanceSummary">
                            <asp:Label ID="OustandingBillsLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <%
                        if (!this._isTIE)
                        { %>
                    <tr>
                        <th>
                            Public Funds Returned
                        </th>
                        <td>
                            <asp:Label ID="PublicFundsReturnedLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                    <%
                        } %>
                </table>
            </div>
        </div>
        <div class="columns cp-FinanceSummaryFooter ms-listviewtable detailsview">
            <asp:Panel ID="ContributorsPanel" runat="server" CssClass="column two-up left">
                <table>
                    <tr>
                        <th>
                            Number of Contributors
                        </th>
                        <td>
                            <asp:Label ID="NumberOfContributorsLabel" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%
                if (!this._isTIE)
                {  %>
            <div class="column two-up">
                <table>
                    <tr>
                        <th>
                            Matching Claims
                        </th>
                        <td>
                            <asp:Label ID="MatchingClaimsLabel" runat="server" Text="$0.00"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <%
                } %>
        </div>
    </div>
    <section class="cp-FinanceSummary" title="Explanation of Terms">
        <details open="open">
            <summary>Explanation of Terms</summary>
            <dl title="Explanation of Terms">
                <dt>Office Sought</dt>
                <dd>
                    <strong>Office Sought</strong> shows the office sought by the candidate as reported
                    on her/his Filer Registration or Certification Form. If this information has not
                    been reported, the candidate&rsquo;s office sought will appear as Undeclared.</dd>
                <%
                    if (!this._isTIE)
                    { %>
                <dt>Classification</dt>
                <dd>
                    <strong>Classification</strong> indicates whether a candidate is a Campaign Finance
                    Program participant, is a limited participant, does not participate, or has not
                    yet decided whether to join the Program. Classification came into use with the 2005
                    Election Cycle.
                </dd>
                <%
                    } %>
                <dt>Last Statement Submitted</dt>
                <dd>
                    <strong>Last Statement Submitted</strong> shows the statement number and due date
                    of the most recent financial disclosure statement from the candidate received by
                    the CFB.</dd>
                <dt>Net Contributions</dt>
                <dd>
                    <strong>Net Contributions</strong> is the total of itemized and non-itemized monetary
                    contributions, in-kind contributions, transfers received from a party or constituted
                    committee, and unreimbursed advances, less refunds. It does not include any surplus
                    funds from previous elections; transfers from non-covered committees controlled
                    by the candidate; any loans that are deemed contributions because they are outstanding
                    after the election, taken out after the election, or paid back by a third party;
                    or any bills that are deemed contributions because they are outstanding more than
                    90 days, paid back by a third party, or forgiven.</dd>
                <dt>Misc Receipts (Miscellaneous Receipts)</dt>
                <dd>
                    <%
                        if (!this._isTIE)
                        { %>
                    <strong>Misc Receipts (Miscellaneous Receipts)</strong> is the total income derived
                    from sources other than contributions, loans, or public funds payments, such as
                    interest and proceeds from sales or leases. It also includes any transfer from a
                    committee solely supporting the same candidate that is not taking part in this election.
                    <%
                        }
                        else
                        { %>
                    <strong>Misc Receipts (Miscellaneous Receipts)</strong> is the total income derived
                    from sources other than contributions or loans, such as interest and proceeds from
                    sales or leases.
                    <%
                        } %>
                </dd>
                <dt>Loans Received</dt>
                <dd>
                    <strong>Loans Received</strong> is the total dollar amount of loans received by
                    the campaign.</dd>
                <%
                    if (!this._isTIE)
                    { %>
                <dt>Public Funds Received</dt>
                <dd>
                    <strong>Public Funds Received</strong> is the total amount of public funds disbursed
                    to the candidate for the primary, runoff, and/or general elections.</dd>
                <%
                    } %>
                <dt>Net Expenditures</dt>
                <dd>
                    <%
                        if (!this._isTIE)
                        { %>
                    <strong>Net Expenditures</strong> is the total of itemized and unitemized expenditure
                    payments, in-kind contributions, and transfers to a party or constituted committee,
                    less expenditure refunds.
                    <%
                        }
                        else
                        { %>
                    <strong>Net Expenditures</strong> is the total of itemized and unitemized expenditure
                    payments, and in-kind contributions.
                    <%
                        } %>
                </dd>
                <dt>Loans Paid</dt>
                <dd>
                    <strong>Loans Paid</strong> includes all payments of loan principal and interest
                    and any loans forgiven.</dd>
                <dt>Outstanding Bills</dt>
                <dd>
                    <strong>Outstanding Bills</strong> consists of the outstanding liabilities reported
                    on the last disclosure statement filed.</dd>
                <%
                    if (!this._isTIE)
                    { %>
                <dt>Public Funds Returned</dt>
                <dd>
                    <strong>Public Funds Returned</strong> is the total amount of public funds returned
                    by the candidate to the CFB to date (for this election cycle).</dd>
                <%
                    } %>
                <dt>Number of Contributors</dt>
                <dd>
                    <strong>Number of Contributors</strong> is the total number of individuals and other
                    entities which have made itemized contributions to the campaign.</dd>
                <%
                    if (!this._isTIE)
                    { %>
                <dt>Matching Claims</dt>
                <dd>
                    <strong>Matching Claims</strong> is the total monetary contributions that candidates
                    have claimed as qualifying for matching public funds. The CFB audits all claims
                    to determine their validity, and valid claims are matched at a rate of $6-to-$1
                    up to the first $175 given by any individual contributor, for a maximum of $1,050
                    in public funds per contributor.</dd>
                <%
                    } %>
            </dl>
        </details>
    </section>
</asp:Content>
