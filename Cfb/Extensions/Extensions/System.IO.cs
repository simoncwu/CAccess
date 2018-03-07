using System;
using System.IO;

namespace Cfb.Extensions
{
	/// <summary>
	/// Extensions to classes in the System namespace.
	/// </summary>
	public static class System_IO
	{
		/// <summary>
		/// Retrieves the name of the file without the extension part.
		/// </summary>
		/// <param name="info">The <see cref="FileInfo"/> to examine.</param>
		/// <returns>The name of the file without the extension part.</returns>
		public static string GetName(this FileInfo info)
		{
			int length = info.Name.LastIndexOf(info.Extension, StringComparison.InvariantCultureIgnoreCase);
			return length > 0 ? info.Name.Remove(length) : info.Name;
		}
	}
}
