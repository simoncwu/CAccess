using System;
using Cfb.CandidatePortal.CPConfiguration;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// Provides access to Campaign Messages Online messages and attachments.
	/// </summary>
	public static class Cmo
	{
		/// <summary>
		/// The default attachment repository.
		/// </summary>
		private static readonly AttachmentRepository _repository = null;

		/// <summary>
		/// Gets the default attachment repository.
		/// </summary>
		public static AttachmentRepository Repository
		{
			get { return _repository; }
		}

		/// <summary>
		/// A collection of attachment repositories for the C-Access application.
		/// </summary>
		private static readonly AttachmentRepositoryCollection _repositories;

		/// <summary>
		/// Gets a collection of attachment repositories for the C-Access application.
		/// </summary>
		public static AttachmentRepositoryCollection Repositories
		{
			get { return _repositories; }
		}

		/// <summary>
		/// Initializes Campaign Messages Online data before any static members are referenced.
		/// </summary>
		static Cmo()
		{
			_repositories = new AttachmentRepositoryCollection();
			// read repositories from configuration settings
			if (CPConfigurationManager.Cmo != null)
			{
				string defaultRepositoryName = null;
				defaultRepositoryName = CPConfigurationManager.Cmo.DefaultRepositoryName;
				bool hasDefault = !string.IsNullOrEmpty(defaultRepositoryName);
				foreach (RepositoryElement are in CPConfigurationManager.Cmo.Repositories)
				{
					AttachmentRepository repository = new AttachmentRepository(are.Name, are.FilePath);
					_repositories.Add(repository);
					if (hasDefault && (_repository == null) && defaultRepositoryName.Equals(are.Name, StringComparison.InvariantCultureIgnoreCase))
					{
						_repository = repository;
					}
				}
			}
		}
	}
}
