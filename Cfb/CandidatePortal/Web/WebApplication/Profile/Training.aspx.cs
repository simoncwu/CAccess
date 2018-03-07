using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.Web.WebApplication.Profile
{
    public partial class Training : CPPage, ISecurePage
    {
        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TrainingStatus status = CPProfile.TrainingStatus;
                if (status != null)
                {
                    if (status.AuditComplianceAchieved)
                        _auditComplianceStatus.Text = "Yes";
                    if (status.CsmartComplianceAchieved)
                        _complianceStatus.Text = "Yes";
                }

                // bind data to controls
                _auditCourses.DataSource = _combinedCourses.DataSource = _supplementalCourses.DataSource = _complianceCourses.DataSource = _csmartCourses.DataSource = _advancedCourses.DataSource = status;
                _auditCourses.DataBind();
                _combinedCourses.DataBind();
                _supplementalCourses.DataBind();
                _complianceCourses.DataBind();
                _csmartCourses.DataBind();
                _advancedCourses.DataBind();
                _combinedWebCourses.DataSource = _combinedCourses.DataSource;
                _combinedWebCourses.DataBind();
                _csmartWebCourses.DataSource = _csmartCourses.DataSource;
                _csmartWebCourses.DataBind();

                // show custom message per section if no data present
                _emptyAuditMessage.Visible = !_auditCourses.HasSessions;
                _emptyComplianceMessage.Visible = !(_combinedCourses.HasSessions || _supplementalCourses.HasSessions || _complianceCourses.HasSessions || _csmartCourses.HasSessions || _csmartWebCourses.HasSessions || _advancedCourses.HasSessions);
                _footnote.Visible = !(_emptyAuditMessage.Visible && _emptyComplianceMessage.Visible);
            }
        }

        /// <summary>
        /// Determines whether or not the page has valid data.
        /// </summary>
        /// <returns>true if there is any data to be shown on the page; otherwise, false.</returns>
        protected override bool HasData()
        {
            return true; // always show training page because it has special language that handles absence of data
        }
    }
}