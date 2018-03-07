using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cfb.Camp.Forms.CustomControls
{
	/// <summary>
	/// Represents a Windows text box control, with overridden behavior.
	/// </summary>
	public class CfbTextBox : TextBox
	{
		/// <summary>
		/// The background color of the control when it is read-only.
		/// </summary>
		private Color _readOnlyBackColor;

		/// <summary>
		/// The foreground color of the control when it is read-only.
		/// </summary>
		private Color _readOnlyForeColor;

		/// <summary>
		/// The cached background color of the control.
		/// </summary>
		private Color _cachedBackColor;

		/// <summary>
		/// The cached foreground color of the control.
		/// </summary>
		private Color _cachedForeColor;

		/// <summary>
		/// Whether or not the ReadOnly property is currently changing. Used to distinguish actual property changes from automated ones resulting from custom behavior.
		/// </summary>
		private bool _readOnlyChanging;

		/// <summary>
		/// Gets or sets a value indicating whether text in the text box is read-only.
		/// </summary>
		public new bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				_readOnlyChanging = true;
				base.ReadOnly = value;
			}
		}

		/// <summary>
		/// Gets or sets whether or not to use the legacy ReadOnly property behavior.
		/// </summary>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Whether or not to use the legacy ReadOnly property behavior.")]
		public bool UseLegacyReadOnly { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CfbTextBox"/> control.
		/// </summary>
		public CfbTextBox()
		{
			_cachedBackColor = this.BackColor;
			_cachedForeColor = this.ForeColor;
			this.BackColorChanged += new EventHandler(CfbTextBox_BackColorChanged);
			this.ForeColorChanged += new EventHandler(CfbTextBox_ForeColorChanged);
			this.ReadOnlyChanged += new EventHandler(CfbTextBox_ReadOnlyChanged);
		}

		/// <summary>
		/// Adjusts the background color to comply with custom read-only state behavior.
		/// </summary>
		private void AdjustBackColor()
		{
			if (!this.UseLegacyReadOnly)
				this.BackColor = this.ReadOnly ? _readOnlyBackColor : _cachedBackColor; // preserve read-only LAF
		}

		/// <summary>
		/// Adjusts the foreground color to comply with custom read-only state behavior.
		/// </summary>
		private void AdjustForeColor()
		{
			if (!this.UseLegacyReadOnly)
				this.ForeColor = this.ReadOnly ? _readOnlyForeColor : _cachedForeColor; // preserve read-only LAF
		}

		#region Event Handlers

		protected override void OnParentChanged(EventArgs e)
		{
			if (this.Parent != null)
			{
				// monitor changes to parent colors
				this.Parent.BackColorChanged += new EventHandler(Parent_BackColorChanged);
				this.Parent.ForeColorChanged += new EventHandler(Parent_ForeColorChanged);

				// force update
				Parent_BackColorChanged(this, e);
				Parent_ForeColorChanged(this, e);
			}
			base.OnParentChanged(e);
		}

		void Parent_BackColorChanged(object sender, EventArgs e)
		{
			_readOnlyBackColor = this.Parent.BackColor;
			AdjustBackColor();
		}

		void Parent_ForeColorChanged(object sender, EventArgs e)
		{
			_readOnlyForeColor = this.Parent.ForeColor;
			AdjustForeColor();
		}

		void CfbTextBox_BackColorChanged(object sender, EventArgs e)
		{
			if (_readOnlyChanging)
				return;
			_cachedBackColor = this.BackColor;
			AdjustBackColor();
		}

		void CfbTextBox_ForeColorChanged(object sender, EventArgs e)
		{
			if (_readOnlyChanging)
				return;
			_cachedForeColor = this.ForeColor;
			AdjustForeColor();
		}

		void CfbTextBox_ReadOnlyChanged(object sender, EventArgs e)
		{
			if (!this.UseLegacyReadOnly && this.ReadOnly)
			{
				AdjustBackColor();
				AdjustForeColor();
				Application.DoEvents();
			}
			_readOnlyChanging = false;
		}

		#endregion
	}
}
