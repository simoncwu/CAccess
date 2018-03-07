using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a tolling letter by tolling codes.
		/// </summary>
		/// <param name="sourceCode">The tolling source code to match.</param>
		/// <param name="eventCode">The tolling event code to match.</param>
		/// <param name="typeCode">The tolling type code to match.</param>
		/// <returns>The tolling letter designated by the specified tolling codes if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "GetTollingLetterByCodes")]
		[FaultContract(typeof(OfflineDataException))]
		TollingLetter GetTollingLetter(string sourceCode, string eventCode, string typeCode);

		/// <summary>
		/// Retrieves a tolling letter by tolling letter ID.
		/// </summary>
		/// <param name="letterID">The ID of the tolling letter to retrieve.</param>
		/// <returns>The tolling letter designated by the specified tolling codes if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "GetTollingLetterByID")]
		[FaultContract(typeof(OfflineDataException))]
		TollingLetter GetTollingLetter(int letterID);

		/// <summary>
		/// Retrieves a collection of all known tolling letters.
		/// </summary>
		/// <returns>A collection of all known tolling letter.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		List<TollingLetter> GetTollingLetters();
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a tolling letter by tolling codes.
		/// </summary>
		/// <param name="sourceCode">The tolling source code to match.</param>
		/// <param name="eventCode">The tolling event code to match.</param>
		/// <param name="typeCode">The tolling type code to match.</param>
		/// <returns>The tolling letter designated by the specified tolling codes if found; otherwise, null.</returns>
		public TollingLetter GetTollingLetter(string sourceCode, string eventCode, string typeCode)
		{
			using (Data.CPEntities context = new Data.CPEntities())
			{
				return CreateTollingLetter(context.TollingLetters.FirstOrDefault(l => l.TollingSource.Code == sourceCode && l.TollingEvent.Code == eventCode && l.TollingType.Code == typeCode));
			}
		}

		/// <summary>
		/// Retrieves a tolling letter by tolling letter ID.
		/// </summary>
		/// <param name="letterID">The ID of the tolling letter to retrieve.</param>
		/// <returns>The tolling letter designated by the specified ID if found; otherwise, null.</returns>
		public TollingLetter GetTollingLetter(int letterID)
		{
			using (Data.CPEntities context = new Data.CPEntities())
			{
				return CreateTollingLetter(context.TollingLetters.FirstOrDefault(l => l.LetterId == letterID));
			}
		}

		/// <summary>
		/// Retrieves a collection of all known tolling letters.
		/// </summary>
		/// <returns>A collection of all known tolling letter.</returns>
		public List<TollingLetter> GetTollingLetters()
		{
			using (Data.CPEntities context = new Data.CPEntities())
			{
				return context.TollingLetters.AsEnumerable().Select(l => CreateTollingLetter(l)).ToList();
			}
		}

		/// <summary>
		/// Creates a new <see cref="TollingLetter"/> instance using C-Access data.
		/// </summary>
		/// <param name="data">The data to use.</param>
		/// <returns>A new tolling letter instance using the specified data if valid; otherwise, null.</returns>
		private TollingLetter CreateTollingLetter(Data.TollingLetter data)
		{
			if (data == null)
				return null;
			var sourceData = data.TollingSource;
			var eventData = data.TollingEvent;
			var typeData = data.TollingType;
			return new TollingLetter(data.LetterId)
			{
				SourceCode = new TollingCode(TollingCodeType.SourceCode, data.SourceId) { Code = sourceData.Code, Description = sourceData.Description },
				EventCode = new TollingCode(TollingCodeType.EventCode, data.EventId) { Code = eventData.Code, Description = eventData.Description },
				TypeCode = new TollingCode(TollingCodeType.TypeCode, data.TypeId) { Code = typeData.Code, Description = typeData.Description },
				Description = data.Description,
				Title = data.Title
			};
		}
	}
}
