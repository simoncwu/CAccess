using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Web.Mvc.Strings;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Cfb.CandidatePortal.Web.MvcApplication.Models
{
    public class AnnouncementViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class UpcomingEventsViewModel : List<CalendarEventModel>
    {
        public bool Condensed { get; set; }
    }

    public class CalendarEventModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool AllDay { get; set; }

        public string Url { get; set; }
    }

    public class CalendarEventJsonModel
    {
        public string title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public bool allDay { get; set; }

        public string url { get; set; }
    }

    public class EventViewModelsFactory
    {
        public static IEnumerable<AnnouncementViewModel> AnnouncementsFrom(IEnumerable<Announcement> source)
        {
            return source.Select(s => AnnouncementFrom(s));
        }

        public static AnnouncementViewModel AnnouncementFrom(Announcement source)
        {
            return new AnnouncementViewModel
            {
                ID = source.ID,
                Title = source.Title,
                Body = source.Body
            };
        }

        public static UpcomingEventsViewModel UpcomingEvents(IEnumerable<CPCalendarItem> source, bool condensed = false)
        {
            var model = new UpcomingEventsViewModel
            {
                Condensed = condensed
            };
            model.AddRange(from s in source select CalendarEventFrom(s));
            return model;
        }

        public static IEnumerable<CalendarEventJsonModel> CalendarEventsJsonFrom(IEnumerable<CPCalendarItem> source)
        {
            return from e in source
                   select CalendarEventJsonFrom(CalendarEventFrom(e));
        }

        private static CalendarEventJsonModel CalendarEventJsonFrom(CalendarEventModel source)
        {
            return source == null ? null : new CalendarEventJsonModel
            {
                title = source.Title,
                start = source.Start,
                end = source.End,
                allDay = source.AllDay,
                url = source.Url
            };
        }

        private static CalendarEventModel CalendarEventFrom(CPCalendarItem source)
        {
            return source == null ? null : new CalendarEventModel
            {
                Title = source.Title,
                Description = source.Description,
                Start = source.StartDate,
                End = source.HasEndDate ? source.EndDate.Date.AddDays(1) : source.StartDate,
                AllDay = source.IsAllDayEvent,
                Url = GetCalendarEventUrl(source)
            };
        }

        private static string GetCalendarEventUrl(CPCalendarItem item)
        {
            if (item == null)
                return null;

            // define route
            string action = null;
            string controller = null;
            switch (item.CalendarItemType)
            {
                case CPCalendarItemType.InitialDocumentRequest:
                case CPCalendarItemType.IdrResponseDeadline:
                case CPCalendarItemType.IdrInadequateEvent:
                case CPCalendarItemType.IdrInadequateDeadline:
                case CPCalendarItemType.IdrAdditionalInadequateEvent:
                case CPCalendarItemType.IdrAdditionalInadequateDeadline:
                case CPCalendarItemType.DraftAuditReport:
                case CPCalendarItemType.DarResponseDeadline:
                case CPCalendarItemType.DarInadequateEvent:
                case CPCalendarItemType.DarInadequateDeadline:
                case CPCalendarItemType.DarAdditionalInadequateEvent:
                case CPCalendarItemType.DarAdditionalInadequateDeadline:
                case CPCalendarItemType.FinalAuditReport:
                    action = PostElectionController.ActionName_Index;
                    controller = PostElectionController.ControllerName;
                    break;
                case CPCalendarItemType.StatementReview:
                case CPCalendarItemType.SRResponseDeadline:
                    action = Actions.Default;
                    controller = StatementReviewsController.ControllerName;
                    break;
                case CPCalendarItemType.ComplianceVisit:
                case CPCalendarItemType.CVResponseDeadline:
                    action = Actions.Default;
                    controller = ComplianceVisitsController.ControllerName;
                    break;
                case CPCalendarItemType.DoingBusinessReview:
                case CPCalendarItemType.DbrResponseDeadline:
                    action = Actions.Default;
                    controller = DoingBusinessReviewsController.ControllerName;
                    break;
                case CPCalendarItemType.FilingDeadline:
                    return "http://www.nyccfb.info/candidate-services/e-learning/disclosure-statement";
                default:
                    return null;
            }

            // define route parameters
            string param = null;
            switch (item.CalendarItemType)
            {
                case CPCalendarItemType.InitialDocumentRequest:
                case CPCalendarItemType.IdrResponseDeadline:
                case CPCalendarItemType.IdrInadequateEvent:
                case CPCalendarItemType.IdrInadequateDeadline:
                case CPCalendarItemType.IdrAdditionalInadequateEvent:
                case CPCalendarItemType.IdrAdditionalInadequateDeadline:
                    param = AuditReportType.InitialDocumentationRequest.ToString();
                    break;
                case CPCalendarItemType.DraftAuditReport:
                case CPCalendarItemType.DarResponseDeadline:
                case CPCalendarItemType.DarInadequateEvent:
                case CPCalendarItemType.DarInadequateDeadline:
                case CPCalendarItemType.DarAdditionalInadequateEvent:
                case CPCalendarItemType.DarAdditionalInadequateDeadline:
                    param = AuditReportType.DraftAuditReport.ToString();
                    break;
                case CPCalendarItemType.FinalAuditReport:
                    param = AuditReportType.FinalAuditReport.ToString();
                    break;
            }
            string url = string.Join("/", Mvc.Strings.Areas.Audit, controller, action);
            if (param != null)
                url = string.Concat(url, "/", param);
            return "/" + url;
        }
    }
}