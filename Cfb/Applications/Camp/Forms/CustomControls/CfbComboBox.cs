using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Cfb.Extensions;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A Windows combo box control with overriden behavior.
	/// </summary>
	public class CfbComboBox : ComboBox
	{
		/// <summary>
		/// Text box to be rendered for read-only view.
		/// </summary>
		private ReadOnlyTextBox _readOnlyTextBox;

		/// <summary>
		/// Whether or not the combo box is read-only.
		/// </summary>
		private bool _readOnly;

		/// <summary>
		/// Whether or not a selection change has been completed.
		/// </summary>
		private bool _selectionChangeCompleted;

		/// <summary>
		/// Whether or not a clear selection request is pending.
		/// </summary>
		private bool _clearSelectionPending;

		/// <summary>
		/// Whether or not a clear data source request is pending;
		/// </summary>
		private bool _clearDataSourcePending;

		/// <summary>
		/// Gets whether or not a clear data source request is pending.
		/// </summary>
		public bool ClearDataSourcePending
		{
			get { return _clearDataSourcePending; }
		}

		/// <summary>
		/// Gets or sets whether or not the combo box is read-only.
		/// </summary>
		[Category("Appearance")]
		[Description("Whether or not the combo box is read-only.")]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return _readOnly;
			}
			set
			{
				_readOnly = value;
				if (this.Visible)
					base.Visible = !(_readOnlyTextBox.Visible = value);
			}
		}

		/// <summary>
		/// Gets or sets the text to display in the control when no item is selected.
		/// </summary>
		[Category("Appearance")]
		[Description("The text to display in the control when no item is selected.")]
		public string EmptySelectionText { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the control and all its child controls are displayed.
		/// </summary>
		public new bool Visible
		{
			get
			{
				return base.Visible || _readOnlyTextBox.Visible;
			}
			set
			{
				base.Visible = !_readOnly && value;
				_readOnlyTextBox.Visible = _readOnly && value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CfbComboBox"/> custom control.
		/// </summary>
		public CfbComboBox()
		{
			_readOnlyTextBox = new ReadOnlyTextBox()
			{
				Location = this.Location,
				Size = this.Size,
				Visible = false
			};
		}

		/// <summary>
		/// Raises the <see cref="ComboBox.DropDownStyleChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnDropDownStyleChanged(EventArgs e)
		{
			if (this.DropDownStyle == ComboBoxStyle.DropDown)
			{
				this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				this.AutoCompleteSource = AutoCompleteSource.ListItems;
				this.KeyDown += new KeyEventHandler(CfbComboBox_KeyDown);
				this.Leave += new EventHandler(CfbComboBox_Leave);
			}
			base.OnDropDownStyleChanged(e);
		}

		/// <summary>
		/// Handles the <see cref="Control.KeyDown"/> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
		protected virtual void CfbComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			this.DroppedDown = string.IsNullOrEmpty(this.Text) && (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey | e.KeyCode == Keys.CapsLock || e.KeyCode == Keys.NumLock || e.KeyCode == Keys.Scroll);
		}

		/// <summary>
		/// Handles the <see cref="Control.Leave"/> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected virtual void CfbComboBox_Leave(object sender, EventArgs e)
		{
			this.AutoSelect();
		}

		/// <summary>
		/// Raises the <see cref="Control.Leave"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnLeave(EventArgs e)
		{
			if (string.IsNullOrEmpty(this.Text))
				this.OnSelectedIndexChanged(e);
			base.OnLeave(e);
		}

		/// <summary>
		/// Raises the <see cref="Control.Resize"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			_readOnlyTextBox.Size = this.Size;
		}

		/// <summary>
		/// Raises the <see cref="Control.LocationChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			_readOnlyTextBox.Location = this.Location;
		}

		/// <summary>
		/// Raises the <see cref="Control.ParentChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			_readOnlyTextBox.Parent = this.Parent;
		}

		/// <summary>
		/// Raises the <see cref="ComboBox.SelectedIndexChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			_clearSelectionPending = !_selectionChangeCompleted;
			base.OnSelectedIndexChanged(e);
		}

		protected override void OnSelectedItemChanged(EventArgs e)
		{
			if (_clearSelectionPending)
			{
				// prevent auto-selection of first item when clearing
				_clearSelectionPending = false;
				this.SelectedIndex = -1;
				_selectionChangeCompleted = true;
			}
			else
			{
				_selectionChangeCompleted = true;
				base.OnSelectedItemChanged(e);
			}
		}

		protected override void OnSelectionChangeCommitted(EventArgs e)
		{
			_selectionChangeCompleted = true;
			base.OnSelectionChangeCommitted(e);
		}

		protected override void OnSelectedValueChanged(EventArgs e)
		{
			_selectionChangeCompleted = false;
			if (this.SelectedIndex == -1)
				this.Text = this.EmptySelectionText;
			_readOnlyTextBox.Text = this.Text;
			base.OnSelectedValueChanged(e);
		}

		/// <summary>
		/// Clears the data source and removes all items for this <see cref="ComboBox"/>.
		/// </summary>
		public void ClearDataSourceItems()
		{
			_clearDataSourcePending = true;
			this.ValueMember = this.DisplayMember = null;
			this.DataSource = null;
			if (this.DataSource == null) // avoid exception when DataSource fails to set to null due to inSetDataConnection flag
			{
				this.Items.Clear();
				_clearDataSourcePending = false;
			}
		}

		/// <summary>
		/// A text box control set to appear as read-only.
		/// </summary>
		private class ReadOnlyTextBox : TextBox
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="ReadOnlyTextBox"/> class.
			/// </summary>
			public ReadOnlyTextBox()
			{
				this.ReadOnly = true;
			}

			protected override void OnPaint(PaintEventArgs e)
			{

			}
			protected override void OnPaintBackground(PaintEventArgs pevent)
			{

			}
		}
	}
}
