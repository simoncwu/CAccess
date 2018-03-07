using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Web;
using Cfb.Cfis.CampaignContacts;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.WebApplication.Controls
{
    public partial class CommitteeProfile : UserControl
    {
        /// <summary>
        /// The authorized committees for the current user.
        /// </summary>
        private AuthorizedCommittees _acs;

        protected override void OnInit(EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(Page);
            if (object.Equals(sm, null))
            {
                sm = new ScriptManager();
                sm.SupportsPartialRendering = true;
                this.Controls.Add(sm);
            }
            _acs = CPProfile.AuthorizedCommittees;
            if (!Page.IsPostBack)
            {
                if (_acs.Committees.Count > 1)
                {
                    foreach (var committee in new[] { _acs.PrincipalCommittee }.Union(_acs.OtherCommittees))
                    {
                        ListItem li = new ListItem(GetCommitteeDisplayName(committee), committee.ID.ToString());
                        li.Attributes["data-class"] = "test";
                        _committeeSelection.Items.Add(li);
                    }
                }
                else if (_acs.Committees.Count == 1)
                {
                    _committeeSelection.Visible = false;
                    _singleCommittee.Visible = true;
                    _singleCommittee.Text = GetCommitteeDisplayName(_acs.Committees.Values.First());
                }
            }
        }

        private string GetCommitteeDisplayName(Committee c)
        {
            return c == null ? null : string.Format("{0} (ID: {1})", c.Name, c.ID);
        }

        protected override void OnLoad(EventArgs e)
        {
            if ((_acs == null) || (_acs.Committees.Count == 0))
            {
                _errorMessage.Text = string.Format("You do not currently have any committees authorized for the {0} election.", CPProfile.ElectionCycle);
                _errorMessage.Visible = true;
                _committeePanel.Visible = false;
            }
            else
            {
                // fetch requested committee
                if (!Page.IsPostBack)
                    ShowCommittee(Page.Request.GetCommitteeID());
            }
        }

        /// <summary>
        /// Updates control fields with an authorized committee's details.
        /// </summary>
        /// <param name="id">The CFIS ID of the authorized committee to display.</param>
        private void ShowCommittee(char? id)
        {
            AuthorizedCommittee ac = id.HasValue ? GetAuthorizedCommittee(id.Value) : GetDefaultCommittee();
            if (ac == null)
            {
                _errorMessage.Text = "Sorry, the committee you have requested could not be retrieved.";
                _errorMessage.Visible = true;
                _committeeDetails.Visible = false;
            }
            else
            {
                ShowCommittee(ac);
            }
        }

        /// <summary>
        /// Updates control fields with an authorized committee's details.
        /// </summary>
        /// <param name="ac">The authorized committee to display.</param>
        private void ShowCommittee(AuthorizedCommittee ac)
        {
            PostalAddress address;
            if (ac == null)
            {
                _errorMessage.Text = string.Format("You do not currently have any committees authorized for the {0} election.", CPProfile.ElectionCycle);
                _errorMessage.Visible = true;
                _committeeDetails.Visible = false;
            }
            else
            {
                // show committee data
                _committeeDetails.Visible = true;
                // committee info
                _principal.Value = ac.IsPrincipal ? "Yes" : "No";
                _active.Value = ac.IsActive ? "Yes" : "No";
                _boeDate.Value = (ac.BoeDate != null) ? ((DateTime)ac.BoeDate).ToDateString() : null;
                _terminationDate.Value = (_terminationDate.Visible = (ac.TerminationDate != null)) ? ((DateTime)ac.TerminationDate).ToDateString() : null;

                // address info
                address = ac.Address;
                if (address != null)
                {
                    _StreetNumber.Value = address.StreetNumber;
                    _StreetName.Value = address.StreetName;
                    _Apartment.Value = address.Apartment;
                    _City.Value = address.City;
                    _State.Value = CPConvert.ParseStateCode(address.State);
                    _Zip.Value = (address.Zip != null) ? address.Zip.ToString() : null;
                }
                else
                {
                    _StreetNumber.Value = _StreetName.Value = _Apartment.Value = _City.Value = _State.Value = _Zip.Value = null;
                }

                // last election info
                _lastDate.Value = (ac.LastElectionDate != null) ? ((DateTime)ac.LastElectionDate).ToDateString() : null;
                _lastOffice.Value = ac.LastElectionOffice;
                _lastDistrict.Value = ac.LastElectionDistrict;
                _lastParty.Value = ac.LastPrimaryParty;

                // mailing address info
                address = ac.MailingAdress;
                if (address != null)
                {
                    _mailingLine1.Value = address.AddressLine1;
                    _mailingStreetNumber.Value = address.StreetNumber;
                    _mailingStreetName.Value = address.StreetName;
                    _mailingApartment.Value = address.Apartment;
                    _mailingCity.Value = address.City;
                    _mailingState.Value = CPConvert.ParseStateCode(address.State);
                    _mailingZip.Value = (address.Zip != null) ? address.Zip.ToString() : null;
                }
                else
                {
                    _mailingLine1.Value = _mailingStreetNumber.Value = _mailingStreetName.Value = _mailingApartment.Value = _mailingCity.Value = _mailingState.Value = _mailingZip.Value = null;
                }

                // contact info
                _daytimePhone.Value = (ac.DaytimePhone != null) ? ac.DaytimePhone.ToString() : null;
                _eveningPhone.Value = (ac.EveningPhone != null) ? ac.EveningPhone.ToString() : null;
                _fax.Value = (ac.Fax != null) ? ac.Fax.ToString() : null;
                _email.Value = (!string.IsNullOrWhiteSpace(ac.Email)) ? string.Format("<a href=\"mailto:{0}\">{0}</a>", ac.Email) : null;
                if (!string.IsNullOrWhiteSpace(ac.WebsiteUrl))
                {
                    string url = ac.WebsiteUrl;
                    if (!url.StartsWith("http://"))
                        url = "http://" + url;
                    _webUrl.Value = string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", url);
                }
                else
                {
                    _webUrl.Value = null;
                }

                // treasurer info
                Entity treasurer = ac.Treasurer;
                if (treasurer != null)
                {
                    _salutation.Value = CPConvert.ToString(treasurer.Honorific);
                    _lastName.Value = treasurer.LastName;
                    _firstName.Value = treasurer.FirstName;
                    _mi.Value = treasurer.MiddleInitial.HasValue ? treasurer.MiddleInitial.Value.ToString() : null;
                    _contactOrder.Value = treasurer.ContactOrder.ToString();
                    address = treasurer.Address;
                    if (address != null)
                    {
                        _treasStreetNumber.Value = address.StreetNumber;
                        _treasStreetName.Value = address.StreetName;
                        _treasApartment.Value = address.Apartment;
                        _treasCity.Value = address.City;
                        _treasState.Value = CPConvert.ParseStateCode(address.State);
                        _treasZip.Value = (address.Zip != null) ? address.Zip.ToString() : null;
                    }
                    else
                    {
                        _treasStreetNumber.Value = _treasStreetName.Value = _treasApartment.Value = _treasCity.Value = _treasState.Value = _treasZip.Value = null;
                    }
                    _treasDaytimePhone.Value = (treasurer.DaytimePhone != null) ? treasurer.DaytimePhone.ToString() : null;
                    _treasEveningPhone.Value = (treasurer.EveningPhone != null) ? treasurer.EveningPhone.ToString() : null;
                    _treasFax.Value = (treasurer.Fax != null) ? treasurer.Fax.ToString() : null;
                    _treasEmail.Value = !string.IsNullOrWhiteSpace(treasurer.Email) ? string.Format("<a href=\"mailto:{0}\">{0}</a>", treasurer.Email) : null;

                    // treasurer employer info
                    Entity employer = treasurer.Employer;
                    if (employer != null)
                    {
                        _empName.Value = employer.LastName;
                        address = employer.Address;
                        if (address != null)
                        {
                            _empStreetNumber.Value = address.StreetNumber;
                            _empStreetName.Value = address.StreetName;
                            _empCity.Value = address.City;
                            _empState.Value = CPConvert.ParseStateCode(address.State);
                            _empZip.Value = (address.Zip != null) ? address.Zip.ToString() : null;
                        }
                        else
                        {
                            _empStreetNumber.Value = _empStreetName.Value = _empCity.Value = _empState.Value = _empZip.Value = null;
                        }
                        _empPhone.Value = (employer.DaytimePhone != null) ? employer.DaytimePhone.ToString() : null;
                        _empFax.Value = (employer.Fax != null) ? employer.Fax.ToString() : null;
                    }
                    else
                    {
                        _empName.Value = _empStreetNumber.Value = _empStreetName.Value = _empCity.Value = _empState.Value = _empZip.Value = _empPhone.Value = _empFax.Value = _empPhone.Value = _empFax.Value = null;
                    }
                }
                else
                {
                    _treasStreetNumber.Value = _treasStreetName.Value = _treasApartment.Value = _treasCity.Value = _treasState.Value = _treasZip.Value = _treasDaytimePhone.Value = _treasEveningPhone.Value = _treasFax.Value = null;
                }

                // update liaison list
                _liaisonError.Visible = false;
                _liaisonDetails.Visible = false;
                ListItem item = _liaisonList.Items[0];
                _liaisonList.Items.Clear();
                _liaisonList.Items.Add(item);
                foreach (Liaison l in ac.Liaisons.Values)
                {
                    string name = l.Name;
                    if (l.LiaisonType == LiaisonType.Consultant)
                        name += " *";
                    _liaisonList.Items.Add(new ListItem(name, ac.ID + l.ID.ToString()));
                }

                // update bank accounts list
                _bankError.Visible = false;
                _bankAccountDetails.Visible = false;
                item = _bankAccountsList.Items[0];
                _bankAccountsList.Items.Clear();
                _bankAccountsList.Items.Add(item);
                foreach (BankAccount ba in ac.BankAccounts.Values)
                {
                    _bankAccountsList.Items.Add(new ListItem(string.Format("{0} ({1})", ba.BankName, ba.Number), ac.ID + ba.ID.ToString()));
                }
            }
        }

        /// <summary>
        /// Event handler for updating control fields with a campaign liaison's details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected void ShowLiaison(object sender, EventArgs e)
        {
            Liaison l;
            PostalAddress address;
            byte id = 0;
            if (byte.TryParse(_liaisonList.SelectedValue.Substring(1), out id) && (id > 0))
            {
                string committeeID = _liaisonList.SelectedValue.Substring(0, 1);
                AuthorizedCommittee ac = GetAuthorizedCommittee(committeeID);
                // fetch liaison
                if (!ac.Liaisons.TryGetValue(id, out l))
                {
                    _liaisonError.Text = "Sorry, the requested campaign contact could not be retrieved.";
                    _liaisonError.Visible = true;
                    _liaisonDetails.Visible = false;
                    return;
                }
                if (l == null)
                {
                    // error: no liaisons
                    _liaisonError.Text = string.Format("You do not currently have any campaign liaisons or consultants for the {0} election.", CPProfile.ElectionCycle);
                    _liaisonError.Visible = true;
                    _liaisonDetails.Visible = false;
                }
                else
                {
                    // show liaison data
                    _liaisonError.Visible = false;
                    _liaisonDetails.Visible = true;
                    _liaisonType.Value = l.LiaisonType.ToString<LiaisonType>();
                    _liaisonOrder.Value = CPConvert.ToString(l.ContactOrder);
                    _liaisonSalutation.Value = CPConvert.ToString(l.Honorific);
                    _liaisonLastName.Value = l.LastName;
                    _liaisonFirstName.Value = l.FirstName;
                    _liaisonMiddleInitial.Value = l.MiddleInitial.HasValue ? l.MiddleInitial.Value.ToString() : null;
                    address = l.Address;
                    if (address != null)
                    {
                        _liaisonStreetNumber.Value = address.StreetNumber;
                        _liaisonStreetName.Value = address.StreetName;
                        _liaisonApartment.Value = address.Apartment;
                        _liaisonCity.Value = address.City;
                        _liaisonState.Value = CPConvert.ParseStateCode(address.State);
                        _liaisonZip.Value = (address.Zip != null) ? address.Zip.ToString() : null;
                    }
                    else
                    {
                        _liaisonStreetNumber.Value = _liaisonStreetName.Value = _liaisonApartment.Value = _liaisonCity.Value = _liaisonState.Value = _liaisonZip.Value = null;
                    }
                    _liaisonDaytimePhone.Value = (l.DaytimePhone != null) ? l.DaytimePhone.ToString() : null;
                    _liaisonEveningPhone.Value = (l.EveningPhone != null) ? l.EveningPhone.ToString() : null;
                    _liaisonFax.Value = (l.Fax != null) ? l.Fax.ToString() : null;
                    _liaisonEmail.Value = l.Email;
                    _liaisonManagerial.Value = l.HasManagerialControl ? "Yes" : "No";
                    _liaisonVG.Value = l.IsVGLiaison ? "Yes" : "No";
                    _entityName.Visible = l.LiaisonType == LiaisonType.Consultant;
                    _entityName.Value = l.EntityName;
                }
            }
            else
            {
                _liaisonDetails.Visible = false;
            }
            _liaisonUpdatePanel.Update();
        }

        /// <summary>
        /// Event handler for updating control fields with the details of a campaign bank account.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected void ShowBankAccount(object sender, EventArgs e)
        {
            BankAccount ba;
            byte id = 0;
            if (byte.TryParse(_bankAccountsList.SelectedValue.Substring(1), out id) && (id > 0))
            {
                string committeeID = _bankAccountsList.SelectedValue.Substring(0, 1);
                AuthorizedCommittee ac = GetAuthorizedCommittee(committeeID);
                // fetch bank account
                if (!ac.BankAccounts.TryGetValue(id, out ba))
                {
                    _bankError.Text = "Sorry, the requested bank account could not be retrieved.";
                    _bankError.Visible = true;
                    _bankAccountDetails.Visible = false;
                    return;
                }
                if (ba == null)
                {
                    // error: no bank accounts
                    _bankError.Text = string.Format("You do not currently have any bank accounts affiliated with your campaign for the {0} election", CPProfile.ElectionCycle);
                    _bankError.Visible = true;
                    _bankAccountDetails.Visible = false;
                }
                else
                {
                    // show bank account data
                    _bankError.Visible = false;
                    _bankAccountDetails.Visible = true;
                    _bankName.Value = ba.BankName;
                    _bankCity.Value = ba.City;
                    _bankState.Value = ba.State;
                    _bankZip.Value = (ba.Zip != null) ? ba.Zip.ToString() : null;
                    _dateOpened.Value = ba.OpeningDate.HasValue ? ba.OpeningDate.Value.ToDateString() : null;
                    _dateClosed.Value = ba.ClosingDate.HasValue ? ba.ClosingDate.Value.ToDateString() : null;
                    _balance.Value = ba.CurrentBalance.ToString("C", CultureInfo.CurrentCulture);
                    _balanceDate.Value = ba.CurrentBalanceDate.HasValue ? ba.CurrentBalanceDate.Value.ToDateString() : null;
                    _accountNumber.Value = ba.Number;
                    _accountName.Value = ba.Name;
                    _directDeposit.Value = ba.HasDirectDeposit ? "Yes" : "No";
                    _accountType.Value = ba.Type == BankAccountType.Other ? ba.OtherTypeSpecification : CPConvert.ToString(ba.Type);
                    _accountPurpose.Value = ba.Purpose == BankAccountPurpose.Other ? ba.OtherPurposeSpecification : CPConvert.ToString(ba.Purpose);
                }
            }
            else
            {
                _bankAccountDetails.Visible = false;
            }
            _bankAccountsUpdatePanel.Update();
        }

        /// <summary>
        /// Fetches an authorized committee by CFIS ID.
        /// </summary>
        /// <param name="id">The desired committee's CFIS ID.</param>
        /// <returns>The requested committee if found, else null.</returns>
        private AuthorizedCommittee GetAuthorizedCommittee(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim();
                if (id.Length >= 1)
                    return GetAuthorizedCommittee(id.ToCharArray()[0]);
            }
            return null;
        }

        /// <summary>
        /// Fetches an authorized committee by CFIS ID.
        /// </summary>
        /// <param name="id">The desired committee's CFIS ID.</param>
        /// <returns>The requested committee if found, else null.</returns>
        private AuthorizedCommittee GetAuthorizedCommittee(char id)
        {
            AuthorizedCommittee ac;
            return _acs.Committees.TryGetValue(id, out ac) ? ac : null;
        }

        /// <summary>
        /// Fetches the default committee for the logged-in user.
        /// </summary>
        /// <returns>The principal committee if one exists, else the first committee found if available, else null.</returns>
        private AuthorizedCommittee GetDefaultCommittee()
        {
            if (!object.Equals(_acs.PrincipalCommittee, null))
                return _acs.PrincipalCommittee;
            else if (_acs.Committees.Count > 0)
            {
                IEnumerator acEnum = _acs.Committees.Values.GetEnumerator();
                if (acEnum.MoveNext())
                    return acEnum.Current as AuthorizedCommittee;
            }
            return null;
        }

        protected void _committeeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_committeeSelection.SelectedValue))
                ShowCommittee(_committeeSelection.SelectedValue[0]);
        }
    }
}