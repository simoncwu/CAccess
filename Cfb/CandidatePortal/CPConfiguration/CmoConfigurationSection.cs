using System.Configuration;

namespace Cfb.CandidatePortal.CPConfiguration
{
	/// <summary>
	/// Provides access to Campaign Messages Online configuration settings.
	/// 
	/// Sample XML Schema:
	/// <![CDATA[
	/// <cAccess>
	///		<cmo defaultRepository="FileRepository">
	///			<repositories>
	///				<add name="FileRepository" filePath="C:\WINDOWS\TEMP" />
	///			</repositories>
	///		</cmo>
	///	</cAccess>
	/// ]]>
	/// </summary>
	public class CmoConfigurationSection : ConfigurationSection
	{
		/// <summary>
		/// The configuration section name.
		/// </summary>
		public const string SectionName = "cmo";

		#region Configuration Property Names

		/// <summary>
		/// Property name for the default attachment repository name property.
		/// </summary>
		private const string DefaultRepositoryPropertyName = "defaultRepository";

		/// <summary>
		/// Property name for the repositories collection property.
		/// </summary>
		private const string RepositoriesPropertyName = "repositories";

		#endregion

		/// <summary>
		/// Gets or sets the default attachment repository name.
		/// </summary>
		[ConfigurationProperty(DefaultRepositoryPropertyName, IsRequired = true)]
		public string DefaultRepositoryName
		{
			get { return this[DefaultRepositoryPropertyName] as string; }
			set { this[DefaultRepositoryPropertyName] = value; }
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
		/// Initializes a new instance of the <see cref="CmoConfigurationSection"/> class.
		/// </summary>
		public CmoConfigurationSection()
		{
		}
	}
}
