using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
    /// <summary>
    /// A custom <see cref="UserControl"/> for displaying and handling a high-level category of account management operations.
    /// </summary>
    internal abstract class AccountManagementControl : WebControl
    {
        /// <summary>
        /// The view state key used to persist candidate data.
        /// </summary>
        readonly string _candidateCacheKey;

        /// <summary>
        /// The view state key used to persist user account data.
        /// </summary>
        readonly string _userCacheKey;

        /// <summary>
        /// The candidate to perform account management operations on.
        /// </summary>
        protected Candidate _candidate;

        /// <summary>
        /// Gets the candidate to perform account management operations on.
        /// </summary>
        public Candidate Candidate
        {
            get { return _candidate; }
        }

        /// <summary>
        /// Loads the <see cref="Candidate"/> specified in the current request.
        /// </summary>
        protected void LoadCandidate()
        {
            string cid = Page.Request[_candidateCacheKey];
            _candidate = !string.IsNullOrWhiteSpace(cid) ? Cfis.GetCandidate(cid) : null;
        }

        /// <summary>
        /// Loads the <see cref="MembershipUser"/> specified in the current request.
        /// </summary>
        protected void LoadUser()
        {
            string username = Page.Request[_userCacheKey];
            _user = !string.IsNullOrWhiteSpace(username) ? Membership.GetUser(username) : null;
        }

        /// <summary>
        /// Sets the candidate for the session.
        /// </summary>
        protected void SetCandidate(Candidate c)
        {
            Page.Session[_candidateCacheKey] = _candidate = c;
        }

        /// <summary>
        /// Sets the user account for the session. Also sets the candidate for the session to the one affiliated with the user.
        /// </summary>
        /// <param name="user">The <see cref="MembershipUser"/> to set.</param>
        protected void SetUser(MembershipUser user)
        {
            Page.Session[_userCacheKey] = _user = user;
            if (user != null)
                SetCandidate(Cfis.GetCandidate(UserManagement.GetCfisId(user.UserName)));
        }

        /// <summary>
        /// The current C-Access user account.
        /// </summary>
        protected MembershipUser _user;

        /// <summary>
        /// Gets the current C-Access user account.
        /// </summary>
        public MembershipUser User
        {
            get { return _user; }
        }

        /// <summary>
        /// The viewer's introductory message.
        /// </summary>
        protected string _messageText;

        /// <summary>
        /// Gets or sets the viewer's introductory message.
        /// </summary>
        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManagementControl"/> control.
        /// </summary>
        protected AccountManagementControl()
        {
            _candidateCacheKey = this.UniqueID + "_candidateCacheKey";
            _userCacheKey = this.UniqueID + "_userCacheKey";
        }

        /// <summary>
        /// A <see cref="Delegate"/> for rendering output to a <see cref="HtmlTextWriter" />.
        /// </summary>
        /// <param name="writer"></param>
        protected delegate void RenderDelegate(HtmlTextWriter writer);

        /// <summary>
        /// The <see cref="Delegate"/> for rendering this control based on view.
        /// </summary>
        protected RenderDelegate _renderByView;

        /// <summary>
        /// Sets the view to be rendered and updates the behavior of controls accordingly.
        /// </summary>
        /// <param name="renderer">A <see cref="RenderDelegate"/> for rendering the control.</param>
        protected abstract void SetView(RenderDelegate renderer);

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            LoadCandidate();
            LoadUser();
            EnsureChildControls();
        }

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event. Also loads a candidate and user from persistence store.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack) ReloadUser();
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.WriteLine("<div class=\"cp-{0}\">", typeof(AccountManagementControl).Name);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.WriteLine("</div>");
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (_renderByView != null) _renderByView(writer);
        }

        /// <summary>
        /// Reloads the current <see cref="MembershipUser"/> from the <see cref="Membership"/> database store.
        /// </summary>
        protected virtual void ReloadUser()
        {
            if (_user != null)
                SetUser(Membership.GetUser(_user.UserName));
        }

        /// <summary>
        /// Populates a <see cref="DropDownList"/> with all known candidates.
        /// </summary>
        /// <param name="list">The <see cref="DropDownList"/> to fill.</param>
        protected void FillCandidates(DropDownList list)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("(Select a candidate below)", string.Empty));
            Dictionary<string, Candidate> candidates = Cfis.GetCandidates();
            foreach (Candidate c in candidates.Values)
            {
                list.Items.Add(new ListItem(string.Format("{0} (ID: {1})", c.Name, c.ID), c.ID));
            }
        }

        /// <summary>
        /// Populates a <see cref="DropDownList"/> with all known candidates active in a specific election cycle.
        /// </summary>
        /// <param name="list">The <see cref="DropDownList"/> to fill.</param>
        /// <param name="electionCycle">The election cycle to filter by.</param>
        protected void FillActiveCandidates(DropDownList list, string electionCycle)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("(Select a candidate below)", string.Empty));
            Dictionary<string, ActiveCandidate> candidates = ActiveCandidate.GetActiveCandidates(electionCycle);
            foreach (var c in candidates.Values)
            {
                list.Items.Add(new ListItem(string.Format("{0} (ID: {1})", c.Name, c.ID), c.ID));
            }
        }

        /// <summary>
        /// Populates a <see cref="DropDownList"/> with all known user accounts.
        /// </summary>
        /// <param name="list">The <see cref="DropDownList"/> to fill.</param>
        protected void FillUsers(DropDownList list)
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            list.Items.Clear();
            if (users.Count > 0)
            {
                list.Items.Add(new ListItem("(select a user account)", string.Empty));
                // BUGFIX #31 - sort users list by user display name and not username
                List<ListItem> tempList = new List<ListItem>();
                foreach (MembershipUser user in users)
                {
                    tempList.Add(new ListItem(UserManagement.GetFullName(user), user.UserName));
                }
                foreach (ListItem item in tempList.OrderBy(i => i.Text))
                {
                    list.Items.Add(item);
                }
            }
            else
            {
                list.Items.Add(new ListItem("(no user accounts exist)"));
            }
        }

        /// <summary>
        /// Populates a <see cref="DropDownList"/> with the current candidate's election cycles.
        /// </summary>
        /// <param name="list">The <see cref="DropDownList"/> to populate.</param>
        protected void FillActiveElections(DropDownList list)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("(no election cycles available)", string.Empty));
            if (_candidate == null) return;
            HashSet<string> aec = Elections.GetActiveElectionCycles(_candidate.ID);
            if (aec.Count > 0)
            {
                list.Items.Clear();
                list.Items.Add(new ListItem("(select an election cycle)", string.Empty));
                foreach (string ec in aec)
                {
                    list.Items.Add(ec);
                }
            }
        }

        /// <summary>
        /// Populates a <see cref="DropDownList"/> with the all supported election cycles.
        /// </summary>
        /// <param name="list">The <see cref="DropDownList"/> to populate.</param>
        protected void FillElectionCycles(DropDownList list)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("(no election cycles available)", string.Empty));
            Elections elections = Elections.GetElections(CPProviders.SettingsProvider.MinimumElectionCycle);
            if (elections.Count > 0)
            {
                list.Items.Clear();
                list.Items.Add(new ListItem("(select an election cycle)", string.Empty));
                foreach (Election ec in elections)
                {
                    list.Items.Add(ec.Cycle);
                }
            }
        }

        /// <summary>
        /// Populates a <see cref="DropDownList"/> with known contacts for a campaign for a specific election cycle.
        /// </summary>
        /// <param name="list">The <see cref="DropDownList"/> to populate.</param>
        /// <param name="electionCycle">The election cycle in which to search for contacts.</param>
        protected void FillEligibleContacts(DropDownList list, string electionCycle)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("(no contacts available)", string.Empty));
            if (_candidate == null) return;
            AnalysisResults results = AccountAnalysis.Analyze(_candidate, electionCycle);
            if (results.EligibleContacts.Count > 0)
            {
                list.Items.Clear();
                list.Items.Add(new ListItem("(select a campaign contact)", string.Empty));
                foreach (string id in results.EligibleContacts.Keys)
                {
                    list.Items.Add(new ListItem(results.EligibleContacts[id].Name, id));
                }
            }
        }
    }
}
