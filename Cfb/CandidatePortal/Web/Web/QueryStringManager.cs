using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Web.Properties;

namespace Cfb.CandidatePortal.Web
{
    /// <summary>
    /// Provides strongly-typed access to C-Access query string parameters.
    /// </summary>
    public static class QueryStringManager
    {
        /// <summary>
        /// The project settings.
        /// </summary>
        private static readonly Settings _settings = Properties.Settings.Default;

        /// <summary>
        /// Gets the name of the parameter for passing election cycle values.
        /// </summary>
        public static string ElectionCycleParameter
        {
            get { return _settings.ElectionCycleParameter; }
        }

        /// <summary>
        /// Compiles a query string using well-known parameters.
        /// </summary>
        /// <param name="committeeID">A committee ID.</param>
        /// <param name="auditReportType">An audit report type.</param>
        /// <param name="extensionType">An audit response extension type.</param>
        /// <param name="date">A date.</param>
        /// <param name="statementNumber">A statement number.</param>
        /// <param name="reviewNumber">An audit review number.</param>
        /// <param name="loggedOut">Indicates whether a logged-out state exists.</param>
        /// <param name="ssoID">A single sign-on enabled application ID.</param>
        /// <param name="returnUrl">A post-redirection return URL.</param>
        /// <returns>A <see cref="NameValueCollection"/> query string containing the specified parameters.</returns>
        public static NameValueCollection MakeQueryString(char? committeeID = null, AuditReportType? auditReportType = null, ExtensionType? extensionType = null, DateTime? date = null, byte statementNumber = byte.MinValue, byte? reviewNumber = byte.MinValue, bool loggedOut = false, byte ssoID = byte.MinValue, string returnUrl = null)
        {
            NameValueCollection qs = new NameValueCollection();
            if (committeeID.HasValue)
                qs[_settings.CommitteeIDParameter] = committeeID.Value.ToString();
            if (auditReportType.HasValue)
                qs[_settings.PostElectionAuditTypeParameter] = auditReportType.Value.ToString();
            if (date.HasValue)
                qs[_settings.DateParameter] = date.Value.ToString();
            if (extensionType.HasValue)
                qs[_settings.ExtensionTypeParameter] = extensionType.Value.ToString();
            if (statementNumber != byte.MinValue)
                qs[_settings.StatementParameter] = statementNumber.ToString();
            if (reviewNumber != byte.MinValue)
                qs[_settings.ReviewNumberParameter] = reviewNumber.ToString();
            if (loggedOut)
                qs[_settings.LoggedOutParameter] = loggedOut.ToString();
            if (ssoID != byte.MinValue)
                qs[CPSecurity.QueryStringSsoAppIDParameter] = ssoID.ToString();
            if (returnUrl != null)
                qs[_settings.ReturnUrlParameter] = returnUrl;
            return qs;
        }

        /// <summary>
        /// Gets the committee ID specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>A committee ID if specifi8ed in the request query string; otherwise, null.</returns>
        public static char? GetCommitteeID(this HttpRequest request)
        {
            char val;
            return char.TryParse(request.QueryString[_settings.CommitteeIDParameter], out val) ? (char?)val : null;
        }

        /// <summary>
        /// Gets the audit report type specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>An audit report type if specified in the request query string; otherwise, null.</returns>
        public static AuditReportType? GetAuditReportType(this HttpRequest request)
        {
            AuditReportType val;
            return Enum.TryParse(request.QueryString[_settings.PostElectionAuditTypeParameter], out val) ? (AuditReportType?)val : null;
        }

        /// <summary>
        /// Gets the audit response extension type specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>An audit response extension type if specified in the request query string; otherwise, null.</returns>
        public static ExtensionType? GetExtensionType(this HttpRequest request)
        {
            ExtensionType val;
            return Enum.TryParse(request.QueryString[_settings.ExtensionTypeParameter], out val) ? (ExtensionType?)val : null;
        }

        /// <summary>
        /// Gets the date specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>A date if specified in the request query string; otherwise, null.</returns>
        public static DateTime? GetDate(this HttpRequest request)
        {
            DateTime val;
            return DateTime.TryParse(request.QueryString[_settings.DateParameter], out val) ? (DateTime?)val : null;
        }

        /// <summary>
        /// Gets the statement number specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTp request to use.</param>
        /// <returns>A statement number if specified in the request query string; otherwise, null.</returns>
        public static byte? GetStatementNumber(this HttpRequest request)
        {
            byte val;
            return byte.TryParse(request.QueryString[_settings.StatementParameter], out val) ? (byte?)val : null;
        }

        /// <summary>
        /// Gets the audit review number specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>An audit review number if specified in the request query string; otherwise, null.</returns>
        public static byte? GetReviewNumber(this HttpRequest request)
        {
            byte val;
            return byte.TryParse(request.QueryString[_settings.ReviewNumberParameter], out val) ? (byte?)val : null;
        }

        /// <summary>
        /// Gets the single sign-on enabled application ID specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>A single sign-on enabled application ID if specified in the request query string; otherwise, null.</returns>
        public static byte? GetSsoAppID(this HttpRequest request)
        {
            byte val;
            return byte.TryParse(request.QueryString[CPSecurity.QueryStringSsoAppIDParameter], out val) ? (byte?)val : null;
        }

        /// <summary>
        /// Gets the return URL specified in the request query string.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>The HTML-decoded return URL specified in the request query string.</returns>
        public static string GetReturnUrl(this HttpRequest request)
        {
            return request.RequestContext.HttpContext.Server.UrlDecode(request.QueryString[_settings.ReturnUrlParameter]);
        }

        /// <summary>
        /// Gets wehter or not the request query string indicates a logout.
        /// </summary>
        /// <param name="request">The HTTP request to use.</param>
        /// <returns>true if the request query string indicates a logout; otherwise, false.</returns>
        public static bool HasLoggedOut(this HttpRequest request)
        {
            return true.ToString().Equals(request.QueryString[_settings.LoggedOutParameter], StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
