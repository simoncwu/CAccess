using System;
using System.IO;
using System.Web;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Security;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web
{
	/// <summary>
	/// A custom HTTP Handler for serving attachment download requests.
	/// </summary>
	public class CmoAttachmentHandler : IHttpHandler
	{
		#region IHttpHandler Members

		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="IHttpHandler"/> instance.
		/// </summary>
		public bool IsReusable
		{
			// Return false in case your Managed Handler cannot be reused for another request.
			// Usually this would be false in case you have some state information preserved per request.
			get { return false; }
		}

		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="IHttpHandler"/> interface.
		/// </summary>
		/// <param name="context">An <see cref="HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext context)
		{
			context.CheckPitStops();
			// process query string for attachment
			string id = context.Request.Path;
			if (id.EndsWith("/"))
				context.Server.RedirectPageNotFound();
			if (id.Contains("/"))
				id = id.Substring(id.LastIndexOf('/') + 1);
			if (id.Contains("."))
				id = id.Substring(0, id.IndexOf('.'));
			CmoAttachment attachment = CmoAttachment.GetAttachment(id);
			if (attachment == null || !attachment.CandidateID.Equals(CPSecurity.Provider.GetCid(context.User.Identity.Name), StringComparison.InvariantCultureIgnoreCase))
				context.Server.RedirectPageNotFound();
			CmoMessage owner = CmoMessage.GetMessage(attachment.CandidateID, attachment.MessageID);
			if ((owner == null) || !owner.IsOpened)
			{
				// if owner message is not posted, open the message first
				context.Response.Redirect(string.Format("/Messages/View.aspx?id={0}", owner.UniqueID));
			}
			else
			{
				context.Response.Clear();
				context.Response.Expires = 0;
				context.Response.ContentType = context.Response.GetContentType(attachment.FileName);
				// BUGFIX #49 - content-disposition filename need to be surrounded by quotes
				context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", attachment.FileName));
				context.Response.AddHeader("Content-Length", new FileInfo(attachment.Path).Length.ToString());
				// BUGFIX #32 - KB823409: Response.WriteFile cannot download a large file so read/write file in chunks
				context.Response.TransmitFile(attachment.Path);
			}
			context.ApplicationInstance.CompleteRequest();
		}

		#endregion
	}
}
