using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's submission document history.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data", Name = "SubmissionHistory{0}")]
	public class SubmissionHistory<T> where T : SubmissionDocument
	{
		/// <summary>
		/// The inner collection of submission documents.
		/// </summary>
		[DataMember(Name = "Documents")]
		private readonly List<T> _documents;

		/// <summary>
		/// Gets the inner collection of submission documents.
		/// </summary>
		public List<T> Documents
		{
			get { return _documents; }
		}

		/// <summary>
		/// Initializes a new, empty collection.
		/// </summary>
		public SubmissionHistory()
		{
			_documents = new List<T>();
		}

		/// <summary>
		/// Initializes a new, empty collection with a predefined initial capacity.
		/// <param name="capacity">The number of objects that the collection can initially store.</param>
		/// </summary>
		public SubmissionHistory(int capacity)
		{
			_documents = new List<T>(capacity);
		}

		/// <summary>
		/// Gets the date when the candidate's submission document history was last updated in CFIS.
		/// </summary>
		public virtual DateTime LastUpdated
		{
			get
			{
				DateTime dt = DateTime.MinValue;
				foreach (SubmissionDocument s in _documents)
				{
					if (dt.CompareTo(s.LastUpdated) < 0)
						dt = s.LastUpdated;
				}
				return dt;
			}
		}
	}
}
