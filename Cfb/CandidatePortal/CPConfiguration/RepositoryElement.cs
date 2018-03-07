using System.Configuration;

namespace Cfb.CandidatePortal.CPConfiguration
{
	/// <summary>
	/// A configuration element that defines an attachment repository.
	/// </summary>
	public class RepositoryElement : ConfigurationElement
	{
		#region Configuration Property Names

		/// <summary>
		/// Property name for repository name property.
		/// </summary>
		private const string NamePropertyName = "name";

		/// <summary>
		/// Property name for repository file path property.
		/// </summary>
		private const string FilePathPropertyName = "filePath";

		/// <summary>
		/// Property name for repository description property.
		/// </summary>
		private const string DescriptionPropertyName = "description";

		#endregion

		/// <summary>
		/// Gets or sets the attachment repository name configuration property.
		/// </summary>
		[ConfigurationProperty(NamePropertyName, IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return this[NamePropertyName] as string; }
			set { this[NamePropertyName] = value; }
		}

		/// <summary>
		/// Gets or sets the attachment repository file path configuration property.
		/// </summary>
		[ConfigurationProperty(FilePathPropertyName, IsRequired = false)]
		public string FilePath
		{
			get { return this[FilePathPropertyName] as string; }
			set { this[FilePathPropertyName] = value; }
		}

		/// <summary>
		/// Gets or sets the attachment repository description configuration property.
		/// </summary>
		[ConfigurationProperty(DescriptionPropertyName, IsRequired = false)]
		public string Description
		{
			get { return this[DescriptionPropertyName] as string; }
			set { this[DescriptionPropertyName] = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryElement"/> class.
		/// </summary>
		public RepositoryElement()
		{
		}
	}
}
