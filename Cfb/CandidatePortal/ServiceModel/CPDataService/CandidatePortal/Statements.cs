using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.StatementDateTdsTableAdapters;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a collection of statement dates for an election cycle.
		/// </summary>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>A collection of statement dates for the election cycle specified, indexed by statement number.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<byte, Statement> GetStatements(string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a collection of statement dates for an election cycle.
		/// </summary>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>A collection of statement dates for the election cycle specified, indexed by statement number.</returns>
		public Dictionary<byte, Statement> GetStatements(string electionCycle)
		{
			using (StatementDateTds ds = new StatementDateTds())
			{
				using (StatementsTableAdapter ta = new StatementsTableAdapter())
				{
					ta.ExecuteWithBackupSource(delegate()
					{
						ta.Fill(ds.Statements, electionCycle);
					});
				}
				Dictionary<byte, Statement> statements = new Dictionary<byte, Statement>();
				foreach (StatementDateTds.StatementsRow row in ds.Statements.Rows)
				{
					Statement s = new Statement(row.Number)
					{
						Deferrable = "Y".Equals(row.Deferrable.Trim()),
						IsCatchUp = "Y".Equals(row.IsCatchUpStatement.Trim()),
						IsPrimaryGeneral = "Y".Equals(row.IsPrimaryGeneral.Trim()),
						PostOptInRequired = "Y".Equals(row.PostOptInRequired.Trim())
					};
					if (!row.IsStartDateNull())
						s.StartDate = row.StartDate;
					if (!row.IsEndDateNull())
						s.EndDate = row.EndDate;
					if (!row.IsDueDateNull())
						s.DueDate = row.DueDate;
					statements.Add(row.Number, s);
				}
				return statements;
			}
		}
	}
}
