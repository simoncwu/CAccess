using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.FilingRequirementTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves filing dates and requirements for a particular candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing dates and requirements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all filing dates and requirements on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		FilingDeadlines GetFilingDeadlines(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves filing dates and requirements for a particular candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing dates and requirements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all filing dates and requirements on record for the specified candidate and election cycle.</returns>
		public FilingDeadlines GetFilingDeadlines(string candidateID, string electionCycle)
		{
			using (FilingRequirementTds ds = new FilingRequirementTds())
			{
				using (FilingRequirementsTableAdapter ta = new FilingRequirementsTableAdapter())
				{
					ta.Fill(ds.FilingRequirements, candidateID, electionCycle);
				}
				FilingDeadlines c = new FilingDeadlines();
				foreach (FilingRequirementTds.FilingRequirementsRow row in ds.FilingRequirements.Rows)
				{
					FilingDeadline fd = new FilingDeadline(row.DueDate.Date, row.StatementNumber)
					{
						IsRequired = !row.IsRequiredIndNull() && "Y".Equals(row.RequiredInd, StringComparison.InvariantCultureIgnoreCase),
						SubmissionReceivedDate = row.IsDateReceivedNull() ? null : row.DateReceived as DateTime?
					};
					c.Deadlines.Add(fd);
					if (fd.IsLate)
						c.Late.Add(fd);
					if (fd.IsMissing)
						c.Missing.Add(fd);
				}
				return c;
			}
		}
	}
}
