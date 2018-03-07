using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfb.CandidatePortal.Web.MvcApplication.Models;

namespace Cfb.CandidatePortal.Web.MvcApplication.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        public const string ControllerName = "Events";

        public const string ActionName_Announcements = "Announcements";

        public const string ActionName_Upcoming = "Upcoming";

        public const string ActionName_CalendarEvents = "CalendarEvents";

        private IEnumerable<Announcement> _defaultAnnouncements = new Dictionary<int, string> {
                { 4, "Welcome to C-Access!" },
                { 12, "Establishing Internal Controls for Your Campaign" },
                { 22, "Campaign Finance Handbook LIVE" },
                { 23, "Don't Wait! Fulfill Your 2017 Training Requirement" },
                { 24, "Compliance Alert: “Paid For By” Requirement" },
                { 25, "Join Us: New Statement Review Webinar!" },
                { 26, "New to the CFB Seminar!" },
                { 27, "Candidate Consultation" },
                { 28, "Compliance Alert: Multiple Committees" },
                { 29, "RSVP! NYC Votes Contribute: A New Way to Accept Online Contributions" }
            }.Select(d => new Announcement(d.Key)
            {
                Title = d.Value
            });

        private Dictionary<int, string> _defaultAnnouncementDetails = new Dictionary<int, string>
        {
            { 4, @"We thank you for taking part in the inaugural launch of C-Access and hope that the information you see on this website is helpful to you and your campaign.
 
This is just the first of many phases in C-Access’ expansion.  Please feel free to provide us with your feedback and to let us know what you would like to see in the future.  Many of the new developments will be based on the ideas you develop as you continue using the website.  You may also contact your liaison in the Candidate Services Unit with your comments, questions, and ideas.
 
New announcements will be posted periodically by the Candidate Services Unit.  Announcements will have important information relating to compliance, disclosure, and important upcoming deadlines.  Hopefully the announcements will serve as a learning aide for the campaign throughout the election cycle. 
 
We hope that by providing a secure, internet-based gateway to the CFB, C-Access will allow candidates and campaign staff to view and update their personalized CFB information 24 hours a day, seven days a week at their convenience." },
            { 12, @"The CFB has released Internal Controls: Best Practices for Political Campaigns in New York City, a helpful set of guidelines describing standard financial controls and procedures to help you protect and manage your assets effectively.  Having a system of internal controls in place can help you accurately report and document transactions in accordance with the Campaign Finance Act and Campaign Finance Board’s Rules.  The document should be read by all campaigns regardless of size and participation in the Campaign Finance Program. 
 
The document can be found on the CFB’s website. If you have any questions, contact your Candidate Services Liaison." },
            { 22, @"The Campaign Finance Handbook covering the 2017 election cycle (version 1) is now LIVE on the CFB website: http://www.nyccfb.info/candidates/candidates/handbooks/2017_Handbook.pdf
 
While the majority of the content is the same as the Handbook used during the 2013 election cycle, this version has been updated with references and examples for the 2017 election cycle, including:
 
·        Disclosure Statement Deadlines
·        Spending Limits
·        Public Funds Maximum
 
Future enhancements to the Handbook will occur in response to changes to the Campaign Finance Act or CFB Rules. We may also update guidance related to specific compliance or documentation requirements. Campaigns will be notified of future changes noting the latest Handbook version.
 
Please familiarize yourself with the updated Handbook and refer to the additional resources, guidance documents, and tips found in the Campaign Tool Box of the CFB website." },
            { 23, @"Did you know for each election cycle your campaign needs to complete a CFB training? The candidate, treasurer, campaign manager or other individual with significant managerial control over a campaign is required to attend both a Compliance and a C-SMART training.
 
Don't wait until the election year! Review the training calendar today on the CFB website and register by contacting CSUmail@nyccfb.info." },
            { 24, @"Effective immediately, all literature, advertisements, or other communications that your campaign pays for must have a “paid for by” disclaimer. Please see Local Law 40 and page 57 of the Handbook for more information.
 
Handbook Version 2 Available Now:
Pages 24, 33, 55, and 57 have been updated to incorporate this requirement. No other changes have been made at this time." },
            { 25, @"Are you ready to receive and respond to your next statement review? Participate in our new “Introduction to Statement Reviews” webinar to receive an overview of what a statement review is, common reports that may be included, and best practices to help your campaign respond timely and completely.
 
To register for an upcoming webinar, click the registration link below:
https://attendee.gotowebinar.com/rt/3809327669849753345
Sessions begin promptly at 2:00 PM, but we recommend joining the webinar 15 minutes before to ensure you are able to view and hear the webinar. FYI: Using GoToWebinar requires a one-time installation of the GoToWebinar app on your computer or device." },
            { 26, @"New to the CFB? Then this optional seminar geared towards first-time candidates, treasurers and campaign staff is for you. As part of our ongoing efforts to better equip campaigns, we created this additional training opportunity based on feedback from the last election cycle. You will receive an overview of some of the expectations and demands of running for a CFB-covered office.

View the schedule here and RSVP today!" },
            { 27, @"Sign Up for a Candidate Consultation! Upon completion of your campaign’s training requirement and submission of at least one disclosure statement, your campaign will have the opportunity to consult with your Candidate Services Liaison in person. This is a chance to get organized and understand Campaign Finance Board requirements outside of our trainings and regular phone/email correspondence. Your liaison will go over best practices, as well as address compliance practices specific to your campaign. Interested? Contact your Candidate Services Liaison at 212-409-1800, or CSUMail@nyccfb.info." },
            { 28, @"All committees authorized by the candidate (including from a previous election) must be disclosed to the CFB unless they have officially terminated with the New York State Board of Elections, the Federal Election Commission, or other relevant agency.
 
Important: Contributions received and expenditures made by any active committee will be presumed to be for your next election. This means that contributions may be subject to the 2017 contribution limits, pursuant to Rule 1-04(f). Further, if you are a Campaign Finance Program participant or anticipate joining the Program, each expenditure by an active committee may count toward the spending limit(s) for your 2017 committee, pursuant to Rules 1-08(c) (1) and (3). In order to prevent compliance problems, all active committees other than your 2017 authorized committee must cease all activity.
 
If you have any questions, please contact your Candidate Services Liaison." },
            { 29, @"The CFB knows how hard campaigns work in order to collect credit card contributions that meet our requirements and could be matched with public funds. That’s why we are excited to share NYC Votes Contribute (NYCVotes.org) with you. Finally, a website that simplifies the process of raising small-dollar contributions by credit card for NYC campaigns and contributors alike!
 
Why NYC Votes Contribute?
It streamlines your campaign’s compliance with CFB audit and reporting requirements.
The CFB-created interface has the ability to import contributions and documentation into C-SMART.
Individuals can contribute directly via your campaign’s page on NYCVotes.org or you can embed a tool on your campaign’s existing website.
It can be set up in an hour or less!
Use of Contribute is optional; it is intended to supplement your campaign’s current fundraising efforts.
 
Orientation – RSVP!
We encourage all campaigns to attend one of the launch events listed below to learn what is Contribute, how you sign up and use it, as well as general compliance reminders. Attendees will also have the opportunity to receive 1-on-1 assistance in setting up your own campaign’s account. Sign up today for the session that works best for you – space is limited.
 
DATE
TIME
 
Wednesday, February 24th
1:00pm-3:00pm
Click Here to Register
Thursday, March 3rd
1:00pm-3:00pm
Click Here to Register
Wednesday, March 9th
5:30pm-7:30pm
Click Here to Register
 
Note: Light refreshments will be served. Each session will last an hour with an additional hour built in for campaigns to receive assistance setting up their accounts.
 
If you have any questions, review About NYC Votes Contribute and contact your Candidate Services Liaison directly." }
        };

        private IEnumerable<Announcement> GetAnnouncements()
        {
            var data = CPProviders.DataProvider.GetAnnouncements(CPProfile.ElectionCycle);
            return data.Any() ? data : _defaultAnnouncements;
        }

        private Announcement GetAnnouncement(int id)
        {
            var data = CPProviders.DataProvider.GetAnnouncement(id.ToString());
            if (data == null)
            {
                data = _defaultAnnouncements.Single(a => a.ID == id);
                data.Body = _defaultAnnouncementDetails[id];
            }
            return data;
        }

        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        // GET: Announcements
        [ChildActionOnly]
        public ActionResult Announcements()
        {
            return PartialView(EventViewModelsFactory.AnnouncementsFrom(GetAnnouncements()));
        }

        // GET: Announcement/{id}
        public ActionResult Announcement(int id)
        {
            return View(EventViewModelsFactory.AnnouncementFrom(GetAnnouncement(id)));
        }

        // GET: Upcoming
        [ChildActionOnly]
        public ActionResult Upcoming(bool condensed = false)
        {
            return PartialView(EventViewModelsFactory.UpcomingEvents(CPProfile.ReminderEvents, condensed));
        }

        // GET: CalendarEvents
        public ActionResult CalendarEvents()
        {
            return Json(EventViewModelsFactory.CalendarEventsJsonFrom(CPProfile.CalendarEvents), JsonRequestBehavior.AllowGet);
        }
    }
}