using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Data.GlobalSettingsTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a global C-Access setting.
		/// </summary>
		/// <param name="settingName">The name of the setting to retrieve.</param>
		/// <returns>The value of the setting if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		string GetGlobalSetting(string settingName);

		/// <summary>
		/// Saves a global C-Access setting.
		/// </summary>
		/// <param name="settingName">The name of the setting to save.</param>
		/// <param name="value">The desired value of the setting.</param>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		void SetGlobalSetting(string settingName, string value);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a global C-Access setting.
		/// </summary>
		/// <param name="settingName">The name of the setting to retrieve.</param>
		/// <returns>The value of the setting if found; otherwise, null.</returns>
		public string GetGlobalSetting(string settingName)
		{
			using (GlobalSettingsTds ds = new GlobalSettingsTds())
			{
				using (GlobalSettingsTableAdapter ta = new GlobalSettingsTableAdapter())
				{
					ta.FillBy(ds.GlobalSettings, settingName);
				}
				foreach (GlobalSettingsTds.GlobalSettingsRow row in ds.GlobalSettings.Rows)
				{
					return row.Value;
				}
			}
			return null;
		}

		/// <summary>
		/// Saves a global C-Access setting.
		/// </summary>
		/// <param name="settingName">The name of the setting to save.</param>
		/// <param name="value">The desired value of the setting.</param>
		public void SetGlobalSetting(string settingName, string value)
		{
			using (GlobalSettingsTableAdapter ta = new GlobalSettingsTableAdapter())
			{
				ta.SaveSetting(settingName, value);
			}
		}
	}
}
