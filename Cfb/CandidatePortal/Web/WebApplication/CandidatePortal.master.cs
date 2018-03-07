using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.Web.WebApplication
{
    /// <summary>
    /// The default master page for the C-Access candidate portal.
    /// </summary>
    public partial class CandidatePortal : CPMasterPage
    {
        /// <summary>
        /// Hides the inaccurate data notice so it does not appear.
        /// </summary>
        public void HideInaccurateDataNotice()
        {
            this.PlaceHolderInaccurateData.Visible = false;
        }

        #region CPMasterPage Members

        /// <summary>
        /// Gets the web control used to display any errors.
        /// </summary>
        public override WebControl ErrorWebControl
        {
            get { return this.PageErrors; }
        }

        /// <summary>
        /// Gets the web control used to display any results.
        /// </summary>
        public override WebControl ResultWebControl
        {
            get { return this.PageResults; }
        }

        #endregion

        /// <summary>
        /// Performs session clean-up prior to log out.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        protected void DoLogOut(object sender, EventArgs e)
        {
            Server.Transfer(CPSecurity.LogoutPath);
        }

        protected void _cmoNewMessageCounter_Load(object sender, EventArgs e)
        {
            _cmoNewMessageCounter.Elections = CPProfile.Elections;
            _cmoNewMessageCounter.DataBind();
        }

        protected void _userHello_Load(object sender, EventArgs e)
        {
            _userHello.DisplayName = CPProfile.DisplayName;
        }
    }
}