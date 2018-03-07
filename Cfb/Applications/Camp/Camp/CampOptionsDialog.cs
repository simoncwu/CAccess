using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;

namespace Cfb.Camp
{
	/// <summary>
	/// A dialog form for setting CAMP options.
	/// </summary>
	public partial class CampOptionsDialog : Form
	{
		#region Change Flags

		/// <summary>
		/// Flags for tracking when fields are changed so they can be saved upon clicking "OK".
		/// </summary>
		private bool _defaultElectionCycleChanged;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="CampOptionsDialog"/> form.
		/// </summary>
		public CampOptionsDialog()
		{
			InitializeComponent();
		}

		#region Control Event Handlers

		private void CampOptionsDialog_Load(object sender, EventArgs e)
		{
			this.electionPicker.SetCandidateID(null);
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (_defaultElectionCycleChanged && this.electionPicker.SelectedElection != null)
				SettingsManager.DefaultElectionCycle = this.electionPicker.SelectedElection.Cycle;
		}

		private void electionPicker_SelectionChangeCommitted(object sender, EventArgs e)
		{
			_defaultElectionCycleChanged = true;
		}

		#endregion

	}
}
