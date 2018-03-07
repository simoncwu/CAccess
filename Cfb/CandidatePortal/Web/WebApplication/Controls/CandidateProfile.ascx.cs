using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.Web;
using Cfb.Cfis.CampaignContacts;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.WebApplication.Controls
{
    public partial class CandidateProfile : UserControl
    {
        protected bool _isTIE = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                Election ec = CPApplication.Elections[CPProfile.ElectionCycle];
                _isTIE = ec != null && ec.IsTIE;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ShowCandidate(CPProfile.ActiveCandidate);
        }

        /// <summary>
        /// Updates control fields with an active candidate's profile information.
        /// </summary>
        /// <param name="ac">The active candidate profile to display.</param>
        private void ShowCandidate(ActiveCandidate ac)
        {
            if (ac == null)
            {
                _errorMessage.Text = string.Format("You do not currently have an active candidate profile for the {0} election.", CPProfile.ElectionCycle);
                _errorMessage.Visible = true;
                _details.Visible = false;
            }
            else
            {
                PostalAddress address;

                // basic candidate info
                _ID.Value = ac.ID;
                _salutation.Value = CPConvert.ToString(ac.Honorific);
                _lastName.Value = ac.LastName;
                _firstName.Value = ac.FirstName;
                _mi.Value = ac.MiddleInitial.HasValue ? ac.MiddleInitial.Value.ToString() : null;
                address = ac.Address;
                if (!object.Equals(address, null))
                {
                    _streetNumber.Value = address.StreetNumber;
                    _streetName.Value = address.StreetName;
                    _apartment.Value = address.Apartment;
                    _city.Value = address.City;
                    _state.Value = CPConvert.ParseStateCode(address.State);
                    if (!object.Equals(address.Zip, null))
                        _zip.Value = address.Zip.ToString();
                }
                if (!object.Equals(ac.DaytimePhone, null))
                    _daytimePhone.Value = ac.DaytimePhone.ToString();
                if (!object.Equals(ac.EveningPhone, null))
                    _eveningPhone.Value = ac.EveningPhone.ToString();
                if (!object.Equals(ac.Fax, null))
                    _fax.Value = ac.Fax.ToString();
                if (!string.IsNullOrEmpty(ac.Email))
                    _email.Value = string.Format("<a href=\"mailto:{0}\">{0}</a>", ac.Email);

                // employer info
                Entity employer = ac.Employer;
                if (!object.Equals(employer, null))
                {
                    _empName.Value = employer.Name;
                    address = employer.Address;
                    if (!object.Equals(address, null))
                    {
                        _empStreetNumber.Value = address.StreetNumber;
                        _empStreetName.Value = address.StreetName;
                        _empCity.Value = address.City;
                        _empState.Value = CPConvert.ParseStateCode(address.State);
                        if (!object.Equals(address.Zip, null))
                            _empZip.Value = address.Zip.ToString();
                    }
                    if (!object.Equals(employer.DaytimePhone, null))
                        _empPhone.Value = employer.DaytimePhone.ToString();
                    if (!object.Equals(employer.Fax, null))
                        _empFax.Value = employer.Fax.ToString();
                }

                // candidate activation info
                if (ac.FilerRegistrationDate.HasValue)
                    _frDate.Value = ac.FilerRegistrationDate.Value.ToDateString();
                else
                    _frDate.Value = "(n/a)";
                _party.Value = ac.PoliticalParty;
                if (_isTIE)
                {
                    _office.Visible = _certDate.Visible = _terminationDate.Visible = _boroDistrict.Visible = _classification.Visible = _ddAuth.Visible = _rrddAuth.Visible = false;
                }
                else
                {
                    _office.Value = CPConvert.ToString(ac.Office.Type);
                    if (ac.CertificationDate.HasValue)
                        _certDate.Value = ac.CertificationDate.Value.ToDateString();
                    else
                        _certDate.Value = "(n/a)";
                    if (ac.IsTerminated)
                    {
                        _terminationDate.Value = ac.TerminationDate.Value.ToDateString();
                        _terminationDate.Visible = true;
                    }
                    if (_boroDistrict.Visible = (ac.Office.Type == NycPublicOfficeType.BoroughPresident) || (ac.Office.Type == NycPublicOfficeType.CityCouncilMember))
                    {
                        NycBorough borough;
                        byte district;
                        if (ac.Office.TryGetBorough(out borough))
                        {
                            _boroDistrict.LabelText = "Borough";
                            _boroDistrict.Value = CPConvert.ToString(borough);
                        }
                        else if (ac.Office.TryGetDistrict(out district))
                        {
                            _boroDistrict.LabelText = "District";
                            _boroDistrict.Value = district.ToString();
                        }
                    }
                    _classification.Value = CPConvert.ToString(ac.Classification);
                    _ddAuth.Value = ac.IsDirectDepositAuthorized ? "Yes" : "No";
                    if (_rrddAuth.Visible = HasRRAccounts(ac))
                    {
                        _rrddAuth.Value = ac.IsRRDirectDepositAuthorized ? "Yes" : "No";
                    }
                }
                _lastUpdated.Text = "Data last modified: " + ac.LastUpdated.ToDateString();
            }
        }

        /// <summary>
        /// Determines whether or not an <see cref="ActiveCandidate"/> has rerun or runoff bank accounts.
        /// </summary>
        /// <param name="ac">The <see cref="ActiveCandidate"/> to examine.</param>
        /// <returns>true if <paramref name="ac"/> has rerun or runoff bank accounts, otherwise false.</returns>
        bool HasRRAccounts(ActiveCandidate ac)
        {
            foreach (AuthorizedCommittee comm in CPProfile.AuthorizedCommittees.Committees.Values)
            {
                foreach (BankAccount ba in comm.BankAccounts.Values)
                {
                    if (ba.Purpose == BankAccountPurpose.RunoffRerun)
                        return true;
                }
            }
            return false;
        }
    }
}