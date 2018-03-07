using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Cfb.CandidatePortal.Cmo.Properties;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// A Campaign Messages Online message category.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CmoCategory : IPersistable
	{
		/// <summary>
		/// Gets the identifier for the generic message category.
		/// </summary>
		public static byte GenericCategoryID
		{
			get { return Settings.Default.GenericCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the statement review category.
		/// </summary>
		public static byte StatementReviewCategoryID
		{
			get { return Settings.Default.StatementReviewCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the compliance visit category.
		/// </summary>
		public static byte ComplianceVisitCategoryID
		{
			get { return Settings.Default.ComplianceVisitCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the Post Election Audit Draft Audit Report category.
		/// </summary>
		public static byte DarCategoryID
		{
			get { return Settings.Default.DarCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the Post Election Audit Final Audit Report category.
		/// </summary>
		public static byte FarCategoryID
		{
			get { return Settings.Default.FarCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the legal correspondence category.
		/// </summary>
		public static byte LegalCategoryID
		{
			get { return Settings.Default.LegalCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the Doing Business Review category.
		/// </summary>
		public static byte DoingBusinessReviewCategoryID
		{
			get { return Settings.Default.DoingBusinessReviewCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the payment/non-payment letters category.
		/// </summary>
		public static byte PaymentCategoryID
		{
			get { return Settings.Default.PaymentCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the no pay findings category.
		/// </summary>
		public static byte NoPayFindingsCategoryID
		{
			get { return Settings.Default.NoPayFindingsCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the Post Election Audit Initial Documentation Request category.
		/// </summary>
		public static byte IdrCategoryID
		{
			get { return Settings.Default.IdrCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the tolling letter category.
		/// </summary>
		public static byte TollingLetterCategoryID
		{
			get { return Settings.Default.TollingLetterCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the IDR Inadequate Response letter category.
		/// </summary>
		public static byte IdrInadequateCategoryID
		{
			get { return Settings.Default.IdrInadequateCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the DAR Inadequate Response letter category.
		/// </summary>
		public static byte DarInadequateCategoryID
		{
			get { return Settings.Default.DarInadequateCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the IDR Additional Inadequate Response letter category.
		/// </summary>
		public static byte IdrAdditionalInadequateCategoryID
		{
			get { return Settings.Default.IdrAdditionalCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the DAR Additional Inadequate Response letter category.
		/// </summary>
		public static byte DarAdditionalInadequateCategoryID
		{
			get { return Settings.Default.DarAdditionalCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the Post Election Enforcement Notice category.
		/// </summary>
		public static byte EnforcementNoticeCategoryID
		{
			get { return Settings.Default.EnforcementCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the CFB Staff Final Recommendation category.
		/// </summary>
		public static byte FinalRecommendationCategoryID
		{
			get { return Settings.Default.FinalRecommendationCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the Final Board Determination category.
		/// </summary>
		public static byte FinalBoardDeterminationCategoryID
		{
			get { return Settings.Default.FinalBoardDeterminationCategoryID; }
		}

		/// <summary>
		/// Gets the identifier for the Post-Election Public Funds Payment category.
		/// </summary>
		public static byte PostElectionPaymentCategoryID
		{
			get { return Settings.Default.PostElectionPaymentCategoryID; }
		}

		/// <summary>
		/// The category identifier.
		/// </summary>
		[DataMember(Name = "ID")]
		private readonly byte _id;

		/// <summary>
		/// Gets the category identifier.
		/// </summary>
		public byte ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets or sets the category name.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a user-friendly description of the category.
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets whether or not the category is hidden from users for manual use.
		/// </summary>
		[DataMember]
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets the templated title for messages of this category.
		/// </summary>
		[DataMember]
		public string MessageTemplateTitle { get; set; }

		/// <summary>
		/// Gets or sets the templated body for messages of this category.
		/// </summary>
		[DataMember]
		public string MessageTemplateBody { get; set; }

		/// <summary>
		/// Gets or sets whether or not the template for messages of this category is editable.
		/// </summary>
		[DataMember]
		public bool MessageTemplateEditable { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CmoCategory"/> class.
		/// </summary>
		/// <param name="id">The category identifier.</param>
		public CmoCategory(byte id)
		{
			_id = id;
		}

		#region IPersistable Members

		/// <summary>
		/// Updates this instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		public bool Update()
		{
			try
			{
				return CmoProviders.DataProvider.Update(this);
			}
			finally
			{
				this.Reload();
			}
		}

		/// <summary>
		/// Reloads this instance from the persistence storage medium.
		/// </summary>
		/// <returns>true if this instance was reloaded successfully; otherwise, false.</returns>
		public bool Reload()
		{
			CmoCategory current = CmoProviders.DataProvider.GetCmoCategory(_id);
			if (current != null)
			{
				this.Description = current.Description;
				this.MessageTemplateBody = current.MessageTemplateBody;
				this.MessageTemplateTitle = current.MessageTemplateTitle;
				this.Name = current.Name;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Deletes this instance from the persistence storage medium.
		/// </summary>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		public bool Delete()
		{
			return CmoProviders.DataProvider.Delete(this);
		}

		#endregion

		/// <summary>
		/// Converts the CMO category ID of a post election audit report type to its <see cref="AuditReportType"/> enum equivalent.
		/// </summary>
		/// <param name="id">A byte containing the category ID to convert.</param>
		/// <returns>The <see cref="AuditReportType"/> value equivalent to the post election report type with id <paramref name="id"/>, if the conversion succeeded; otherwise, null.</returns>
		public static AuditReportType? ToAuditReportType(byte id)
		{
			if (id == CmoCategory.IdrCategoryID)
				return AuditReportType.InitialDocumentationRequest;
			if (id == CmoCategory.IdrInadequateCategoryID)
				return AuditReportType.IdrInadequateResponse;
			if (id == CmoCategory.IdrAdditionalInadequateCategoryID)
				return AuditReportType.IdrAdditionalInadequateResponse;
			if (id == CmoCategory.DarCategoryID)
				return AuditReportType.DraftAuditReport;
			if (id == CmoCategory.DarInadequateCategoryID)
				return AuditReportType.DarInadequateResponse;
			if (id == CmoCategory.DarAdditionalInadequateCategoryID)
				return AuditReportType.DarAdditionalInadequateResponse;
			if (id == CmoCategory.FarCategoryID)
				return AuditReportType.FinalAuditReport;
			return null;
		}

		/// <summary>
		/// Gets a Campaign Message Online category by ID.
		/// </summary>
		/// <param name="id">The ID of the category to get.</param>
		/// <returns>The CMO category matching the specified ID if found; otherwise, null.</returns>
		public static CmoCategory GetCmoCategory(byte id)
		{
			return CmoProviders.DataProvider.GetCmoCategory(id);
		}

		/// <summary>
		/// Gets a collection of all Campaign Messages Online categories, indexed by category ID.
		/// </summary>
		/// <returns>A collection of all Campaign Messages Online categories, indexed by category ID.</returns>
		public static Dictionary<byte, CmoCategory> GetCmoCategories()
		{
			return CmoProviders.DataProvider.GetCmoCategories();
		}
	}
}

namespace Cfb.CandidatePortal.PostElection
{
	using Cfb.CandidatePortal.Cmo;

	/// <summary>
	/// Extensions to the <see cref="Cfb.CandidatePortal.Cmo.CmoCategory"/> namespace.
	/// </summary>
	public static class Cfb_CandidatePortal_Cmo_CmoCategory_Extensions
	{
		/// <summary>
		/// Gets a tolling letter's corresponding CMO category ID.
		/// </summary>
		/// <param name="letter">The <see cref="TollingLetter"/> to examine.</param>
		/// <returns>The CMO category ID corresponding to the specified tolling letter.</returns>
		public static byte GetCmoCategoryID(this TollingLetter letter)
		{
			if (letter.IsInadequate)
			{
				if (letter.HasIdrInadequateScope)
				{
					return letter.HasAdditionalInadequateScope ? CmoCategory.IdrAdditionalInadequateCategoryID : CmoCategory.IdrInadequateCategoryID;
				}
				if (letter.HasDarInadequateScope)
				{
					return letter.HasAdditionalInadequateScope ? CmoCategory.DarAdditionalInadequateCategoryID : CmoCategory.DarInadequateCategoryID;
				}
			}
			return CmoCategory.TollingLetterCategoryID;
		}
	}
}