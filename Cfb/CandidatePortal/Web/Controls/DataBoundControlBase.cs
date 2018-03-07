using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// Serves as the base class for controls that bind to object data using an ASP.NET data source control.
	/// </summary>
	public abstract class DataBoundControlBase : BaseDataBoundControl
	{
		/// <summary>
		/// View state key for tracking data bound status.
		/// </summary>
		private const string DataBoundViewStateKey = "_!DataBound";

		/// <summary>
		/// Gets a value indicating whether or not data was previously bound.
		/// </summary>
		public bool IsDataBound
		{
			get { return this.ViewState[DataBoundViewStateKey] != null; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataBoundControlBase"/> class.
		/// </summary>
		public DataBoundControlBase()
		{
		}

		/// <summary>
		/// Sets the initialized state of the data-bound control before the control is loaded.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			// enables BaseDataBoundControl to automatically bind to data during control life cycle when necessary
			if (!this.Page.IsPostBack || (base.IsViewStateEnabled && this.ViewState[DataBoundViewStateKey] == null))
			{
				base.RequiresDataBinding = true;
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.Load"/> event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.ConfirmInitState();
			// enables BaseDataBoundControl to automatically bind to data during control life cycle when necessary
			if (this.ViewState[DataBoundViewStateKey] == null)
			{
				if (!this.Page.IsPostBack || base.IsViewStateEnabled)
				{
					base.RequiresDataBinding = true;
				}
			}
			base.OnLoad(e);
		}

		/// <summary>
		/// Marks the control as having been bound to data.
		/// </summary>
		protected void MarkAsDataBound()
		{
			this.ViewState[DataBoundViewStateKey] = true;
		}
	}
}
