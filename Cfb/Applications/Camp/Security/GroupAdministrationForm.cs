using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.CandidatePortal.Security;

namespace Cfb.Camp.Security
{
	/// <summary>
	/// A form for managing C-Access security groups.
	/// </summary>
	public partial class GroupAdministrationForm : CampMdiChild
	{
		public GroupAdministrationForm()
		{
			InitializeComponent();
		}

		#region Event Handlers

		private void GroupAdministrationForm_Load(object sender, EventArgs e)
		{
			if (!this.DesignMode)
			{
				LoadGroups();
			}
		}

		private void _newButton_Click(object sender, EventArgs e)
		{
			using (InputDialog dialog = new InputDialog()
			{
				Title = "Create New Group",
				Prompt = "New Group Name:",
				ValidationErrorText = "Invalid name specified. Group names must be new and cannot contain commas."
			})
			{
				dialog.ValidatingResponse += new InputDialog.ValidatingEventHandler(dialog_ValidatingResponse);
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					// create new group (role)
					if (CPSecurity.Provider.CreateGroup(dialog.Response) == null)
						dialog.ShowErrorMessage();
					LoadGroups();
				}
			}
		}

		bool dialog_ValidatingResponse(string value)
		{
			return !string.IsNullOrWhiteSpace(value) && !value.Contains(',') && CPSecurity.Provider.GetGroup(value) == null;
		}

		private void _deleteButton_Click(object sender, EventArgs e)
		{
			// delete selected group
			var selection = _groupListBox.SelectedItem as CPGroup;
			if (selection != null)
			{
				string name = selection.Name;
				if (CPSecurity.Provider.GetGroup(name) != null)
				{
					if (MessageBox.Show(string.Format("Delete group {0} permanently? This cannot be undone.", name), "Delete Group?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
					{
						if (CPSecurity.Provider.GetGroupMembers(name).Count > 0)
						{
							MessageBox.Show(string.Format("Unable to delete the {0} group because it still has members. Please reassign them before deleting this group.", name), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						}
						else
						{
							CPSecurity.Provider.DeleteGroup(name);
						}
					}
				}
				LoadGroups();
			}
		}

		private void _renameButton_Click(object sender, EventArgs e)
		{
			// rename selected group
			var selection = _groupListBox.SelectedItem as CPGroup;
			if (selection != null)
			{
				string name = selection.Name;
				if (CPSecurity.Provider.GetGroup(name) != null)
				{
					using (InputDialog dialog = new InputDialog()
					{
						Title = "Rename Group",
						Prompt = "Group Name:",
						Response = name,
						ValidationErrorText = "Invalid name specified. Group names must be new and cannot contain commas."
					})
					{
						dialog.ValidatingResponse += new InputDialog.ValidatingEventHandler(dialog_ValidatingResponse);
						if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							if (name != dialog.Response && !string.IsNullOrWhiteSpace(dialog.Response))
							{
								try
								{
									selection.Name = dialog.Response;
									selection = CPSecurity.Provider.UpdateGroup(selection);
								}
								catch
								{
									MessageBox.Show(string.Format("An error occurred while attempting to rename group {0} to {1}.", name, dialog.Response), dialog.Title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show("Error", string.Format("Error: Group {0} was not found", name), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
				LoadGroups();
			}
		}

		private void _groupListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_membersButton.Enabled = _renameButton.Enabled = _deleteButton.Enabled = _groupListBox.SelectedItem != null;
		}

		private void _groupListBox_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F2:
					_renameButton.PerformClick();
					break;
				case Keys.F5:
					_refreshButton.PerformClick();
					break;
				case Keys.Enter:
					_membersButton.PerformClick();
					break;
				case Keys.Delete:
					_deleteButton.PerformClick();
					break;
				case Keys.N:
					if (e.Control) _newButton.PerformClick();
					break;
			}
		}

		private void _refreshButton_Click(object sender, EventArgs e)
		{
			LoadGroups();
		}

		private void _membersButton_Click(object sender, EventArgs e)
		{

		}

		#endregion

		private void LoadGroups()
		{
			_groupListBox.DisplayMember = "Name";
			_groupListBox.ValueMember = "ID";
			_groupListBox.DataSource = CPSecurity.Provider.GetGroups().OrderBy(g => g.Name).ToList();
			_groupListBox.SelectedIndex = -1;
		}
	}
}
