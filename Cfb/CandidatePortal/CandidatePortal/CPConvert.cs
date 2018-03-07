using System;
using System.Globalization;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Utility class for converting and parsing enumerations.
    /// </summary>
    public static partial class CPConvert
    {
        #region ToString Methods

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="LiaisonType"/> representation.
        /// </summary>
        /// <param name="code">A liaison type code.</param>
        /// <returns>The <see cref="LiaisonType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static LiaisonType ToLiaisonType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "CAMMGR":
                    return LiaisonType.CampaignManager;
                case "CONSUL":
                    return LiaisonType.Consultant;
                case "TIE":
                    return LiaisonType.TIE;
                default:
                    return LiaisonType.Liaison;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="ElectionType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="electionType">An election type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="electionType"/>.</returns>
        public static string ToString(ElectionType electionType)
        {
            switch (electionType)
            {
                case ElectionType.General: return "General";
                case ElectionType.Primary: return "Primary";
                case ElectionType.Runoff: return "Runoff/Rerun";
                case ElectionType.Special: return "Special";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Convers the value of the specified <see cref="PaymentMethod"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="method">A payment method.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="method"/>.</returns>
        public static string ToString(PaymentMethod method)
        {
            switch (method)
            {
                case PaymentMethod.Check: return "Check";
                case PaymentMethod.Eft: return "EFT";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="NycPublicOfficeType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="office">A New York City publicly elected office.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="office"/>.</returns>
        public static string ToString(NycPublicOfficeType office)
        {
            switch (office)
            {
                case NycPublicOfficeType.BoroughPresident: return "Borough President";
                case NycPublicOfficeType.CityCouncilMember: return "City Council Member";
                case NycPublicOfficeType.Comptroller: return "Comptroller";
                case NycPublicOfficeType.Mayor: return "Mayor";
                case NycPublicOfficeType.PublicAdvocate: return "Public Advocate";
                case NycPublicOfficeType.Undeclared: return "Undeclared";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="NycBorough"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="borough">A New York City borough.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="borough"/>.</returns>
        public static string ToString(NycBorough borough)
        {
            switch (borough)
            {
                case NycBorough.Bronx: return "Bronx";
                case NycBorough.Brooklyn: return "Brooklyn";
                case NycBorough.Manhattan: return "Manhattan";
                case NycBorough.Queens: return "Queens";
                case NycBorough.StatenIsland: return "Staten Island";
                case NycBorough.OutOfCity: return "Out of City";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="SubmissionType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="submissionType">A submission type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="submissionType"/>.</returns>
        public static string ToString(SubmissionType submissionType)
        {
            switch (submissionType)
            {
                case SubmissionType.Amendment: return "Amendment";
                case SubmissionType.Documentation: return "Documentation";
                case SubmissionType.InternalAmendment: return "Internal Amendment";
                case SubmissionType.Regular: return "Initial";
                case SubmissionType.Resubmission: return "Resubmission";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="DocumentType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="documentType">A document type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="documentType"/>.</returns>
        public static string ToString(DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.Certification: return "Certification";
                case DocumentType.CoibReceipt: return "COIB Receipt";
                case DocumentType.DisclosureStatement: return "Disclosure Statement";
                case DocumentType.FilerRegistration: return "Filer Registration";
                case DocumentType.CsmartIdsRequest: return "IDS Password Request";
                case DocumentType.PreGeneralDisclosure: return "General Pre-Election Disclosure Statement";
                case DocumentType.PrePrimaryDisclosure: return "Primary Pre-Election Disclosure Statement";
                case DocumentType.StatementOfNeedGeneral: return "General Election Statement of Need";
                case DocumentType.StatementOfNeedPrimary: return "Primary Election Statement of Need";
                case DocumentType.Termination: return "Verification of Terminated Candidacy";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="DocumentStatus"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="status">A document status.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="status"/>.</returns>
        public static string ToString(DocumentStatus status)
        {
            switch (status)
            {
                case DocumentStatus.Accepted: return "Accepted";
                case DocumentStatus.Disregarded: return "No Processing Required";
                case DocumentStatus.OnHold: return "On-Hold";
                case DocumentStatus.Received: return "Processing";
                case DocumentStatus.Rejected: return "Rejected";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="CfbClassification"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="classification">A CFB classification.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="classification"/>.</returns>
        public static string ToString(CfbClassification classification)
        {
            switch (classification)
            {
                case CfbClassification.LimitedParticipant: return "Limited Participant";
                case CfbClassification.NonParticipant: return "Non-Participant";
                case CfbClassification.Participant: return "Participant";
                case CfbClassification.Undetermined: return "Undetermined";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="PaymentType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="paymentType">A candidate payment type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="paymentType"/>.</returns>
        public static string ToString(PaymentType paymentType)
        {
            switch (paymentType)
            {
                case PaymentType.AutomaticPenalty: return "Automatic Penalty Payment";
                case PaymentType.Candidate: return "Candidate Payment";
                case PaymentType.ManualPenalty: return "Manual Penalty Payment";
                case PaymentType.Penalty: return "Penalty";
                case PaymentType.ReimbursedReturn: return "Reimbursed Returned Funds";
                case PaymentType.ReturnedFunds: return "Returned Funds";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="PaymentPeriod"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="period">A payment period.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="period"/>.</returns>
        public static string ToString(PaymentPeriod period)
        {
            switch (period)
            {
                case PaymentPeriod.Annual: return "Annual";
                case PaymentPeriod.Monthly: return "Monthly";
                case PaymentPeriod.Quarterly: return "Quarterly";
                case PaymentPeriod.SemiAnnual: return "Semi-Annal";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="ContactOrder"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="order">A contact order.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="order"/>.</returns>
        public static string ToString(ContactOrder order)
        {
            switch (order)
            {
                case ContactOrder.First: return "First";
                case ContactOrder.Second: return "Second";
                case ContactOrder.Third: return "Third";
                case ContactOrder.Fourth: return "Fourth";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="Honorific"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="honorific">An honorific.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="honorific"/>.</returns>
        public static string ToString(Honorific honorific)
        {
            switch (honorific)
            {
                case Honorific.Mr: return "Mr.";
                case Honorific.Mrs: return "Mrs.";
                case Honorific.Ms: return "Ms.";
                case Honorific.Rev: return "Rev.";
                case Honorific.Sister: return "Sister";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="BankAccountType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="accountType">A bank account type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="accountType"/>.</returns>
        public static string ToString(BankAccountType accountType)
        {
            switch (accountType)
            {
                case BankAccountType.Checking: return "Checking";
                case BankAccountType.CreditCard: return "Credit Card";
                case BankAccountType.Custodial: return "Custodial";
                case BankAccountType.MoneyMarket: return "Money Market";
                case BankAccountType.Savings: return "Savings";
                case BankAccountType.Other: return "Other";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="BankAccountPurpose"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="accountPurpose">A bank account purpose.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="accountPurpose"/>.</returns>
        public static string ToString(BankAccountPurpose accountPurpose)
        {
            switch (accountPurpose)
            {
                case BankAccountPurpose.Regular: return "Primary/General elections";
                case BankAccountPurpose.Segregated: return "Segregated account for soliciting non-matchable contributions";
                case BankAccountPurpose.Other: return "Other";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="TerminationReason"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="reason">A termination reason code.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="reason"/>.</returns>
        public static string ToString(TerminationReason reason)
        {
            switch (reason)
            {
                case TerminationReason.ByBoard: return "Terminated by the Board";
                case TerminationReason.CeasedCampaign: return "Ceased Campaign";
                case TerminationReason.OffBallot: return "Off the Ballot";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Converts the value of the specified <see cref="ThresholdRevisionType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="thresholdType">A threshold revision type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="thresholdType"/>.</returns>
        public static string ToString(ThresholdRevisionType thresholdType)
        {
            switch (thresholdType)
            {
                case ThresholdRevisionType.Initial: return "Initial";
                case ThresholdRevisionType.Revised: return "Revised";
                case ThresholdRevisionType.Summary: return "Summary";
                default: return string.Empty;
            }
        }

        #endregion

        #region String Parser Methods

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="DocumentStatusReason"/> representation.
        /// </summary>
        /// <param name="code">A document status reason code.</param>
        /// <returns>The <see cref="DocumentStatusReason"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static DocumentStatusReason ToDocumentStatusReason(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "CORDSK": return DocumentStatusReason.CorruptDisk;
                case "CVSIGN": return DocumentStatusReason.CoverNotSigned;
                case "ILLEGI": return DocumentStatusReason.Illegible;
                case "INVBKA": return DocumentStatusReason.BankAccount;
                case "MISDSK": return DocumentStatusReason.MissingDisk;
                case "MISPAP": return DocumentStatusReason.MissingPapers;
                case "MISPGS": return DocumentStatusReason.MissingSummary;
                case "MSCCPG": return DocumentStatusReason.MissingCover;
                case "MSPATC": return DocumentStatusReason.MissingPatch;
                case "MSREF": return DocumentStatusReason.MissingReference;
                case "MSSUB": return DocumentStatusReason.MissingFile;
                case "NOFILE": return DocumentStatusReason.NoFilesOnDisk;
                case "NOSIGN": return DocumentStatusReason.NoSignature;
                case "OK": return DocumentStatusReason.Okay;
                case "SOFTUP": return DocumentStatusReason.NotUpdated;
                case "SPSUB": return DocumentStatusReason.SplitFile;
                case "OTHER":
                default: return DocumentStatusReason.Other;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="DocumentStatus"/> representation.
        /// </summary>
        /// <param name="code">A document status code.</param>
        /// <returns>The <see cref="DocumentStatus"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static DocumentStatus ToDocumentStatus(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "ACCEPT": return DocumentStatus.Accepted;
                case "ONHOLD":
                case "AWAIT": return DocumentStatus.OnHold;
                case "DISREG": return DocumentStatus.Disregarded;
                case "RCVD": return DocumentStatus.Received;
                case "REJECT": return DocumentStatus.Rejected;
                default: return DocumentStatus.Unknown;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="SubmissionType"/> representation.
        /// </summary>
        /// <param name="code">A submission type code.</param>
        /// <returns>The <see cref="SubmissionType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static SubmissionType ToSubmissionType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "REG": return SubmissionType.Regular;
                case "AMEND": return SubmissionType.Amendment;
                case "RESUB": return SubmissionType.Resubmission;
                case "IAMEND": return SubmissionType.InternalAmendment;
                case "DOCONL": return SubmissionType.Documentation;
                default: return SubmissionType.Unknown;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="DocumentType"/> representation.
        /// </summary>
        /// <param name="code">A document type code.</param>
        /// <returns>The <see cref="DocumentType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static DocumentType ToDocumentType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "CC": return DocumentType.Certification;
                case "COIB": return DocumentType.CoibReceipt;
                case "DS": return DocumentType.DisclosureStatement;
                case "FR": return DocumentType.FilerRegistration;
                case "IDSREQ": return DocumentType.CsmartIdsRequest;
                case "SPG": return DocumentType.PreGeneralDisclosure;
                case "SPP": return DocumentType.PrePrimaryDisclosure;
                case "STMGEN": return DocumentType.StatementOfNeedGeneral;
                case "STMPRI": return DocumentType.StatementOfNeedPrimary;
                default: return DocumentType.Unknown;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="PaymentType"/> representation.
        /// </summary>
        /// <param name="code">A payment type code.</param>
        /// <returns>The <see cref="PaymentType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static PaymentType ToPaymentType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "APENPY": return PaymentType.AutomaticPenalty;
                case "CPAY": return PaymentType.Candidate;
                case "MPENPY": return PaymentType.ManualPenalty;
                case "PNLTY": return PaymentType.Penalty;
                case "RETFND": return PaymentType.ReturnedFunds;
                case "RMBFND": return PaymentType.ReimbursedReturn;
                default: return PaymentType.Unknown;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="PaymentPeriod"/> representation.
        /// </summary>
        /// <param name="code">A payment period code.</param>
        /// <returns>The <see cref="PaymentPeriod"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static PaymentPeriod ToPaymentPeriod(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "1": return PaymentPeriod.Annual;
                case "2": return PaymentPeriod.SemiAnnual;
                case "3": return PaymentPeriod.Quarterly;
                case "4": return PaymentPeriod.Monthly;
                default: return PaymentPeriod.Unknown;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="NycPublicOfficeType"/> representation.
        /// </summary>
        /// <param name="code">A New York City public office code.</param>
        /// <returns>The <see cref="NycPublicOfficeType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static NycPublicOfficeType ToNycPublicOfficeType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "1": return NycPublicOfficeType.Mayor;
                case "2": return NycPublicOfficeType.PublicAdvocate;
                case "3": return NycPublicOfficeType.Comptroller;
                case "4": return NycPublicOfficeType.BoroughPresident;
                case "5": return NycPublicOfficeType.CityCouncilMember;
                case "6":
                default: return NycPublicOfficeType.Undeclared;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="NycBorough"/> representation.
        /// </summary>
        /// <param name="code">A New York City borough code.</param>
        /// <returns>The <see cref="NycBorough"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static NycBorough ToNycBorough(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "K": return NycBorough.Brooklyn;
                case "M": return NycBorough.Manhattan;
                case "Q": return NycBorough.Queens;
                case "S": return NycBorough.StatenIsland;
                case "X": return NycBorough.Bronx;
                case "Z": return NycBorough.OutOfCity;
                default: return NycBorough.Unknown;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="CfbClassification"/> representation.
        /// </summary>
        /// <param name="code">A Campaign Finance Board candidate classification code.</param>
        /// <returns>The <see cref="CfbClassification"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static CfbClassification ToCfbClassification(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "LP": return CfbClassification.LimitedParticipant;
                case "NP": return CfbClassification.NonParticipant;
                case "P": return CfbClassification.Participant;
                case "UN":
                default: return CfbClassification.Undetermined;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="ContactOrder"/> representation.
        /// </summary>
        /// <param name="code">A contact order code.</param>
        /// <returns>The <see cref="ContactOrder"/> equivalent of <paramref name="code"/>.</returns>
        public static ContactOrder ToContactOrder(short code)
        {
            switch (code)
            {
                case 1: return ContactOrder.First;
                case 2: return ContactOrder.Second;
                case 3: return ContactOrder.Third;
                case 4: return ContactOrder.Fourth;
                default: return ContactOrder.Unknown;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="BankAccountType"/> representation.
        /// </summary>
        /// <param name="code">A bank account type code.</param>
        /// <returns>The <see cref="BankAccountType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static BankAccountType ToBankAccountType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "CHECK": return BankAccountType.Checking;
                case "CREDCD": return BankAccountType.CreditCard;
                case "CUSTOD": return BankAccountType.Custodial;
                case "MONMAK": return BankAccountType.MoneyMarket;
                case "SAVE": return BankAccountType.Savings;
                default: return BankAccountType.Other;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="BankAccountPurpose"/> representation.
        /// </summary>
        /// <param name="code">A bank account purpose code.</param>
        /// <returns>The <see cref="BankAccountPurpose"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static BankAccountPurpose ToBankAccountPurpose(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "2001P&":
                case "2003P&":
                case "2005P&":
                case "2009P&":
                case "REG":
                    return BankAccountPurpose.Regular;
                case "RR":
                    return BankAccountPurpose.RunoffRerun;
                case "501N":
                case "SEG":
                    return BankAccountPurpose.Segregated;
                default: return BankAccountPurpose.Other;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="ContactOrder"/> representation.
        /// </summary>
        /// <param name="code">A contact order code.</param>
        /// <returns>The <see cref="ContactOrder"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static Honorific ToHonorific(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "MR.": return Honorific.Mr;
                case "MRS.": return Honorific.Mrs;
                case "MS": return Honorific.Ms;
                case "REVERE": return Honorific.Rev;
                case "SISTER": return Honorific.Sister;
                default: return Honorific.None;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="TerminationReason"/> representation.
        /// </summary>
        /// <param name="code">A termination reason code.</param>
        /// <returns>The <see cref="TerminationReason"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static TerminationReason ToTerminationReason(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "BOARD": return TerminationReason.ByBoard;
                case "CEASE": return TerminationReason.CeasedCampaign;
                case "OFFBAL": return TerminationReason.OffBallot;
                default: return TerminationReason.Other;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="ThresholdRevisionType"/> representation.
        /// </summary>
        /// <param name="code">A threshold status revision type code.</param>
        /// <returns>The <see cref="ThresholdRevisionType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static ThresholdRevisionType ToThresholdRevisionType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "INIT": return ThresholdRevisionType.Initial;
                case "REV": return ThresholdRevisionType.Revised;
                default: return ThresholdRevisionType.Summary;
            }
        }

        /// <summary>
        /// Parses and converts the specified code to its equivalent <see cref="ElectionType"/> representation.
        /// </summary>
        /// <param name="code">An election type code.</param>
        /// <returns>The <see cref="ElectionType"/> equivalent of <paramref name="code"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
        public static ElectionType ToElectionType(string code)
        {
            if (code == null) throw new ArgumentNullException("code");
            switch (code.ToUpper(CultureInfo.InvariantCulture))
            {
                case "P": return ElectionType.Primary;
                case "R": return ElectionType.Runoff;
                case "S": return ElectionType.Special;
                default: return ElectionType.General;
            }
        }

        /// <summary>
        /// Parses a two-character U.S. postal state abbreviation into its unabbreviated equivalent.
        /// </summary>
        /// <param name="code">A two-character U.S. postal state abbreviation.</param>
        /// <returns>The unabbreviated equivalent of <paramref name="code"/> if it is a valid state; otherwise, the original abbreviation.</returns>
        public static string ParseStateCode(string code)
        {
            switch (code)
            {
                case "AL": return "Alabama";
                case "AK": return "Alaska";
                case "AZ": return "Arizona";
                case "AR": return "Arkansas";
                case "CA": return "California";
                case "CO": return "Colorado";
                case "CT": return "Connecticut";
                case "DE": return "Delaware";
                case "FL": return "Florida";
                case "GA": return "Georgia";
                case "HI": return "Hawaii";
                case "ID": return "Idaho";
                case "IL": return "Illinois";
                case "IN": return "Indiana";
                case "IA": return "Iowa";
                case "KS": return "Kansas";
                case "KY": return "Kentucky";
                case "LA": return "Louisiana";
                case "ME": return "Maine";
                case "MD": return "Maryland";
                case "MA": return "Massachusetts";
                case "MI": return "Michigan";
                case "MN": return "Minnesota";
                case "MS": return "Mississippi";
                case "MO": return "Missouri";
                case "MT": return "Montana";
                case "NE": return "Nebraska";
                case "NV": return "Nevada";
                case "NH": return "New Hampshire";
                case "NJ": return "New Jersey";
                case "NM": return "New Mexico";
                case "NY": return "New York";
                case "NC": return "North Carolina";
                case "ND": return "North Dakota";
                case "OH": return "Ohio";
                case "OK": return "Oklahoma";
                case "OR": return "Oregon";
                case "PA": return "Pennsylvania";
                case "RI": return "Rhode Island";
                case "SC": return "South Carolina";
                case "SD": return "South Dakota";
                case "TN": return "Tennessee";
                case "TX": return "Texas";
                case "UT": return "Utah";
                case "VT": return "Vermont";
                case "VA": return "Virginia";
                case "WA": return "Washington";
                case "WV": return "West Virginia";
                case "WI": return "Wisconsin";
                case "WY": return "Wyoming";
                case "AS": return "American Samoa";
                case "DC": return "District of Columbia";
                case "FM": return "Federated States of Micronesia";
                case "GU": return "Guam";
                case "MH": return "Marshall Islands";
                case "MP": return "Northern Mariana Islands";
                case "PW": return "Palau";
                case "PR": return "Puerto Rico";
                case "VI": return "Virgin Islands";
                default: return code;
            }
        }

        #endregion

        /// <summary>
        /// Generic method for parsing and converting a string to an equivalent enumeration value.
        /// </summary>
        /// <typeparam name="T">The type of enumeration to parse for.</typeparam>
        /// <param name="value">A <see cref="String"/> indicating an enumeration value.</param>
        /// <returns>The <typeparamref name="T"/> equivalent of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
        /// <exception cref="ArgumentException">enumType is not an Enum.
        /// -or-
        /// The type of value is not a <typeparamref name="T"/> enumeration.
        /// -or-
        /// The type of value is not an underlying type of <typeparamref name="T"/>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="value"/> is not a <see cref="String"/>.</exception>
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Gets an entity's full name with honorific title.
        /// </summary>
        /// <param name="entity">The entity whose honorific title is to be retrieved.</param>
        /// <returns>The full name with honorific title for <paramref name="entity"/>.</returns>
        public static string GetProperName(this Entity entity)
        {
            if (entity == null)
                return null;
            string name = (entity.Honorific != Honorific.None) ? string.Format("{0} {1}", CPConvert.ToString(entity.Honorific), entity.Name) : entity.Name;
            return name.Trim();
        }
    }

    public static class LiaisonType_Extensions
    {
        /// <summary>
        /// Converts the value of the specified <see cref="LiaisonType"/> to its equivalent <see cref="String"/> representation.
        /// </summary>
        /// <param name="liaisonType">A liaison type.</param>
        /// <returns>The <see cref="String"/> equivalent of the value of <paramref name="liaisonType"/>.</returns>
        public static string ToString<T>(this LiaisonType liaisonType)
        {
            switch (liaisonType)
            {
                case LiaisonType.CampaignManager: return "Campaign Mananger";
                case LiaisonType.Consultant: return "Consultant";
                case LiaisonType.TIE: return "TIE Liaison";
                default: return "Liaison";
            }
        }
    }
}
