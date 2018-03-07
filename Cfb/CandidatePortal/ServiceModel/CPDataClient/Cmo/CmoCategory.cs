using System.Collections.Generic;
using System.Linq;
using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to CMO category data via WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Gets a collection of all Campaign Messages Online categories, indexed by category ID.
		/// </summary>
		/// <returns>A collection of all Campaign Messages Online categories, indexed by category ID.</returns>
		public Dictionary<byte, CmoCategory> GetCmoCategories()
		{
			using (DataClient client = new DataClient())
			{
				return client.GetCmoCategories();
			}
		}

		/// <summary>
		/// Gets a Campaign Message Online category by ID.
		/// </summary>
		/// <param name="id">The ID of the category to get.</param>
		/// <returns>The CMO category matching the specified ID if found; otherwise, null.</returns>
		public CmoCategory GetCmoCategory(byte id)
		{
			return GetCmoCategories().FirstOrDefault(c => c.Key == id).Value;
		}

		/// <summary>
		/// Updates this instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="category">The category to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		public bool Update(CmoCategory category)
		{
			using (DataClient client = new DataClient())
			{
				return client.UpdateCmoCategory(category);
			}
		}

		/// <summary>
		/// Deletes this instance from the persistence storage medium.
		/// </summary>
		/// <param name="category">The category to delete.</param>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		public bool Delete(CmoCategory category)
		{
			using (DataClient client = new DataClient())
			{
				return client.DeleteCmoCategory(category);
			}
		}
	}
}
