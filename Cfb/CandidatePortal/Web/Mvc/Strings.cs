using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfb.CandidatePortal.Web.Mvc.Strings
{
    /// <summary>
    /// Defines standardized data format strings for global use in the C-Access MVC application.
    /// </summary>
    public class DataFormats
    {
        public const string ShortDate = "{0:d}";

        public const string MediumDate = "{0:m}, {0:yyyy}";

        public const string ShortDateTime = "{0:g}";

        public const string DateField = "{0:yyyy-MM-dd}";

        public const string MediumDateTime = MediumDate + " {0:t}";

        public const string Phone = "(ddd) ddd-dddd";

        public const string CurrencyField = "{0:F2}";

        public const string Currency = "{0:C}";

        public const string WholeNumber = "{0:N0}";
    }

    public class Text
    {
        public const string EmptyData = "No records were found for the currently selected election cycle.";

        public const string NullDisplayNA = "(n/a)";
    }

    public class Views
    {
        public const string TrainingCourse = "TrainingCourse";

        public const string AuditReviewTracking = "_AuditReviewTracking";

        public const string PostElectionResponse = "_PostElectionResponse";

        public const string PostElectionSummary = "StageSummary";

        public const string TollingEvents = "TollingEvents";

        public const string DocumentStatusList = "_DocumentStatusList";

        public const string DisclosureStatementTable = "_DisclosureStatementTable";

        public const string PrimaryGeneralStatusList = "_PrimaryGeneralStatusList";

        public const string EmptyDataPartial = "~/Views/Shared/_EmptyDataPartial.cshtml";

        public const string LoaderOverlayPartial = "~/Views/Shared/_LoaderOverlayPartial.cshtml";
    }

    /// <summary>
    /// Defines the names of actions in the C-Access MVC application.
    /// </summary>
    public class Actions
    {
        public const string Default = "Index";
    }

    /// <summary>
    /// Defines the names of areas in the C-Access MVC application.
    /// </summary>
    public class Areas
    {
        public const string Profile = "Profile";

        public const string Audit = "Audit";

        public const string Filings = "Filings";
    }

    public class AreaNames
    {
        public const string Profile = "Campaign Profile";

        public const string Audit = "Audit Reviews";

        public const string Filings = "Filings";

        public const string Help = "Help and Support";
    }
}
