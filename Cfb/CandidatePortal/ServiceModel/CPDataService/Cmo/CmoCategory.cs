using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Deletes a CMO category instance from the persistence storage medium.
		/// </summary>
		/// <param name="category">The CMO category instance to update.</param>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "DeleteCmoCategory")]
		[FaultContract(typeof(OfflineDataException))]
		bool Delete(CmoCategory category);

		/// <summary>
		/// Gets a collection of all Campaign Messages Online categories, indexed by category ID.
		/// </summary>
		/// <returns>A collection of all Campaign Messages Online categories, indexed by category ID.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<byte, CmoCategory> GetCmoCategories();

		/// <summary>
		/// Gets a Campaign Message Online category by ID.
		/// </summary>
		/// <param name="id">The ID of the category to get.</param>
		/// <returns>The CMO category matching the specified ID if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		CmoCategory GetCmoCategory(byte id);

		/// <summary>
		/// Updates a CMO category instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="category">The CMO category instance to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "UpdateCmoCategory")]
		[FaultContract(typeof(OfflineDataException))]
		bool Update(CmoCategory category);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Updates a CMO category instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="category">The CMO category instance to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		public bool Update(CmoCategory category)
		{
			if (category != null)
			{
				using (Data.CmoEntities context = new Data.CmoEntities())
				{
					var match = context.CmoCategories.FirstOrDefault(c => c.CategoryId == category.ID);
					if (match != null)
					{
						match.CategoryName = category.Name;
						match.Description = category.Description;
						match.Hidden = category.Hidden;
						if (category.MessageTemplateTitle != null)
							match.TemplateTitle = category.MessageTemplateTitle;
						if (category.MessageTemplateBody != null)
							match.TemplateBody = category.MessageTemplateBody;
						match.TemplateEditable = category.MessageTemplateEditable;
						try
						{
							return context.SaveChanges(SaveOptions.DetectChangesBeforeSave) > 0;
						}
						catch (OptimisticConcurrencyException) { }
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Deletes a CMO category instance from the persistence storage medium.
		/// </summary>
		/// <param name="category">The CMO category instance to update.</param>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		public bool Delete(CmoCategory category)
		{
			if (category != null)
			{
				using (Data.CmoEntities context = new Data.CmoEntities())
				{
					var match = context.CmoCategories.FirstOrDefault(c => c.CategoryId == category.ID);
					if (match != null)
					{
						context.DeleteObject(match);
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Gets a Campaign Message Online category by ID.
		/// </summary>
		/// <param name="id">The ID of the category to get.</param>
		/// <returns>The CMO category matching the specified ID if found; otherwise, null.</returns>
		public CmoCategory GetCmoCategory(byte id)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				return CreateCmoCategory(context.CmoCategories.FirstOrDefault(c => c.CategoryId == id));
			}
		}

		/// <summary>
		/// Gets a collection of all Campaign Messages Online categories, indexed by category ID.
		/// </summary>
		/// <returns>A collection of all Campaign Messages Online categories, indexed by category ID.</returns>
		public Dictionary<byte, CmoCategory> GetCmoCategories()
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				return context.CmoCategories.ToDictionary(c => c.CategoryId, c => CreateCmoCategory(c));
			}
		}

		/// <summary>
		/// Converts data from a persistence storage record into its CMO category equivalent.
		/// </summary>
		/// <param name="data">A row containing the data to convert.</param>
		/// <returns>A CMO category equivalent to the data contained in <paramref name="data"/>.</returns>
		private static CmoCategory CreateCmoCategory(Data.CmoCategory data)
		{
			if (data == null)
				return null;
			return new CmoCategory(data.CategoryId)
			{
				Name = data.CategoryName,
				Description = data.Description,
				Hidden = data.Hidden,
				MessageTemplateTitle = data.TemplateTitle,
				MessageTemplateBody = data.TemplateBody,
				MessageTemplateEditable = data.TemplateEditable
			};
		}
	}
}
