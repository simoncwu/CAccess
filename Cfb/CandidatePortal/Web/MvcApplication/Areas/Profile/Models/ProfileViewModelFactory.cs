using Cfb.CandidatePortal.TrainingTracking;
using Cfb.Cfis.CampaignContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public sealed class ProfileViewModelFactory
    {
        public static CandidateProfileViewModel CandidateProfileFrom(ActiveCandidate cand, IEnumerable<AuthorizedCommittee> committees = null)
        {
            var model = new CandidateProfileViewModel
            {
                ID = cand.ID,
                FullName = cand.Name,
                Classification = cand.Classification,
                OfficeSoughtDetails = cand.Office.ToString(),
                RegistrationDate = cand.FilerRegistrationDate,
                CertificationDate = cand.CertificationDate,
                PrincipalCommittee = cand.PrincipalCommittee,
                CsuLiaison = cand.CsuLiaisonName,
                Auditor = cand.AuditorName
            };
            if (committees == null)
            {
                model.ContactInfo = new ContactInfoViewModel();
            }
            else
            {
                // candidate info
                model.Name = PersonNameFrom(cand);
                model.ContactInfo = ContactInfoFrom(cand);

                // employer info
                model.Employer = EmployerFrom(cand.Employer);

                // activation info
                model.PoliticalParty = cand.PoliticalParty;
                model.TerminationDate = cand.TerminationDate;
                if (cand.Office != null)
                {
                    var office = cand.Office;
                    model.OfficeSought = office.Type;
                    NycBorough borough;
                    byte district;
                    if (office.TryGetBorough(out borough))
                        model.Borough = borough;
                    else if (office.TryGetDistrict(out district))
                        model.District = district;
                }
                model.DDAuth = cand.IsDirectDepositAuthorized;
                if (model.HasRRAccounts = committees.Any(c => c.BankAccounts.Values.Any(a => a.Purpose == BankAccountPurpose.RunoffRerun)))
                    model.RRAuth = cand.IsRRDirectDepositAuthorized;
                model.LastUpdated = cand.LastUpdated;
            }
            model.ContactInfo.Email = cand.Email;
            return model;
        }

        private static ContactInfoViewModel ContactInfoFrom(Entity source, bool employerFlag = false, string urls = null, bool hasUrls = false)
        {
            if (source == null)
                source = new Entity();
            var addy = source.Address ?? new PostalAddress();
            PhoneNumber daytime = source.DaytimePhone, evening = source.EveningPhone, fax = source.Fax;

            return new ContactInfoViewModel
            {
                Address = AddressFrom(source.Address, false),
                DaytimePhone = DigitsOrDefault(daytime),
                DaytimePhoneExt = daytime != null ? daytime.Extension : null,
                EveningPhone = DigitsOrDefault(evening),
                EveningPhoneExt = evening != null ? evening.Extension : null,
                Fax = DigitsOrDefault(fax),
                Email = source.Email,
                Urls = urls,
                IsEmployer = employerFlag,
                HasUrls = hasUrls
            };
        }

        private static PersonNameViewModel PersonNameFrom(Entity source)
        {
            if (source == null)
                source = new Entity();
            return new PersonNameViewModel
            {
                Salutation = source.Honorific,
                LastName = source.LastName,
                FirstName = source.FirstName,
                MiddleInitial = source.MiddleInitial.HasValue ? source.MiddleInitial.ToString() : null
            };
        }

        private static AddressViewModel AddressFrom(PostalAddress address, bool aptFlag = true)
        {
            if (address == null)
                address = new PostalAddress();
            return new AddressViewModel
            {
                StreetNumber = address.StreetNumber,
                StreetName = address.StreetName,
                Apartment = address.Apartment,
                City = address.City,
                StateCode = address.State,
                State = CPConvert.ParseStateCode(address.State),
                Zip = address.Zip != null ? address.Zip.ToString() : null,
                HasApartment = aptFlag
            };
        }

        private static EmployerViewModel EmployerFrom(Entity source)
        {
            if (source == null)
                source = new Entity();
            return new EmployerViewModel
            {
                Name = source.Name,
                ContactInfo = ContactInfoFrom(source, employerFlag: true)
            };
        }

        public static CampaignStaffViewModel CampaignStaffFrom(Entity source)
        {
            if (source == null)
                source = new Entity();
            var model = new CampaignStaffViewModel
            {
                Name = PersonNameFrom(source),
                ContactInfo = ContactInfoFrom(source),
                Employer = EmployerFrom(source.Employer),
                ContactOrder = source.ContactOrder,
            };
            Liaison l = source as Liaison;
            if (l != null)
            {
                model.ContactType = l.LiaisonType;
                model.ConsultantEntityName = l.EntityName;
                model.ManagerialControl = l.HasManagerialControl;
                model.VGLiaison = l.IsVGLiaison;
            }
            return model;
        }

        public static CommitteeProfileViewModel CommitteeProfileFrom(AuthorizedCommittee comm)
        {
            if (comm == null)
                return new CommitteeProfileViewModel
                {
                    ContactInfo = new ContactInfoViewModel(),
                    MailingAddress = new AddressViewModel(),
                    Treasurer = new CampaignStaffViewModel()
                };
            return new CommitteeProfileViewModel
            {
                ID = comm.ID,
                Name = comm.Name,
                ContactInfo = ContactInfoFrom(comm, urls: comm.WebsiteUrl, hasUrls: true),
                IsPrincipal = comm.IsPrincipal,
                IsActive = comm.IsActive,
                BoeAuthDate = comm.BoeDate,
                TerminationDate = comm.TerminationDate,
                MailingAddress = AddressFrom(comm.MailingAdress),
                Treasurer = CampaignStaffFrom(comm.Treasurer),
                // previous election
                LastElectionDate = comm.LastElectionDate,
                LastOfficeSought = comm.LastElectionOffice,
                LastDistrict = comm.LastElectionDistrict,
                LastPrimaryParty = comm.LastPrimaryParty
            };
        }

        public static CampaignStaffViewModel CampaignContactSummaryFrom(Liaison source, char committeeID)
        {
            if (source == null)
                source = new Liaison(0);
            return new CampaignStaffViewModel
            {
                CommitteeID = committeeID,
                ID = source.ID,
                Name = PersonNameFrom(source),
                ContactType = source.LiaisonType
            };
        }

        public static BankAccountViewModel BankAccountFrom(BankAccount source, char committeeID)
        {
            return source == null ? new BankAccountViewModel() : new BankAccountViewModel
            {
                CommitteeID = committeeID,
                ID = source.ID,
                BankName = source.BankName,
                City = source.City,
                State = CPConvert.ParseStateCode(source.State),
                StateCode = source.State,
                Zip = source.Zip == null ? null : source.Zip.ToString(),
                AccountNumber = source.Number,
                FriendlyName = source.Name,
                OpenedDate = source.OpeningDate,
                ClosedDate = source.ClosingDate,
                Balance = source.CurrentBalance,
                BalanceDate = source.CurrentBalanceDate,
                Type = source.Type == BankAccountType.Other ? source.OtherTypeSpecification : CPConvert.ToString(source.Type),
                Purpose = source.Purpose == BankAccountPurpose.Other ? source.OtherPurposeSpecification : CPConvert.ToString(source.Purpose),
                DirectDeposit = source.HasDirectDeposit
            };
        }

        public static BankAccountViewModel BankAccountSummaryFrom(BankAccount source, char committeeID)
        {
            return source == null ? new BankAccountViewModel() : new BankAccountViewModel
            {
                CommitteeID = committeeID,
                ID = source.ID,
                BankName = source.BankName,
                FriendlyName = source.Name,
                AccountNumber = source.Number,
                Type = source.Type == BankAccountType.Other ? source.OtherTypeSpecification : CPConvert.ToString(source.Type),
                Balance = source.CurrentBalance,
                BalanceDate = source.CurrentBalanceDate
            };
        }

        public static FinancialSummaryViewModel FinancialSummaryFrom(FinancialSummary summary, ActiveCandidate candidate, Election election)
        {
            if (summary == null || candidate == null)
                return new FinancialSummaryViewModel();
            var statement = election == null ? null : (from s in election.Statements
                                                       where s.Key == summary.LastStatementSubmitted
                                                       select s.Value).SingleOrDefault();
            return new FinancialSummaryViewModel
            {
                IsTie = election != null && election.IsTIE,
                ElectionCycle = candidate.ElectionCycle,
                CandidateName = string.Format("{0} (ID: {1})", candidate.Name, candidate.ID),
                OfficeSought = candidate.Office.ToString(),
                Classification = candidate.Classification,
                LastFiledStatement = statement == null ? null : statement.ToDetailString(),
                NetContributions = summary.NetContributions,
                MiscReceipts = summary.MiscellaneousReceipts,
                LoansReceived = summary.LoansReceived,
                PublicFundsReceived = summary.PublicFundsReceived,
                NetExpenditures = summary.NetExpenditures,
                LoansPaid = summary.LoansPaid,
                OutstandingBills = summary.OutstandingBills,
                FundsReturned = summary.PublicFundsReturned,
                ContributorsNumber = summary.ContributorCount,
                MatchingClaims = summary.MatchingClaims
            };
        }

        public static TrainingCourseViewModel TrainingCourseFrom(TrainingStatus source, TrainingCourseType courseType, DateTime cutoffDate)
        {
            if (source == null)
            {
                return new TrainingCourseViewModel
                {
                    Trainees = new List<TraineeViewModel>()
                };
            }
            var sessions = courseType == TrainingCourseType.Unknown ? source.Sessions.Sessions.Values.AsEnumerable() : source.Sessions[courseType];
            return new TrainingCourseViewModel
            {
                CourseType = courseType,
                Trainees = from t in source.Trainees.Values
                           select new TraineeViewModel
                           {
                               Name = t.Name,
                               Relationship = t.CampaignRelationship,
                               Sessions = from r in source.Registrations.Registrations
                                          join s in sessions on r.SessionID equals s.ID
                                          where r.TraineeID == t.ID
                                          select new TrainingSessionViewModel
                                          {
                                              Attended = s.Date.Date > cutoffDate ? null : (bool?)r.Attended,
                                              Completed = s.Date.Date > cutoffDate ? null : (bool?)r.Completed,
                                              AchievesCompliance = source.ComplianceAchievedBy(r),
                                              Date = s.Date
                                          }
                           }
            };
        }

        public static TrainingTrackingViewModel TrainingTrackingFrom(TrainingStatus source, DateTime cutoffDate)
        {
            if (source == null)
                return new TrainingTrackingViewModel();
            var model = new TrainingTrackingViewModel
            {
                HasAuditCompliance = source.AuditComplianceAchieved,
                HasCsmartCompliance = source.CsmartComplianceAchieved,
                Courses = (from c in Enum.GetValues(typeof(TrainingCourseType)).Cast<TrainingCourseType>()
                           where c != TrainingCourseType.Unknown
                           select TrainingCourseFrom(source, c, cutoffDate)).ToDictionary(c => c.CourseType)
            };
            model.HasAuditTrainings = model.Courses[TrainingCourseType.Audit].Trainees.Any(t => t.Sessions.Any());
            model.HasCsmartComplianceTrainings = model.Courses.Where(c => c.Key != TrainingCourseType.Audit).Any(p => p.Value.Trainees.Any(t => t.Sessions.Any()));
            return model;
        }

        private static string DigitsOrDefault(PhoneNumber phone)
        {
            return phone != null && phone.IsValid ? phone.AreaCode + phone.Number : null;
        }
    }
}