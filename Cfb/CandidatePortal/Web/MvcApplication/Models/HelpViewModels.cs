using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CfbEntity = Cfb.Cfis.CampaignContacts.Entity;

namespace Cfb.CandidatePortal.Web.MvcApplication.Models
{
    public class ContactViewModel
    {
        public string SelectedCategory { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public string Message { get; set; }
    }

    public class CampaignContactViewModel
    {
        public string ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        public char? MiddleInitial { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        public string CandidateID { get; set; }

        public string Type { get; set; }
    }

    public class AccountRequestViewModel
    {
        public IEnumerable<SelectListItem> EligibleContacts;

        public IEnumerable<CampaignContactViewModel> IneligibleContacts;
    }

    public sealed class HelpViewModelFactory
    {
        public static ContactViewModel Contact()
        {
            return new ContactViewModel
            {
                Categories = new[] {
                    new SelectListItem() { Text = "General Comments", Value = "general" },
                    new SelectListItem() { Text = "Candidate Services", Value = "csu" },
                    new SelectListItem() { Text = "Audit and Compliance", Value = "audit" }
                }
            };
        }

        private static CampaignContactViewModel CampaignContactFrom(AccountEligibilityStatus source)
        {
            if (source == null || source.Entity == null)
                return new CampaignContactViewModel();
            var model = CampaignContactFrom(source.ContactID, source.Entity);
            model.Reason = source.Status.GetDescription();
            return model;
        }

        public static CampaignContactViewModel CampaignContactFrom(string id, CfbEntity source, string candidateID = null)
        {
            return source == null ? new CampaignContactViewModel() : new CampaignContactViewModel
            {
                ID = id,
                FirstName = source.FirstName,
                MiddleInitial = source.MiddleInitial,
                LastName = source.LastName,
                FullName = source.GetFullName(false),
                Email = source.Email,
                Type = source.Type.ToString(),
                CandidateID = candidateID
            };
        }

        private static CampaignContactViewModel CampaignContactFrom(CPUser source)
        {
            return source == null ? new CampaignContactViewModel() : new CampaignContactViewModel
            {
                FullName = source.DisplayName,
                Email = source.Email,
                Reason = string.Format("Existing User ({0})", source.Email)
            };
        }

        public static AccountRequestViewModel AccountRequestFrom(AnalysisResults source)
        {
            return source == null ? new AccountRequestViewModel() : new AccountRequestViewModel
            {
                EligibleContacts = source.EligibleContacts.Select(c => new SelectListItem() { Text = c.Value.Name, Value = c.Key }),
                IneligibleContacts = source.IneligibleContacts.Select(c => CampaignContactFrom(c)).Concat(
                    source.CurrentUsers.Select(c => CampaignContactFrom(c)))
            };
        }
    }
}