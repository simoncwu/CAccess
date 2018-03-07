using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cfb.Camp.Forms
{
    /// <summary>
    /// Defines methods for merging menustrip and toolstrip items with a parent <see cref="CampMdiParent"/> form..
    /// </summary>
    public interface ICampMdiParentMergeable
    {
        /// <summary>
        /// Gets the form toolstrips that are to be merged with the main CAMP toolstrip.
        /// </summary>
        /// <returns>A collection of toolstrips to merge.</returns>
        IEnumerable<ToolStrip> GetMergingToolStrips();

        /// <summary>
        /// Occurs as the "File" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        void FileMenuDropDownOpening(object sender, EventArgs e);

        /// <summary>
        /// Occurs as the "View" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        void ViewMenuDropDownOpening(object sender, EventArgs e);

        /// <summary>
        /// Occurs as the "Tools" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        void ToolsMenuDropDownOpening(object sender, EventArgs e);

        /// <summary>
        /// Occurs as the "Windows" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        void WindowsMenuDropDownOpening(object sender, EventArgs e);

        /// <summary>
        /// Occurs as the "Help" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        void HelpMenuDropDownOpening(object sender, EventArgs e);
    }
}
