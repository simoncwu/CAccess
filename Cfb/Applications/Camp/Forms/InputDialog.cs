using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A dialog box that prompts the user for a text input response.
	/// </summary>
	public partial class InputDialog : Form
	{
		/// <summary>
		/// Represents the method that will handle the event raised when the dialog validates the response. 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public delegate bool ValidatingEventHandler(string value);

		/// <summary>
		/// Occurs when the dialog validates the response.
		/// </summary>
		public event ValidatingEventHandler ValidatingResponse;

		/// <summary>
		/// Gets or sets the message in the dialog box. Use newline characters to set a multi-line message.
		/// </summary>
		public string Prompt
		{
			get { return this.PromptLabel.Text; }
			set { this.PromptLabel.Text = value; }
		}

		/// <summary>
		/// Gets or sets the text displayed in the title bar of the dialog box. If omitted, the name of the owning thread is used instead.
		/// </summary>
		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		/// <summary>
		/// Gets or sets the text to display when validation fails.
		/// </summary>
		public string ValidationErrorText { get; set; }

		/// <summary>
		/// Gets or sets the response entered by the user.
		/// </summary>
		public string Response
		{
			get { return this.inputTextBox.Text; }
			set { this.inputTextBox.Text = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputDialog"/> form.
		/// </summary>
		public InputDialog()
		{
			InitializeComponent();
			this.Text = Thread.CurrentThread.Name;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (this.ValidatingResponse != null && !this.ValidatingResponse(this.Response))
			{
				ShowErrorMessage();
			}
			else
			{
				this.DialogResult = DialogResult.OK;
			}
		}

		/// <summary>
		/// Shows the currently set validation error message.
		/// </summary>
		public void ShowErrorMessage()
		{
			MessageBox.Show(this.ValidationErrorText, "Invalid Response", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
		}
	}
}
