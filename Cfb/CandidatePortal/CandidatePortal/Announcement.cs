using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A broadcast announcement to all C-Access user accounts.
	/// </summary>
	public class Announcement
	{
		/// <summary>
		/// The collection of election cycles targeted.
		/// </summary>
		private StringCollection _electionCycles;

		/// <summary>
		/// Gets the collection of election cycles targeted.
		/// </summary>
		public StringCollection ElectionCycles
		{
			get { return _electionCycles; }
		}

		/// <summary>
		/// The unique identifier for the announcement.
		/// </summary>
		private readonly int _id;

		/// <summary>
		/// Gets the unique identifier for the announcement.
		/// </summary>
		public int ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets or sets the date the announcement was posted.
		/// </summary>
		public DateTime? Posted { get; set; }

		/// <summary>
		/// Gets or sets the announcement title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the announcement body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Gets or sets whether or not the announcement was approved.
		/// </summary>
		public bool Approved { get; set; }

		/// <summary>
		/// Gets or sets whether or not the announcement overrides the default filing date information template.
		/// </summary>
		public bool OverridesFilingInfoTemplate { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Announcement"/> class.
		/// </summary>
		/// <param name="id">The unique identifier for the announcement.</param>
		public Announcement(int id)
		{
			_electionCycles = new StringCollection();
			_id = id;
		}
	}
}