using System;
using System.Linq;
using System.Collections.Generic;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to web transfer dates by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves the Post Election Initial Documentation Request for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Initial Documentation Request for the candidate and election cycle specified if found; otherwise, null.</returns>
		public InitialDocumentationRequest GetInitialDocumentationRequest(string candidateID, string electionCycle, bool published)
		{
			using (DataClient client = new DataClient())
			{
				InitialDocumentationRequest idr = client.GetInitialDocumentationRequest(candidateID, electionCycle, published);
				if (idr != null && published)
				{
					// populate tolling events, which have to be retrieved separately due to WCF serialization limitation with respect to interfaces
					idr.LoadTollingEvents(this.GetTollingEvents(candidateID, electionCycle, false));
				}
				return idr;
			}
		}

		/// <summary>
		/// Retrieves the Post Election Initial Documentation Request Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response event is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Initial Documentation Request Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		public IdrInadequateEvent GetIdrInadequateEvent(string candidateID, string electionCycle, bool published)
		{
			using (DataClient client = new DataClient()) { return client.GetIdrInadequateEvent(candidateID, electionCycle, published); }
		}

		/// <summary>
		/// Gets whether or not a Post Election Initial Documentation Request response analysis worksheet exists for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>true if an IDR response analysis exists for the candidate and election cycle specified; otherwise, false.</returns>
		public bool HasIdrResponseAnalysis(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.HasIdrResponseAnalysis(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves the Post Election Audit Draft Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		public DraftAuditReport GetDraftAuditReport(string candidateID, string electionCycle, bool published)
		{
			using (DataClient client = new DataClient())
			{
				DraftAuditReport dar = client.GetDraftAuditReport(candidateID, electionCycle, published);
				if (dar != null && published)
				{
					// populate tolling events, which have to be retrieved separately due to WCF serialization limitation with respect to interfaces
					dar.LoadTollingEvents(this.GetTollingEvents(candidateID, electionCycle, true));
				}
				return dar;
			}
		}

		/// <summary>
		/// Retrieves the Post Election Audit Final Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The Post Election Final Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		public FinalAuditReport GetFinalAuditReport(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient())
			{
				return client.GetFinalAuditReport(candidateID, electionCycle);
			}
		}

		/// <summary>
		/// Retrieves the Post Election Draft Audit Report Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response event is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		public DarInadequateEvent GetDarInadequateEvent(string candidateID, string electionCycle, bool published)
		{
			using (DataClient client = new DataClient()) { return client.GetDarInadequateEvent(candidateID, electionCycle, published); }
		}

		/// <summary>
		/// Retrieves a tolling letter by tolling codes.
		/// </summary>
		/// <param name="sourceCode">The tolling source code to match.</param>
		/// <param name="eventCode">The tolling event code to match.</param>
		/// <param name="typeCode">The tolling type code to match.</param>
		/// <returns>The tolling letter designated by the specified tolling codes if one exists; otherwise, null.</returns>
		public TollingLetter GetTollingLetter(string sourceCode, string eventCode, string typeCode)
		{
			using (DataClient client = new DataClient()) { return client.GetTollingLetterByCodes(sourceCode, eventCode, typeCode); }
		}

		/// <summary>
		/// Retrieves a tolling letter by tolling letter ID.
		/// </summary>
		/// <param name="letterID">The ID of the tolling letter to retrieve.</param>
		/// <returns>The tolling letter designated by the specified tolling codes if found; otherwise, null.</returns>
		public TollingLetter GetTollingLetter(int letterID)
		{
			using (DataClient client = new DataClient()) { return client.GetTollingLetterByID(letterID); }
		}

		/// <summary>
		/// Retrieves a collection of post election events that affect tolling for the Draft Audit Report or Final Audit Report.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose events are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="far">true to retrieve events for the Final Audit Report; otherwise, false to retreive events for the Draft Audit Report.</param>
		/// <returns>A collection of tolling events that affect Draft Audit Report tolling.</returns>
		public List<ITollingEvent> GetTollingEvents(string candidateID, string electionCycle, bool far)
		{
			using (DataClient client = new DataClient())
			{
				var events = client.GetTollingEvents(candidateID, electionCycle, far);
				List<ITollingEvent> tollingEvents = new List<ITollingEvent>(events != null ? events.Count : 0);

				// get inadequate events separately
				List<InadequateEventBase> inadequates = new List<InadequateEventBase>();
				if (far)
				{
					foreach (var t in client.GetDarInadequateEvents(candidateID, electionCycle, InadequateEventRetrieval.AllEvents))
					{
						inadequates.Add(t);
						tollingEvents.Add(t);
					}
				}
				else
				{
					foreach (var t in client.GetIdrInadequateEvents(candidateID, electionCycle, InadequateEventRetrieval.AllEvents))
					{
						inadequates.Add(t);
						tollingEvents.Add(t);
					}
				}

				// collection of extensions for each inadequate vent type
				Dictionary<int, List<ITollingEvent>> allExtensions = new Dictionary<int, List<ITollingEvent>>();

				// add remaining standard tolling events (latenesses, extensions)
				foreach (var t in events)
				{
					TollingLetter letter = t.TollingLetter;
					if (letter != null && letter.HasInadequateScope)
					{
						// add inadequate event to collection for matching reference inadequate event
						List<ITollingEvent> inadequateSourceEvents;
						if (!allExtensions.ContainsKey(t.ReferenceEventNumber))
							allExtensions.Add(t.ReferenceEventNumber, new List<ITollingEvent>());
						if (allExtensions.TryGetValue(t.ReferenceEventNumber, out inadequateSourceEvents))
						{
							inadequateSourceEvents.Add(t);
						}
					}
					tollingEvents.Add(t);
				}

				// process extensions for inadequates
				foreach (var i in inadequates)
				{
					List<ITollingEvent> inadequateSourceEvents;
					if (allExtensions.TryGetValue(i.TollingEventNumber, out inadequateSourceEvents))
					{
						i.LoadTollingEvents(inadequateSourceEvents);
					}
				}

				return tollingEvents;
			}
		}

		/// <summary>
		/// Counts the number of post election audit tolling days incurred by a candidate for a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <param name="isFar">true to count Final Audit Report tolling days; otherwise, false to count Draft Audit Report tolling days.</param>
		/// <returns>The number of tolling days incurred that match the criteria specified.</returns>
		public int CountTollingDaysIncurred(string candidateID, string electionCycle, bool isFar)
		{
			using (DataClient client = new DataClient()) { return client.CountTollingDaysIncurred(candidateID, electionCycle, isFar); }
		}

		/// <summary>
		/// Retrieves a collection of Post Election Initial Documentation Inadequate Response events for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
		/// <returns>A collection of Post Election Initial Documentation Inadequate Response events matching the specified criteria.</returns>
		public List<IdrInadequateEvent> GetIdrInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType)
		{
			using (DataClient client = new DataClient()) { return client.GetIdrInadequateEvents(candidateID, electionCycle, retrievalType); }
		}

		/// <summary>
		/// Retrieves a collection of Post Election Draft Audit Response Inadequate Response events for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response events are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
		/// <returns>A collection of Post Election Draft Audit Response Inadequate Response events matching the specified criteria.</returns>
		public List<DarInadequateEvent> GetDarInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType)
		{
			using (DataClient client = new DataClient()) { return client.GetDarInadequateEvents(candidateID, electionCycle, retrievalType); }
		}

		/// <summary>
		/// Retrieves Post Election Audit suspension information for a specfic candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose suspension information is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>Suspension information matching the specified criteria if found; otherwise, null.</returns>
		public Suspension GetSuspension(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetSuspension(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a collection of all known tolling letters.
		/// </summary>
		/// <returns>A collection of all known tolling letter.</returns>
		public List<TollingLetter> GetTollingLetters()
		{
			using (DataClient client = new DataClient()) { return client.GetTollingLetters(); }
		}

		/// <summary>
		/// Retrieves the date when post-election tolling events begin for a specific election cycle.
		/// </summary>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The date when post-election tolling events begin for the specified election cycle.</returns>
		public DateTime? GetTollingStartDate(string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetTollingStartDate(electionCycle); }
		}
	}
}