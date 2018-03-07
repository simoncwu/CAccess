using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
	public partial class Threshold : CPPage, ISecurePage
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!Page.IsPostBack)
			{
				ThresholdHistory history = CPProfile.ThresholdHistory;
				if (history != null && history.Current != null)
				{
					_lastStatement.Text = history.Current.Statement.ToDetailString();
					if (history.Current.IsUndetermined)
					{
						_threshold.Text = _thresholdRequired.Text = _dollarsRequired.Text = "(n/a)";
					}
					else
					{
						_threshold.Text = string.Format("{0:N0}", history.Current.Number);
						_thresholdRequired.Text = string.Format("{0:N0}", history.Current.NumberRequired);
						_dollarsRequired.Text = string.Format("{0:C}", history.Current.FundsRequired);
					}
					_dollars.Text = string.Format("{0:C}", history.Current.Funds);
					_officeSought.Text = history.Current.Office.ToString();
				}
				_history.DataSource = CPProfile.ThresholdHistory;
				_history.DataBind();
			}
		}

		protected override bool HasData()
		{
			return CPProfile.HasThresholdHistory;
		}
	}
}