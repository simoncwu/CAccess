using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.WebTransferDateTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a web transfer date for a specific election cycle.
		/// </summary>
		/// <returns>A web transfer date for a specific election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		DateTime? GetWebTransferDate(string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a web transfer date for a specific election cycle.
		/// </summary>
		/// <returns>A web transfer date for a specific election cycle.</returns>
		public DateTime? GetWebTransferDate(string electionCycle)
		{
			using (WebTransferDateTds ds = new WebTransferDateTds())
			{
				using (WebTransferDateTableAdapter ta = new WebTransferDateTableAdapter())
				{
					ta.Fill(ds.WebTransferDate, electionCycle);
				}
				foreach (WebTransferDateTds.WebTransferDateRow row in ds.WebTransferDate.Rows)
				{
					return row.TransferDate;
				}
			}
			return null;
		}
	}
}
