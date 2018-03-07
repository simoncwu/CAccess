namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Defines type-specific methods allowing persistence of an object to a persistence medium, such as a database.
	/// </summary>
	public interface IPersistable
	{
		/// <summary>
		/// Updates this instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		bool Update();

		/// <summary>
		/// Reloads this instance from the persistence storage medium.
		/// </summary>
		/// <returns>true if this instance was reloaded successfully; otherwise, false.</returns>
		bool Reload();

		/// <summary>
		/// Deletes this instance from the persistence storage medium.
		/// </summary>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		bool Delete();
	}
}
