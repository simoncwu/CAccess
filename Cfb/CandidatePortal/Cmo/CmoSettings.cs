using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// Business object representation of a candidate's Campaign Message Online settings.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CmoSettings : IPersistable
	{
		[DataMember(Name = "CandidateID")]
		private readonly string _candidateID;

		/// <summary>
		/// Gets or sets the candidate ID for the targeted candidate recipient.
		/// </summary>
		public string CandidateID
		{
			get { return _candidateID; }
		}

		/// <summary>
		/// Gets or sets whether or not the candidate has requested to stop receiving paper mailings.
		/// </summary>
		[DataMember]
		public bool IsPaperless { get; set; }

		/// <summary>
		/// Gets or sets the username of the user who last updated the Campaign Message Online settings.
		/// </summary>
		[DataMember]
		public string UpdaterUserName { get; set; }

		/// <summary>
		/// Gets or sets the date when the Campaign Message Online settings was udpated.
		/// </summary>
		[DataMember]
		public DateTime? UpdatedDate { get; set; }

		/// <summary>
		/// Gets the version of the settings.
		/// </summary>
		[DataMember]
		public byte[] Version { get; private set; }

		/// <summary>
		/// Creates a new instance of the <see cref="CmoSettings"/> class.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate associated with these settings.</param>
		internal CmoSettings(string candidateID)
		{
			_candidateID = candidateID;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="CmoSettings"/> class.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate associated with these settings.</param>
		/// <param name="version">The settings version.</param>
		internal CmoSettings(string candidateID, byte[] version)
			: this(candidateID)
		{
			this.Version = version;
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
			CmoSettings current = CmoProviders.DataProvider.GetCmoSettings(_candidateID);
			if (current != null)
			{
				this.IsPaperless = current.IsPaperless;
				this.UpdatedDate = current.UpdatedDate;
				this.UpdaterUserName = current.UpdaterUserName;
				this.Version = current.Version;
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
			return false;
		}

		#endregion

		/// <summary>
		/// Gets the C-Access Campaign Message Online settings for a specific candidate.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate to query.</param>
		/// <returns>The specified candidate's Campaign Message Online settings if one exists; otherwise, null.</returns>
		public static CmoSettings GetSettings(string candidateID)
		{
			return CmoProviders.DataProvider.GetCmoSettings(candidateID);
		}

		/// <summary>
		/// Creates a new settings entry for a candidate and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate to whom this preference applies.</param>
		/// <param name="isPaperless">Whether or not the candidate has a paperless preference.</param>
		/// <param name="username">The C-Access username of the updating user.</param>
		/// <returns>A new <see cref="CmoSettings"/> object if the settings were added successfully; otherwise, null.</returns>
		public static CmoSettings Add(string candidateID, bool isPaperless, string username)
		{
			return CmoProviders.DataProvider.AddCmoSettings(candidateID, isPaperless, username);
		}
	}
}
