using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// Makes the associated <see cref="CheckBox"/> control a required field.
	/// </summary>
	[ToolboxData("<{0}:SiteMenu runat=server />")]
	public class RequiredCheckBoxValidator : BaseValidator
	{
		/// <summary>
		/// They key of the client script block to be registered for client-side validation.
		/// </summary>
		private const string ClientScriptBlockKey = "RequiredCheckBoxValidatorClientScript";

		/// <summary>
		/// Raises the <see cref="Control.Init"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		/// <summary>
		/// Raises the <see cref="Control.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		/// <summary>
		/// Determines whether the checkbox is checked.
		/// </summary>
		/// <returns>true if the checkbox is checked; otherwise, false.</returns>
		protected override bool EvaluateIsValid()
		{
			CheckBox cb = this.FindControl(ControlToValidate) as CheckBox;
			return (cb != null) && cb.Checked;
		}

		/// <summary>
		/// Determines whether the control specified by the <see cref="BaseValidator.ControlToValidate"/> property is a valid <see cref="CheckBox"/>.
		/// </summary>
		/// <returns>true if the control specified is a valid <see cref="CheckBox"/>; otherwise, false.</returns>
		protected override bool ControlPropertiesValid()
		{
			if (string.IsNullOrEmpty(ControlToValidate))
				throw new HttpException(string.Format("The ControlToValidate property of '{0}' cannot be blank.", this.ID));
			if (!(this.FindControl(ControlToValidate) is CheckBox))
				throw new HttpException(string.Format("Unable to find CheckBox control id '{0}' referenced by the 'ControlToValidate' property of '{1}'.", ControlToValidate, this.ID));
			return true;
		}

		/// <summary>
		/// Raises the <see cref="Control.PreRender"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			// inject client-side validation if supported
			if (this.DetermineRenderUplevel() && this.EnableClientScript && !Page.ClientScript.IsClientScriptBlockRegistered("RequiredCheckBoxValidatorClientScript"))
			{
				Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "ValidateCheckBox");
				string js = @"<script type=""text/javascript"" language=""javascript"">function ValidateCheckBox(val){var cb=document.getElementById(val.controltovalidate);return cb&&cb.checked;}</script>";
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RequiredCheckBoxValidatorClientScript", js, false);
			}
			base.OnPreRender(e);
		}
	}
}
