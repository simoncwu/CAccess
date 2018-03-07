using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Web.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cfb.CandidatePortal.Web.MvcApplication.Controllers
{
    [Authorize]
    public class HelpController : Controller
    {
        public const string ControllerName = "Help";

        public const string ActionName_About = "About";
        public const string ActionName_Contact = "Contact";
        public const string ActionName_Breakdown = "Breakdown";
        public const string ActionName_TermsDefinitions = "TermsDefinitions";
        public const string ActionName_ReportInaccurateData = "ReportInaccurateData";
        public const string ActionName_AccountRequest = "AccountRequest";
        public const string ActionName_DataDisclaimer = "DataDisclaimer";
        public const string ActionName_Privacy = "Privacy";

        private const string Audit_Email_Subject = "C-Access Audit/Compliance Inquiry";
        private const string CSU_Email_Subject = "C-Access Candidate Services Inquiry";
        private const string Default_Email_Subject = "C-Access General Comments";
        private const string InaccurateData_Email_Subject = "Data Inaccuracy Issues";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View(HelpViewModelFactory.Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(ActionName_Contact);

            string toEmail = "simon.c.wu@gmail.com";
            string subject;
            switch (model.SelectedCategory)
            {
                case "audit":
                    //toEmail = CPApplication.AuditEmail;
                    subject = Audit_Email_Subject;
                    break;
                case "csu":
                    //toEmail = CPApplication.CsuEmail;
                    subject = CSU_Email_Subject;
                    break;
                case "data":
                    //toEmail = CPApplication.CsuEmail;
                    subject = InaccurateData_Email_Subject;
                    break;
                default:
                    //toEmail = CPApplication.GeneralEmail;
                    subject = Default_Email_Subject;
                    break;
            }

            // format message body
            StringBuilder body = new StringBuilder();
            body.AppendLine("Sender Details:");
            body.AppendLine("---------------");
            body.AppendFormat("{0} Username: {1}\r\n", CPProviders.SettingsProvider.ApplicationName, User.Identity.Name);
            body.AppendLine("Full name: " + CPProfile.DisplayName);
            body.AppendLine("Candidate ID: " + CPProfile.Cid);
            body.AppendLine("Candidate Name: " + CPProfile.ActiveCandidate.Name);
            body.AppendLine("Election Cycle: " + CPProfile.ElectionCycle);
            body.AppendLine("---------------");
            body.AppendLine();
            body.AppendLine("Message Details:");
            body.AppendLine("----------------");
            body.AppendLine(model.Message);

            // assemble/send message
            using (CPMailMessage message = new CPMailMessage())
            {
                message.Sender = new MailAddress(toEmail); //CPProfile.GetMailAddress();
                message.Recipient = new MailAddress(toEmail);
                message.Subject = subject;
                message.Body = body.ToString();
                //message.Send();
            }

            return PartialView("_ContactConfirmation");
        }

        public ActionResult Breakdown()
        {
            return View();
        }

        public ActionResult TermsDefinitions()
        {
            return View();
        }

        public ActionResult ReportInaccurateData()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AccountRequest(string id = null)
        {
            var rawData = CPProfile.AnalysisResults;
            rawData.BypassEmail(CPSecurity.Provider.GetEmail(User.Identity.Name));
            if (id == null || !Request.IsAjaxRequest())
            {
                var model = HelpViewModelFactory.AccountRequestFrom(rawData);
                model.IneligibleContacts = model.IneligibleContacts;
                return View(model);
            }
            return PartialView("_AccountRequestContact", (from c in rawData.EligibleContacts
                                                          where c.Key == id
                                                          select HelpViewModelFactory.CampaignContactFrom(c.Key, c.Value, CPProfile.Cid)).SingleOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(ActionName_AccountRequest)]
        public ActionResult SubmitAccountRequest(string id)
        {
            if (!Request.IsAjaxRequest())
                return RedirectToAction(ActionName_AccountRequest);

            var rawData = CPProfile.AnalysisResults;
            var contact = (from c in rawData.EligibleContacts
                           where c.Key == id
                           select HelpViewModelFactory.CampaignContactFrom(c.Key, c.Value, CPProfile.Cid)).Single();

            // assemble message
            StringBuilder body = new StringBuilder();
            MailAddress requestorEmail = CPProfile.GetMailAddress();
            body.AppendFormat("{0} user {1} (username: {2}) has requested an additional account for the following campaign contact:", CPProviders.SettingsProvider.ApplicationName, requestorEmail.DisplayName, User.Identity.Name);
            body.AppendLine();
            body.AppendLine();
            body.AppendFormat("First Name: {0}", contact.FirstName);
            body.AppendFormat("Middle Initial: {0}", contact.MiddleInitial);
            body.AppendFormat("Last Name: {0}", contact.LastName);
            body.AppendFormat("E-mail Address: {0}", contact.Email);
            body.AppendLine();
            body.AppendLine("Candidate ID: " + CPProfile.Cid);
            body.AppendLine("Contact Type: " + contact.Type);
            using (CPMailMessage message = new CPMailMessage
            {
                Sender = requestorEmail,
                Recipient = new MailAddress(CPApplication.CsuEmail),
                Subject = string.Format("{0} Account Request (Candidate: {1})", CPProviders.SettingsProvider.ApplicationName, CPProfile.ActiveCandidate.Name),
                Body = body.ToString()
            })
            {
                //message.Send();
            }

            return PartialView("_AccountRequestConfirmation");
        }

        public ActionResult DataDisclaimer()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Privacy()
        {
            return View();
        }
    }
}