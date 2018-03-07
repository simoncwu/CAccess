using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.Security;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web
{
	/// <summary>
	/// A custom HTTP Handler for serving attachment download requests.
	/// </summary>
	public class ProfilePhotoHandler : IHttpHandler
	{
		/// <summary>
		/// The file extension for candidate photo images.
		/// </summary>
		public const string PhotoExtension = ".cpi";

		/// <summary>
		/// The candidate photo file repository path as defined in the configuration settings.
		/// </summary>
		private string _photoPath;

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
			context.Response.Expires = 0;
			context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			context.Response.ContentType = "application/octet-stream";
			string filePath = null;

			// get photo path from photo repository
			_photoPath = GetPhotoRepositoryPath();
			if (!string.IsNullOrEmpty(_photoPath))
			{
				filePath = GetPhotoFilePath(_photoPath, CPSecurity.Provider.GetCid(context.User.Identity.Name));
				if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
				{
					using (Image i = Image.FromFile(filePath))
					{
						if (i != null)
						{
							if (i.RawFormat.Guid.Equals(ImageFormat.Bmp.Guid))
								context.Response.ContentType = "image/bmp";
							else if (i.RawFormat.Guid.Equals(ImageFormat.Gif.Guid))
								context.Response.ContentType = "image/gif";
							else if (i.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid))
								context.Response.ContentType = "image/jpeg";
							else if (i.RawFormat.Guid.Equals(ImageFormat.Png.Guid))
								context.Response.ContentType = "image/x-png";
							else if (i.RawFormat.Guid.Equals(ImageFormat.Tiff.Guid))
								context.Response.ContentType = "image/tiff";
						}
					}
				}
			}
			if (!File.Exists(filePath))
			{
				// show default image if no file path was found
				filePath = context.Request.PhysicalPath;
				if (File.Exists(filePath))
					context.Response.WriteFile(filePath, true);
				else
					context.Server.RedirectPageNotFound();
			}
			else
			{
				context.Response.WriteFile(filePath, true);
			}
		}

		#endregion

		/// <summary>
		/// Gets the file path for a candidate's photo.
		/// </summary>
		/// <param name="path">The path of the file repository to search in.</param>
		/// <param name="cid">The CFIS ID of the candidate desired.</param>
		/// <returns>The file path to the photo for the candidate with ID <paramref name="cid"/>.</returns>
		/// <remarks>If the candidate photo does not exist in the file system, the file path for the default image will be returned.</remarks>
		public static string GetPhotoFilePath(string path, string cid)
		{
			if (!string.IsNullOrEmpty(path))
			{
				path.TrimEnd('\\');
				path = path + '\\';
			}
			return string.Format("{0}{1}{2}", path, cid, PhotoExtension);
		}

		/// <summary>
		/// Gets the file path for a candidate's photo from the currently defined photo repository.
		/// </summary>
		/// <param name="cid">The CFIS ID of the candidate desired.</param>
		/// <returns>The file path to the photo for the candidate with ID <paramref name="cid"/>.</returns>
		/// <remarks>If the candidate photo does not exist in the file system, the file path for the default image will be returned.</remarks>
		public static string GetPhotoFilePath(string cid)
		{
			return GetPhotoFilePath(GetPhotoRepositoryPath(), cid);
		}

		/// <summary>
		/// Gets the system path to the candidate profile photo repository from the current configuration settings.
		/// </summary>
		/// <returns>The system path to the currently configured candidate profile photo repository if defined; otherwise, null.</returns>
		public static string GetPhotoRepositoryPath()
		{
			CPProfileSection profileSection = CPConfigurationManager.Profile;
			if (profileSection != null)
			{
				string repositoryName = profileSection.PhotoRepositoryName;
				foreach (RepositoryElement r in profileSection.Repositories)
				{
					if (!string.IsNullOrEmpty(repositoryName) && repositoryName.Equals(r.Name, StringComparison.InvariantCultureIgnoreCase))
					{
						return r.FilePath;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Gets whether or not a custom photo exists for a specific candidate.
		/// </summary>
		/// <param name="cid">The CFIS ID of the candidate to search for.</param>
		/// <returns>true if a custom photo file was found for the candidate specified; otherwise, false.</returns>
		public static bool CustomPhotoExists(string cid)
		{
			string photoPath = GetPhotoRepositoryPath();
			if (!string.IsNullOrEmpty(photoPath) && !string.IsNullOrEmpty(cid))
			{
				return File.Exists(GetPhotoFilePath(photoPath, cid));
			}
			return false;
		}
	}
}
