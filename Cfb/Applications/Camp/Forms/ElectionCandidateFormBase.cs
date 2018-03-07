using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.CPConfiguration;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A base form template that allows selection of an active candidate by eleciton cycle.
	/// </summary>
	public partial class ElectionCandidateFormBase : CampMdiChild
	{
		/// <summary>
		/// Creates a new instance of the <see cref="ElectionCandidateFormBase"/> form.
		/// </summary>
		public ElectionCandidateFormBase()
		{
			InitializeComponent();
		}
	}
}
