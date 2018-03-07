using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.PostElection;
using System.ComponentModel;

namespace Cfb.CandidatePortal.PostElection
{
    /// <summary>
    /// Enumeration of the types of Post Election Audit reports and letters.
    /// </summary>
    public enum AuditReportType
    {
        /// <summary>
        /// Initial Documentation Request
        /// </summary>
        [Description("Request for Documentation")]
        InitialDocumentationRequest,
        /// <summary>
        /// Initial Documentation Request Inadequate Response
        /// </summary>
        IdrInadequateResponse,
        /// <summary>
        /// IDR Additional Inadequate Reponse
        /// </summary>
        IdrAdditionalInadequateResponse,
        /// <summary>
        /// Draft Audit Report
        /// </summary>
        [Description("Draft Audit Report")]
        DraftAuditReport,
        /// <summary>
        /// Draft Audit Report Inadequate Response
        /// </summary>
        DarInadequateResponse,
        /// <summary>
        /// DAR Additional Inadequate Response
        /// </summary>
        DarAdditionalInadequateResponse,
        /// <summary>
        /// Final Audit Report
        /// </summary>
        [Description("Final Audit Report")]
        FinalAuditReport
    }
}

namespace Cfb.CandidatePortal
{
    partial class CPConvert
    {
        /// <summary>
        /// Converts the value of the specified <see cref="AuditReportType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="type">An election type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="type"/>.</returns>
        public static string ToString(AuditReportType type)
        {
            switch (type)
            {
                case AuditReportType.InitialDocumentationRequest:
                    return "Request for Documentation";
                case AuditReportType.IdrInadequateResponse:
                    return "Request for Documentation—Inadequate Response Notice";
                case AuditReportType.IdrAdditionalInadequateResponse:
                    return "Request for Documentation—Additional Inadequate Response Notice";
                case AuditReportType.DraftAuditReport:
                    return "Draft Audit Report";
                case AuditReportType.DarInadequateResponse:
                    return "Draft Audit Report—Inadequate Response Notice";
                case AuditReportType.DarAdditionalInadequateResponse:
                    return "Draft Audit Report—Additional Inadequate Response Notice";
                case AuditReportType.FinalAuditReport:
                    return "Final Audit Report";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Converts a post election audit report type to its CFIS code equivalent.
        /// </summary>
        /// <param name="type">The post election audit report type to convert.</param>
        /// <returns>A CFIS code corresponding to the post election type specified by <paramref name="type"/>.</returns>
        public static string ToCfisCode(AuditReportType type)
        {
            switch (type)
            {
                case AuditReportType.InitialDocumentationRequest:
                    return "IDRREQ";
                case AuditReportType.IdrInadequateResponse:
                    return "DOCINA";
                case AuditReportType.IdrAdditionalInadequateResponse:
                case AuditReportType.DarAdditionalInadequateResponse:
                    return "ADDINA";
                case AuditReportType.DraftAuditReport:
                    return "DARRPT";
                case AuditReportType.DarInadequateResponse:
                    return "DARINS";
                case AuditReportType.FinalAuditReport:
                    return "FARRPT";
                default:
                    return string.Empty;
            }
        }
    }
}