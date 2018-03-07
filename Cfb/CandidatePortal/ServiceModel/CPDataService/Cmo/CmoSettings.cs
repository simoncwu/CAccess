using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Data.CmoSettingsTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Creates a new settings entry for a candidate and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateId">The CFIS ID of the candidate to whom this preference applies.</param>
		/// <param name="isPaperless">Whether or not the candidate has a paperless preference.</param>
		/// <param name="username">The C-Access username of the updating user.</param>
		/// <returns>A new <see cref="CmoSettings"/> object if the settings were added successfully; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		CmoSettings AddCmoSettings(string candidateId, bool isPaperless, string username);

		/// <summary>
		/// Gets the C-Access Campaign Message Online settings for a specific candidate.
		/// </summary>
		/// <param name="candidateId">The CFIS ID of the candidate to query.</param>
		/// <returns>The specified candidate's Campaign Message Online settings if one exists; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		CmoSettings GetCmoSettings(string candidateId);

		/// <summary>
		/// Saves a CMO message in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="settings">The CMO settings to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "UpdateCmoSettings")]
		[FaultContract(typeof(OfflineDataException))]
		bool Update(CmoSettings settings);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Saves a CMO message in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="settings">The CMO settings to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		public bool Update(CmoSettings settings)
		{
			if (settings == null)
				return false;
			using (CmoSettingsTableAdapter ta = new CmoSettingsTableAdapter())
			{
				return (Convert.ToInt32(ta.SetData(settings.CandidateID, settings.IsPaperless, settings.UpdaterUserName, settings.Version)) > 0) ? true : false;
			}
		}

		/// <summary>
		/// Creates a new settings entry for a candidate and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateId">The CFIS ID of the candidate to whom this preference applies.</param>
		/// <param name="isPaperless">Whether or not the candidate has a paperless preference.</param>
		/// <param name="username">The C-Access username of the updating user.</param>
		/// <returns>A new <see cref="CmoSettings"/> object if the settings were added successfully; otherwise, null.</returns>
		public CmoSettings AddCmoSettings(string candidateId, bool isPaperless, string username)
		{
			using (CmoSettingsTableAdapter ta = new CmoSettingsTableAdapter())
			{
				return (Convert.ToInt32(ta.SetData(candidateId, isPaperless, username, new byte[0])) > 0) ? GetCmoSettings(ta, candidateId) : null;
			}
		}

		/// <summary>
		/// Gets the C-Access Campaign Message Online settings for a specific candidate.
		/// </summary>
		/// <param name="candidateId">The CFIS ID of the candidate to query.</param>
		/// <returns>The specified candidate's Campaign Message Online settings if one exists; otherwise, null.</returns>
		public CmoSettings GetCmoSettings(string candidateId)
		{
			return GetCmoSettings(null, candidateId);

		}

		/// <summary>
		/// Converts data from a persistence storage record into its CMO settings equivalent. A return value indicates whether the operation succeeded.
		/// </summary>
		/// <param name="row">A row containing the data to convert.</param>
		/// <param name="settings">When this method returns, contains the CMO settings equivalent to the data contained in <paramref name="row"/>, if the parse succeeded, or null if the parse failed. The parse fails if the <paramref name="row"/> parameter is null or is not of the correct format. This parameter is passed uninitialized.</param>
		/// <returns>true if <paramref name="row"/> was parsed successfully; otherwise, false.</returns>
		private bool TryParse(CmoSettingsTds.CmoSettingsRow row, out CmoSettings settings)
		{
			settings = null;
			if (row != null)
			{
				settings = new CmoSettings(row.CandidateId, row.Version)
				{
					IsPaperless = row.IsPaperless,
					UpdaterUserName = row.UpdaterUserName,
					UpdatedDate = row.UpdatedDate,
				};
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the C-Access Campaign Message Online settings for a specific candidate.
		/// </summary>
		/// <param name="adapter">A <see cref="CmoSettingsTableAdapter"/> for access to storage medium.</param>
		/// <param name="candidateId">The CFIS ID of the candidate to query.</param>
		/// <returns>The specified candidate's Campaign Messages Online settings if one exists; otherwise, null.</returns>
		private CmoSettings GetCmoSettings(CmoSettingsTableAdapter adapter, string candidateId)
		{
			using (CmoSettingsTds ds = new CmoSettingsTds())
			{
				if (adapter == null)
				{
					using (adapter = new CmoSettingsTableAdapter())
					{
						adapter.FillBy(ds.CmoSettings, candidateId);
					}
				}
				else
				{
					adapter.FillBy(ds.CmoSettings, candidateId);
				}
				foreach (CmoSettingsTds.CmoSettingsRow row in ds.CmoSettings.Rows)
				{
					CmoSettings cos;
					if (TryParse(row, out cos))
						return cos;
				}
			}
			return null;
		}
	}
}
