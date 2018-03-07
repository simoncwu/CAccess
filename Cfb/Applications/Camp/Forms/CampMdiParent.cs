using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.Camp.Settings;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A shell for providing access to components in the top-level CAMP MDI parent form.
	/// </summary>
	public partial class CampMdiParent : Form
	{
		/// <summary>
		/// Gets the main toolstrip for the form.
		/// </summary>
		public virtual ToolStrip MainToolStrip
		{
			get { return null; }
		}

		/// <summary>
		/// Gets the client size of the client area of the form, adjusted to not include any strips.
		/// </summary>
		public virtual Size AdjustedClientSize
		{
			get { return this.ClientSize; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CampMdiParent"/> form.
		/// </summary>
		public CampMdiParent()
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint |
						  ControlStyles.AllPaintingInWmPaint |
						  ControlStyles.ResizeRedraw |
						  ControlStyles.ContainerControl |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.SupportsTransparentBackColor
						  , true);
			this.MinimumSize = new Size(SettingsManager.MinApplicationWidth, SettingsManager.MinApplicationHeight);
			this.StartPosition = FormStartPosition.Manual;
		}

		private void CampMdiParent_Shown(object sender, EventArgs e)
		{
			Rectangle area = Screen.GetWorkingArea(this);
			// ensure form appears on-screen
			if (!area.Contains(this.Location))
			{
				this.Location = area.Location;
			}
			// ensure form fits within screen
			if (this.Width > area.Width)
				this.Width = area.Width;
			if (this.Height > area.Height)
				this.Height = area.Height;
		}
	}
}
