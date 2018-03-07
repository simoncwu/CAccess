using System.Configuration;

namespace Cfb.CandidatePortal.CPConfiguration
{
	/// <summary>
	/// Provides access to Campaign Messages Online configuration settings.
	/// 
	/// Sample XML Schema:
	/// <![CDATA[
	/// <cAccess>
	///		<profile photoRepository="FileRepository">
	///			<repositories>
	///				<add name="FileRepository" filePath="C:\WINDOWS\TEMP" />
	///			</repositories>
	///		</profile>
	///	</cAccess>
	/// ]]>
	/// </summary>
	public class CPProfileSection : ConfigurationSection
	{
		/// <summary>
		/// The configuration section name.
		/// </summary>
		public const string SectionName = "profile";

		#region Configuration Property Names

		/// <summary>
		/// Property name for the default attachment repository name property.
		/// </summary>
		private const string PhotoRepositoryPropertyName = "photoRepository";

		/// <summary>
		/// Property name for the repositories collection property.
		/// </summary>
		private const string RepositoriesPropertyName = "repositories";

		#endregion

		/// <summary>
		/// Gets or sets the default attachment repository name.
		/// </summary>
		[ConfigurationProperty(PhotoRepositoryPropertyName, IsRequired = true)]
		public string PhotoRepositoryName
		{
			get { return this[PhotoRepositoryPropertyName] as string; }
			set { this[PhotoRepositoryPropertyName] = value; }
		}

		/// <summary>
		/// Gets a collection of attachment repository definitions.
		/// </summary>
		[ConfigurationProperty(RepositoriesPropertyName, IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(RepositoryElementCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
		public RepositoryElementCollection Repositories
		{
			get { return this[RepositoriesPropertyName] as RepositoryElementCollection; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CPProfileSection"/> class.
		/// </summary>
		public CPProfileSection()
		{
		}
	}
}
