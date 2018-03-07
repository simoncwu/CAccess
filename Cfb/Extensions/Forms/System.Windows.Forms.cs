using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cfb.Extensions
{
    /// <summary>
    /// Extension methods for the System.Windows.Forms namespace.
    /// </summary>
    public static class System_Windows_Forms
    {
        /// <summary>
        /// Displays a form to the user as a multiple-document interface (MDI) sibling of another form.
        /// </summary>
        /// <param name="form">The <see cref="Form"/> to display.</param>
        /// <param name="sibling">The sibling <see cref="Form"/> whose MDI parent will be shared.</param>
        public static void ShowAsMdiSiblingOf(this Form form, Form sibling)
        {
            if (form == null) return;
            form.ShowAsChildOf(sibling == null ? null : sibling.MdiParent);
        }

        /// <summary>
        /// Displays a form to the user as being owned by another form.
        /// </summary>
        /// <param name="form">The <see cref="Form"/> to display.</param>
        /// <param name="owner">The <see cref="Form"/> that owns <paramref name="form"/>.</param>
        public static void ShowAsOwnedBy(this Form form, Form owner)
        {
            if (form == null) return;
            if (owner != null)
            {
                form.Owner = owner;
                form.Show(owner);
            }
            else
            {
                form.Show();
            }
        }

        /// <summary>
        /// Displays a form to the user as being a child of another form.
        /// </summary>
        /// <param name="form">The <see cref="Form"/> to display.</param>
        /// <param name="parent">The <see cref="Form"/> that will be parent of <paramref name="form"/></param>
        public static void ShowAsChildOf(this Form form, Form parent)
        {
            if (form == null) return;
            if (parent != null && parent.IsMdiContainer)
                form.MdiParent = parent;
            form.Show();
        }

        /// <summary>
        /// Creates a new form and displays it to the user as a child of a multiple-document interface (MDI) parent form.
        /// </summary>
        /// <typeparam name="T">The type of form to create and display.</typeparam>
        /// <param name="parent">The parent MDI <see cref="Form"/>.</param>
        /// <param name="setup">A method to perform additional setup processing on the form prior to it being shown.</param>
        public static T SpawnMdiChild<T>(this Form parent, Action<T> setup = null) where T : Form, new()
        {
            T child = new T();
            if (parent != null && parent.IsMdiContainer)
                child.MdiParent = parent;
            if (setup != null)
                setup(child);
            child.Show();
            return child;
        }

        /// <summary>
        /// Automatically selects the item that matches the contents of an editable <see cref="ComboBox"/>.
        /// </summary>
        /// <param name="comboBox">The <see cref="ComboBox"/> to examine.</param>
        public static void AutoSelect(this ComboBox comboBox)
        {
            if ((comboBox.AutoCompleteMode != AutoCompleteMode.None) && !string.IsNullOrEmpty(comboBox.Text))
            {
                int index = comboBox.FindStringExact(comboBox.Text);
                comboBox.SelectedIndex = index == -1 ? comboBox.FindString(comboBox.Text) : index;
                if (comboBox.SelectedIndex == -1)
                    comboBox.SelectedText = comboBox.Text = null;
            }
        }
    }
}
