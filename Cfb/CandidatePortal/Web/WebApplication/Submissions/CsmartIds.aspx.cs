using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
	public partial class CsmartIds : CPPage, ISecurePage
	{
		protected override bool HasData()
		{
			return CPProfile.HasCsmartIdsRequests;
		}
	}
}